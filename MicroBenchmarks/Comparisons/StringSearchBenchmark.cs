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
using System.Text;
using BenchmarkDotNet.Attributes;
using MicroBenchmarks.Extensions;

namespace MicroBenchmarks
{
	[Config(typeof(DefaultBenchmarkConfig))]
	public class StringSearchBenchmark
	{
		[Params(10, 10000)]
		public int LengthToTarget { get; set; }

		private const char TargetChar = '|';
		private const string TargetString = "|";
		private string stringData;

		[GlobalSetup]
		public void PrepareBenchmark()
		{
			Random random = new Random();
			const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
			StringBuilder sb = new StringBuilder();
			for (int i = 0; i < 2 * LengthToTarget + 1; i++)
			{
				if (i == LengthToTarget)
				{
					sb.Append(TargetString);
					continue;
				}

				sb.Append(random.Next(chars.Length));
			}

			stringData = sb.ToString();
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
