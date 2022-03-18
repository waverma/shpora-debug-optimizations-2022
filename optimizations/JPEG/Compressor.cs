using System;
using System.Collections.Generic;
using BenchmarkDotNet.Attributes;
using JPEG;
using JPEG.Images;
using static JPEG.Program;

namespace JPEG
{
    public static class Compressor
    {
        public static CompressedImage Compress(Matrix matrix, int quality = 50)
        {
            var allQuantizedBytes = new byte[matrix.Height * matrix.Width * 3];
            var subMatrix = new float[DCTSize, DCTSize];
            var channelFreqs = new float[DCTSize, DCTSize];
            var quantizedFreqs = new byte[DCTSize, DCTSize];
            var componentSelectors = new Func<StructPixel, float>[] {p => p.Y, p => p.Cb, p => p.Cr};

            for(var y = 0; y < matrix.Height; y += DCTSize)
            {
                for(var x = 0; x < matrix.Width; x += DCTSize)
                {
                    foreach (var selector in componentSelectors)
                    {
                        GetSubMatrix(matrix, y, DCTSize, x, DCTSize, selector, subMatrix);
                        // ShiftMatrixValues(subMatrix, -128);
                        MyDCT.DCT2D(subMatrix, channelFreqs);
                        Quantize(channelFreqs, quality, quantizedFreqs);
                        ZigZager.FillZigZagScan(quantizedFreqs, allQuantizedBytes);
                    }
                }
            }

            long bitsCount;
            Dictionary<BitsWithLength, byte> decodeTable;
            var compressedBytes = HuffmanCodec.Encode(allQuantizedBytes, out decodeTable, out bitsCount);

            return new CompressedImage {Quality = quality, CompressedBytes = compressedBytes, BitsCount = bitsCount, DecodeTable = decodeTable, Height = matrix.Height, Width = matrix.Width};
        }
        
        private static unsafe void Quantize(float[,] channelFreqs, int quality, byte[,] result)
        {
            var height = channelFreqs.GetLength(0);
            var width = channelFreqs.GetLength(1);
			
            var quantizationMatrix = GetQuantizationMatrix(quality);
            fixed (int* qm = quantizationMatrix)
            {
                fixed(float* input = channelFreqs)
                {
                    fixed (byte* output = result)
                    {
                        var source = new Span<float>(input, height * width);
                        var target = new Span<byte>(output, height * width);
                        var coeff = new Span<int>(qm, height * width);
						
                        for(var y = 0; y < height * width; y++)
                        {
                            target[y] = (byte)(source[y] / coeff[y]);
                        }
                    }
                }
            }
        }
        
        private static unsafe void GetSubMatrix(Matrix matrix, int yOffset, int yLength, int xOffset, int xLength, Func<StructPixel, float> componentSelector, float[,] result)
        {
            var w = matrix.Width;
            fixed(StructPixel* input = matrix.Pixels)
            {
                fixed (float* output = result)
                {
                    StructPixel* source = input;
                    float* target = output;
                    source += yOffset * w;
                    source += xOffset;
					
                    for(var j = 0; j < yLength; j++)
                    {
                        for (var i = 0; i < xLength; i++)
                        {
                            var a = *source;
                            *target = componentSelector(a)-128;
							
                            target++;
                            source++;
                        }
					
                        source -= yLength;
                        source += w;
                    }
                }
            }
        }
    }
}