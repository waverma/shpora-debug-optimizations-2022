using System;
using System.Threading.Tasks;

namespace JPEG
{
    public class MyDCT
    {
	    private static byte width = 8;
	    private static byte height = 8;
	    private static float beta = 1f / width + 1f / height;
	    private static float sqrt2 = 1f / (float)Math.Sqrt(2);
	    private static double piCoef = Math.PI / (2f * height);
		
		public static void DCT2D(float[,] input, float[,] coeffs)
		{
			Parallel.For(0, width, step =>
			{
				var a = step * piCoef;
				float b, sum;
				byte x, y;
				var c = step == 0 ? sqrt2 : 1f;
				
				for (byte j = 0; j < height; j++)
				{
					b = j * (float)piCoef;
					sum = 0f;
        
					for (x = 0; x < width; x++)
					for (y = 0; y < height; y++)
						sum += input[x, y] * (float)Math.Cos((2f * x + 1f) * a) * (float)Math.Cos((2f * y + 1f) * b);
				
					coeffs[step, j] = sum * c * (j == 0 ? sqrt2 * beta : beta);
				}
			});
		}

		public static void IDCT2D(float[,] coeffs, float[,] output)
		{
			Parallel.For(0, width, step =>
			{
				var a = (2f * step + 1f) * piCoef;
				float b, sum;
				byte u, v;
				
				for (byte j = 0; j < height; j++)
				{
					sum = 0f;
					b = (2f * j + 1f) * (float)piCoef;
        
					for (u = 0; u < width; u++)
					for (v = 0; v < height; v++)
						sum += coeffs[u, v] * (float)Math.Cos(a * u) * (float)Math.Cos(b * v) * (u == 0 ? sqrt2 : 1f) * (v == 0 ? sqrt2 * beta : beta);
					
					
					output[step, j] = sum + 128;
				}
			});
		}
	}
}