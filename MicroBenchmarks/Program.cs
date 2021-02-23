using BenchmarkDotNet.Extensions;
using BenchmarkDotNet.Running;

namespace MicroBenchmarks
{
	public static class Program
	{
		private static int Main(string[] args)
		{
			// Run the test benchmark to see if all runtimes are working as expected
			//BenchmarkRunner.Run<TestBenchmark>();

			return BenchmarkSwitcher
				.FromAssembly(typeof(Program).Assembly)
				.Run(args)
				.ToExitCode();
		}
	}
}
