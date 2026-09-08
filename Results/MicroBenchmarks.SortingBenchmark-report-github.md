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
| Method               | ArraySize | Mean           | Error         | StdDev        | 
|--------------------- |---------- |---------------:|--------------:|--------------:|
| **SortArrayIComparable** | **10**        |       **7.330 ns** |     **0.1213 ns** |     **0.1135 ns** | 
| SortArrayIComparer   | 10        |      18.756 ns |     0.1674 ns |     0.1484 ns | 
| SortArrayLambda      | 10        |      12.055 ns |     0.1388 ns |     0.1230 ns | 
| SortArrayMethod      | 10        |      20.983 ns |     0.6943 ns |     0.7996 ns | 
| **SortArrayIComparable** | **10000**     |  **52,256.607 ns** |   **331.7690 ns** |   **310.3369 ns** | 
| SortArrayIComparer   | 10000     |  82,954.469 ns | 1,006.9245 ns |   941.8778 ns | 
| SortArrayLambda      | 10000     |  82,033.831 ns |   360.5162 ns |   319.5882 ns | 
| SortArrayMethod      | 10000     | 185,672.450 ns | 2,018.1512 ns | 1,887.7799 ns | 
