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
| Method                         | FirstValue | SecondValue | Mean      | Error     | StdDev    | Median    | 
|------------------------------- |----------- |------------ |----------:|----------:|----------:|----------:|
| InlinedCalculation             | 1          | 1           | 0.0000 ns | 0.0000 ns | 0.0000 ns | 0.0000 ns | 
| MethodCall                     | 1          | 1           | 0.0000 ns | 0.0000 ns | 0.0000 ns | 0.0000 ns | 
| StaticMethodCall               | 1          | 1           | 0.0000 ns | 0.0000 ns | 0.0000 ns | 0.0000 ns | 
| LocalFunctionCall              | 1          | 1           | 0.0000 ns | 0.0000 ns | 0.0000 ns | 0.0000 ns | 
| PreparedFuncDelegateInvocation | 1          | 1           | 0.0091 ns | 0.0163 ns | 0.0181 ns | 0.0000 ns | 
| FuncDelegateInvocation         | 1          | 1           | 0.7094 ns | 0.0219 ns | 0.0194 ns | 0.7130 ns | 
| PreparedActionInvocation       | 1          | 1           | 0.7799 ns | 0.0196 ns | 0.0184 ns | 0.7735 ns | 
| ActionInvocation               | 1          | 1           | 0.0000 ns | 0.0000 ns | 0.0000 ns | 0.0000 ns | 
| PreparedLambdaInvocation       | 1          | 1           | 1.2015 ns | 0.0422 ns | 0.0395 ns | 1.2064 ns | 
| LambdaInvocation               | 1          | 1           | 0.5715 ns | 0.0404 ns | 0.0450 ns | 0.5525 ns | 
