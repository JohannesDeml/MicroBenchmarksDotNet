// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StringSearchBenchmark.cs">
//   Copyright (c) 2022 Johannes Deml. All rights reserved.
// </copyright>
// <author>
//   Johannes Deml
//   public@deml.io
// </author>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Linq;
using BenchmarkDotNet.Attributes;
using MicroBenchmarks.Extensions;

namespace MicroBenchmarks
{
	[Config(typeof(DefaultBenchmarkConfig))]
	public class StringSearchBenchmark
	{
		[Params(11, 10001)]
		public int Length { get; set; }

		public const char TargetChar = '|';
		public const string TargetString = "|";
		private string stringData;

		[GlobalSetup]
		public void PrepareBenchmark()
		{
			Random random = new Random();
			const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
			stringData = new string(Enumerable.Repeat(chars, Length -1)
				.Select(s => s[random.Next(s.Length)]).ToArray());
			stringData = stringData.Insert((int)Math.Floor(Length / 2.0f), TargetString);
		}


		[Benchmark]
		public int IndexOfString()
		{
			return stringData.IndexOf(TargetString, StringComparison.Ordinal);
		}

		[Benchmark]
		public int LastIndexOfString()
		{
			return stringData.LastIndexOf(TargetString, StringComparison.Ordinal);
		}

		[Benchmark]
		public int IndexOfChar()
		{
			return stringData.IndexOf(TargetChar);
		}

		[Benchmark]
		public int LastIndexOfChar()
		{
			return stringData.LastIndexOf(TargetChar);
		}
	}
}
