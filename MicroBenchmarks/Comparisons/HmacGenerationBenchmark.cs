// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HmacGenerationBenchmark.cs">
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
	public class HmacGenerationBenchmark
	{
		[Params(10, 100, 1000)]
		public int ArraySize { get; set; }

		private byte[] data;
		private byte[] hashResult;

		private HMACMD5 md5Provider;
		private HMACSHA1 sha1Provider;
		private HMACSHA256 sha256Provider;

		[GlobalSetup]
		public void PrepareBenchmark()
		{
			var secretKey = new byte[64];
			using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
			{
				rng.GetBytes(secretKey);
			}

			data = ValuesGenerator.Array<byte>(ArraySize);

			md5Provider = new HMACMD5(secretKey);
			sha1Provider = new HMACSHA1(secretKey);
			sha256Provider = new HMACSHA256(secretKey);
			hashResult = new byte[64];
		}

		[Benchmark]
		public byte[] Md5Hmac()
		{
			var result = md5Provider.ComputeHash(data, 0, data.Length);
			return result;
		}

		[Benchmark]
		public byte[] Sha1Hmac()
		{
			var result = sha1Provider.ComputeHash(data, 0, data.Length);
			return result;
		}

		[Benchmark]
		public byte[] Sha256Hmac()
		{
			var result = sha256Provider.ComputeHash(data, 0, data.Length);
			return result;
		}


		[Benchmark]
		public byte[] TryMd5Hmac()
		{
			#if NET48
			// Not supported
			return new byte[0];
			#else

			if (md5Provider.TryComputeHash(data, hashResult, out int bytesWritten))
			{
				return hashResult;
			}

			throw new InvalidOperationException("Error when computing HMAC");
			#endif
		}

		[Benchmark]
		public byte[] TrySha1Hmac()
		{
			#if NET48
			// Not supported
			return new byte[0];
			#else

			if (sha1Provider.TryComputeHash(data, hashResult, out int bytesWritten))
			{
				return hashResult;
			}

			throw new InvalidOperationException("Error when computing HMAC");
			#endif
		}

		[Benchmark]
		public byte[] TrySha256Hmac()
		{
			#if NET48
			// Not supported
			return new byte[0];
			#else

			if (sha256Provider.TryComputeHash(data, hashResult, out int bytesWritten))
			{
				return hashResult;
			}

			throw new InvalidOperationException("Error when computing HMAC");
			#endif
		}
	}
}
