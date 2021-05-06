// --------------------------------------------------------------------------------------------------------------------
// <copyright file="_TestBenchmark.cs">
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
	/// <summary>
	/// This benchmark is for quick testing if all target platforms compile and are working as expected
	/// </summary>
	[Config(typeof(DefaultBenchmarkConfig))]
	public class _TestBenchmark
	{
		[Benchmark]
		public int InstantReturn()
		{
			return 0;
		}
	}
}
