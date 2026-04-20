using System;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Extensions;
using MicroBenchmarks.Extensions;

namespace MicroBenchmarks
{
	[Config(typeof(DefaultBenchmarkConfig))]
	public class StringSpiltBenchmark
	{
		[Params(100, 10_000)]
		public int StringLength { get; set; }

		[Params(1, 100)]
		public int NumSeparators { get; set; }


		private char TargetChar = '|';
		private string TargetString = "|";
		private string stringData;

		[GlobalSetup]
		public void PrepareBenchmark()
		{
			stringData = ValuesGenerator.GenerateRandomString(StringLength - NumSeparators);
			// Spread out targets evenly across the string
			for (int i = 0; i < NumSeparators; i++)
			{
				stringData = stringData.Insert((StringLength / NumSeparators) * i, TargetString);
			}
		}

		[Benchmark]
		public string[] SplitByChar()
		{
			return stringData.Split(TargetChar);
		}

		[Benchmark]
		public string[] SplitByString()
		{
#if NET48
			// Not supported
			return new string[0];
#else
			return stringData.Split(TargetString);
#endif
		}
	}
}
