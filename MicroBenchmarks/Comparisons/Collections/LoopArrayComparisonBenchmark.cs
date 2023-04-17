// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LoopComparisonBenchmark.cs">
//   Copyright (c) 2021 Johannes Deml. All rights reserved.
// </copyright>
// <author>
//   Johannes Deml
//   public@deml.io
// </author>
// --------------------------------------------------------------------------------------------------------------------

using System.Linq;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Extensions;
using MicroBenchmarks.Extensions;

namespace MicroBenchmarks
{
	[Config(typeof(DefaultBenchmarkConfig))]
	public class LoopArrayComparisonBenchmark
	{
		// Needs to be a multiple of 4 to support ForLoopUnroll4
		[Params(100, 100_000)]
		public int ArraySize { get; set; }

		private byte[] data;

		[GlobalSetup]
		public void PrepareBenchmark()
		{
			data = ValuesGenerator.Array<byte>(ArraySize);
		}

		[Benchmark(Baseline = true)]
		public int ForLoop()
		{
			var sum = 0;
			for (int i = 0; i < data.Length; i++)
			{
				sum += data[i];
			}

			return sum;
		}

		[Benchmark]
		public int ForLoopPreIncrement()
		{
			var sum = 0;
			for (int i = 0; i < data.Length; ++i)
			{
				sum += data[i];
			}

			return sum;
		}

		[Benchmark]
		public int ForLoopCachedLength()
		{
			var sum = 0;
			int length = data.Length;
			for (int i = 0; i < length; i++)
			{
				sum += data[i];
			}

			return sum;
		}

		[Benchmark]
		public int ForLoopUnroll4()
		{
			var sum = 0;
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
		public int ForeachLoop()
		{
			var sum = 0;
			foreach (var i in data)
			{
				sum += i;
			}

			return sum;
		}

		[Benchmark]
		public int LinqSum()
		{
			return data.Sum(b => (int)b);
		}

		[Benchmark]
		public int LinqAggregate()
		{
			return data.Aggregate(0, (current, i) => current + i);
		}
	}
}
