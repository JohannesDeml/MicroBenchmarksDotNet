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
| Method                    | CollectionSize | Mean      | Error     | StdDev    | 
|-------------------------- |--------------- |----------:|----------:|----------:|
| **RentReturnConcurrentBag**   | **16**             | **23.417 ns** | **0.2502 ns** | **0.2089 ns** | 
| RentReturnConcurrentStack | 16             | 16.191 ns | 0.1191 ns | 0.1114 ns | 
| RentReturnConcurrentQueue | 16             |  9.602 ns | 0.0869 ns | 0.0771 ns | 
| **RentReturnConcurrentBag**   | **128**            | **22.609 ns** | **0.2492 ns** | **0.2209 ns** | 
| RentReturnConcurrentStack | 128            | 16.134 ns | 0.1317 ns | 0.1168 ns | 
| RentReturnConcurrentQueue | 128            | 11.224 ns | 0.1399 ns | 0.1309 ns | 
| **RentReturnConcurrentBag**   | **1024**           | **23.829 ns** | **0.2276 ns** | **0.1901 ns** | 
| RentReturnConcurrentStack | 1024           | 16.247 ns | 0.3375 ns | 0.2992 ns | 
| RentReturnConcurrentQueue | 1024           | 11.202 ns | 0.0586 ns | 0.0519 ns | 
