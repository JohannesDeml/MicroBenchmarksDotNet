using System;
using BenchmarkDotNet.Attributes;
using MicroBenchmarks.Extensions;

namespace MicroBenchmarks
{
	[Config(typeof(DefaultBenchmarkConfig))]
	public class StringCultureVariantBenchmark
	{
		[Params(StringComparison.Ordinal, StringComparison.OrdinalIgnoreCase,
			StringComparison.InvariantCulture, StringComparison.InvariantCultureIgnoreCase)]
		public StringComparison Comparison { get; set; }

		private const string TargetString = "aBc";
		private string stringData = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

		[Benchmark]
		public bool StartsWithString()
		{
			return stringData.StartsWith(TargetString, Comparison);
		}
	}
}
