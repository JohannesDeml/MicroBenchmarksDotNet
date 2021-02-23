// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TestBenchmark.cs">
//   Copyright (c) 2021 Johannes Deml. All rights reserved.
// </copyright>
// <author>
//   Johannes Deml
//   public@deml.io
// </author>
// --------------------------------------------------------------------------------------------------------------------

using BenchmarkDotNet.Attributes;
using MicroBenchmarks.Extensions;

namespace MicroBenchmarks
{
	[Config(typeof(DefaultBenchmarkConfig))]
	public class TestBenchmark
	{
		[Benchmark]
		public int InstantReturn()
		{
			return 0;
		}
	}
}
