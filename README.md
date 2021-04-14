# Micro Benchmarks for C#

*Benchmarks for a better understanding of performance costs*



## Findings

### Hash Generation
![Hash Generation Comparison Chart](./Docs/hashgeneration-windows10-1.0.0.png)

* If you can, always use TryHash instead of Hash
* Surprisingly, Sha256 performs better, than Sha1 which performs better than Md5 for .NET Core and .NET framework on modern hardware. Always test on your target hardware, which is faster and don't assume that Md5 will be faster than Sha256 not matter what.


### UDP Sockets
![UDP Comparison Chart](./Docs/udpsocket-sendreceive-1.0.0.png)

* For some (for me yet unknown) reason, UDP performs a lot better on Windows safe mode, than it does with normal windows mode. This is the only benchmark, that shows that kind of behavior. For all other benchmarks the results are statistically the same for normal and safe mode.
* A further discussion can be found on [superuser](https://superuser.com/questions/1640588/windows-10-udp-socket-benchmark-a-lot-faster-in-safe-mode). If you have any input, I would love to know!

### Pause Accuracy
![Pause Accuracy Chart](./Docs/pauseaccuracy2ms-windows10-1.0.0.png)

* `Thread.SpinWait(10)` and `Thread.Sleep(0)` allow for maximum precision, also on windows, but they block processing time that might be needed by other processes.
* `Thread.Sleep(TimeoutDuration)`  and all others have the problem that the granularity follows that of the system timer. For windows this is ~15ms.  
* There is trick to change the granularity by using winmm.dll's `timeBeginPeriod(uint period)` and `timeEndPeriod(uint period)`. This can be seen with ThreadSleepEnhanced - It works for .NET 5 and .NET Core 3.1, but not the others.

