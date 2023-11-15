#!/bin/bash

# Options: https://docs.microsoft.com/en-us/dotnet/core/tools/dotnet-build
# Build targets: https://docs.microsoft.com/en-us/dotnet/core/rid-catalog

# Needs to be compiled as an exe, since benchmarkdotnet .netcore builds can't run mono targets 
dotnet build --configuration Release --framework net8.0 --output ./bin/MicroBenchmarks-Linux/
mono ./bin/MicroBenchmarks-Linux/MicroBenchmarks.exe

echo "--- Benchmark finished ---"
echo "Save current process list"
# Folder should exist, just to be sure create it if it does not
mkdir -p BenchmarkDotNet.Artifacts

ps -aux > ./BenchmarkDotNet.Artifacts/running-processes.txt
ps -e -o %p, -o lstart -o ,%C, -o %mem -o ,%c > ./BenchmarkDotNet.Artifacts/running-processes.csv