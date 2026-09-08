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
| Method            | ArraySize | Mean       | Error     | StdDev    | Ratio | RatioSD | 
|------------------ |---------- |-----------:|----------:|----------:|------:|--------:|
| **ArrayCopy**         | **10**        |   **1.395 ns** | **0.0394 ns** | **0.0368 ns** |  **1.00** |    **0.04** | 
| ArrayCopyInstance | 10        |   2.379 ns | 0.0171 ns | 0.0160 ns |  1.71 |    0.04 | 
| BufferBlockCopy   | 10        |   1.910 ns | 0.0161 ns | 0.0143 ns |  1.37 |    0.04 | 
|                   |           |            |           |           |       |         | 
| **ArrayCopy**         | **10000**     | **171.409 ns** | **8.4403 ns** | **9.7198 ns** |  **1.00** |    **0.08** | 
| ArrayCopyInstance | 10000     | 170.015 ns | 6.7176 ns | 7.7361 ns |  0.99 |    0.07 | 
| BufferBlockCopy   | 10000     | 165.777 ns | 5.7861 ns | 6.6633 ns |  0.97 |    0.06 | 
