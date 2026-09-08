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
| Method        | ArraySize | Mean        | Error     | StdDev    | 
|-------------- |---------- |------------:|----------:|----------:|
| **Md5Hmac**       | **100**       |  **1,133.2 ns** |  **10.50 ns** |   **8.77 ns** | 
| Sha1Hmac      | 100       |    682.2 ns |   5.96 ns |   5.28 ns | 
| Sha256Hmac    | 100       |    649.6 ns |   5.52 ns |   4.89 ns | 
| Sha512Hmac    | 100       |  1,110.4 ns |  11.70 ns |  10.94 ns | 
| TryMd5Hmac    | 100       |  1,121.0 ns |  18.51 ns |  17.31 ns | 
| TrySha1Hmac   | 100       |    648.9 ns |   4.62 ns |   4.32 ns | 
| TrySha256Hmac | 100       |    616.2 ns |   5.64 ns |   5.00 ns | 
| TrySha512Hmac | 100       |  1,069.1 ns |  10.27 ns |   8.58 ns | 
| **Md5Hmac**       | **10000**     | **16,328.8 ns** | **150.62 ns** | **140.89 ns** | 
| Sha1Hmac      | 10000     |  4,790.9 ns |  58.60 ns |  51.95 ns | 
| Sha256Hmac    | 10000     |  4,739.5 ns |  35.83 ns |  31.76 ns | 
| Sha512Hmac    | 10000     |  8,215.9 ns |  59.92 ns |  53.12 ns | 
| TryMd5Hmac    | 10000     | 16,247.5 ns | 141.25 ns | 125.21 ns | 
| TrySha1Hmac   | 10000     |  4,749.3 ns |  49.27 ns |  46.09 ns | 
| TrySha256Hmac | 10000     |  4,693.5 ns |  19.60 ns |  15.30 ns | 
| TrySha512Hmac | 10000     |  8,318.5 ns | 163.43 ns | 144.87 ns | 
