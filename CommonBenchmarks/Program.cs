using System;
using BenchmarkDotNet.Running;

namespace CommonBenchmarks
{
	class Program
	{
		private static int Main(string[] args)
		{
			BenchmarkRunner.Run<ConcurrentCollectionsBenchmark>();
			BenchmarkRunner.Run<CopyBytesBenchmark>();
			BenchmarkRunner.Run<HashGenerationBenchmark>();
			BenchmarkRunner.Run<LoopComparisonBenchmark>();
			BenchmarkRunner.Run<PauseAccuracyBenchmark>();
			BenchmarkRunner.Run<TimeMeasurementBenchmark>();

			return 0;
		}
	}
}
