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
| Method                 | Mean        | Error     | StdDev    | 
|----------------------- |------------:|----------:|----------:|
| LogFormatted           |   0.0000 ns | 0.0000 ns | 0.0000 ns | 
| LogStringInterpolation |   0.0000 ns | 0.0000 ns | 0.0000 ns | 
| LogStringFormat        |   0.0000 ns | 0.0000 ns | 0.0000 ns | 
| LogStringWrapped       | 121.0958 ns | 1.1048 ns | 1.0334 ns | 
| LogPreparedString      |  79.5761 ns | 0.5235 ns | 0.4371 ns | 
