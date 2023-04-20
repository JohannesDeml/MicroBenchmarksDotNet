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
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Extensions;
using MicroBenchmarks.Extensions;

namespace MicroBenchmarks
{
	[Config(typeof(DefaultBenchmarkConfig))]
	public class StringSearchBenchmark
	{
		[Params(10, 10_000)]
		public int LengthToTarget { get; set; }

		private const char TargetChar = '|';
		private const string TargetString = "|";
		private string stringData;

		[GlobalSetup]
		public void PrepareBenchmark()
		{
			// Make sure distance to target is the same from start and end of the string
			stringData = ValuesGenerator.GenerateRandomString(LengthToTarget * 2);
			stringData.Insert(LengthToTarget, TargetString);
		}

		[Benchmark]
		public bool StartsWithString()
		{
			return stringData.StartsWith(TargetString, StringComparison.Ordinal);
		}

		[Benchmark]
		public bool EndsWithString()
		{
			return stringData.EndsWith(TargetString, StringComparison.Ordinal);
		}

		[Benchmark]
		public bool ContainsString()
		{
			return stringData.Contains(TargetString);
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
