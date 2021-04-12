// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ThreadingBenchmark.cs">
//   Copyright (c) 2021 Johannes Deml. All rights reserved.
// </copyright>
// <author>
//   Johannes Deml
//   public@deml.io
// </author>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Threading;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Engines;
using MicroBenchmarks.Extensions;

namespace MicroBenchmarks
{
	[SimpleJob(RunStrategy.Monitoring, launchCount: 1, warmupCount: 3, targetCount: 15, invocationCount: 1)]
	//[Config(typeof(DefaultBenchmarkConfig))]
	public class ThreadingBenchmark
	{
		[Params(1, 100)]
		public int ThreadsPerCore { get; set; }

		public int BucketSize = 1_000;
		public long MoveTargetPerCore = 100_000_000;

		private static List<Client> clients;

		[GlobalSetup]
		public void PrepareBenchmark()
		{
			Client.KeepThreadsAlive = true;
			Client.BenchmarkActive = false;

			CreateClients();
			StartClientThreads();
		}

		[GlobalCleanup]
		public void CleanupBenchmark()
		{
			for (int i = 0; i < clients.Count; i++)
			{
				var client = clients[i];
				client.ResetProgress();
			}

			Client.KeepThreadsAlive = false;
		}

		private void CreateClients()
		{
			clients = new List<Client>();
			var clientCount = ThreadsPerCore * Environment.ProcessorCount;
			var clientMoveTarget = MoveTargetPerCore / ThreadsPerCore;

			for (int i = 0; i < clientCount; i++)
			{
				var client = new Client(i, clientMoveTarget);
				client.AddMessages(BucketSize);
				clients.Add(client);
			}
			Console.WriteLine($"Clients: {clientCount}, clientMoveTarget: {clientMoveTarget}, totalMove: {clientMoveTarget * clientCount}");
		}

		private void StartClientThreads()
		{
			for (int i = 0; i < clients.Count; i++)
			{
				var client = clients[i];
				client.StartThread();
			}
		}

		[Benchmark]
		public int ThreadedQueueing()
		{
			for (int i = 0; i < clients.Count; i++)
			{
				var client = clients[i];
				client.ResetProgress();
			}

			Client.ClientsFinished = 0;
			Client.BenchmarkActive = true;

			while (Client.ClientsFinished < clients.Count)
			{
				Thread.SpinWait(10);
			}

			Client.BenchmarkActive = false;
			return Client.ClientsFinished;
		}
	}

	public class Client
	{
		public static bool KeepThreadsAlive = false;
		public static bool BenchmarkActive = false;
		public static int ClientsFinished = 0;

		private readonly int id;
		private readonly long clientMoveTarget;
		private readonly Thread clientThread;
		private readonly Queue<int> firstQueue;
		private readonly Queue<int> secondQueue;

		private long itemsMoved = 0;
		private bool finished;
		public Client(int id, long clientMoveTarget)
		{
			this.id = id;
			this.clientMoveTarget = clientMoveTarget;

			firstQueue = new Queue<int>();
			secondQueue = new Queue<int>();

			clientThread = new Thread(Run);
			clientThread.Name += $"Client_{id}";
		}

		public void AddMessages(int count)
		{
			for (int i = 0; i < count; i++)
			{
				secondQueue.Enqueue(i);
			}
		}

		public void ResetProgress()
		{
			itemsMoved = 0;
			finished = false;
		}

		public void StartThread()
		{
			clientThread.Start();
		}

		private void Run()
		{
			while (KeepThreadsAlive)
			{
				while (!BenchmarkActive && KeepThreadsAlive)
				{
					Thread.Sleep(1);
				}

				bool moveToSecond = true;
				while (BenchmarkActive)
				{
					int element;
					if (moveToSecond)
					{
						while (firstQueue.TryDequeue(out element))
						{
							secondQueue.Enqueue(element);
							itemsMoved++;
						}
					}
					else
					{
						while (secondQueue.TryDequeue(out element))
						{
							firstQueue.Enqueue(element);
							itemsMoved++;
						}
					}

					if (itemsMoved >= clientMoveTarget)
					{
						finished = true;
						Interlocked.Increment(ref ClientsFinished);
						break;
					}

					// switch mode
					moveToSecond = !moveToSecond;

					// Switch context if there is another client waiting for thread time
					Thread.Yield();
				}

				while (finished)
				{
					Thread.Sleep(1);
				}
			}
		}
	}
}
