using System.IO;
using BenchmarkDotNet.Attributes;
using ImageProcessing;

namespace DotTraceExamples.Benchmarks
{
	[SimpleJob(warmupCount: 5, targetCount: 5)]
	public class EdgePreservingSmoothingBenchmark
	{
		private RGBImage image;

		[IterationSetup]
		public void Setup()
		{
			var fileName = @"TestImages\TestImage.jpg";
			using (var fileStream = File.OpenRead(fileName))
			{
				image = RGBImage.FromStream(fileStream);
			}
		}

		[Benchmark]
		public void EdgePreservingSmoothing()
		{
			image.EdgePreservingSmoothing();
		}
	}
}