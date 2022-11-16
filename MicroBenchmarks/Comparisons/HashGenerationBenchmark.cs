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
using BenchmarkDotNet.Extensions;
using MicroBenchmarks.Extensions;

namespace MicroBenchmarks
{
	[Config(typeof(DefaultBenchmarkConfig))]
	public class HashGenerationBenchmark
	{
		[Params(100, 10000)]
		public int ArraySize { get; set; }

		private byte[] data;
		private byte[] hashResult;

		private MD5 md5Provider;
		private SHA1 sha1Provider;
		private SHA256 sha256Provider;
		private SHA512 sha512Provider;

		[GlobalSetup]
		public void PrepareBenchmark()
		{
			data = ValuesGenerator.Array<byte>(ArraySize);

			md5Provider = MD5.Create();
			sha1Provider = SHA1.Create();
			sha256Provider = SHA256.Create();
			sha512Provider = SHA512.Create();
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
		public byte[] Sha512Hash()
		{
			var result = sha512Provider.ComputeHash(data, 0, data.Length);
			return result;
		}

		[Benchmark]
		public byte[] TryMd5Hash()
		{
			#if NET48
			// Not supported
			return new byte[0];
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
			return new byte[0];
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
			return new byte[0];
			#else

			if (sha256Provider.TryComputeHash(data, hashResult, out int bytesWritten))
			{
				return hashResult;
			}

			throw new InvalidOperationException("Hash was not computed");
			#endif
		}

		[Benchmark]
		public byte[] TrySha512Hash()
		{
			#if NET48
			// Not supported
			return new byte[0];
			#else

			if (sha512Provider.TryComputeHash(data, hashResult, out int bytesWritten))
			{
				return hashResult;
			}

			throw new InvalidOperationException("Hash was not computed");
			#endif
		}
	}
}
