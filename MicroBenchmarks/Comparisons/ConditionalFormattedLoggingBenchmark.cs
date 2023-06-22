// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ConditionalFormattedLoggingBenchmark.cs">
//   Copyright (c) 2021 Johannes Deml. All rights reserved.
// </copyright>
// <author>
//   Johannes Deml
//   public@deml.io
// </author>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Diagnostics;
using BenchmarkDotNet.Attributes;
using MicroBenchmarks.Extensions;

namespace MicroBenchmarks
{
	/// <summary>
	/// This benchmark takes a look at which point code stripping happens for conditional methods
	/// Good news is, that for all net-core 3.1, .NET 4.8, .NET5 and mono, the stripping already happens
	/// for the method input parameter.
	/// </summary>
	[Config(typeof(DefaultBenchmarkConfig))]
	public class ConditionalLoggingBenchmark
	{
		private string firstParam = "FirstParam";
		private int secondParam = 1000;
		private float thirdParam = 1234.567f;

		[Conditional("ALWAYS_FALSE_CONDITION")]
		private void Log(string message)
		{
			Console.Write(message);
		}

		[Conditional("ALWAYS_FALSE_CONDITION")]
		private void LogFormat(string message, object param0, object param1, object param2)
		{
			Console.Write(message, param0, param1, param2);
		}

		[Benchmark]
		public int LogFormatted()
		{
			LogFormat("firstParam: {0}, secondParam: {1}, thirdParam:{2}",
				firstParam, secondParam, thirdParam);
			return 1;
		}

		[Benchmark]
		public int LogStringInterpolation()
		{
			Log($"firstParam: {firstParam}, secondParam: {secondParam}, thirdParam:{thirdParam}");
			return 1;
		}

		[Benchmark]
		public int LogStringFormat()
		{
			Log(string.Format("firstParam: {0}, secondParam: {1}, thirdParam:{2}",
				firstParam, secondParam, thirdParam));
			return 1;
		}

		[Benchmark]
		public int LogPreparedString()
		{
			string preparedMessage = $"firstParam: {firstParam}, secondParam: {secondParam}, thirdParam:{thirdParam}";
			Log(preparedMessage);
			return 1;
		}
	}
}
