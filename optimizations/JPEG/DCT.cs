using System;
using System.Threading.Tasks;
using JPEG.Utilities;

namespace JPEG
{
	public class DCT
	{
		public static float[,] DCT2D(float[,] input)
		{
			var width = (byte)input.GetLength(1);
			var height = (byte)input.GetLength(0);
			var coeffs = new float[width, height];

			if (width <= Environment.ProcessorCount)
			{
				Parallel.For(0, width, step =>
				{
					MathEx.LoopByTwoVariables(
						(byte)step, (byte)(step + 1),
						0, height,
						(u, v) =>
						{
							var sum = MathEx
								.SumByTwoVariables(
									0, width,
									0, height,
									(byte x, byte y) => BasisFunction(input[x, y], u, v, x, y, height, width) * Alpha(u) * Alpha(v));

							coeffs[u, v] = sum * Beta(height, width);
						});
				});
			}
			else
			{
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
			}
			return coeffs;
		}

		public static void IDCT2D(float[,] coeffs, float[,] output)
		{
			var height = (byte)coeffs.GetLength(0);
			var width = (byte)coeffs.GetLength(1);
			
			if (width <= Environment.ProcessorCount)
			{
				Parallel.For(0, width, step =>
				{
					// for(byte y = 0; y < height; y++)
					// {
					// 	var sum = MathEx
					// 		.SumByTwoVariables(
					// 			0, width,
					// 			0, height,
					// 			(u, v) => BasisFunction(coeffs[u, v], u, v, (byte)step, y, height, width) * Alpha(u) * Alpha(v));
					//
					// 	output[step, y] = sum * Beta(height, width);
					// }
					
					MathEx.LoopByTwoVariables(
						(byte)step, (byte)(step + 1),
						0, height,
						(x, y) =>
						{
							var sum = MathEx
								.SumByTwoVariables(
									0, width,
									0, height,
									(u, v) => BasisFunction(coeffs[u, v], u, v, x, y, height, width) * Alpha(u) * Alpha(v));

							output[x, y] = sum * Beta(height, width);
						});
				});
			}
			else
			{
				for(byte x = 0; x < width; x++)
				{
					for(byte y = 0; y < height; y++)
					{
						var sum = MathEx
							.SumByTwoVariables(
								0, width,
								0, height,
								(u, v) => BasisFunction(coeffs[u, v], u, v, x, y, height, width) * Alpha(u) * Alpha(v));

						output[x, y] = sum * Beta(height, width);
					}
				}
			}
		}

		public static float BasisFunction(float a, byte u, byte v, byte x, byte y, byte height, byte width)
		{
			var b = (float)Math.Cos(((2f * x + 1f) * u * (float)Math.PI) / (2f * width));
			var c = (float)Math.Cos(((2f * y + 1f) * v * (float)Math.PI) / (2f * height));

			return a * b * c;
		}

		private static float Alpha(byte u)
		{
			if(u == 0)
				return 1f / (float)Math.Sqrt(2);
			return 1f;
		}

		private static float Beta(byte height, byte width)
		{
			return 1f / width + 1f / height;
		}
	}
}