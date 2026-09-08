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
| Method                      | AddCount | Mean           | Error          | StdDev         | 
|---------------------------- |--------- |---------------:|---------------:|---------------:|
| **ListAddAndClear**             | **10**       |      **26.726 ns** |      **0.5479 ns** |      **0.5382 ns** | 
| ListAddRangeAndClear        | 10       |       7.395 ns |      0.1952 ns |      0.2248 ns | 
| IListAddAndClear            | 10       |      25.400 ns |      0.6474 ns |      0.7196 ns | 
| LinkedListAddAndClear       | 10       |     111.166 ns |      0.7723 ns |      0.6846 ns | 
| SortedSetAddAndClear        | 10       |     139.030 ns |      1.3015 ns |      1.2174 ns | 
| HashSetAddAndClear          | 10       |      34.047 ns |      0.6018 ns |      0.5629 ns | 
| DictionaryAddAndClear       | 10       |      38.178 ns |      0.2031 ns |      0.1800 ns | 
| SortedDictionaryAddAndClear | 10       |     149.830 ns |      1.7311 ns |      1.6192 ns | 
| **ListAddAndClear**             | **10000**    |  **22,576.786 ns** |    **286.6322 ns** |    **254.0920 ns** | 
| ListAddRangeAndClear        | 10000    |     614.443 ns |     29.2596 ns |     33.6954 ns | 
| IListAddAndClear            | 10000    |  22,904.846 ns |    398.9083 ns |    311.4413 ns | 
| LinkedListAddAndClear       | 10000    | 122,385.935 ns |  1,103.4405 ns |  1,032.1589 ns | 
| SortedSetAddAndClear        | 10000    | 901,841.596 ns | 11,486.2002 ns | 10,744.1988 ns | 
| HashSetAddAndClear          | 10000    |  60,117.583 ns |  1,752.9522 ns |  1,948.4017 ns | 
| DictionaryAddAndClear       | 10000    |  77,158.901 ns |  1,790.3271 ns |  2,061.7433 ns | 
| SortedDictionaryAddAndClear | 10000    | 965,346.198 ns |  9,276.3878 ns |  8,677.1389 ns | 
