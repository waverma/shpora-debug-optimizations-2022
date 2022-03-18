using System;
using System.IO;
using JPEG.Images;
using static JPEG.Program;

namespace JPEG
{
    public class Decompressor
    {
        public static Matrix Uncompress(CompressedImage image)
        {
            var result = new Matrix(image.Height, image.Width);
            var _y = new float[DCTSize, DCTSize];
            var cb = new float[DCTSize, DCTSize];
            var cr = new float[DCTSize, DCTSize];
            var components = new[] {_y, cb, cr};
            var channelFreqs = new float[DCTSize, DCTSize];
            var quantizedBytes = new byte[DCTSize * DCTSize];
            var quantizedFreqs = new byte[DCTSize, DCTSize];
			
            using (var allQuantizedBytes = new MemoryStream(HuffmanCodec.Decode(image.CompressedBytes, image.DecodeTable, image.BitsCount)))
            {
                for (var y = 0; y < image.Height; y += DCTSize)
                {
                    for (var x = 0; x < image.Width; x += DCTSize)
                    {
                        foreach (var channel in components)
                        {
                            allQuantizedBytes.ReadAsync(quantizedBytes, 0, quantizedBytes.Length).Wait();
                            ZigZager.ZigZagUnScan(quantizedBytes, quantizedFreqs);
                            DeQuantize(quantizedFreqs, image.Quality, channelFreqs);
                            MyDCT.IDCT2D(channelFreqs, channel);
                            // ShiftMatrixValues(channel, 128);
                        }
                        SetYCbCrPixels(result, _y, cb, cr, y, x);
                    }
                }
            }

            return result;
        }
        
        private static unsafe void SetYCbCrPixels(Matrix matrix, float[,] a, float[,] b, float[,] c, int yOffset, int xOffset)
        {
            var height = a.GetLength(0);
            var width = a.GetLength(1);
            var w = matrix.Width;

            fixed(StructPixel* output = matrix.Pixels)
            {
                fixed (float* inputA = a, inputB = b, inputC = c)
                {
                    StructPixel* target = output;
                    float* sourceA = inputA;
                    float* sourceB = inputB;
                    float* sourceC = inputC;
                    target += yOffset * w;
                    target += xOffset;

                    for(var j = 0; j < width; j++)
                    {
                        for (var i = 0; i < height; i++)
                        {
                            *target = new StructPixel(*sourceA, *sourceB, *sourceC);
							
                            target++;
                            sourceA++;
                            sourceB++;
                            sourceC++;
                        }

                        target -= width;
                        target += w;
                    }
                }
            }
        }
        
        private static unsafe void DeQuantize(byte[,] quantizedBytes, int quality, float[,] result)
        {
            var height = quantizedBytes.GetLength(0);
            var width = quantizedBytes.GetLength(1);
            var quantizationMatrix = GetQuantizationMatrix(quality);
            fixed (int* qm = quantizationMatrix)
            {
                fixed(byte* input = quantizedBytes)
                {
                    fixed (float* output = result)
                    {
                        var source = new Span<byte>(input, height * width);
                        var target = new Span<float>(output, height * width);
                        var coeff = new Span<int>(qm, height * width);
						
                        for(var y = 0; y < height * width; y++)
                        {
                            target[y] = ((sbyte) source[y]) * coeff[y];
                        }
                    }
                }
            }
        }
    }
}