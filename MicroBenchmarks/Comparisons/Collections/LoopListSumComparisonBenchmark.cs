// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LoopListSumComparisonBenchmark.cs">
//   Copyright (c) 2023 Johannes Deml. All rights reserved.
// </copyright>
// <author>
//   Johannes Deml
//   public@deml.io
// </author>
// --------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Linq;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Extensions;
using MicroBenchmarks.Extensions;

namespace MicroBenchmarks
{
	/// <summary>
	/// Compares different loop logics for a list to sum up all values
	/// Interesting to compare the results to <see cref="LoopArraySumComparisonBenchmark"/>
	/// </summary>
	[Config(typeof(DefaultBenchmarkConfig))]
	public class LoopListSumComparisonBenchmark
	{
		// Needs to be a multiple of 4 to support ForLoopUnroll4
		[Params(100, 100_000)]
		public int ListSize { get; set; }

		private List<byte> data;

		[GlobalSetup]
		public void PrepareBenchmark()
		{
			data = new List<byte>(ValuesGenerator.Array<byte>(ListSize));
		}

		[Benchmark(Baseline = true)]
		public int ForLoop()
		{
			var sum = 0;
			for (int i = 0; i < data.Count; i++)
			{
				sum += data[i];
			}

			return sum;
		}

		/// <summary>
		/// Does not make a difference, since the other loops wil also be changed to preincrement by the compiler
		/// </summary>
		[Benchmark]
		public int ForLoopPreIncrement()
		{
			var sum = 0;
			for (int i = 0; i < data.Count; ++i)
			{
				sum += data[i];
			}

			return sum;
		}

		[Benchmark]
		public int ForLoopCachedLength()
		{
			var sum = 0;
			int count = data.Count;
			for (int i = 0; i < count; i++)
			{
				sum += data[i];
			}

			return sum;
		}

		[Benchmark]
		public int ForLoopLocalVariable()
		{
			var sum = 0;

			List<byte> localData = data;
			for (int i = 0; i < localData.Count; i++)
			{
				sum += localData[i];
			}

			return sum;
		}

		[Benchmark]
		public int ForLoopUnroll4()
		{
			var sum = 0;
			for (int i = 0; i < data.Count; i += 4)
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
			return data.Aggregate(0, (sum, i) => sum + i);
		}
	}
}
