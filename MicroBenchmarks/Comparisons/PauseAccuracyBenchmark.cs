﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PauseAccuracyBenchmark.cs">
//   Copyright (c) 2021 Johannes Deml. All rights reserved.
// </copyright>
// <author>
//   Johannes Deml
//   public@deml.io
// </author>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using BenchmarkDotNet.Attributes;
using MicroBenchmarks.Extensions;
#if WINDOWS
using System.Runtime.InteropServices;
#endif

namespace MicroBenchmarks
{
	/// <summary>
	/// Testing different pause/sleep/time logics for their accuracy for small durations
	/// Interesting resources:
	/// http://geekswithblogs.net/akraus1/archive/2015/06/18/165219.aspx
	/// https://gamedev.stackexchange.com/questions/174240/server-game-loop
	/// https://blogs.msmvps.com/peterritchie/2007/04/26/thread-sleep-is-a-sign-of-a-poorly-designed-program/
	/// </summary>
	[Config(typeof(DefaultBenchmarkConfig))]
	public class PauseAccuracyBenchmark
	{
		/// <summary>
		/// Time to wait in milliseconds
		/// </summary>
		[Params(2, 5, 20)]
		public int TimeoutDuration { get; set; }

		private Stopwatch sw;

		/// <summary>
		/// Another thread running to get a better grasp of how those times behave in a multi-threaded environment
		/// Results seem to be the same right now, but might be interesting in the future
		/// </summary>
		private Thread otherThread;

		#if WINDOWS
		/// Get higher precision for Thread.Sleep on Windows
		/// See https://web.archive.org/web/20051125042113/http://www.dotnet247.com/247reference/msgs/57/289291.aspx
		/// See https://docs.microsoft.com/en-us/windows/win32/api/timeapi/nf-timeapi-timebeginperiod
		[DllImport("winmm.dll")]
		private static extern uint timeBeginPeriod(uint period);

		[DllImport("winmm.dll")]
		private static extern uint timeEndPeriod(uint period);
		#endif

		private bool loopOtherThread;

		[GlobalSetup]
		public void PrepareBenchmark()
		{
			if (sw == null)
			{
				sw = new Stopwatch();
			}

			StartOtherThread();
		}

		[GlobalCleanup]
		public void CleanupBenchmark()
		{
			loopOtherThread = false;
			Console.WriteLine("Stopping other thread");
			while (otherThread.IsAlive)
			{
				Thread.Sleep(0);
			}

			Console.WriteLine("Other thread stopped");
		}

		private void StartOtherThread()
		{
			loopOtherThread = true;
			if (otherThread == null)
			{
				otherThread = new Thread(OtherThreadLoop);
				otherThread.Name = "OtherThread";
			}

			Console.WriteLine("Starting other thread");
			otherThread.Start();
			while (!otherThread.IsAlive)
			{
				Thread.Sleep(0);
			}

			Console.WriteLine("Other thread started");
		}

		private void OtherThreadLoop()
		{
			while (loopOtherThread)
			{
				// Do stuff
				Thread.Sleep(0);
			}
		}


		[Benchmark]
		public long ThreadSpinWait()
		{
			sw.Restart();
			while (sw.ElapsedMilliseconds < TimeoutDuration)
			{
				Thread.SpinWait(10);
			}

			return sw.ElapsedMilliseconds;
		}

		[Benchmark]
		public long ThreadSleep0()
		{
			sw.Restart();
			while (sw.ElapsedMilliseconds < TimeoutDuration)
			{
				Thread.Sleep(0);
			}

			return sw.ElapsedMilliseconds;
		}

		[Benchmark]
		public long ThreadSleep()
		{
			sw.Restart();
			Thread.Sleep(TimeoutDuration);
			return sw.ElapsedMilliseconds;
		}

		[Benchmark]
		public long ThreadSleepEnhanced()
		{
			#if WINDOWS
				timeBeginPeriod(1);
			#endif
			sw.Restart();
			Thread.Sleep(TimeoutDuration);
			#if WINDOWS
				timeEndPeriod(1);
			#endif

			return sw.ElapsedMilliseconds;
		}

		[Benchmark]
		public async Task<long> TaskDelay()
		{
			sw.Restart();
			await Task.Delay(TimeoutDuration);
			return sw.ElapsedMilliseconds;
		}

		private System.Timers.Timer aTimer;
		private bool timerFinished = false;

		[GlobalSetup(Target = nameof(TimerWait))]
		public void PrepareTimerWait()
		{
			PrepareBenchmark();
			aTimer = new System.Timers.Timer(TimeoutDuration);
			aTimer.Elapsed += (Object source, ElapsedEventArgs e) => { timerFinished = true; };
			aTimer.AutoReset = false;
			aTimer.Enabled = true;
			aTimer.Stop();
		}

		[GlobalCleanup(Target = nameof(TimerWait))]
		public void CleanupTimerWait()
		{
			CleanupBenchmark();
			aTimer.Dispose();
		}

		[Benchmark]
		public long TimerWait()
		{
			sw.Restart();
			aTimer.Start();
			while (!timerFinished)
			{
				Thread.SpinWait(10);
			}

			timerFinished = false;
			return sw.ElapsedMilliseconds;
		}

		private AutoResetEvent timerEvent;

		[GlobalSetup(Target = nameof(AutoResetEvent))]
		public void PrepareTimerEvent()
		{
			PrepareBenchmark();
			timerEvent = new AutoResetEvent(true);
		}

		[Benchmark]
		public long AutoResetEvent()
		{
			sw.Restart();
			timerEvent.WaitOne(TimeoutDuration);
			return sw.ElapsedMilliseconds;
		}
	}
}
