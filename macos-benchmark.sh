#!/bin/bash

# Build and run the benchmarks with .NET 10 on macOS
# Reports are collected in ./Results and indexed in ./results.md
#
# Usage: ./macos-benchmark.sh [BenchmarkDotNet args]
#   ./macos-benchmark.sh                       # interactive benchmark selection
#   ./macos-benchmark.sh --filter '*String*'   # run all string benchmarks
#   ./macos-benchmark.sh --filter '*'          # run everything
#
# Options: https://docs.microsoft.com/en-us/dotnet/core/tools/dotnet-build
# Console args: https://benchmarkdotnet.org/articles/guides/console-args.html

set -euo pipefail

SCRIPT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"
cd "$SCRIPT_DIR"

FRAMEWORK="net10.0"
PROJECT="MicroBenchmarks/MicroBenchmarks.csproj"
# Run from the project output folder, BenchmarkDotNet needs to find the csproj next to it to build its jobs
BENCHMARK_EXE="MicroBenchmarks/bin/Release/$FRAMEWORK/MicroBenchmarks"
ARTIFACTS_DIR="$SCRIPT_DIR/BenchmarkDotNet.Artifacts"
RESULTS_DIR="$SCRIPT_DIR/Results"
INDEX_FILE="$SCRIPT_DIR/results.md"

# Only target .NET 10, see DefaultBenchmarkConfig.cs for all available runtimes
export TARGET_RUNTIMES=Core10_0
# Uncomment for slower, but stable results that can be compared across runs
# export HIGH_PRECISION=true

# Writes results.md in the repository root, linking every markdown report in ./Results
update_results_index() {
	local reports report title updated

	reports="$( (cd "$RESULTS_DIR" && find . -type f -name '*.md' | sed 's|^\./||' | LC_ALL=C sort) )"

	{
		echo "# Benchmark Results"
		echo
		echo "All benchmark reports in [Results](./Results), generated with [macos-benchmark.sh](./macos-benchmark.sh)."
		echo
		echo "_Index updated $(date '+%Y-%m-%d %H:%M:%S %z')_"
		echo

		if [ -z "$reports" ]; then
			echo "No reports yet. Run \`./macos-benchmark.sh --filter '*'\` to create them."
		else
			echo "| Benchmark | Last run |"
			echo "| --------- | -------- |"
			while IFS= read -r report; do
				# MicroBenchmarks.StringSearchBenchmark-report-github.md -> StringSearchBenchmark
				title="${report##*/}"
				title="${title%.md}"
				title="${title#MicroBenchmarks.}"
				title="${title%-report-github}"
				title="${title%-report}"
				updated="$(stat -f '%Sm' -t '%Y-%m-%d' "$RESULTS_DIR/$report")"
				echo "| [$title](./Results/${report// /%20}) | $updated |"
			done <<< "$reports"
		fi
	} > "$INDEX_FILE"
}

echo "--- Building $PROJECT for $FRAMEWORK ---"
dotnet build "$PROJECT" --configuration Release --framework "$FRAMEWORK"

echo "--- Running benchmarks ---"
"$BENCHMARK_EXE" --artifacts "$ARTIFACTS_DIR" "$@"

echo "--- Benchmarks finished ---"
echo "Save current process list"
mkdir -p "$ARTIFACTS_DIR"
ps aux > "$ARTIFACTS_DIR/running-processes.txt"
ps -eo pid,lstart,%cpu,%mem,comm > "$ARTIFACTS_DIR/running-processes.csv"

echo "Copy reports to Results"
mkdir -p "$RESULTS_DIR"
if [ -d "$ARTIFACTS_DIR/results" ]; then
	cp -f "$ARTIFACTS_DIR/results/"* "$RESULTS_DIR/"
else
	echo "No reports found in $ARTIFACTS_DIR/results"
fi

echo "Update results.md"
update_results_index
