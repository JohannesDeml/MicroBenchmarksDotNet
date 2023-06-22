// --------------------------------------------------------------------------------------------------------------------
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
		[Params(1)]
		public int FirstValue { get; set; }

		[Params(1)]
		public int SecondValue { get; set; }

		private int result;
		private Func<int, int, int> preparedFuncDelegate;
		private Action<int, int> preparedAction;
		private Action preparedLambdaAction;

		[GlobalSetup]
		public void PrepareBenchmark()
		{
			preparedFuncDelegate = (a, b) => a + b;
			preparedLambdaAction = () => { result = FirstValue + SecondValue; };
			preparedAction = AddWithoutReturn;
		}

		private int Add(int a, int b)
		{
			return a + b;
		}

		private static int StaticAdd(int a, int b)
		{
			return a + b;
		}

		private void AddWithoutReturn(int a, int b)
		{
			result = a + b;
		}

		[Benchmark]
		public int InlinedCalculation()
		{
			return FirstValue + SecondValue;
		}

		[Benchmark]
		public int MethodCall()
		{
			return Add(FirstValue, SecondValue);
		}

		[Benchmark]
		public int StaticMethodCall()
		{
			return StaticAdd(FirstValue, SecondValue);
		}

		[Benchmark]
		public int LocalFunctionCall()
		{
			int LocalAdd(int a, int b)
			{
				return a + b;
			}

			return LocalAdd(FirstValue, SecondValue);
		}


		[Benchmark]
		public int PreparedFuncDelegateInvocation()
		{
			return preparedFuncDelegate.Invoke(FirstValue,SecondValue);
		}

		[Benchmark]
		public int FuncDelegateInvocation()
		{
			var lambda = new Func<int, int, int>((a, b) => a + b);
			return lambda.Invoke(FirstValue, SecondValue);
		}

		[Benchmark]
		public int PreparedActionInvocation()
		{
			preparedAction.Invoke(FirstValue, SecondValue);
			return result;
		}

		[Benchmark]
		public int ActionInvocation()
		{
			Action<int, int> action = AddWithoutReturn;
			action.Invoke(FirstValue, SecondValue);
			return result;
		}

		[Benchmark]
		public int PreparedLambdaInvocation()
		{
			preparedLambdaAction.Invoke();
			return result;
		}

		[Benchmark]
		public int LambdaInvocation()
		{
			Action lambda = () => { result = FirstValue + SecondValue; };
			lambda.Invoke();
			return result;
		}

	}
}
