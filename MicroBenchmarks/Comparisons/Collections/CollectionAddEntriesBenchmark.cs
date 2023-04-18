// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CollectionAddEntriesBenchmark.cs">
//   Copyright (c) 2023 Johannes Deml. All rights reserved.
// </copyright>
// <author>
//   Johannes Deml
//   public@deml.io
// </author>
// --------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Extensions;
using MicroBenchmarks.Extensions;

namespace MicroBenchmarks
{
	[Config(typeof(DefaultBenchmarkConfig))]
	public class CollectionAddEntriesBenchmark
	{
		[Params(10, 10_000)]
		public int AddCount { get; set; }

		private int[] valuesToAdd;
		private List<int> list;
		private IList<int> iList;
		private LinkedList<int> linkedList;
		private SortedSet<int> sortedSet;
		private HashSet<int> hashSet;
		private Dictionary<int, int> dictionary;
		private SortedDictionary<int, int> sortedDictionary;

		[GlobalSetup]
		public void PrepareBenchmark()
		{
			valuesToAdd = ValuesGenerator.ArrayOfUniqueValues<int>(AddCount);
			list = new List<int>(AddCount);
			iList = new List<int>(AddCount);
			linkedList = new LinkedList<int>();
			sortedSet = new SortedSet<int>();
			hashSet = new HashSet<int>(AddCount);
			dictionary = new Dictionary<int, int>(AddCount);
			sortedDictionary = new SortedDictionary<int, int>();
		}

		[Benchmark]
		public int ListAddAndClear()
		{
			for (int i = 0; i < valuesToAdd.Length; i++)
			{
				list.Add(valuesToAdd[i]);
			}
			list.Clear();
			return list.Count;
		}

		[Benchmark]
		public int ListAddRangeAndClear()
		{
			list.AddRange(valuesToAdd);
			list.Clear();
			return list.Count;
		}

		[Benchmark]
		public int IListAddAndClear()
		{
			for (int i = 0; i < valuesToAdd.Length; i++)
			{
				iList.Add(valuesToAdd[i]);
			}
			iList.Clear();
			return iList.Count;
		}

		[Benchmark]
		public int LinkedListAddAndClear()
		{
			for (int i = 0; i < valuesToAdd.Length; i++)
			{
				linkedList.AddLast(valuesToAdd[i]);
			}
			linkedList.Clear();
			return linkedList.Count;
		}

		[Benchmark]
		public int SortedSetAddAndClear()
		{
			for (int i = 0; i < valuesToAdd.Length; i++)
			{
				sortedSet.Add(valuesToAdd[i]);
			}
			sortedSet.Clear();
			return sortedSet.Count;
		}

		[Benchmark]
		public int HashSetAddAndClear()
		{
			for (int i = 0; i < valuesToAdd.Length; i++)
			{
				hashSet.Add(valuesToAdd[i]);
			}
			hashSet.Clear();
			return hashSet.Count;
		}

		[Benchmark]
		public int DictionaryAddAndClear()
		{
			for (int i = 0; i < valuesToAdd.Length; i++)
			{
				int value = valuesToAdd[i];
				dictionary.Add(value, value);
			}
			dictionary.Clear();
			return dictionary.Count;
		}

		[Benchmark]
		public int SortedDictionaryAddAndClear()
		{
			for (int i = 0; i < valuesToAdd.Length; i++)
			{
				int value = valuesToAdd[i];
				sortedDictionary.Add(value, value);
			}
			sortedDictionary.Clear();
			return sortedDictionary.Count;
		}
	}
}
