// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SortingBenchmark.cs">
//   Copyright (c) 2022 Johannes Deml. All rights reserved.
// </copyright>
// <author>
//   Johannes Deml
//   public@deml.io
// </author>
// --------------------------------------------------------------------------------------------------------------------

using System;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Extensions;
using MicroBenchmarks.Extensions;

namespace MicroBenchmarks
{
	/// <summary>
	/// Benchmarks for sorting arrays with different approaches
	/// </summary>
	[Config(typeof(DefaultBenchmarkConfig))]
	public class SortingBenchmark
	{
		[Params(10, 10000)]
		public int ArraySize { get; set; }

		private int[] data;

		[GlobalSetup]
		public void PrepareBenchmark()
		{
			data = ValuesGenerator.Array<int>(ArraySize);
		}

		[Benchmark]
		public int[] SortArray()
		{
			Array.Sort(data);
			return data;
		}

		[Benchmark]
		public int[] SortArrayLambda()
		{
			Array.Sort(data, (a, b) => a-b);
			return data;
		}

		[Benchmark]
		public int[] SortArrayMethod()
		{
			Array.Sort(data, Comparison);
			return data;
		}

		private static int Comparison(int a, int b)
		{
			return a - b;
		}
	}
}
