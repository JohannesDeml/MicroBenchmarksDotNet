:: build and run benchmark for windows
:: Options: https://docs.microsoft.com/en-us/dotnet/core/tools/dotnet-build
:: Build targets: https://docs.microsoft.com/en-us/dotnet/core/rid-catalog

SET MONO_UNITY=C:\Program Files\Unity\2020.2.5f1\Editor\Data\MonoBleedingEdge\bin\mono.exe

dotnet build --configuration Release --framework net5.0 --output .\bin\MicroBenchmarks-Windows\
.\bin\MicroBenchmarks-Windows\MicroBenchmarks
PAUSE
