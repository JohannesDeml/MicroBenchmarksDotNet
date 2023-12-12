#!/bin/bash

# Options: https://docs.microsoft.com/en-us/dotnet/core/tools/dotnet-build
# Build targets: https://docs.microsoft.com/en-us/dotnet/core/rid-catalog

# Target all runtimes
export TARGET_RUNTIMES=Core80,Core60,Mono
# Create stable results
SET HIGH_PRECISION=true
# Define your mono path you want to test, or remove this line to use the default mono installation
export MONO_UNITY="/home/johannes/Unity/Hub/Editor/2023.3.15f1/Editor/Data/MonoBleedingEdge/bin-linux64/mono"

# Build & run the benchmark
./unix-benchmark.sh