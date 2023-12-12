// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DefaultBenchmarkConfig.cs">
//   Copyright (c) 2021 Johannes Deml. All rights reserved.
// </copyright>
// <author>
//   Johannes Deml
//   public@deml.io
// </author>
// --------------------------------------------------------------------------------------------------------------------

using System;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Environments;
using BenchmarkDotNet.Jobs;
using Perfolizer.Horology;

namespace MicroBenchmarks.Extensions
{
	public class DefaultBenchmarkConfig : ManualConfig
	{
		public DefaultBenchmarkConfig()
		{
			var precise = Environment.GetEnvironmentVariable("HIGH_PRECISION");

			Job baseJob;
			if (precise == null || precise != "true")
			{
				baseJob = DefineFastBaseJob();
			}
			else
			{
				baseJob = DefineHighPrecisionBaseJob();
			}

			var runtimes = Environment.GetEnvironmentVariable("TARGET_RUNTIMES");
			if (runtimes == null)
			{
				AddJob(baseJob.WithRuntime(CoreRuntime.Core80));
			}
			else
			{
				AddRuntimesFromEnvironment(runtimes, baseJob);
			}

			ConfigHelper.AddDefaultColumns(this);
			ConfigHelper.AddDefaultExporters(this);
			WithOptions(ConfigOptions.StopOnFirstError);
		}

		private void AddRuntimesFromEnvironment(string runtimes, Job baseJob)
		{
			string[] runtimeArray = runtimes.Split(',');
			for (int i = 0; i < runtimeArray.Length; i++)
			{
				var runtime = runtimeArray[i].Trim();
				switch (runtime)
				{
					case "Core90":
						AddJob(baseJob.WithRuntime(CoreRuntime.Core90));
						break;
					case "Aot90":
						AddJob(baseJob.WithRuntime(NativeAotRuntime.Net90));
						break;
					case "Core80":
						AddJob(baseJob.WithRuntime(CoreRuntime.Core80));
						break;
					case "Aot80":
						AddJob(baseJob.WithRuntime(NativeAotRuntime.Net80));
						break;
					case "Core70":
						AddJob(baseJob.WithRuntime(CoreRuntime.Core70));
						break;
					case "Aot70":
						AddJob(baseJob.WithRuntime(NativeAotRuntime.Net70));
						break;
					case "Core60":
						AddJob(baseJob.WithRuntime(CoreRuntime.Core60));
						break;
					case "Aot60":
						AddJob(baseJob.WithRuntime(NativeAotRuntime.Net60));
						break;
					case "Core50":
						AddJob(baseJob.WithRuntime(CoreRuntime.Core50));
						break;
					case "Net48":
						AddJob(baseJob.WithRuntime(ClrRuntime.Net48));
						break;
					case "Mono":
						AddMonoJob(baseJob);
						break;
					default:
						throw new Exception($"Runtime {runtime} not supported");
				}
			}
		}

		private void AddMonoJob(Job baseJob)
		{
			// See win-benchmark.bat / linux-benchmark.sh
			var monoUnityPath = Environment.GetEnvironmentVariable("MONO_UNITY");
			if (monoUnityPath == null)
			{
				AddJob(baseJob.WithRuntime(MonoRuntime.Default));
				return;
			}

			var unityMonoRuntime = new MonoRuntime("Unity Mono x64", monoUnityPath);
			AddJob(baseJob.WithRuntime(unityMonoRuntime));
		}

		/// <summary>
		/// Fast in execution and good enough to get a rough performance estimate
		/// Use this setting in development to get fast feedback
		/// </summary>
		protected virtual Job DefineFastBaseJob()
		{
			return Job.Default
				.WithUnrollFactor(16)
				.WithWarmupCount(1)
				.WithIterationTime(TimeInterval.FromMilliseconds(250))
				.WithMinIterationCount(15)
				.WithMaxIterationCount(20)
				.WithGcServer(true)
				.WithGcConcurrent(true)
				.WithGcForce(true)
				.WithPlatform(Platform.AnyCpu);
		}

		/// <summary>
		/// Slower in execution, but more precise results that are stable throughout multiple runs
		/// Use this setting if you want to present data to others
		/// </summary>
		private Job DefineHighPrecisionBaseJob()
		{
			return Job.Default
				.WithGcServer(true)
				.WithGcConcurrent(true)
				.WithGcForce(true)
				.WithPlatform(Platform.AnyCpu);
		}
	}
}
