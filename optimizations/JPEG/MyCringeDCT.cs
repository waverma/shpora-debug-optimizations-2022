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
				fixed (float* a = DCTCosCache, cc = coeffs)
				{
					var source = new Span<float>(a, 64);
					var coeffsC = new Span<float>(cc, 64);

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

						fixed (float* ic = input)
						{
							var inputC = new Span<float>(ic, 64);

							sum += inputC[0] * cosA0 * cosB0;
							sum += inputC[1] * cosA0 * cosB1;
							sum += inputC[2] * cosA0 * cosB2;
							sum += inputC[3] * cosA0 * cosB3;
							sum += inputC[4] * cosA0 * cosB4;
							sum += inputC[5] * cosA0 * cosB5;
							sum += inputC[6] * cosA0 * cosB6;
							sum += inputC[7] * cosA0 * cosB7;

							sum += inputC[8] * cosA1 * cosB0;
							sum += inputC[9] * cosA1 * cosB1;
							sum += inputC[10] * cosA1 * cosB2;
							sum += inputC[11] * cosA1 * cosB3;
							sum += inputC[12] * cosA1 * cosB4;
							sum += inputC[13] * cosA1 * cosB5;
							sum += inputC[14] * cosA1 * cosB6;
							sum += inputC[15] * cosA1 * cosB7;

							sum += inputC[16] * cosA2 * cosB0;
							sum += inputC[17] * cosA2 * cosB1;
							sum += inputC[18] * cosA2 * cosB2;
							sum += inputC[19] * cosA2 * cosB3;
							sum += inputC[20] * cosA2 * cosB4;
							sum += inputC[21] * cosA2 * cosB5;
							sum += inputC[22] * cosA2 * cosB6;
							sum += inputC[23] * cosA2 * cosB7;

							sum += inputC[24] * cosA3 * cosB0;
							sum += inputC[25] * cosA3 * cosB1;
							sum += inputC[26] * cosA3 * cosB2;
							sum += inputC[27] * cosA3 * cosB3;
							sum += inputC[28] * cosA3 * cosB4;
							sum += inputC[29] * cosA3 * cosB5;
							sum += inputC[30] * cosA3 * cosB6;
							sum += inputC[31] * cosA3 * cosB7;

							sum += inputC[32] * cosA4 * cosB0;
							sum += inputC[33] * cosA4 * cosB1;
							sum += inputC[34] * cosA4 * cosB2;
							sum += inputC[35] * cosA4 * cosB3;
							sum += inputC[36] * cosA4 * cosB4;
							sum += inputC[37] * cosA4 * cosB5;
							sum += inputC[38] * cosA4 * cosB6;
							sum += inputC[39] * cosA4 * cosB7;

							sum += inputC[40] * cosA5 * cosB0;
							sum += inputC[41] * cosA5 * cosB1;
							sum += inputC[42] * cosA5 * cosB2;
							sum += inputC[43] * cosA5 * cosB3;
							sum += inputC[44] * cosA5 * cosB4;
							sum += inputC[45] * cosA5 * cosB5;
							sum += inputC[46] * cosA5 * cosB6;
							sum += inputC[47] * cosA5 * cosB7;

							sum += inputC[48] * cosA6 * cosB0;
							sum += inputC[49] * cosA6 * cosB1;
							sum += inputC[50] * cosA6 * cosB2;
							sum += inputC[51] * cosA6 * cosB3;
							sum += inputC[52] * cosA6 * cosB4;
							sum += inputC[53] * cosA6 * cosB5;
							sum += inputC[54] * cosA6 * cosB6;
							sum += inputC[55] * cosA6 * cosB7;

							sum += inputC[56] * cosA7 * cosB0;
							sum += inputC[57] * cosA7 * cosB1;
							sum += inputC[58] * cosA7 * cosB2;
							sum += inputC[59] * cosA7 * cosB3;
							sum += inputC[60] * cosA7 * cosB4;
							sum += inputC[61] * cosA7 * cosB5;
							sum += inputC[62] * cosA7 * cosB6;
							sum += inputC[63] * cosA7 * cosB7;
							
							coeffsC[step * 8 + j] = sum * c * (j == 0 ? sqrt2 * beta : beta);
						}
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

				fixed (float* a = IDCTCosCache, oc = output)
				{
					var source = new Span<float>(a, 64);
					var outputC = new Span<float>(oc, 64);
					
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

						
						fixed (float* c = coeffs)
						{
							var coeffsC = new Span<float>(c, 64);

							sum += coeffsC[0] * cosA0 * cosB0 * sqrt2;
							sum += coeffsC[8] * cosA1 * cosB0; 
							sum += coeffsC[16] * cosA2 * cosB0;
							sum += coeffsC[24] * cosA3 * cosB0;
							sum += coeffsC[32] * cosA4 * cosB0;
							sum += coeffsC[40] * cosA5 * cosB0;
							sum += coeffsC[48] * cosA6 * cosB0;
							sum += coeffsC[56] * cosA7 * cosB0;
							
							sum += coeffsC[1] * cosA0 * cosB1;
							sum += coeffsC[2] * cosA0 * cosB2;
							sum += coeffsC[3] * cosA0 * cosB3;
							sum += coeffsC[4] * cosA0 * cosB4;
							sum += coeffsC[5] * cosA0 * cosB5;
							sum += coeffsC[6] * cosA0 * cosB6;
							sum += coeffsC[7] * cosA0 * cosB7;
							sum *= sqrt2;

							sum += coeffsC[9] * cosA1 * cosB1;
							sum += coeffsC[10] * cosA1 * cosB2;
							sum += coeffsC[11] * cosA1 * cosB3;
							sum += coeffsC[12] * cosA1 * cosB4;
							sum += coeffsC[13] * cosA1 * cosB5;
							sum += coeffsC[14] * cosA1 * cosB6;
							sum += coeffsC[15] * cosA1 * cosB7;

							sum += coeffsC[17] * cosA2 * cosB1;
							sum += coeffsC[18] * cosA2 * cosB2;
							sum += coeffsC[19] * cosA2 * cosB3;
							sum += coeffsC[20] * cosA2 * cosB4;
							sum += coeffsC[21] * cosA2 * cosB5;
							sum += coeffsC[22] * cosA2 * cosB6;
							sum += coeffsC[23] * cosA2 * cosB7;

							sum += coeffsC[25] * cosA3 * cosB1;
							sum += coeffsC[26] * cosA3 * cosB2;
							sum += coeffsC[27] * cosA3 * cosB3;
							sum += coeffsC[28] * cosA3 * cosB4;
							sum += coeffsC[29] * cosA3 * cosB5;
							sum += coeffsC[30] * cosA3 * cosB6;
							sum += coeffsC[31] * cosA3 * cosB7;

							sum += coeffsC[33] * cosA4 * cosB1;
							sum += coeffsC[34] * cosA4 * cosB2;
							sum += coeffsC[35] * cosA4 * cosB3;
							sum += coeffsC[36] * cosA4 * cosB4;
							sum += coeffsC[37] * cosA4 * cosB5;
							sum += coeffsC[38] * cosA4 * cosB6;
							sum += coeffsC[39] * cosA4 * cosB7;

							sum += coeffsC[41] * cosA5 * cosB1;
							sum += coeffsC[42] * cosA5 * cosB2;
							sum += coeffsC[43] * cosA5 * cosB3;
							sum += coeffsC[44] * cosA5 * cosB4;
							sum += coeffsC[45] * cosA5 * cosB5;
							sum += coeffsC[46] * cosA5 * cosB6;
							sum += coeffsC[47] * cosA5 * cosB7;

							sum += coeffsC[49] * cosA6 * cosB1;
							sum += coeffsC[50] * cosA6 * cosB2;
							sum += coeffsC[51] * cosA6 * cosB3;
							sum += coeffsC[52] * cosA6 * cosB4;
							sum += coeffsC[53] * cosA6 * cosB5;
							sum += coeffsC[54] * cosA6 * cosB6;
							sum += coeffsC[55] * cosA6 * cosB7;

							sum += coeffsC[57] * cosA7 * cosB1;
							sum += coeffsC[58] * cosA7 * cosB2;
							sum += coeffsC[59] * cosA7 * cosB3;
							sum += coeffsC[60] * cosA7 * cosB4;
							sum += coeffsC[61] * cosA7 * cosB5;
							sum += coeffsC[62] * cosA7 * cosB6;
							sum += coeffsC[63] * cosA7 * cosB7;
							
							outputC[step * 8 + j] = sum * beta + 128;
						}
					}
				}
			});
		}
    }
}
