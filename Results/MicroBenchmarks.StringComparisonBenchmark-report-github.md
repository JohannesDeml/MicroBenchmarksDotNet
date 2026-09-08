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
| Method                  | Comparison           | SearchStringLength | Mean      | Error     | StdDev    | 
|------------------------ |--------------------- |------------------- |----------:|----------:|----------:|
| **StartsWithStringFail**    | **CurrentCulture**       | **100**                | **99.103 ns** | **0.6086 ns** | **0.5693 ns** | 
| **StartsWithStringFail**    | **Curre(...)eCase [24]** | **100**                | **99.012 ns** | **0.8681 ns** | **0.7249 ns** | 
| **StartsWithStringFail**    | **InvariantCulture**     | **100**                | **97.724 ns** | **1.7705 ns** | **1.6562 ns** | 
| **StartsWithStringFail**    | **Invar(...)eCase [26]** | **100**                | **96.317 ns** | **0.5032 ns** | **0.4461 ns** | 
| **StartsWithStringFail**    | **Ordinal**              | **100**                |  **7.849 ns** | **0.0671 ns** | **0.0595 ns** | 
| **StartsWithStringFail**    | **OrdinalIgnoreCase**    | **100**                | **13.775 ns** | **0.1103 ns** | **0.0861 ns** | 
| **StartsWithStringSuccess** | **CurrentCulture**       | **100**                | **99.142 ns** | **0.5665 ns** | **0.5299 ns** | 
| **StartsWithStringSuccess** | **Curre(...)eCase [24]** | **100**                | **99.122 ns** | **0.9909 ns** | **0.8784 ns** | 
| **StartsWithStringSuccess** | **InvariantCulture**     | **100**                | **96.491 ns** | **0.6114 ns** | **0.4773 ns** | 
| **StartsWithStringSuccess** | **Invar(...)eCase [26]** | **100**                | **96.201 ns** | **0.3747 ns** | **0.3321 ns** | 
| **StartsWithStringSuccess** | **Ordinal**              | **100**                | **16.079 ns** | **0.3236 ns** | **0.3026 ns** | 
| **StartsWithStringSuccess** | **OrdinalIgnoreCase**    | **100**                | **17.697 ns** | **0.1485 ns** | **0.1240 ns** | 
