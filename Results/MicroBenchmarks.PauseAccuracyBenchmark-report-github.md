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
| Method              | TimeoutDuration | Mean      | Error     | StdDev    | 
|-------------------- |---------------- |----------:|----------:|----------:|
| **ThreadSpinWait**      | **2**               |  **2.000 ms** | **0.0001 ms** | **0.0000 ms** | 
| ThreadSleep0        | 2               |  2.001 ms | 0.0002 ms | 0.0002 ms | 
| ThreadSleep         | 2               |  2.955 ms | 0.0315 ms | 0.0279 ms | 
| ThreadSleepEnhanced | 2               |  2.943 ms | 0.0220 ms | 0.0205 ms | 
| TaskDelay           | 2               |  2.510 ms | 0.0094 ms | 0.0079 ms | 
| TimerWait           | 2               |  2.508 ms | 0.0110 ms | 0.0098 ms | 
| AutoResetEvent      | 2               |  2.951 ms | 0.0270 ms | 0.0240 ms | 
| **ThreadSpinWait**      | **5**               |  **5.001 ms** | **0.0002 ms** | **0.0002 ms** | 
| ThreadSleep0        | 5               |  5.001 ms | 0.0005 ms | 0.0004 ms | 
| ThreadSleep         | 5               |  7.287 ms | 0.1422 ms | 0.1330 ms | 
| ThreadSleepEnhanced | 5               |  7.254 ms | 0.1007 ms | 0.0942 ms | 
| TaskDelay           | 5               |  6.216 ms | 0.0269 ms | 0.0238 ms | 
| TimerWait           | 5               |  6.224 ms | 0.0355 ms | 0.0332 ms | 
| AutoResetEvent      | 5               |  7.247 ms | 0.0840 ms | 0.0786 ms | 
| **ThreadSpinWait**      | **20**              | **20.001 ms** | **0.0003 ms** | **0.0003 ms** | 
| ThreadSleep0        | 20              | 20.002 ms | 0.0003 ms | 0.0003 ms | 
| ThreadSleep         | 20              | 26.952 ms | 0.7986 ms | 0.9197 ms | 
| ThreadSleepEnhanced | 20              | 27.290 ms | 0.5762 ms | 0.6635 ms | 
| TaskDelay           | 20              | 21.892 ms | 0.0998 ms | 0.0933 ms | 
| TimerWait           | 20              | 21.827 ms | 0.1353 ms | 0.1265 ms | 
| AutoResetEvent      | 20              | 27.098 ms | 0.7191 ms | 0.8281 ms | 
