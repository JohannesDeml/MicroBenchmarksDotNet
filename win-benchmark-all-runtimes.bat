:: build and run benchmark for windows
:: Options: https://docs.microsoft.com/en-us/dotnet/core/tools/dotnet-build
:: Build targets: https://docs.microsoft.com/en-us/dotnet/core/rid-catalog

:: Target all runtimes
SET TARGET_RUNTIMES=Core80,Core60,Mono
:: Create stable results
SET HIGH_PRECISION=true
:: Define your mono path you want to test, or remove this line to use the default mono installation
SET MONO_UNITY=C:\Program Files\Unity\2022.3.15f1\Editor\Data\MonoBleedingEdge\bin\mono.exe

:: Build & run the benchmark
.\win-benchmark.bat
