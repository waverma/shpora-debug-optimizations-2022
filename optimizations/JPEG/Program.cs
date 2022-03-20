using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using BenchmarkDotNet.Running;
using JPEG.Images;
using PixelFormat = JPEG.Images.PixelFormat;

namespace JPEG
{
	class Program
	{
		const int CompressionQuality = 70;

		static void Main(string[] args)
		{
			// BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run(args);
			// BenchmarkRunner.Run<JpegBenchmark>();
			try
			{
				Console.WriteLine(IntPtr.Size == 8 ? "64-bit version" : "32-bit version");
				var sw = Stopwatch.StartNew();
				var fileName = @"earth.bmp";
				fileName = @"sample.bmp";
				//fileName = @"MARBLES.bmp";
				//fileName = "Big_Black_River_Railroad_Bridge.bmp";
				var compressedFileName = fileName + ".compressed." + CompressionQuality;
				var uncompressedFileName = fileName + ".uncompressed." + CompressionQuality + ".bmp";
				
				using (var fileStream = File.OpenRead(fileName))
				using (var bmp = (Bitmap) Image.FromStream(fileStream, false, false))
				{
					var imageMatrix = (Matrix) bmp;
					// GC.Collect();
					sw.Stop();
					Console.WriteLine($"{bmp.Width}x{bmp.Height} - {fileStream.Length / (1024.0 * 1024):F2} MB " + sw.Elapsed);
					sw.Restart();
					// GC.Collect();
					var compressionResult = Compressor.Compress(imageMatrix, CompressionQuality);
					imageMatrix = default;
					GC.Collect();
					compressionResult.Save(compressedFileName);
					// GC.Collect();
				}
			
				sw.Stop();
				Console.WriteLine("Compression: " + sw.Elapsed);
				sw.Restart();
				var compressedImage = CompressedImage.Load(compressedFileName);
				// GC.Collect();
				var uncompressedImage = Decompressor.Uncompress(compressedImage);
				// GC.Collect();
				var resultBmp = (Bitmap) uncompressedImage;
				// GC.Collect();
				resultBmp.Save(uncompressedFileName, ImageFormat.Bmp);
				// GC.Collect();
				sw.Stop();
				Console.WriteLine("Decompression: " + sw.Elapsed);
				Console.WriteLine($"Peak commit size: {MemoryMeter.PeakPrivateBytes() / (1024.0*1024):F2} MB");
				Console.WriteLine($"Peak working set: {MemoryMeter.PeakWorkingSet() / (1024.0*1024):F2} MB");
			}
			catch(Exception e)
			{
				Console.WriteLine(e);
			}
		}
		
		private static Dictionary<int, int[,]> QuantizationMatrixCache = new();
		public static int[,] GetQuantizationMatrix(int quality)
		{
			if(quality < 1 || quality > 99)
				throw new ArgumentException("quality must be in [1,99] interval");

			var cachedResult = QuantizationMatrixCache.GetValueOrDefault(quality);
			if (cachedResult is not null) return cachedResult!;

			var multiplier = quality < 50 ? 5000 / quality : 200 - 2 * quality;

			var result = new[,]
			{
				{16, 11, 10, 16, 24, 40, 51, 61},
				{12, 12, 14, 19, 26, 58, 60, 55},
				{14, 13, 16, 24, 40, 57, 69, 56},
				{14, 17, 22, 29, 51, 87, 80, 62},
				{18, 22, 37, 56, 68, 109, 103, 77},
				{24, 35, 55, 64, 81, 104, 113, 92},
				{49, 64, 78, 87, 103, 121, 120, 101},
				{72, 92, 95, 98, 112, 100, 103, 99}
			};

			var resultW = result.GetLength(0);
			var resultH = result.GetLength(1);
			for(int y = 0; y < resultW; y++)
			{
				for(int x = 0; x < resultH; x++)
				{
					result[y, x] = (multiplier * result[y, x] + 50) / 100;
				}
			}

			QuantizationMatrixCache[quality] = result;
			return result;
		}

	 public	const byte DCTSize = 8;
	}
}
