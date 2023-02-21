# Micro Benchmarks for C#

*Various benchmarks for showing hidden performance costs*  
[![Releases](https://img.shields.io/github/release/JohannesDeml/MicroBenchmarksDotNet/all.svg)](../../releases)[![.NET 6.0](https://img.shields.io/badge/.NET-6.0-blueviolet.svg)](https://dotnet.microsoft.com/download/dotnet/6.0)

## Setup
To reproduce the results, run `win-benchmark.bat` or `linux-benchmark.sh` as admin/root.  
The benchmarks are run with 2020 gaming PC after bootup of the system - [Hardware Details](https://pcpartpicker.com/b/8MMcCJ)  

```ini
BenchmarkDotNet=v0.12.1, OS=Windows 10.0.19042
AMD Ryzen 7 3700X, 1 CPU, 16 logical and 8 physical cores
  [Host]     : .NET Framework 4.8 (4.8.4341.0), X64 RyuJIT
  Job-YCYQHD : .NET Framework 4.8 (4.8.4341.0), X64 RyuJIT
  Job-ZUSNEY : .NET Core 3.1.9 (CoreCLR 4.700.20.47201, CoreFX 4.700.20.47203), X64 RyuJIT
  Job-BBIORM : .NET Core 5.0.4 (CoreCLR 5.0.421.11614, CoreFX 5.0.421.11614), X64 RyuJIT
  Job-OVYJNY : Mono 5.11.0 (Visual Studio), X86 

Platform=X64  Concurrent=True  Force=True  
Server=True  IterationTime=250.0000 ms  MaxIterationCount=20  
MinIterationCount=15  UnrollFactor=16  WarmupCount=1  
Version=1.0.0  OS=Microsoft Windows 10.0.19042   DateTime=04/13/2021 12:37:54  
```

By default only **.NET 6** is tested. However, you can target different runtimes with the environment variable `TARGET_RUNTIMES` (example: `export TARGET_RUNTIMES=Core60,Core50,Net48,Mono`). See also [linux-benchmark-all-runtimes.sh](./linux-benchmark-all-runtimes.sh) as an example for targeting all platforms.

| Runtime      | Variable |
| ------------ | -------- |
| .NET 7       | Core70   |
| NativeAOT 7  | Aot70    |
| .NET 6       | Core60   |
| NativeAOT 6, | Aot60    |
| .NET 5       | Core50   |
| .NET 4.8     | Net48    |
| (Unity) Mono | Mono     |

## Findings

### Conditional methods ([Test](./MicroBenchmarks/Comparisons/ConditionalFormattedLoggingBenchmark.cs))

* stripping method call logic happens also for the parameters inside the method call, so you don't have to worry about string concatenation if you do it in the call itself.

  * ```csharp
    // No overhead when stripped
    // the string interpolation is stripped away, if Log is stripped away
    Log($"firstParam: {firstParam}, secondParam: {secondParam}, thirdParam:{thirdParam}");
    ```

  * ```csharp
    // Adds overheas when stripped
    // String interpolation happens independant of stripping of Log
    string preparedMessage = $"firstParam: {firstParam}, secondParam: {secondParam}, thirdParam:{thirdParam}";
    Log(preparedMessage);
    ```



### Lambda calls ([Test](./MicroBenchmarks/Comparisons/LambdaBenchmark.cs))

* Even though lambdas are a nice way to add code directly within the method, it does result in less performance than having a direct method call and adds additional memory pressure. Using functions which don't require access to local variables does not result in additional memory pressure and has less general overhead.

  * ```csharp
    // Slow
    // accesses local variables and therefore allocates additional memory
    Action lambda = () => { result = FirstValue + SecondValue; };
    lambda.Invoke();
    return result;
    ```

  * ```csharp
    // Faster
    // no need to access local variables
    var lambda = new Func<int, int, int>((a, b) => a + b);
    return lambda.Invoke(FirstValue, SecondValue);
    ```

  * ```csharp
    // Fastest
    // direct calls are still the way to go for critical code paths
    int AddWithReturn(int a, int b)
    {
    	return a + b;
    }
    return AddWithReturn(FirstValue, SecondValue);
    ```



### Caller Information ([Test](./MicroBenchmarks/Comparisons/CallerInformationAttributesBenchmark.cs))

* Using attributes such as `[CallerMemberName]`, `[CallerFilePath]` and `[CallerLineNumber]` are a great addition to retrieve information about the calling methods without relying on expensive stacktrace methods. The overhead of the attributes is not measurable with Benchmarkdotnet (and therefore virtually nothing).



### String Search ([Test](./MicroBenchmarks/Comparisons/StringSearchBenchmark.cs))

* For short strings: `IndexOfChar` and `LastIndexOfChar` are significantly faster than both `IndexOfString` and `LastIndexOfString`.
* For long strings: While `IndexOfChar` is only slightly faster than `IndexOfString`, the increase between `LastIndexOfChar` and `LastIndexOfString`  is significant for .NET 6. `LastIndexOfChar` is over 7 times faster than `LastIndexOfString`, but still slower than the other two methods.



### Hash Generation ([Test](./MicroBenchmarks/Comparisons/HashGenerationBenchmark.cs))

![Hash Generation Comparison Chart](./Docs/hashgeneration100bytes-1.0.0.png)

* If you can, always use `TryXHash()` instead of `XHash()`
* Surprisingly, Sha256 performs better, than Sha1 which performs better than Md5 for .NET Core and .NET framework on modern hardware for .NET, for Mono it is the other way around. Always test on your target hardware, which is faster and don't assume that Md5 will be faster than Sha256 not matter what.



### UDP Sockets ([Test](./MicroBenchmarks/Comparisons/UdpBenchmark.cs))

![UDP Comparison Chart](./Docs/udpsocket-sendreceive-1.0.0.png)

* For some (for me yet unknown) reason, UDP performs a lot better on Windows safe mode, than it does with normal windows mode. This is the only benchmark, that shows that kind of behavior. For all other benchmarks the results are statistically the same for normal and safe mode.
* A further discussion can be found on [superuser](https://superuser.com/questions/1640588/windows-10-udp-socket-benchmark-a-lot-faster-in-safe-mode). If you have any input, I would love to know!



### Pause Accuracy ([Test](./MicroBenchmarks/Comparisons/PauseAccuracyBenchmark.cs))

![Pause Accuracy Chart](./Docs/pauseaccuracy2ms-1.0.0.png)

* `Thread.SpinWait(10)` and `Thread.Sleep(0)` allow for maximum precision, also on windows, but they block processing time that might be needed by other processes.
* `Thread.Sleep(TimeoutDuration)`  and all others have the problem that the granularity follows that of the system timer. For windows this is ~15ms.  
* There is trick to change the granularity by using winmm.dll's `timeBeginPeriod(uint period)` and `timeEndPeriod(uint period)`. This can be seen with ThreadSleepEnhanced - It works for .NET 5 and .NET Core 3.1, but not the others.
* Linux is a lot better in its granularity, but `TaskDelay` and `TimerAwait` have precision problems as well on .NET Core.
