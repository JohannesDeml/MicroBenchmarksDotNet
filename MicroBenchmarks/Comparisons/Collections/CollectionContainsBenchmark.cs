using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Extensions;
using MicroBenchmarks.Extensions;

namespace MicroBenchmarks
{
	/// <summary>
	/// Checking if an entry exists in different collections
	/// </summary>
	[Config(typeof(DefaultBenchmarkConfig))]
	public class CollectionContainsBenchmark
	{
		[Params(10, 10_000)] public int CollectionLength { get; set; }

		private int[] array;
		private IEnumerable<int> enumerable;
		private List<int> list;
		private ReadOnlyCollection<int> readOnlyCollection;
		private LinkedList<int> linkedList;
		private List<int> listSorted;
		private SortedSet<int> sortedSet;
		private HashSet<int> hashSet;
		private Dictionary<int, int> dictionary;
		private SortedDictionary<int, int> sortedDictionary;
		private int target;

		[GlobalSetup]
		public void PrepareBenchmark()
		{
			array = ValuesGenerator.ArrayOfUniqueValues<int>(CollectionLength);
			enumerable = array;
			list = new List<int>(array);
			readOnlyCollection = new ReadOnlyCollection<int>(array);
			linkedList = new LinkedList<int>(array);
			listSorted = new List<int>(array);
			listSorted.Sort();
			sortedSet = new SortedSet<int>(array);
			hashSet = new HashSet<int>(array);
			dictionary = list.ToDictionary(x => x, x => x);
			sortedDictionary = new SortedDictionary<int, int>(dictionary);

			target = array[CollectionLength / 2];
		}

		[Benchmark]
		public bool ArrayForLoopContains()
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
		public bool ArrayForEachLoopContains()
		{
			foreach (int value in array)
			{
				if (value == target)
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
		public bool EnumerableForEachLoopContains()
		{
			foreach (int value in enumerable)
			{
				if (value == target)
				{
					return true;
				}
			}

			return false;
		}

		[Benchmark]
		public bool ListForLoopContains()
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
		public bool ListForEachLoopContains()
		{
			foreach (int value in list)
			{
				if (value == target)
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
		public bool ListExists()
		{
			return list.Exists(x => x == target);
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
		public bool LinkedListContains()
		{
			return linkedList.Contains(target);
		}

		[Benchmark]
		public bool ListSortedBinarySearch()
		{
			// since the value is exactly in the middle, this is the worst case for the binary search
			return listSorted.BinarySearch(target) != -1;
		}

		[Benchmark]
		public bool SortedSetContains()
		{
			return sortedSet.Contains(target);
		}

		[Benchmark]
		public bool SortedSetTryGetValue()
		{
			return sortedSet.TryGetValue(target, out int value);
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
		public bool DictionaryTryGetValue()
		{
			return dictionary.TryGetValue(target, out int value);
		}

		[Benchmark]
		public bool DictionaryContainsValue()
		{
			return dictionary.ContainsValue(target);
		}

		[Benchmark]
		public bool SortedDictionaryContainsKey()
		{
			return sortedDictionary.ContainsKey(target);
		}
	}
}
