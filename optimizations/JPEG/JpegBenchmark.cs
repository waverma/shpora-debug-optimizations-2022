// using System;
// using System.Drawing;
// using System.Drawing.Imaging;
// using System.IO;
// using BenchmarkDotNet.Attributes;
// using JPEG.Images;
//
// namespace JPEG
// {
//     [SimpleJob(warmupCount: 1, targetCount: 3)]
//     public class JpegBenchmark
//     {
//         const int CompressionQuality = 70;
//
//         [Benchmark]
//         public void Jpeg()
//         {
//             try
//             {
//                 var fileName = @"earth.bmp";
//                 fileName = @"sample.bmp";
//                 var compressedFileName = fileName + ".compressed." + CompressionQuality;
//                 var uncompressedFileName = fileName + ".uncompressed." + CompressionQuality + ".bmp";
// 				
//                 using (var fileStream = File.OpenRead(fileName))
//                 using (var bmp = (Bitmap) Image.FromStream(fileStream, false, false))
//                 {
//                     var imageMatrix = (Matrix) bmp;
//                     var compressionResult = Compressor.Compress(imageMatrix, CompressionQuality);
//                     imageMatrix = default;
//                     GC.Collect();
//                     compressionResult.Save(compressedFileName);
//                 }
//
//                 var compressedImage = CompressedImage.Load(compressedFileName);
//                 var uncompressedImage = Decompressor.Uncompress(compressedImage, new Matrix());
//                 var resultBmp = (Bitmap) uncompressedImage;
//                 resultBmp.Save(uncompressedFileName, ImageFormat.Bmp);
//             }
//             catch(Exception e)
//             {
//                 Console.WriteLine(e);
//             }
//         }
//     }
// }