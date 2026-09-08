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
| Method               | StringCount | StringLength | Mean            | Error        | StdDev       | 
|--------------------- |------------ |------------- |----------------:|-------------:|-------------:|
| **StringConcatenation**  | **5**           | **10**           |        **42.55 ns** |     **0.277 ns** |     **0.246 ns** | 
| StringBuilderAppend  | 5           | 10           |        76.23 ns |     0.930 ns |     0.726 ns | 
| StringBuilderInsert  | 5           | 10           |       103.07 ns |     1.201 ns |     1.180 ns | 
| ZStringConcatenation | 5           | 10           |        29.17 ns |     0.179 ns |     0.158 ns | 
| ZStringBuilderAppend | 5           | 10           |        31.80 ns |     0.379 ns |     0.355 ns | 
| ZStringBuilderInsert | 5           | 10           |        83.32 ns |     0.754 ns |     0.706 ns | 
| **StringConcatenation**  | **5**           | **1000**         |     **1,751.44 ns** |    **23.125 ns** |    **20.499 ns** | 
| StringBuilderAppend  | 5           | 1000         |     1,898.58 ns |    31.695 ns |    29.647 ns | 
| StringBuilderInsert  | 5           | 1000         |     1,326.09 ns |    13.244 ns |    11.741 ns | 
| ZStringConcatenation | 5           | 1000         |       924.81 ns |    16.698 ns |    14.802 ns | 
| ZStringBuilderAppend | 5           | 1000         |       907.11 ns |     5.807 ns |     4.849 ns | 
| ZStringBuilderInsert | 5           | 1000         |     1,280.91 ns |    18.232 ns |    16.162 ns | 
| **StringConcatenation**  | **100**         | **10**           |     **6,368.38 ns** |    **53.006 ns** |    **49.582 ns** | 
| StringBuilderAppend  | 100         | 10           |       577.95 ns |     5.702 ns |     5.055 ns | 
| StringBuilderInsert  | 100         | 10           |    12,161.80 ns |    72.332 ns |    60.400 ns | 
| ZStringConcatenation | 100         | 10           |       419.28 ns |     2.574 ns |     2.408 ns | 
| ZStringBuilderAppend | 100         | 10           |       398.40 ns |     4.103 ns |     3.838 ns | 
| ZStringBuilderInsert | 100         | 10           |     2,602.26 ns |    11.727 ns |    10.395 ns | 
| **StringConcatenation**  | **100**         | **1000**         | **1,258,517.03 ns** | **7,660.661 ns** | **6,790.977 ns** | 
| StringBuilderAppend  | 100         | 1000         |    43,758.85 ns |   277.699 ns |   246.173 ns | 
| StringBuilderInsert  | 100         | 1000         |    61,638.20 ns |   522.127 ns |   488.398 ns | 
| ZStringConcatenation | 100         | 1000         |    36,130.61 ns |   489.507 ns |   433.935 ns | 
| ZStringBuilderAppend | 100         | 1000         |    36,947.20 ns |   675.600 ns |   598.901 ns | 
| ZStringBuilderInsert | 100         | 1000         |              NA |           NA |           NA | 

Benchmarks with issues:
  StringConcatenationBenchmark.ZStringBuilderInsert: Job-GEROSC(Platform=AnyCpu, Runtime=.NET 10.0, Concurrent=True, Force=True, Server=True, IterationTime=250ms, MaxIterationCount=20, MinIterationCount=15, UnrollFactor=16, WarmupCount=1) [StringCount=100, StringLength=1000]
