using System;
using System.Threading.Tasks;

namespace JPEG
{
    public static class MyCringeDCT
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
				var c = step == 0 ? sqrt2 : 1f;
				
				for (byte j = 0; j < height; j++)
				{
					b = j * (float)piCoef;
					sum = 0f;
        
					sum += input[0, 0] * (float)Math.Cos((2f * 0 + 1f) * a) * (float)Math.Cos((2f * 0 + 1f) * b);
				    sum += input[0, 1] * (float)Math.Cos((2f * 0 + 1f) * a) * (float)Math.Cos((2f * 1 + 1f) * b);
				    sum += input[0, 2] * (float)Math.Cos((2f * 0 + 1f) * a) * (float)Math.Cos((2f * 2 + 1f) * b);
				    sum += input[0, 3] * (float)Math.Cos((2f * 0 + 1f) * a) * (float)Math.Cos((2f * 3 + 1f) * b);
				    sum += input[0, 4] * (float)Math.Cos((2f * 0 + 1f) * a) * (float)Math.Cos((2f * 4 + 1f) * b);
				    sum += input[0, 5] * (float)Math.Cos((2f * 0 + 1f) * a) * (float)Math.Cos((2f * 5 + 1f) * b);
				    sum += input[0, 6] * (float)Math.Cos((2f * 0 + 1f) * a) * (float)Math.Cos((2f * 6 + 1f) * b);
				    sum += input[0, 7] * (float)Math.Cos((2f * 0 + 1f) * a) * (float)Math.Cos((2f * 7 + 1f) * b);
				    
				    sum += input[1, 0] * (float)Math.Cos((2f * 1 + 1f) * a) * (float)Math.Cos((2f * 0 + 1f) * b);
				    sum += input[1, 1] * (float)Math.Cos((2f * 1 + 1f) * a) * (float)Math.Cos((2f * 1 + 1f) * b);
				    sum += input[1, 2] * (float)Math.Cos((2f * 1 + 1f) * a) * (float)Math.Cos((2f * 2 + 1f) * b);
				    sum += input[1, 3] * (float)Math.Cos((2f * 1 + 1f) * a) * (float)Math.Cos((2f * 3 + 1f) * b);
				    sum += input[1, 4] * (float)Math.Cos((2f * 1 + 1f) * a) * (float)Math.Cos((2f * 4 + 1f) * b);
				    sum += input[1, 5] * (float)Math.Cos((2f * 1 + 1f) * a) * (float)Math.Cos((2f * 5 + 1f) * b);
				    sum += input[1, 6] * (float)Math.Cos((2f * 1 + 1f) * a) * (float)Math.Cos((2f * 6 + 1f) * b);
				    sum += input[1, 7] * (float)Math.Cos((2f * 1 + 1f) * a) * (float)Math.Cos((2f * 7 + 1f) * b);
				    
				    sum += input[2, 0] * (float)Math.Cos((2f * 2 + 1f) * a) * (float)Math.Cos((2f * 0 + 1f) * b);
				    sum += input[2, 1] * (float)Math.Cos((2f * 2 + 1f) * a) * (float)Math.Cos((2f * 1 + 1f) * b);
				    sum += input[2, 2] * (float)Math.Cos((2f * 2 + 1f) * a) * (float)Math.Cos((2f * 2 + 1f) * b);
				    sum += input[2, 3] * (float)Math.Cos((2f * 2 + 1f) * a) * (float)Math.Cos((2f * 3 + 1f) * b);
				    sum += input[2, 4] * (float)Math.Cos((2f * 2 + 1f) * a) * (float)Math.Cos((2f * 4 + 1f) * b);
				    sum += input[2, 5] * (float)Math.Cos((2f * 2 + 1f) * a) * (float)Math.Cos((2f * 5 + 1f) * b);
				    sum += input[2, 6] * (float)Math.Cos((2f * 2 + 1f) * a) * (float)Math.Cos((2f * 6 + 1f) * b);
				    sum += input[2, 7] * (float)Math.Cos((2f * 2 + 1f) * a) * (float)Math.Cos((2f * 7 + 1f) * b);
				    
				    sum += input[3, 0] * (float)Math.Cos((2f * 3 + 1f) * a) * (float)Math.Cos((2f * 0 + 1f) * b);
				    sum += input[3, 1] * (float)Math.Cos((2f * 3 + 1f) * a) * (float)Math.Cos((2f * 1 + 1f) * b);
				    sum += input[3, 2] * (float)Math.Cos((2f * 3 + 1f) * a) * (float)Math.Cos((2f * 2 + 1f) * b);
				    sum += input[3, 3] * (float)Math.Cos((2f * 3 + 1f) * a) * (float)Math.Cos((2f * 3 + 1f) * b);
				    sum += input[3, 4] * (float)Math.Cos((2f * 3 + 1f) * a) * (float)Math.Cos((2f * 4 + 1f) * b);
				    sum += input[3, 5] * (float)Math.Cos((2f * 3 + 1f) * a) * (float)Math.Cos((2f * 5 + 1f) * b);
				    sum += input[3, 6] * (float)Math.Cos((2f * 3 + 1f) * a) * (float)Math.Cos((2f * 6 + 1f) * b);
				    sum += input[3, 7] * (float)Math.Cos((2f * 3 + 1f) * a) * (float)Math.Cos((2f * 7 + 1f) * b);
				    
				    sum += input[4, 0] * (float)Math.Cos((2f * 4 + 1f) * a) * (float)Math.Cos((2f * 0 + 1f) * b);
				    sum += input[4, 1] * (float)Math.Cos((2f * 4 + 1f) * a) * (float)Math.Cos((2f * 1 + 1f) * b);
				    sum += input[4, 2] * (float)Math.Cos((2f * 4 + 1f) * a) * (float)Math.Cos((2f * 2 + 1f) * b);
				    sum += input[4, 3] * (float)Math.Cos((2f * 4 + 1f) * a) * (float)Math.Cos((2f * 3 + 1f) * b);
				    sum += input[4, 4] * (float)Math.Cos((2f * 4 + 1f) * a) * (float)Math.Cos((2f * 4 + 1f) * b);
				    sum += input[4, 5] * (float)Math.Cos((2f * 4 + 1f) * a) * (float)Math.Cos((2f * 5 + 1f) * b);
				    sum += input[4, 6] * (float)Math.Cos((2f * 4 + 1f) * a) * (float)Math.Cos((2f * 6 + 1f) * b);
				    sum += input[4, 7] * (float)Math.Cos((2f * 4 + 1f) * a) * (float)Math.Cos((2f * 7 + 1f) * b);
				    
				    sum += input[5, 0] * (float)Math.Cos((2f * 5 + 1f) * a) * (float)Math.Cos((2f * 0 + 1f) * b);
				    sum += input[5, 1] * (float)Math.Cos((2f * 5 + 1f) * a) * (float)Math.Cos((2f * 1 + 1f) * b);
				    sum += input[5, 2] * (float)Math.Cos((2f * 5 + 1f) * a) * (float)Math.Cos((2f * 2 + 1f) * b);
				    sum += input[5, 3] * (float)Math.Cos((2f * 5 + 1f) * a) * (float)Math.Cos((2f * 3 + 1f) * b);
				    sum += input[5, 4] * (float)Math.Cos((2f * 5 + 1f) * a) * (float)Math.Cos((2f * 4 + 1f) * b);
				    sum += input[5, 5] * (float)Math.Cos((2f * 5 + 1f) * a) * (float)Math.Cos((2f * 5 + 1f) * b);
				    sum += input[5, 6] * (float)Math.Cos((2f * 5 + 1f) * a) * (float)Math.Cos((2f * 6 + 1f) * b);
				    sum += input[5, 7] * (float)Math.Cos((2f * 5 + 1f) * a) * (float)Math.Cos((2f * 7 + 1f) * b);
				    
				    sum += input[6, 0] * (float)Math.Cos((2f * 6 + 1f) * a) * (float)Math.Cos((2f * 0 + 1f) * b);
				    sum += input[6, 1] * (float)Math.Cos((2f * 6 + 1f) * a) * (float)Math.Cos((2f * 1 + 1f) * b);
				    sum += input[6, 2] * (float)Math.Cos((2f * 6 + 1f) * a) * (float)Math.Cos((2f * 2 + 1f) * b);
				    sum += input[6, 3] * (float)Math.Cos((2f * 6 + 1f) * a) * (float)Math.Cos((2f * 3 + 1f) * b);
				    sum += input[6, 4] * (float)Math.Cos((2f * 6 + 1f) * a) * (float)Math.Cos((2f * 4 + 1f) * b);
				    sum += input[6, 5] * (float)Math.Cos((2f * 6 + 1f) * a) * (float)Math.Cos((2f * 5 + 1f) * b);
				    sum += input[6, 6] * (float)Math.Cos((2f * 6 + 1f) * a) * (float)Math.Cos((2f * 6 + 1f) * b);
				    sum += input[6, 7] * (float)Math.Cos((2f * 6 + 1f) * a) * (float)Math.Cos((2f * 7 + 1f) * b);
				    
				    sum += input[7, 0] * (float)Math.Cos((2f * 7 + 1f) * a) * (float)Math.Cos((2f * 0 + 1f) * b);
				    sum += input[7, 1] * (float)Math.Cos((2f * 7 + 1f) * a) * (float)Math.Cos((2f * 1 + 1f) * b);
				    sum += input[7, 2] * (float)Math.Cos((2f * 7 + 1f) * a) * (float)Math.Cos((2f * 2 + 1f) * b);
				    sum += input[7, 3] * (float)Math.Cos((2f * 7 + 1f) * a) * (float)Math.Cos((2f * 3 + 1f) * b);
				    sum += input[7, 4] * (float)Math.Cos((2f * 7 + 1f) * a) * (float)Math.Cos((2f * 4 + 1f) * b);
				    sum += input[7, 5] * (float)Math.Cos((2f * 7 + 1f) * a) * (float)Math.Cos((2f * 5 + 1f) * b);
				    sum += input[7, 6] * (float)Math.Cos((2f * 7 + 1f) * a) * (float)Math.Cos((2f * 6 + 1f) * b);
				    sum += input[7, 7] * (float)Math.Cos((2f * 7 + 1f) * a) * (float)Math.Cos((2f * 7 + 1f) * b);
				
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
				for (byte j = 0; j < height; j++)
				{
					sum = 0f;
					b = (2f * j + 1f) * (float)piCoef;
        
					sum += coeffs[0, 0] * (float)Math.Cos(a * 0) * (float)Math.Cos(b * 0) * (sqrt2) * (sqrt2 * beta);
					sum += coeffs[0, 1] * (float)Math.Cos(a * 0) * (float)Math.Cos(b * 1) * (sqrt2) * (beta);
					sum += coeffs[0, 2] * (float)Math.Cos(a * 0) * (float)Math.Cos(b * 2) * (sqrt2) * (beta);
					sum += coeffs[0, 3] * (float)Math.Cos(a * 0) * (float)Math.Cos(b * 3) * (sqrt2) * (beta);
					sum += coeffs[0, 4] * (float)Math.Cos(a * 0) * (float)Math.Cos(b * 4) * (sqrt2) * (beta);
					sum += coeffs[0, 5] * (float)Math.Cos(a * 0) * (float)Math.Cos(b * 5) * (sqrt2) * (beta);
					sum += coeffs[0, 6] * (float)Math.Cos(a * 0) * (float)Math.Cos(b * 6) * (sqrt2) * (beta);
					sum += coeffs[0, 7] * (float)Math.Cos(a * 0) * (float)Math.Cos(b * 7) * (sqrt2) * (beta);
					
					sum += coeffs[1, 0] * (float)Math.Cos(a * 1) * (float)Math.Cos(b * 0) * (sqrt2 * beta);
					sum += coeffs[1, 1] * (float)Math.Cos(a * 1) * (float)Math.Cos(b * 1) * (beta);
					sum += coeffs[1, 2] * (float)Math.Cos(a * 1) * (float)Math.Cos(b * 2) * (beta);
					sum += coeffs[1, 3] * (float)Math.Cos(a * 1) * (float)Math.Cos(b * 3) * (beta);
					sum += coeffs[1, 4] * (float)Math.Cos(a * 1) * (float)Math.Cos(b * 4) * (beta);
					sum += coeffs[1, 5] * (float)Math.Cos(a * 1) * (float)Math.Cos(b * 5) * (beta);
					sum += coeffs[1, 6] * (float)Math.Cos(a * 1) * (float)Math.Cos(b * 6) * (beta);
					sum += coeffs[1, 7] * (float)Math.Cos(a * 1) * (float)Math.Cos(b * 7) * (beta);
					
					sum += coeffs[2, 0] * (float)Math.Cos(a * 2) * (float)Math.Cos(b * 0) * (sqrt2 * beta);
					sum += coeffs[2, 1] * (float)Math.Cos(a * 2) * (float)Math.Cos(b * 1) * (beta);
					sum += coeffs[2, 2] * (float)Math.Cos(a * 2) * (float)Math.Cos(b * 2) * (beta);
					sum += coeffs[2, 3] * (float)Math.Cos(a * 2) * (float)Math.Cos(b * 3) * (beta);
					sum += coeffs[2, 4] * (float)Math.Cos(a * 2) * (float)Math.Cos(b * 4) * (beta);
					sum += coeffs[2, 5] * (float)Math.Cos(a * 2) * (float)Math.Cos(b * 5) * (beta);
					sum += coeffs[2, 6] * (float)Math.Cos(a * 2) * (float)Math.Cos(b * 6) * (beta);
					sum += coeffs[2, 7] * (float)Math.Cos(a * 2) * (float)Math.Cos(b * 7) * (beta);
					
					sum += coeffs[3, 0] * (float)Math.Cos(a * 3) * (float)Math.Cos(b * 0) * (sqrt2 * beta);
					sum += coeffs[3, 1] * (float)Math.Cos(a * 3) * (float)Math.Cos(b * 1) * (beta);
					sum += coeffs[3, 2] * (float)Math.Cos(a * 3) * (float)Math.Cos(b * 2) * (beta);
					sum += coeffs[3, 3] * (float)Math.Cos(a * 3) * (float)Math.Cos(b * 3) * (beta);
					sum += coeffs[3, 4] * (float)Math.Cos(a * 3) * (float)Math.Cos(b * 4) * (beta);
					sum += coeffs[3, 5] * (float)Math.Cos(a * 3) * (float)Math.Cos(b * 5) * (beta);
					sum += coeffs[3, 6] * (float)Math.Cos(a * 3) * (float)Math.Cos(b * 6) * (beta);
					sum += coeffs[3, 7] * (float)Math.Cos(a * 3) * (float)Math.Cos(b * 7) * (beta);
					
					sum += coeffs[4, 0] * (float)Math.Cos(a * 4) * (float)Math.Cos(b * 0) * (sqrt2 * beta);
					sum += coeffs[4, 1] * (float)Math.Cos(a * 4) * (float)Math.Cos(b * 1) * (beta);
					sum += coeffs[4, 2] * (float)Math.Cos(a * 4) * (float)Math.Cos(b * 2) * (beta);
					sum += coeffs[4, 3] * (float)Math.Cos(a * 4) * (float)Math.Cos(b * 3) * (beta);
					sum += coeffs[4, 4] * (float)Math.Cos(a * 4) * (float)Math.Cos(b * 4) * (beta);
					sum += coeffs[4, 5] * (float)Math.Cos(a * 4) * (float)Math.Cos(b * 5) * (beta);
					sum += coeffs[4, 6] * (float)Math.Cos(a * 4) * (float)Math.Cos(b * 6) * (beta);
					sum += coeffs[4, 7] * (float)Math.Cos(a * 4) * (float)Math.Cos(b * 7) * (beta);
					
					sum += coeffs[5, 0] * (float)Math.Cos(a * 5) * (float)Math.Cos(b * 0) * (sqrt2 * beta);
					sum += coeffs[5, 1] * (float)Math.Cos(a * 5) * (float)Math.Cos(b * 1) * (beta);
					sum += coeffs[5, 2] * (float)Math.Cos(a * 5) * (float)Math.Cos(b * 2) * (beta);
					sum += coeffs[5, 3] * (float)Math.Cos(a * 5) * (float)Math.Cos(b * 3) * (beta);
					sum += coeffs[5, 4] * (float)Math.Cos(a * 5) * (float)Math.Cos(b * 4) * (beta);
					sum += coeffs[5, 5] * (float)Math.Cos(a * 5) * (float)Math.Cos(b * 5) * (beta);
					sum += coeffs[5, 6] * (float)Math.Cos(a * 5) * (float)Math.Cos(b * 6) * (beta);
					sum += coeffs[5, 7] * (float)Math.Cos(a * 5) * (float)Math.Cos(b * 7) * (beta);
					
					sum += coeffs[6, 0] * (float)Math.Cos(a * 6) * (float)Math.Cos(b * 0) * (sqrt2 * beta);
					sum += coeffs[6, 1] * (float)Math.Cos(a * 6) * (float)Math.Cos(b * 1) * (beta);
					sum += coeffs[6, 2] * (float)Math.Cos(a * 6) * (float)Math.Cos(b * 2) * (beta);
					sum += coeffs[6, 3] * (float)Math.Cos(a * 6) * (float)Math.Cos(b * 3) * (beta);
					sum += coeffs[6, 4] * (float)Math.Cos(a * 6) * (float)Math.Cos(b * 4) * (beta);
					sum += coeffs[6, 5] * (float)Math.Cos(a * 6) * (float)Math.Cos(b * 5) * (beta);
					sum += coeffs[6, 6] * (float)Math.Cos(a * 6) * (float)Math.Cos(b * 6) * (beta);
					sum += coeffs[6, 7] * (float)Math.Cos(a * 6) * (float)Math.Cos(b * 7) * (beta);
					
					sum += coeffs[7, 0] * (float)Math.Cos(a * 7) * (float)Math.Cos(b * 0) * (sqrt2 * beta);
					sum += coeffs[7, 1] * (float)Math.Cos(a * 7) * (float)Math.Cos(b * 1) * (beta);
					sum += coeffs[7, 2] * (float)Math.Cos(a * 7) * (float)Math.Cos(b * 2) * (beta);
					sum += coeffs[7, 3] * (float)Math.Cos(a * 7) * (float)Math.Cos(b * 3) * (beta);
					sum += coeffs[7, 4] * (float)Math.Cos(a * 7) * (float)Math.Cos(b * 4) * (beta);
					sum += coeffs[7, 5] * (float)Math.Cos(a * 7) * (float)Math.Cos(b * 5) * (beta);
					sum += coeffs[7, 6] * (float)Math.Cos(a * 7) * (float)Math.Cos(b * 6) * (beta);
					sum += coeffs[7, 7] * (float)Math.Cos(a * 7) * (float)Math.Cos(b * 7) * (beta);
					
					output[step, j] = sum + 128;
				}
			});
		}
    }
}