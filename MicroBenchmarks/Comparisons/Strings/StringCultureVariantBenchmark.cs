using System;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Extensions;
using MicroBenchmarks.Extensions;

namespace MicroBenchmarks
{
	[Config(typeof(DefaultBenchmarkConfig))]
	public class StringComparisonBenchmark
	{
		[Params(StringComparison.Ordinal, StringComparison.OrdinalIgnoreCase,
			StringComparison.InvariantCulture, StringComparison.InvariantCultureIgnoreCase,
			StringComparison.CurrentCulture, StringComparison.CurrentCultureIgnoreCase)]
		public StringComparison Comparison { get; set; }

		[Params(100)]
		public int SearchStringLength { get; set; }
		private const int TotalStringLength = 500;

		private string searchString;
		private string stringDataFail;
		private string stringDataSuccess;



		[GlobalSetup]
		public void PrepareBenchmark()
		{
			int randomLength = TotalStringLength - SearchStringLength;
			searchString = ValuesGenerator.GenerateRandomString(SearchStringLength);
			stringDataFail = $"{ValuesGenerator.GenerateRandomString(randomLength)}{searchString}";
			stringDataSuccess = $"{searchString}{ValuesGenerator.GenerateRandomString(randomLength)}";
		}

		[Benchmark]
		[BenchmarkCategory("StartsWithFail")]
		public bool StartsWithStringFail()
		{
			return stringDataFail.StartsWith(searchString, Comparison);
		}

		[Benchmark]
		[BenchmarkCategory("StartsWithSuccess")]
		public bool StartsWithStringSuccess()
		{
			return stringDataSuccess.StartsWith(searchString, Comparison);
		}
	}
}
