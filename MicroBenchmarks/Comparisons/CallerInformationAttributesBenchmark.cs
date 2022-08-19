// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CallerInformationAttributesBenchmark.cs">
//   Copyright (c) 2022 Johannes Deml. All rights reserved.
// </copyright>
// <author>
//   Johannes Deml
//   public@deml.io
// </author>
// --------------------------------------------------------------------------------------------------------------------

using System.Runtime.CompilerServices;
using BenchmarkDotNet.Attributes;
using MicroBenchmarks.Extensions;

namespace MicroBenchmarks
{
	[Config(typeof(DefaultBenchmarkConfig))]
	public class CallerInformationAttributesBenchmark
	{
		[Benchmark]
		public int CallWithMemberName()
		{
			return GetMemberNameLength();
		}

		[Benchmark]
		public int CallWithFilePath()
		{
			return GetFilePathLength();
		}

		[Benchmark]
		public int CallWithLineNumber()
		{
			return GetLineNumber();
		}

		[Benchmark]
		public int CallWithAll()
		{
			return GetMemberNameLength() + GetFilePathLength() + GetLineNumber();
		}

		private int GetMemberNameLength([CallerMemberName] string memberName = "")
		{
			return memberName.Length;
		}

		private int GetFilePathLength([CallerFilePath] string sourceFilePath = "")
		{
			return sourceFilePath.Length;
		}

		private int GetLineNumber([CallerLineNumber] int sourceLineNumber = 0)
		{
			return sourceLineNumber;
		}
	}
}
