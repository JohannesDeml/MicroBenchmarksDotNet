﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LambdaBenchmark.cs">
//   Copyright (c) 2022 Johannes Deml. All rights reserved.
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
	/// Test the time it takes to run a method compared to a prepared lambda statement
	/// For all tested frameworks the method call is 5-15 times faster than the lambda call
	/// </summary>
	[Config(typeof(DefaultBenchmarkConfig))]
	public class LambdaBenchmark
	{
		[Params(2)]
		public int FirstValue { get; set; }

		[Params(3)]
		public int SecondValue { get; set; }
		private int result;

		private Func<int, int, int> preparedLambdaFunction;

		[GlobalSetup]
		public void PrepareBenchmark()
		{
			preparedLambdaFunction = new Func<int, int, int>((a, b) => a + b);
		}

		[Benchmark]
		public int MethodCall()
		{
			int AddWithReturn(int a, int b)
			{
				return a + b;
			}

			return AddWithReturn(FirstValue, SecondValue);
		}


		[Benchmark]
		public int PreparedLambdaFunctionInvocation()
		{
			return preparedLambdaFunction.Invoke(FirstValue,SecondValue);
		}

		[Benchmark]
		public int LambdaFunctionInvocation()
		{
			var lambda = new Func<int, int, int>((a, b) => a + b);
			return lambda.Invoke(FirstValue, SecondValue);
		}

		[Benchmark]
		public int LambdaInvocation()
		{
			Action lambda = () => { result = FirstValue + SecondValue; };
			lambda.Invoke();
			return result;
		}

		[Benchmark]
		public int IndirectMethodCall()
		{
			void AddWithoutReturn(int a, int b)
			{
				result = a + b;
			}

			Action<int, int> action = AddWithoutReturn;
			action.Invoke(FirstValue, SecondValue);
			return result;
		}

	}
}