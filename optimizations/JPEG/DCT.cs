using System;
using JPEG.Utilities;

namespace JPEG
{
	public class DCT
	{
		public static float[,] DCT2D(float[,] input)
		{
			var height = input.GetLength(0);
			var width = input.GetLength(1);
			var coeffs = new float[width, height];

			MathEx.LoopByTwoVariables(
				0, width,
				0, height,
				(u, v) =>
				{
					var sum = MathEx
						.SumByTwoVariables(
							0, width,
							0, height,
							(x, y) => BasisFunction(input[x, y], u, v, x, y, height, width));

					coeffs[u, v] = sum * Beta(height, width) * Alpha(u) * Alpha(v);
				});
			
			return coeffs;
		}

		public static void IDCT2D(float[,] coeffs, float[,] output)
		{
			for(var x = 0; x < coeffs.GetLength(1); x++)
			{
				for(var y = 0; y < coeffs.GetLength(0); y++)
				{
					var sum = MathEx
						.SumByTwoVariables(
							0, coeffs.GetLength(1),
							0, coeffs.GetLength(0),
							(u, v) => BasisFunction(coeffs[u, v], u, v, x, y, coeffs.GetLength(0), coeffs.GetLength(1)) * Alpha(u) * Alpha(v));

					output[x, y] = sum * Beta(coeffs.GetLength(0), coeffs.GetLength(1));
				}
			}
		}

		public static float BasisFunction(float a, float u, float v, float x, float y, int height, int width)
		{
			var b = (float)Math.Cos(((2f * x + 1f) * u * (float)Math.PI) / (2f * width));
			var c = (float)Math.Cos(((2f * y + 1f) * v * (float)Math.PI) / (2f * height));

			return a * b * c;
		}

		private static float Alpha(int u)
		{
			if(u == 0)
				return 1f / (float)Math.Sqrt(2);
			return 1f;
		}

		private static float Beta(int height, int width)
		{
			return 1f / width + 1f / height;
		}
	}
}