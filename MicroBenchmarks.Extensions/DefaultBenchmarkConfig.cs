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
using System.Runtime.InteropServices;
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

			var baseJob = Job.Default
				.WithUnrollFactor(16)
				.WithWarmupCount(1)
				.WithIterationTime(TimeInterval.FromMilliseconds(250))
				.WithMinIterationCount(15)
				.WithMaxIterationCount(20)
				.WithGcServer(true)
				.WithGcConcurrent(true)
				.WithGcForce(true);

			AddJob(baseJob
				.WithRuntime(CoreRuntime.Core50)
				.WithPlatform(Platform.X64));

			// AddJob(baseJob
			// 	.WithRuntime(CoreRuntime.Core31)
			// 	.WithPlatform(Platform.X64));

			// See win-benchmark.bat as an example
			var monoUnityPath = Environment.GetEnvironmentVariable("MONO_UNITY");
			if (monoUnityPath != null)
			{
				AddJob(baseJob
					.WithRuntime(new MonoRuntime("Unity Mono x64", monoUnityPath))
					.WithPlatform(Platform.X64));
			}


			AddColumn(FixedColumn.VersionColumn);
			AddColumn(FixedColumn.OperatingSystemColumn);
			AddColumn(FixedColumn.DateTimeColumn);

			AddExporter(MarkdownExporter.GitHub);
			AddExporter(new CsvExporter(CsvSeparator.Comma, ConfigConstants.CsvStyle));
		}
	}
}
