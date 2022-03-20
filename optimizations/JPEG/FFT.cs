using System;
using System.Numerics;
using System.Threading.Tasks;
using JPEG.Utilities;

namespace JPEG
{
    public static class FFT
    {
	    private static int width;
	    private static int height;

	    private static Complex wNegativeWidth;
	    private static Complex wNegativeHeight;
	    private static Complex wWidth;
	    private static Complex wHeight;
		
		public static void Setup(int width, int height)
		{
			FFT.width = width;
			FFT.height = height;
			
			wNegativeWidth = Complex.Pow(MathF.E, -2 * MathF.PI * Complex.ImaginaryOne / FFT.width);
			wNegativeHeight = Complex.Pow(MathF.E, -2 * MathF.PI * Complex.ImaginaryOne / FFT.height);
			
			wWidth = Complex.Pow(MathF.E, 2 * MathF.PI * Complex.ImaginaryOne / FFT.width);
			wHeight = Complex.Pow(MathF.E, 2 * MathF.PI * Complex.ImaginaryOne / FFT.height);
		}
		
		public static void FFT2D(float[,] input, Complex[,] output)
		{
			var sum = Complex.Zero;
			
			for (var targetX = 0; targetX < width; targetX++)
			for (var targetY = 0; targetY < height; targetY++)
			{
				for (var x = 0; x < width; x++)
				for (var y = 0; y < height; y++) 
					sum += input[x, y] * Complex.Pow(wNegativeWidth, x * targetX) * Complex.Pow(wNegativeHeight, y * targetY);
				
				output[targetX, targetY] = sum;
			}
		}

		public static void IFFT2D(Complex[,] input, float[,] output)
		{
			var sum = 0f;
			
			for (var targetX = 0; targetX < width; targetX++)
			for (var targetY = 0; targetY < height; targetY++)
			{
				for (var x = 0; x < width; x++)
				for (var y = 0; y < height; y++) 
					sum += (float)(input[x, y] * Complex.Pow(wWidth, x * targetX) * Complex.Pow(wHeight, y * targetY)).Real;
				
				output[targetX, targetY] = sum / (width * height);
			}
		}
	}
}