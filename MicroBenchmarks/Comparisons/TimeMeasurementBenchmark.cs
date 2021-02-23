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
		[BenchmarkCategory("Creation")]
		public Stopwatch CreateStopwatch()
		{
			stopwatch = new Stopwatch();
			return stopwatch;
		}

		[Benchmark]
		[BenchmarkCategory("Creation")]
		public DateTime CreateDateTime()
		{
			dateTime = DateTime.UtcNow;
			return dateTime;
		}

		[Benchmark]
		[BenchmarkCategory("Creation")]
		public DateTimeOffset CreateDateTimeOffset()
		{
			dateTimeOffset = DateTimeOffset.UtcNow;
			return dateTimeOffset;
		}



		[Benchmark]
		[BenchmarkCategory("TimeDifference")]
		public long StopwatchElapsed()
		{
			var elapsed = stopwatch.ElapsedMilliseconds;
			return elapsed;
		}

		[Benchmark]
		[BenchmarkCategory("TimeDifference")]
		public double DateTimeDifference()
		{
			var elapsed = (dateTime-DateTime.UtcNow).TotalMilliseconds;
			return elapsed;
		}

		[Benchmark]
		[BenchmarkCategory("TimeDifference")]
		public double DateTimeOffsetDifference()
		{
			var elapsed = (dateTimeOffset-DateTime.UtcNow).TotalMilliseconds;
			return elapsed;
		}
	}
}
