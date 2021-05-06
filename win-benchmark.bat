:: build and run benchmark for windows
:: Options: https://docs.microsoft.com/en-us/dotnet/core/tools/dotnet-build
:: Build targets: https://docs.microsoft.com/en-us/dotnet/core/rid-catalog

SET MONO_UNITY=C:\Program Files\Unity\2020.3.3f1\Editor\Data\MonoBleedingEdge\bin\mono.exe

dotnet build --configuration Release --framework net48 --output .\bin\MicroBenchmarks-Windows\
.\bin\MicroBenchmarks-Windows\MicroBenchmarks

echo off
Echo --- Benchmarks finished ---
Echo Save current process list
:: Folder should exist, just to be sure create it if it does not
if not exist "BenchmarkDotNet.Artifacts" mkdir BenchmarkDotNet.Artifacts

echo on
:: Store currently running processes
tasklist /V /FO CSV > "BenchmarkDotNet.Artifacts\running-processes.csv"
tasklist /V > "BenchmarkDotNet.Artifacts\running-processes.txt"

PAUSE
