// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TimeMeasurementBenchmark.cs">
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
	[Config(typeof(DefaultBenchmarkConfig))]
	public class TimeMeasurementBenchmark
	{
		private const string CreationCategory = "Creation";
		private const string DifferenceCategory = "TimeDifference";

		private Stopwatch stopwatch;
		private DateTime dateTime;
		private DateTimeOffset dateTimeOffset;

		[GlobalSetup]
		public void PrepareBenchmark()
		{
			CreateStopwatch();
			CreateDateTime();
			CreateDateTimeOffset();
		}

		[Benchmark]
		[BenchmarkCategory(CreationCategory)]
		public Stopwatch CreateStopwatch()
		{
			stopwatch = new Stopwatch();
			return stopwatch;
		}

		[Benchmark]
		[BenchmarkCategory(CreationCategory)]
		public DateTime CreateDateTime()
		{
			dateTime = DateTime.UtcNow;
			return dateTime;
		}

		[Benchmark]
		[BenchmarkCategory(CreationCategory)]
		public DateTimeOffset CreateDateTimeOffset()
		{
			dateTimeOffset = DateTimeOffset.UtcNow;
			return dateTimeOffset;
		}


		[Benchmark]
		[BenchmarkCategory(DifferenceCategory)]
		public long StopwatchElapsed()
		{
			var elapsed = stopwatch.ElapsedMilliseconds;
			return elapsed;
		}

		[Benchmark]
		[BenchmarkCategory(DifferenceCategory)]
		public double DateTimeDifference()
		{
			var elapsed = (dateTime - DateTime.UtcNow).TotalMilliseconds;
			return elapsed;
		}

		[Benchmark]
		[BenchmarkCategory(DifferenceCategory)]
		public double DateTimeOffsetDifference()
		{
			var elapsed = (dateTimeOffset - DateTime.UtcNow).TotalMilliseconds;
			return elapsed;
		}
	}
}
