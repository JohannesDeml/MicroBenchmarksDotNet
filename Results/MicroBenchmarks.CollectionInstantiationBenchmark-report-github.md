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
| Method                        | CollectionLength | Mean           | Error         | StdDev        | 
|------------------------------ |----------------- |---------------:|--------------:|--------------:|
| **ArrayForLoop**                  | **10**               |       **9.719 ns** |     **0.0657 ns** |     **0.0582 ns** | 
| ArrayCopy                     | 10               |       6.211 ns |     0.0460 ns |     0.0384 ns | 
| ArrayClone                    | 10               |      65.166 ns |     0.4953 ns |     0.4633 ns | 
| ListForLoop                   | 10               |      48.016 ns |     0.2594 ns |     0.2166 ns | 
| ListCapacityForLoop           | 10               |      19.421 ns |     0.4166 ns |     0.3693 ns | 
| ListAddRange                  | 10               |      13.961 ns |     0.1482 ns |     0.1387 ns | 
| ListConstructor               | 10               |      13.692 ns |     0.1252 ns |     0.1171 ns | 
| ListConstructorHashSet        | 10               |      19.021 ns |     0.2507 ns |     0.2093 ns | 
| HashSetCapacityForLoop        | 10               |      52.076 ns |     0.4042 ns |     0.3375 ns | 
| HashSetConstructor            | 10               |      54.542 ns |     0.4013 ns |     0.3351 ns | 
| HashSetConstructorWithHashSet | 10               |      78.208 ns |     0.9959 ns |     0.9316 ns | 
| DictionaryCapacityForLoop     | 10               |      60.229 ns |     0.7322 ns |     0.6491 ns | 
| **ArrayForLoop**                  | **10000**            |   **6,474.224 ns** |    **87.8421 ns** |    **77.8697 ns** | 
| ArrayCopy                     | 10000            |   1,815.743 ns |    89.6457 ns |   103.2362 ns | 
| ArrayClone                    | 10000            |   1,404.130 ns |    51.9402 ns |    55.5754 ns | 
| ListForLoop                   | 10000            |  16,115.072 ns |   181.9951 ns |   142.0898 ns | 
| ListCapacityForLoop           | 10000            |  11,041.545 ns |    33.8374 ns |    28.2558 ns | 
| ListAddRange                  | 10000            |   2,012.309 ns |    46.3212 ns |    49.5631 ns | 
| ListConstructor               | 10000            |   2,021.863 ns |    77.5954 ns |    89.3589 ns | 
| ListConstructorHashSet        | 10000            |  11,185.999 ns |   146.3661 ns |   129.7497 ns | 
| HashSetCapacityForLoop        | 10000            |  94,521.971 ns | 1,278.8367 ns | 1,067.8866 ns | 
| HashSetConstructor            | 10000            |  64,817.971 ns |   743.6966 ns |   621.0204 ns | 
| HashSetConstructorWithHashSet | 10000            |  19,619.813 ns |   549.9036 ns |   633.2699 ns | 
| DictionaryCapacityForLoop     | 10000            | 110,187.468 ns | 1,217.6899 ns | 1,079.4504 ns | 
