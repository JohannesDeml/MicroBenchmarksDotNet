// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HashGenerationBenchmark.cs">
//   Copyright (c) 2021 Johannes Deml. All rights reserved.
// </copyright>
// <author>
//   Johannes Deml
//   public@deml.io
// </author>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Security.Cryptography;
using BenchmarkDotNet.Attributes;
using MicroBenchmarks.Extensions;

namespace MicroBenchmarks
{
	[Config(typeof(DefaultBenchmarkConfig))]
	public class HashGenerationBenchmark
	{
		public const int Seed = 1337;

		[Params(10, 100, 1000)]
		public int ArraySize { get; set; }

		private byte[] data;
		private byte[] hashResult;

		private MD5CryptoServiceProvider md5Provider;
		private SHA1CryptoServiceProvider sha1Provider;
		private SHA256CryptoServiceProvider sha256Provider;

		[GlobalSetup]
		public void PrepareBenchmark()
		{
			data = new byte[ArraySize];
			Random rnd = new Random(Seed);
			rnd.NextBytes(data);

			md5Provider = new MD5CryptoServiceProvider();
			sha1Provider = new SHA1CryptoServiceProvider();
			sha256Provider = new SHA256CryptoServiceProvider();
			hashResult = new byte[64];
		}


		[Benchmark]
		public byte[] Md5Hash()
		{
			var result = md5Provider.ComputeHash(data, 0, data.Length);
			return result;
		}

		[Benchmark]
		public byte[] Sha1Hash()
		{
			var result = sha1Provider.ComputeHash(data, 0, data.Length);
			return result;
		}

		[Benchmark]
		public byte[] Sha256Hash()
		{
			var result = sha256Provider.ComputeHash(data, 0, data.Length);
			return result;
		}


		[Benchmark]
		public byte[] TryMd5Hash()
		{
			#if NET48
			// Not supported
			return null;
			#else

			if (md5Provider.TryComputeHash(data, hashResult, out int bytesWritten))
			{
				return hashResult;
			}

			throw new InvalidOperationException("Hash was not computed");
			#endif
		}

		[Benchmark]
		public byte[] TrySha1Hash()
		{
			#if NET48
			// Not supported
			return null;
			#else

			if (sha1Provider.TryComputeHash(data, hashResult, out int bytesWritten))
			{
				return hashResult;
			}

			throw new InvalidOperationException("Hash was not computed");
			#endif
		}

		[Benchmark]
		public byte[] TrySha256Hash()
		{
			#if NET48
			// Not supported
			return null;
			#else

			if (sha256Provider.TryComputeHash(data, hashResult, out int bytesWritten))
			{
				return hashResult;
			}

			throw new InvalidOperationException("Hash was not computed");
			#endif
		}
	}
}
