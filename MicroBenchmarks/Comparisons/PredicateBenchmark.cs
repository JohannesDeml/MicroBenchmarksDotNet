using System;
using System.Collections.Generic;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Extensions;
using MicroBenchmarks.Extensions;

namespace MicroBenchmarks
{
	/// <summary>
	/// Check if there are any performance advantages when using Predicate<T> instead of Func<T, bool>
	/// </summary>
	[Config(typeof(DefaultBenchmarkConfig))]
	public class PredicateBenchmark
	{
		[Params(1_000)] public int ListSize { get; set; }
		[Params(1)] public int SearchValue { get; set; }

		private List<int> data;
		private Func<int, bool> preparedFuncDelegate;
		private Func<int, bool> preparedPredicateDelegate;

		[GlobalSetup]
		public void PrepareBenchmark()
		{
			data = new List<int>(ValuesGenerator.Array<int>(ListSize));
			preparedFuncDelegate = a => a == SearchValue;
			preparedPredicateDelegate = a => a == SearchValue;
		}

		[Benchmark]
		public int FindIndexFunc()
		{
			for (int i = 0; i < ListSize; i++)
			{
				if(preparedFuncDelegate.Invoke(data[i]))
				{
					return i;
				}
			}

			return -1;
		}

		[Benchmark]
		public int FindIndexPredicate()
		{
			for (int i = 0; i < ListSize; i++)
			{
				if(preparedPredicateDelegate.Invoke(data[i]))
				{
					return i;
				}
			}

			return -1;
		}
	}
}
