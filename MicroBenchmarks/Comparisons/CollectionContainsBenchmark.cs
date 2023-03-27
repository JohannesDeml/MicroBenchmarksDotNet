using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Extensions;
using MicroBenchmarks.Extensions;

namespace MicroBenchmarks
{
	[Config(typeof(DefaultBenchmarkConfig))]
	public class CollectionContainsBenchmark
	{
		[Params(10, 10_000)] public int CollectionLength { get; set; }

		private int[] array;
		private List<int> list;
		private ReadOnlyCollection<int> readOnlyCollection;
		private HashSet<int> hashSet;
		private Dictionary<int, int> dictionary;
		private int target;

		[GlobalSetup]
		public void PrepareBenchmark()
		{
			array = ValuesGenerator.ArrayOfUniqueValues<int>(CollectionLength);
			list = new List<int>(array);
			readOnlyCollection = new ReadOnlyCollection<int>(array);
			hashSet = new HashSet<int>(array);
			dictionary = list.ToDictionary(x => x, x => x);

			target = array[CollectionLength / 2];
		}

		[Benchmark]
		public bool ArrayIterateContains()
		{
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i] == target)
				{
					return true;
				}
			}

			return false;
		}

		[Benchmark]
		public bool ArrayLinqContains()
		{
			return array.Contains(target);
		}

		[Benchmark]
		public bool ListIterateContains()
		{
			for (int i = 0; i < list.Count; i++)
			{
				if (list[i] == target)
				{
					return true;
				}
			}

			return false;
		}

		[Benchmark]
		public bool ListFindIndex()
		{
			return list.FindIndex(x => x == target) != -1;
		}

		[Benchmark]
		public bool ListContains()
		{
			return list.Contains(target);
		}

		[Benchmark]
		public bool ReadOnlyCollectionContains()
		{
			return readOnlyCollection.Contains(target);
		}

		[Benchmark]
		public bool HashSetContains()
		{
			return hashSet.Contains(target);
		}

		[Benchmark]
		public bool DictionaryContainsKey()
		{
			return dictionary.ContainsKey(target);
		}

		[Benchmark]
		public bool DictionaryContainsValue()
		{
			return dictionary.ContainsValue(target);
		}
	}
}
