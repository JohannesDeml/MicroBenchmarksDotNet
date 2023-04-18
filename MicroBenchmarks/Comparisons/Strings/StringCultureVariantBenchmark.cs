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

		[Params(100)]
		public int StringLength { get; set; }

		private const string TargetString = "Target42";
		private string stringDataFail;
		private string stringDataSuccess;

		[GlobalSetup]
		public void PrepareBenchmark()
		{
			int randomLength = StringLength - TargetString.Length;
			stringDataFail = $"{ValuesGenerator.GenerateRandomString(randomLength)}{TargetString}";
			stringDataSuccess = $"{TargetString}{ValuesGenerator.GenerateRandomString(randomLength)}";
		}

		[Benchmark]
		public bool StartsWithStringFail()
		{
			return stringDataFail.StartsWith(TargetString, Comparison);
		}

		[Benchmark]
		public bool StartsWithStringSuccess()
		{
			return stringDataSuccess.StartsWith(TargetString, Comparison);
		}
	}
}
