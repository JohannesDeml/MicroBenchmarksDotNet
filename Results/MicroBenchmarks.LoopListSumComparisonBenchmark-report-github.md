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
| Method               | ListSize | Mean         | Error      | StdDev     | Ratio | RatioSD | 
|--------------------- |--------- |-------------:|-----------:|-----------:|------:|--------:|
| **ForLoop**              | **100**      |     **55.59 ns** |   **0.695 ns** |   **0.650 ns** |  **1.00** |    **0.02** | 
| ForLoopPreIncrement  | 100      |     55.18 ns |   0.686 ns |   0.642 ns |  0.99 |    0.02 | 
| ForLoopCachedLength  | 100      |     49.86 ns |   0.554 ns |   0.491 ns |  0.90 |    0.01 | 
| ForLoopLocalVariable | 100      |     53.98 ns |   0.642 ns |   0.601 ns |  0.97 |    0.02 | 
| ForLoopUnroll4       | 100      |     34.12 ns |   0.394 ns |   0.349 ns |  0.61 |    0.01 | 
| ForeachLoop          | 100      |     50.25 ns |   0.584 ns |   0.518 ns |  0.90 |    0.01 | 
| ZLinqSum             | 100      |     71.34 ns |   0.379 ns |   0.354 ns |  1.28 |    0.02 | 
| LinqSum              | 100      |     70.75 ns |   0.733 ns |   0.650 ns |  1.27 |    0.02 | 
| ZLinqAggregate       | 100      |     36.88 ns |   0.303 ns |   0.269 ns |  0.66 |    0.01 | 
| LinqAggregate        | 100      |     32.67 ns |   0.501 ns |   0.469 ns |  0.59 |    0.01 | 
|                      |          |              |            |            |       |         | 
| **ForLoop**              | **100000**   | **51,144.46 ns** | **600.377 ns** | **561.593 ns** |  **1.00** |    **0.01** | 
| ForLoopPreIncrement  | 100000   | 51,172.97 ns | 709.673 ns | 629.107 ns |  1.00 |    0.02 | 
| ForLoopCachedLength  | 100000   | 51,010.93 ns | 489.099 ns | 408.420 ns |  1.00 |    0.01 | 
| ForLoopLocalVariable | 100000   | 47,602.72 ns | 917.595 ns | 942.302 ns |  0.93 |    0.02 | 
| ForLoopUnroll4       | 100000   | 35,847.44 ns | 679.441 ns | 635.549 ns |  0.70 |    0.01 | 
| ForeachLoop          | 100000   | 63,123.64 ns | 894.775 ns | 836.973 ns |  1.23 |    0.02 | 
| ZLinqSum             | 100000   | 63,043.96 ns | 840.508 ns | 745.089 ns |  1.23 |    0.02 | 
| LinqSum              | 100000   | 62,898.88 ns | 619.578 ns | 579.553 ns |  1.23 |    0.02 | 
| ZLinqAggregate       | 100000   | 32,002.70 ns | 444.223 ns | 415.527 ns |  0.63 |    0.01 | 
| LinqAggregate        | 100000   | 32,010.12 ns | 196.070 ns | 163.727 ns |  0.63 |    0.01 | 
