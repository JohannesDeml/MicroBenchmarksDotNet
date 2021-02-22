// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LoopComparisonBenchmark.cs">
//   Copyright (c) 2021 Johannes Deml. All rights reserved.
// </copyright>
// <author>
//   Johannes Deml
//   public@deml.io
// </author>
// --------------------------------------------------------------------------------------------------------------------

using System;
using BenchmarkDotNet.Attributes;

namespace CommonBenchmarks
{
	[Config(typeof(DefaultBenchmarkConfig))]
	public class LoopComparisonBenchmark
	{
		private const int Seed = 1337;

		[Params(100, 1000, 10000)]
		public int ArraySize { get; set; }

		private byte[] data;
		private int sum;

		[GlobalSetup]
		public void PrepareBenchmark()
		{
			data = new byte[ArraySize];

			var rnd = new Random(Seed);
			rnd.NextBytes(data);
		}

		[Benchmark(Baseline = true)]
		public int ForLoopPostIncrement()
		{
			sum = 0;
			for (int i = 0; i < data.Length; i++)
			{
				sum += data[i];
			}

			return sum;
		}

		[Benchmark]
		public int ForLoopPostIncrementUnroll4()
		{
			sum = 0;
			for (int i = 0; i < data.Length; i += 4)
			{
				sum += data[i];
				sum += data[i + 1];
				sum += data[i + 2];
				sum += data[i + 3];
			}

			return sum;
		}

		[Benchmark]
		public int ForLoopPreIncrement()
		{
			sum = 0;
			for (int i = 0; i < data.Length; ++i)
			{
				sum += data[i];
			}

			return sum;
		}

		[Benchmark]
		public int ForeachLoop()
		{
			sum = 0;
			foreach (var i in data)
			{
				sum += i;
			}

			return sum;
		}
	}
}