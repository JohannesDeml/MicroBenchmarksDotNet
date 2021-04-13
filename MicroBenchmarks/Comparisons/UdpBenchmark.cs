// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UdpBenchmark.cs">
//   Copyright (c) 2021 Johannes Deml. All rights reserved.
// </copyright>
// <author>
//   Johannes Deml
//   public@deml.io
// </author>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Net;
using System.Net.Sockets;
using BenchmarkDotNet.Attributes;
using MicroBenchmarks.Extensions;

namespace MicroBenchmarks
{
	[Config(typeof(DefaultBenchmarkConfig))]
	public class UdpBenchmark
	{
		[Params(32, 1_000, 10_000)]
		public int MessageSize { get; set; }

		private int port = 3333;

		UdpClient udpServer;
		private IPEndPoint endPoint;
		UdpClient udpClient;
		private byte[] message;

		[GlobalSetup]
		public void PrepareBenchmark()
		{
			PrepareServer();
			PrepareClient();
		}

		private void PrepareServer()
		{
			udpServer = new UdpClient(port);
			endPoint = new IPEndPoint(IPAddress.Any, port);
		}

		private void PrepareClient()
		{
			udpClient = new UdpClient();
			IPEndPoint ep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), port);
			udpClient.Connect(ep);

			message = new byte[MessageSize];
			var random = new Random(0);
			random.NextBytes(message);
		}

		[Benchmark]
		public int SendReceive()
		{
			udpClient.Send(message, message.Length);
			var receivedData = udpServer.Receive(ref endPoint);

			return receivedData.Length;
		}
	}
}
