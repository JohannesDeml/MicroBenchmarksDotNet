using BenchmarkDotNet.Extensions;
using BenchmarkDotNet.Running;

namespace MicroBenchmarks
{
	class Program
	{
		private static int Main(string[] args)
		{
			return BenchmarkSwitcher
				.FromAssembly(typeof(Program).Assembly)
				.Run(args)
				.ToExitCode();
		}
	}
}
