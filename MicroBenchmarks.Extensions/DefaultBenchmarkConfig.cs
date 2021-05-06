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
using BenchmarkDotNet.Exporters;
using BenchmarkDotNet.Exporters.Csv;
using BenchmarkDotNet.Jobs;
using Perfolizer.Horology;

namespace MicroBenchmarks.Extensions
{
	public class DefaultBenchmarkConfig : ManualConfig
	{
		public DefaultBenchmarkConfig()
		{
			Add(DefaultConfig.Instance);

			var baseJob = DefineBaseJob();

			AddJob(baseJob.WithRuntime(CoreRuntime.Core50));
			AddJob(baseJob.WithRuntime(CoreRuntime.Core31));
			AddJob(baseJob.WithRuntime(ClrRuntime.Net48));
			AddMonoJob(baseJob);
			
			ConfigHelper.AddDefaultColumns(this);
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

		protected virtual Job DefineBaseJob()
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
				.WithPlatform(Platform.X64);
		}
	}
}
