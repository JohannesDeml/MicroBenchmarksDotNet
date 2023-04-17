using System;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Extensions;
using MicroBenchmarks.Extensions;

namespace MicroBenchmarks
{
	[Config(typeof(DefaultBenchmarkConfig))]
	public class StringCultureVariantBenchmark
	{
		[Params(StringComparison.Ordinal, StringComparison.OrdinalIgnoreCase,
			StringComparison.InvariantCulture, StringComparison.InvariantCultureIgnoreCase)]
		public StringComparison Comparison { get; set; }

		[Params(10, 10_000)]
		public int LengthToTarget { get; set; }

		private const string TargetString = "{TargetString}";
		private string stringData;

		[GlobalSetup]
		public void PrepareBenchmark()
		{
			stringData = $"{ValuesGenerator.GenerateRandomString(LengthToTarget)}{TargetString}";
		}

		[Benchmark]
		public bool StartsWithString()
		{
			return stringData.StartsWith(TargetString, Comparison);
		}
	}
}
