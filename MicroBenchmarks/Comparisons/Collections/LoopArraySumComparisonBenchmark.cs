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
using ZLinq;

namespace MicroBenchmarks
{
	/// <summary>
	/// Compares different loop logics for an array to sum up all values
	/// including Linq and ZLinq (<see href="https://github.com/Cysharp/ZLinq"/>)
	/// Interesting to compare the results to <see cref="LoopListSumComparisonBenchmark"/>
	/// </summary>
	[Config(typeof(DefaultBenchmarkConfig))]
	public class LoopArraySumComparisonBenchmark
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

		/// <summary>
		/// Does not make a difference, since the other loops wil also be changed to preincrement by the compiler
		/// </summary>
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
		public int ForLoopLocalVariable()
		{
			var sum = 0;

			byte[] localData = data;
			for (int i = 0; i < localData.Length; i++)
			{
				sum += localData[i];
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
		public int ZLinqSum()
		{
			return data.AsValueEnumerable().Sum(b => (int)b);
		}

		[Benchmark]
		public int LinqSum()
		{
			return data.Sum(b => (int)b);
		}

		[Benchmark]
		public int ZLinqAggregate()
		{
			return data.AsValueEnumerable().Aggregate(0, (sum, i) => sum + i);
		}


		[Benchmark]
		public int LinqAggregate()
		{
			return data.Aggregate(0, (sum, i) => sum + i);
		}
	}
}
