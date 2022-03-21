using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JPEG
{
	public static partial class MyCringeDCT
    {
        private static byte width = 8;
	    private static byte height = 8;
	    private static float beta = 1f / width + 1f / height;
	    private static float sqrt2 = 1f / (float)Math.Sqrt(2);
	    private static double piCoef = Math.PI / (2f * height);

	    private static readonly float[,] DCTCosCache = new float[8, 8];
	    private static readonly float[,] IDCTCosCache = new float[8, 8];

	    public static unsafe void DCT2D(float[,] input, float[,] coeffs)
	    {
			Parallel.For(0, width, step =>
			{
				var cosA0 = DCTCosCache[step, 0];
				var cosA1 = DCTCosCache[step, 1];
				var cosA2 = DCTCosCache[step, 2];
				var cosA3 = DCTCosCache[step, 3];
				var cosA4 = DCTCosCache[step, 4];
				var cosA5 = DCTCosCache[step, 5];
				var cosA6 = DCTCosCache[step, 6];
				var cosA7 = DCTCosCache[step, 7];
				
				float sum, cosB0, cosB1, cosB2, cosB3, cosB4, cosB5, cosB6, cosB7;
				var c = step == 0 ? sqrt2 : 1f;
				fixed (float* a = DCTCosCache)
				{
					var source = new Span<float>(a, 64);

					for (byte j = 0; j < height; j++)
					{
						cosB0 = source[j * 8 + 0];
						cosB1 = source[j * 8 + 1];
						cosB2 = source[j * 8 + 2];
						cosB3 = source[j * 8 + 3];
						cosB4 = source[j * 8 + 4];
						cosB5 = source[j * 8 + 5];
						cosB6 = source[j * 8 + 6];
						cosB7 = source[j * 8 + 7];
						sum = 0f;
	        
						sum += input[0, 0] * cosA0 * cosB0;
					    sum += input[0, 1] * cosA0 * cosB1;
					    sum += input[0, 2] * cosA0 * cosB2;
					    sum += input[0, 3] * cosA0 * cosB3;
					    sum += input[0, 4] * cosA0 * cosB4;
					    sum += input[0, 5] * cosA0 * cosB5;
					    sum += input[0, 6] * cosA0 * cosB6;
					    sum += input[0, 7] * cosA0 * cosB7;
					    
					    sum += input[1, 0] * cosA1 * cosB0;
					    sum += input[1, 1] * cosA1 * cosB1;
					    sum += input[1, 2] * cosA1 * cosB2;
					    sum += input[1, 3] * cosA1 * cosB3;
					    sum += input[1, 4] * cosA1 * cosB4;
					    sum += input[1, 5] * cosA1 * cosB5;
					    sum += input[1, 6] * cosA1 * cosB6;
					    sum += input[1, 7] * cosA1 * cosB7;
					    
					    sum += input[2, 0] * cosA2 * cosB0;
					    sum += input[2, 1] * cosA2 * cosB1;
					    sum += input[2, 2] * cosA2 * cosB2;
					    sum += input[2, 3] * cosA2 * cosB3;
					    sum += input[2, 4] * cosA2 * cosB4;
					    sum += input[2, 5] * cosA2 * cosB5;
					    sum += input[2, 6] * cosA2 * cosB6;
					    sum += input[2, 7] * cosA2 * cosB7;
					    
					    sum += input[3, 0] * cosA3 * cosB0;
					    sum += input[3, 1] * cosA3 * cosB1;
					    sum += input[3, 2] * cosA3 * cosB2;
					    sum += input[3, 3] * cosA3 * cosB3;
					    sum += input[3, 4] * cosA3 * cosB4;
					    sum += input[3, 5] * cosA3 * cosB5;
					    sum += input[3, 6] * cosA3 * cosB6;
					    sum += input[3, 7] * cosA3 * cosB7;
					    
					    sum += input[4, 0] * cosA4 * cosB0;
					    sum += input[4, 1] * cosA4 * cosB1;
					    sum += input[4, 2] * cosA4 * cosB2;
					    sum += input[4, 3] * cosA4 * cosB3;
					    sum += input[4, 4] * cosA4 * cosB4;
					    sum += input[4, 5] * cosA4 * cosB5;
					    sum += input[4, 6] * cosA4 * cosB6;
					    sum += input[4, 7] * cosA4 * cosB7;
					    
					    sum += input[5, 0] * cosA5 * cosB0;
					    sum += input[5, 1] * cosA5 * cosB1;
					    sum += input[5, 2] * cosA5 * cosB2;
					    sum += input[5, 3] * cosA5 * cosB3;
					    sum += input[5, 4] * cosA5 * cosB4;
					    sum += input[5, 5] * cosA5 * cosB5;
					    sum += input[5, 6] * cosA5 * cosB6;
					    sum += input[5, 7] * cosA5 * cosB7;
					    
					    sum += input[6, 0] * cosA6 * cosB0;
					    sum += input[6, 1] * cosA6 * cosB1;
					    sum += input[6, 2] * cosA6 * cosB2;
					    sum += input[6, 3] * cosA6 * cosB3;
					    sum += input[6, 4] * cosA6 * cosB4;
					    sum += input[6, 5] * cosA6 * cosB5;
					    sum += input[6, 6] * cosA6 * cosB6;
					    sum += input[6, 7] * cosA6 * cosB7;
					    
					    sum += input[7, 0] * cosA7 * cosB0;
					    sum += input[7, 1] * cosA7 * cosB1;
					    sum += input[7, 2] * cosA7 * cosB2;
					    sum += input[7, 3] * cosA7 * cosB3;
					    sum += input[7, 4] * cosA7 * cosB4;
					    sum += input[7, 5] * cosA7 * cosB5;
					    sum += input[7, 6] * cosA7 * cosB6;
					    sum += input[7, 7] * cosA7 * cosB7;
					
						coeffs[step, j] = sum * c * (j == 0 ? sqrt2 * beta : beta);
					}
				}
			});
		}

		public static unsafe void IDCT2D(float[,] coeffs, float[,] output)
		{
			Parallel.For(0, width, step =>
			{
				var cosA0 = IDCTCosCache[step, 0];
				var cosA1 = IDCTCosCache[step, 1];
				var cosA2 = IDCTCosCache[step, 2];
				var cosA3 = IDCTCosCache[step, 3];
				var cosA4 = IDCTCosCache[step, 4];
				var cosA5 = IDCTCosCache[step, 5];
				var cosA6 = IDCTCosCache[step, 6];
				var cosA7 = IDCTCosCache[step, 7];
				
				float sum, cosB0, cosB1, cosB2, cosB3, cosB4, cosB5, cosB6, cosB7;

				fixed (float* a = IDCTCosCache)
				{
					var source = new Span<float>(a, 64);
					for (byte j = 0; j < height; j++)
					{
						sum = 0f;
						cosB0 = source[j * 8 + 0];
						cosB1 = source[j * 8 + 1];
						cosB2 = source[j * 8 + 2];
						cosB3 = source[j * 8 + 3];
						cosB4 = source[j * 8 + 4];
						cosB5 = source[j * 8 + 5];
						cosB6 = source[j * 8 + 6];
						cosB7 = source[j * 8 + 7];

						// сюда тоже указатели))))
						sum += coeffs[0, 0] * cosA0 * cosB0 * sqrt2 * sqrt2;
						sum += coeffs[0, 1] * cosA0 * cosB1 * sqrt2;
						sum += coeffs[0, 2] * cosA0 * cosB2 * sqrt2;
						sum += coeffs[0, 3] * cosA0 * cosB3 * sqrt2;
						sum += coeffs[0, 4] * cosA0 * cosB4 * sqrt2;
						sum += coeffs[0, 5] * cosA0 * cosB5 * sqrt2;
						sum += coeffs[0, 6] * cosA0 * cosB6 * sqrt2;
						sum += coeffs[0, 7] * cosA0 * cosB7 * sqrt2;

						sum += coeffs[1, 0] * cosA1 * cosB0 * sqrt2;
						sum += coeffs[1, 1] * cosA1 * cosB1;
						sum += coeffs[1, 2] * cosA1 * cosB2;
						sum += coeffs[1, 3] * cosA1 * cosB3;
						sum += coeffs[1, 4] * cosA1 * cosB4;
						sum += coeffs[1, 5] * cosA1 * cosB5;
						sum += coeffs[1, 6] * cosA1 * cosB6;
						sum += coeffs[1, 7] * cosA1 * cosB7;

						sum += coeffs[2, 0] * cosA2 * cosB0 * sqrt2;
						sum += coeffs[2, 1] * cosA2 * cosB1;
						sum += coeffs[2, 2] * cosA2 * cosB2;
						sum += coeffs[2, 3] * cosA2 * cosB3;
						sum += coeffs[2, 4] * cosA2 * cosB4;
						sum += coeffs[2, 5] * cosA2 * cosB5;
						sum += coeffs[2, 6] * cosA2 * cosB6;
						sum += coeffs[2, 7] * cosA2 * cosB7;

						sum += coeffs[3, 0] * cosA3 * cosB0 * sqrt2;
						sum += coeffs[3, 1] * cosA3 * cosB1;
						sum += coeffs[3, 2] * cosA3 * cosB2;
						sum += coeffs[3, 3] * cosA3 * cosB3;
						sum += coeffs[3, 4] * cosA3 * cosB4;
						sum += coeffs[3, 5] * cosA3 * cosB5;
						sum += coeffs[3, 6] * cosA3 * cosB6;
						sum += coeffs[3, 7] * cosA3 * cosB7;

						sum += coeffs[4, 0] * cosA4 * cosB0 * sqrt2;
						sum += coeffs[4, 1] * cosA4 * cosB1;
						sum += coeffs[4, 2] * cosA4 * cosB2;
						sum += coeffs[4, 3] * cosA4 * cosB3;
						sum += coeffs[4, 4] * cosA4 * cosB4;
						sum += coeffs[4, 5] * cosA4 * cosB5;
						sum += coeffs[4, 6] * cosA4 * cosB6;
						sum += coeffs[4, 7] * cosA4 * cosB7;

						sum += coeffs[5, 0] * cosA5 * cosB0 * sqrt2;
						sum += coeffs[5, 1] * cosA5 * cosB1;
						sum += coeffs[5, 2] * cosA5 * cosB2;
						sum += coeffs[5, 3] * cosA5 * cosB3;
						sum += coeffs[5, 4] * cosA5 * cosB4;
						sum += coeffs[5, 5] * cosA5 * cosB5;
						sum += coeffs[5, 6] * cosA5 * cosB6;
						sum += coeffs[5, 7] * cosA5 * cosB7;

						sum += coeffs[6, 0] * cosA6 * cosB0 * sqrt2;
						sum += coeffs[6, 1] * cosA6 * cosB1;
						sum += coeffs[6, 2] * cosA6 * cosB2;
						sum += coeffs[6, 3] * cosA6 * cosB3;
						sum += coeffs[6, 4] * cosA6 * cosB4;
						sum += coeffs[6, 5] * cosA6 * cosB5;
						sum += coeffs[6, 6] * cosA6 * cosB6;
						sum += coeffs[6, 7] * cosA6 * cosB7;

						sum += coeffs[7, 0] * cosA7 * cosB0 * sqrt2;
						sum += coeffs[7, 1] * cosA7 * cosB1;
						sum += coeffs[7, 2] * cosA7 * cosB2;
						sum += coeffs[7, 3] * cosA7 * cosB3;
						sum += coeffs[7, 4] * cosA7 * cosB4;
						sum += coeffs[7, 5] * cosA7 * cosB5;
						sum += coeffs[7, 6] * cosA7 * cosB6;
						sum += coeffs[7, 7] * cosA7 * cosB7;

						output[step, j] = sum * beta + 128;
					}
				}
			});
		}
    }
}