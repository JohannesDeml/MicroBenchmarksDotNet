using System;
using System.Text;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Extensions;
using Cysharp.Text;
using MicroBenchmarks.Extensions;

namespace MicroBenchmarks
{
	/// <summary>
	/// Compare classic string concatenation with string builder (with new instances created in each method)
	/// Additionally compare everything with ZString (<see href="https://github.com/Cysharp/ZString"/>)
	/// </summary>
	[Config(typeof(DefaultBenchmarkConfig))]
	public class StringConcatenationBenchmark
	{
		[Params(5, 100)]
		public int StringCount { get; set; }

		[Params(10, 1_000)]
		public int StringLength { get; set; }

		private string[] stringArray;

		[GlobalSetup]
		public void PrepareBenchmark()
		{
			stringArray = new string[StringCount];
			for (int i = 0; i < stringArray.Length; i++)
			{
				stringArray[i] = ValuesGenerator.GenerateRandomString(StringLength);
			}
		}

		[Benchmark]
		public string StringConcatenation()
		{
			string completeString = String.Empty;
			for (var i = 0; i < stringArray.Length; i++)
			{
				var part = stringArray[i];
				completeString += part;
			}

			return completeString;
		}

		[Benchmark]
		public string StringBuilderAppend()
		{
			StringBuilder sb = new StringBuilder();
			for (var i = 0; i < stringArray.Length; i++)
			{
				var part = stringArray[i];
				sb.Append(part);
			}

			return sb.ToString();
		}

		[Benchmark]
		public string StringBuilderInsert()
		{
			StringBuilder sb = new StringBuilder();
			for (var i = stringArray.Length - 1; i >= 0; i--)
			{
				var part = stringArray[i];
				sb.Insert(0, part);
			}

			return sb.ToString();
		}

		[Benchmark]
		public string ZStringConcatenation()
		{
			return ZString.Concat(stringArray);
		}

		[Benchmark]
		public string ZStringBuilderAppend()
		{
			using (Utf16ValueStringBuilder sb = ZString.CreateStringBuilder())
			{
				for (var i = 0; i < stringArray.Length; i++)
				{
					var part = stringArray[i];
					sb.Append(part);
				}

				return sb.ToString();
			}
		}

		[Benchmark]
		public string ZStringBuilderInsert()
		{
			using (Utf16ValueStringBuilder sb = ZString.CreateStringBuilder())
			{
				for (var i = stringArray.Length - 1; i >= 0; i--)
				{
					var part = stringArray[i];
					sb.Insert(0, part);
				}

				return sb.ToString();
			}
		}
	}
}
