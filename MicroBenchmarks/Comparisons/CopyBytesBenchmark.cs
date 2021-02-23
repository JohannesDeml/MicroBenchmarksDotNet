// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CopyBytesBenchmark.cs">
//   Copyright (c) 2021 Johannes Deml. All rights reserved.
// </copyright>
// <author>
//   Johannes Deml
//   public@deml.io
// </author>
// --------------------------------------------------------------------------------------------------------------------

using System;
using BenchmarkDotNet.Attributes;
using MicroBenchmarks.Extensions;

namespace MicroBenchmarks
{
	/// <summary>
	/// Interesting to read: https://github.com/dotnet/runtime/issues/4847#issuecomment-167397458
	/// </summary>
	[Config(typeof(DefaultBenchmarkConfig))]
	public class CopyBytesBenchmark
	{
		public const int Seed = 1337;

		[Params(10, 100, 1000, 10000)]
		public int ArraySize { get; set; }

		private byte[] sourceArray;
		private byte[] destinationArray;

		[GlobalSetup]
		public void PrepareBenchmark()
		{
			sourceArray = new byte[ArraySize];
			destinationArray = new byte[ArraySize];

			Random rnd = new Random(Seed);
			rnd.NextBytes(sourceArray);
		}

		[Benchmark(Baseline = true)]
		public byte[] ArrayCopy()
		{
			Array.Copy(sourceArray, 0, destinationArray, 0, ArraySize);
			return destinationArray;
		}

		[Benchmark]
		public byte[] BufferBlockCopy()
		{
			Buffer.BlockCopy(sourceArray, 0, destinationArray, 0, ArraySize);
			return destinationArray;
		}

		// Always slower
		// [Benchmark]
		public byte[] LoopCopy()
		{
			for (int i = 0; i < ArraySize; i++)
			{
				destinationArray[i] = sourceArray[i];
			}

			return destinationArray;
		}
	}
}
