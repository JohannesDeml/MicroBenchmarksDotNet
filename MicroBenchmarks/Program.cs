// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Program.cs">
//   Copyright (c) 2021 Johannes Deml. All rights reserved.
// </copyright>
// <author>
//   Johannes Deml
//   public@deml.io
// </author>
// --------------------------------------------------------------------------------------------------------------------

using BenchmarkDotNet.Extensions;
using BenchmarkDotNet.Running;

namespace MicroBenchmarks
{
	public static class Program
	{
		private static int Main(string[] args)
		{
			// Run the _TestBenchmark (0) to see if all platforms are working as expected

			return BenchmarkSwitcher
				.FromAssembly(typeof(Program).Assembly)
				.Run(args)
				.ToExitCode();
		}
	}
}
