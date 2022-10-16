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
using System.Collections.Generic;
using System.Linq;
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
		public struct IntWrapper : IComparable<IntWrapper>
		{
			public int Value;

			public IntWrapper(int value)
			{
				Value = value;
			}

			public int CompareTo(IntWrapper other)
			{
				return Value.CompareTo(other.Value);
			}
		}

		private class IntWrapperComparer : IComparer<IntWrapper>
		{
			public int Compare(IntWrapper x, IntWrapper y)
			{
				return x.Value.CompareTo(y.Value);
			}
		}

		[Params(10, 10000)]
		public int ArraySize { get; set; }

		private IntWrapper[] data;
		private IComparer<IntWrapper> intWrapperComparer;

		[GlobalSetup]
		public void PrepareBenchmark()
		{
			data = ValuesGenerator.Array<int>(ArraySize).Select(x => new IntWrapper(x)).ToArray();
			intWrapperComparer = new IntWrapperComparer();
		}

		[Benchmark]
		public int SortArrayIComparable()
		{
			Array.Sort(data);
			return data[0].Value;
		}

		[Benchmark]
		public int SortArrayIComparer()
		{
			Array.Sort(data, intWrapperComparer);
			return data[0].Value;
		}

		[Benchmark]
		public int SortArrayLambda()
		{
			Array.Sort(data, (a, b) => a.Value.CompareTo(b.Value));
			return data[0].Value;
		}

		[Benchmark]
		public int SortArrayMethod()
		{
			Array.Sort(data, Comparison);
			return data[0].Value;
		}

		private static int Comparison(IntWrapper a, IntWrapper b)
		{
			return a.Value.CompareTo(b.Value);
		}
	}
}
