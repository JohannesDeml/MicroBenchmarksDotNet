using System;
using System.Collections.Generic;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Extensions;
using MicroBenchmarks.Extensions;

namespace MicroBenchmarks
{
	/// <summary>
	/// Instantiating different collections with a predefined set of values
	/// See also <see cref="CollectionAddEntriesBenchmark"/> for a similar benchmark
	/// </summary>
	[Config(typeof(DefaultBenchmarkConfig))]
	public class CollectionInstantiationBenchmark
	{
		[Params(10, 10_000)] public int CollectionLength { get; set; }

		private int[] data;

		[GlobalSetup]
		public void PrepareBenchmark()
		{
			data = ValuesGenerator.ArrayOfUniqueValues<int>(CollectionLength);
		}

		[Benchmark]
		public int[] ArrayForLoop()
		{
			int[] array = new int[CollectionLength];
			for (int i = 0; i < CollectionLength; i++)
			{
				array[i] = data[i];
			}

			return array;
		}

		[Benchmark]
		public int[] ArrayCopy()
		{
			int[] array = new int[CollectionLength];
			Array.Copy(data, array, CollectionLength);
			return array;
		}

		[Benchmark]
		public int[] ArrayClone()
		{
			int[] array = data.Clone() as int[];
			return array;
		}

		[Benchmark]
		public List<int> ListForLoop()
		{
			List<int> list = new List<int>();
			for (int i = 0; i < CollectionLength; i++)
			{
				list.Add(data[i]);
			}

			return list;
		}

		[Benchmark]
		public List<int> ListCapacityForLoop()
		{
			List<int> list = new List<int>(CollectionLength);
			for (int i = 0; i < CollectionLength; i++)
			{
				list.Add(data[i]);
			}

			return list;
		}

		[Benchmark]
		public List<int> ListAddRange()
		{
			List<int> list = new List<int>();
			list.AddRange(data);

			return list;
		}

		[Benchmark]
		public List<int> ListConstructor()
		{
			List<int> list = new List<int>(data);

			return list;
		}

		[Benchmark]
		public HashSet<int> HashSetCapacityForLoop()
		{
			HashSet<int> hashSet = new HashSet<int>(CollectionLength);
			for (int i = 0; i < CollectionLength; i++)
			{
				hashSet.Add(data[i]);
			}

			return hashSet;
		}

		[Benchmark]
		public HashSet<int> HashSetConstructor()
		{
			HashSet<int> hashSet = new HashSet<int>(data);

			return hashSet;
		}

		[Benchmark]
		public Dictionary<int,int> DictionaryCapacityForLoop()
		{
			Dictionary<int,int> dictionary = new Dictionary<int,int>(CollectionLength);
			for (int i = 0; i < CollectionLength; i++)
			{
				dictionary.Add(data[i],data[i]);
			}

			return dictionary;
		}
	}
}
