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
| Method               | ArraySize | Mean         | Error      | StdDev     | Ratio | RatioSD | 
|--------------------- |---------- |-------------:|-----------:|-----------:|------:|--------:|
| **ForLoop**              | **100**       |     **49.77 ns** |   **0.360 ns** |   **0.336 ns** |  **1.00** |    **0.01** | 
| ForLoopPreIncrement  | 100       |     48.93 ns |   0.256 ns |   0.214 ns |  0.98 |    0.01 | 
| ForLoopCachedLength  | 100       |     44.49 ns |   0.257 ns |   0.241 ns |  0.89 |    0.01 | 
| ForLoopLocalVariable | 100       |     32.24 ns |   0.454 ns |   0.402 ns |  0.65 |    0.01 | 
| ForLoopUnroll4       | 100       |     25.18 ns |   0.181 ns |   0.169 ns |  0.51 |    0.00 | 
| ForeachLoop          | 100       |     31.44 ns |   0.162 ns |   0.143 ns |  0.63 |    0.00 | 
| ZLinqSum             | 100       |     31.86 ns |   0.351 ns |   0.329 ns |  0.64 |    0.01 | 
| LinqSum              | 100       |     56.46 ns |   0.494 ns |   0.412 ns |  1.13 |    0.01 | 
| ZLinqAggregate       | 100       |     37.03 ns |   0.246 ns |   0.218 ns |  0.74 |    0.01 | 
| LinqAggregate        | 100       |     32.00 ns |   0.320 ns |   0.300 ns |  0.64 |    0.01 | 
|                      |           |              |            |            |       |         | 
| **ForLoop**              | **100000**    | **43,409.44 ns** | **490.545 ns** | **434.855 ns** |  **1.00** |    **0.01** | 
| ForLoopPreIncrement  | 100000    | 43,707.27 ns | 614.117 ns | 574.445 ns |  1.01 |    0.02 | 
| ForLoopCachedLength  | 100000    | 39,426.35 ns | 441.447 ns | 412.929 ns |  0.91 |    0.01 | 
| ForLoopLocalVariable | 100000    | 32,316.27 ns | 632.929 ns | 677.227 ns |  0.74 |    0.02 | 
| ForLoopUnroll4       | 100000    | 31,674.77 ns | 372.044 ns | 348.010 ns |  0.73 |    0.01 | 
| ForeachLoop          | 100000    | 31,900.43 ns | 419.170 ns | 392.092 ns |  0.73 |    0.01 | 
| ZLinqSum             | 100000    | 31,539.70 ns | 536.904 ns | 475.951 ns |  0.73 |    0.01 | 
| LinqSum              | 100000    | 46,965.82 ns | 335.801 ns | 297.679 ns |  1.08 |    0.01 | 
| ZLinqAggregate       | 100000    | 31,922.84 ns | 134.327 ns | 104.874 ns |  0.74 |    0.01 | 
| LinqAggregate        | 100000    | 31,875.35 ns | 213.627 ns | 199.827 ns |  0.73 |    0.01 | 
