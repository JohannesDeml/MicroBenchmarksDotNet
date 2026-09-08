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
| Method        | ArraySize | Mean         | Error      | StdDev     | 
|-------------- |---------- |-------------:|-----------:|-----------:|
| **Md5Hash**       | **100**       |    **417.92 ns** |   **5.743 ns** |   **5.372 ns** | 
| Sha1Hash      | 100       |    309.60 ns |   1.426 ns |   1.333 ns | 
| Sha256Hash    | 100       |    126.21 ns |   0.801 ns |   0.625 ns | 
| Sha512Hash    | 100       |    179.23 ns |   1.283 ns |   1.200 ns | 
| TryMd5Hash    | 100       |    388.52 ns |   5.069 ns |   4.494 ns | 
| TrySha1Hash   | 100       |    270.38 ns |   2.636 ns |   2.201 ns | 
| TrySha256Hash | 100       |     91.63 ns |   0.637 ns |   0.595 ns | 
| TrySha512Hash | 100       |    151.23 ns |   1.895 ns |   1.680 ns | 
| **Md5Hash**       | **10000**     | **15,533.10 ns** | **107.112 ns** |  **94.952 ns** | 
| Sha1Hash      | 10000     |  4,379.81 ns |  27.973 ns |  23.359 ns | 
| Sha256Hash    | 10000     |  4,217.69 ns |  52.307 ns |  48.928 ns | 
| Sha512Hash    | 10000     |  7,185.05 ns | 139.456 ns | 130.447 ns | 
| TryMd5Hash    | 10000     | 15,580.11 ns | 173.134 ns | 153.479 ns | 
| TrySha1Hash   | 10000     |  4,359.21 ns |  42.794 ns |  35.735 ns | 
| TrySha256Hash | 10000     |  4,146.57 ns |  23.460 ns |  20.797 ns | 
| TrySha512Hash | 10000     |  7,105.45 ns |  60.875 ns |  53.964 ns | 
