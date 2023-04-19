// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ConcurrentCollectionsBenchmark.cs">
//   Copyright (c) 2021 Johannes Deml. All rights reserved.
// </copyright>
// <author>
//   Johannes Deml
//   public@deml.io
// </author>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Concurrent;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Extensions;
using MicroBenchmarks.Extensions;

namespace MicroBenchmarks
{
	[Config(typeof(DefaultBenchmarkConfig))]
	public class ConcurrentCollectionsBenchmark
	{
		[Params(16, 128, 1024)]
		public int CollectionSize { get; set; }

		private ConcurrentBag<byte> concurrentBag;
		private ConcurrentStack<byte> concurrentStack;
		private ConcurrentQueue<byte> concurrentQueue;


		[GlobalSetup]
		public void PrepareBenchmark()
		{
			var collection = ValuesGenerator.Array<byte>(CollectionSize);

			concurrentBag = new ConcurrentBag<byte>(collection);
			concurrentStack = new ConcurrentStack<byte>(collection);
			concurrentQueue = new ConcurrentQueue<byte>(collection);
		}

		[Benchmark]
		public byte RentReturnConcurrentBag()
		{
			if (concurrentBag.TryTake(out var result))
			{
				concurrentBag.Add(result);
				return result;
			}

			throw new InvalidOperationException("Should not be empty");
		}

		[Benchmark]
		public byte RentReturnConcurrentStack()
		{
			if (concurrentStack.TryPop(out var result))
			{
				concurrentStack.Push(result);
				return result;
			}

			throw new InvalidOperationException("Should not be empty");
		}

		[Benchmark]
		public byte RentReturnConcurrentQueue()
		{
			if (concurrentQueue.TryDequeue(out var result))
			{
				concurrentQueue.Enqueue(result);
				return result;
			}

			throw new InvalidOperationException("Should not be empty");
		}
	}
}
