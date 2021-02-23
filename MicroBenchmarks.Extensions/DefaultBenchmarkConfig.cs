// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DefaultBenchmarkConfig.cs">
//   Copyright (c) 2021 Johannes Deml. All rights reserved.
// </copyright>
// <author>
//   Johannes Deml
//   public@deml.io
// </author>
// --------------------------------------------------------------------------------------------------------------------

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
				.WithRuntime(new MonoRuntime("Mono x64", @"C:\Program Files\Unity\2020.2.5f1\Editor\Data\MonoBleedingEdge\bin\mono.exe"))
				.WithPlatform(Platform.X64));

			AddJob(baseJob
				.WithRuntime(CoreRuntime.Core50)
				.WithPlatform(Platform.X64));

			AddJob(baseJob
				.WithRuntime(CoreRuntime.Core31)
				.WithPlatform(Platform.X64));

			#if WINDOWS
			AddJob(baseJob
				.WithRuntime(ClrRuntime.Net48)
				.WithPlatform(Platform.X64));
			#endif

			AddColumn(FixedColumn.VersionColumn);
			AddColumn(FixedColumn.OperatingSystemColumn);
			AddColumn(FixedColumn.DateTimeColumn);

			AddExporter(MarkdownExporter.GitHub);
			AddExporter(new CsvExporter(CsvSeparator.Comma, ConfigConstants.CsvStyle));
		}
	}
}
