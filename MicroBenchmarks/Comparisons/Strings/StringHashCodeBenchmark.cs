using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Extensions;
using MicroBenchmarks.Extensions;

namespace MicroBenchmarks;

[Config(typeof(DefaultBenchmarkConfig))]
public class StringHashCodeBenchmark
{
	[Params(100, 10_000)]
	public int StringLength { get; set; }


	private string stringData;

	[GlobalSetup]
	public void PrepareBenchmark()
	{
		stringData = ValuesGenerator.GenerateRandomString(StringLength);
	}

	[Benchmark]
	public int DefaultHashCode()
	{
		return stringData.GetHashCode();
	}

	[Benchmark]
	public ulong Fnv1a64()
	{
		ulong hash = 0xcbf29ce484222325;

		foreach (char c in stringData)
		{
			hash ^= c;
			hash *= 0x00000100000001B3;
		}

		return hash;
	}
}
