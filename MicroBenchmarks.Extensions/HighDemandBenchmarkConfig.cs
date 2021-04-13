// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HighDemandBenchmarkConfig.cs">
//   Copyright (c) 2021 Johannes Deml. All rights reserved.
// </copyright>
// <author>
//   Johannes Deml
//   public@deml.io
// </author>
// --------------------------------------------------------------------------------------------------------------------

using BenchmarkDotNet.Engines;
using BenchmarkDotNet.Jobs;

namespace MicroBenchmarks.Extensions
{
	public class HighDemandBenchmarkConfig : DefaultBenchmarkConfig
	{
		protected override Job DefineBaseJob()
		{
			return Job.Default
				.WithStrategy(RunStrategy.Monitoring)
				.WithUnrollFactor(1)
				.WithLaunchCount(1)
				.WithWarmupCount(3)
				.WithIterationCount(15)
				.WithInvocationCount(1)
				.WithGcServer(true)
				.WithGcConcurrent(true)
				.WithGcForce(true);
		}
	}
}
