```

BenchmarkDotNet v0.15.8, macOS Tahoe 26.6.2 (25G83) [Darwin 25.6.0]
Apple M1 Pro, 1 CPU, 10 logical and 10 physical cores
.NET SDK 10.0.400
  [Host]     : .NET 10.0.11 (10.0.11, 10.0.1126.37416), Arm64 RyuJIT armv8.0-a
  Job-GEROSC : .NET 10.0.11 (10.0.11, 10.0.1126.37416), Arm64 RyuJIT armv8.0-a

Platform=AnyCpu  Runtime=.NET 10.0  Concurrent=True  
Force=True  Server=True  IterationTime=250ms  
MaxIterationCount=20  MinIterationCount=15  UnrollFactor=16  
WarmupCount=1  Version=1.2.0  OS=macOS 26.6.2  
DateTime=09/08/2026 21:42:10  SystemTag=Not set  

```
| Method                                 | CollectionLength | Mean         | Error       | StdDev      | 
|--------------------------------------- |----------------- |-------------:|------------:|------------:|
| **ArrayForEachLoopContains**               | **10**               |     **2.311 ns** |   **0.0264 ns** |   **0.0247 ns** | 
| ArrayLinqContains                      | 10               |     1.685 ns |   0.0220 ns |   0.0195 ns | 
| ListForEachLoopContains                | 10               |    10.655 ns |   0.1624 ns |   0.1356 ns | 
| ListFindIndex                          | 10               |     8.395 ns |   0.0607 ns |   0.0538 ns | 
| ListExists                             | 10               |     8.356 ns |   0.0709 ns |   0.0629 ns | 
| ListContains                           | 10               |     1.866 ns |   0.0202 ns |   0.0179 ns | 
| EnumerableInterfaceForEachLoopContains | 10               |     2.823 ns |   0.0273 ns |   0.0242 ns | 
| ReadOnlyListInterfaceContains          | 10               |     2.508 ns |   0.0433 ns |   0.0338 ns | 
| ReadOnlyCollectionContains             | 10               |     1.792 ns |   0.0117 ns |   0.0110 ns | 
| ReadOnlyCollectionInterfaceContains    | 10               |     2.260 ns |   0.0392 ns |   0.0367 ns | 
| LinkedListContains                     | 10               |     2.347 ns |   0.0305 ns |   0.0270 ns | 
| ListSortedBinarySearch                 | 10               |     3.927 ns |   0.0253 ns |   0.0225 ns | 
| SortedSetContains                      | 10               |     4.476 ns |   0.0610 ns |   0.0570 ns | 
| SortedSetTryGetValue                   | 10               |     4.116 ns |   0.0445 ns |   0.0416 ns | 
| HashSetContains                        | 10               |     2.255 ns |   0.0233 ns |   0.0218 ns | 
| DictionaryContainsKey                  | 10               |     2.267 ns |   0.0167 ns |   0.0156 ns | 
| DictionaryInterfaceContainsKey         | 10               |     2.510 ns |   0.0275 ns |   0.0243 ns | 
| DictionaryTryGetValue                  | 10               |     2.217 ns |   0.0215 ns |   0.0201 ns | 
| DictionaryContainsValue                | 10               |     5.750 ns |   0.0785 ns |   0.0696 ns | 
| SortedDictionaryContainsKey            | 10               |     9.564 ns |   0.0841 ns |   0.0702 ns | 
| **ArrayForEachLoopContains**               | **10000**            | **1,600.989 ns** |  **25.1196 ns** |  **22.2679 ns** | 
| ArrayLinqContains                      | 10000            |   461.840 ns |   2.4749 ns |   2.0667 ns | 
| ListForEachLoopContains                | 10000            | 2,673.071 ns |  22.1858 ns |  19.6671 ns | 
| ListFindIndex                          | 10000            | 6,513.711 ns |  59.9730 ns |  56.0988 ns | 
| ListExists                             | 10000            | 6,481.391 ns |  35.8130 ns |  29.9055 ns | 
| ListContains                           | 10000            |   474.548 ns |   5.7380 ns |   5.0866 ns | 
| EnumerableInterfaceForEachLoopContains | 10000            | 2,385.810 ns |  31.3422 ns |  27.7841 ns | 
| ReadOnlyListInterfaceContains          | 10000            |   472.372 ns |   5.1029 ns |   4.2612 ns | 
| ReadOnlyCollectionContains             | 10000            |   464.087 ns |   4.8401 ns |   4.5274 ns | 
| ReadOnlyCollectionInterfaceContains    | 10000            |   460.580 ns |   2.1517 ns |   1.9074 ns | 
| LinkedListContains                     | 10000            | 5,973.968 ns | 211.8229 ns | 243.9356 ns | 
| ListSortedBinarySearch                 | 10000            |    15.140 ns |   0.1701 ns |   0.1508 ns | 
| SortedSetContains                      | 10000            |    16.276 ns |   0.0838 ns |   0.0743 ns | 
| SortedSetTryGetValue                   | 10000            |    16.024 ns |   0.2385 ns |   0.2231 ns | 
| HashSetContains                        | 10000            |     2.278 ns |   0.0267 ns |   0.0250 ns | 
| DictionaryContainsKey                  | 10000            |     2.554 ns |   0.0278 ns |   0.0246 ns | 
| DictionaryInterfaceContainsKey         | 10000            |     2.629 ns |   0.0199 ns |   0.0176 ns | 
| DictionaryTryGetValue                  | 10000            |     2.379 ns |   0.0742 ns |   0.0854 ns | 
| DictionaryContainsValue                | 10000            | 3,186.424 ns |  63.7109 ns |  56.4781 ns | 
| SortedDictionaryContainsKey            | 10000            |    18.739 ns |   0.2211 ns |   0.2068 ns | 
