// using System;
// using System.Threading.Tasks;
// using JPEG.Utilities;
//
// namespace JPEG
// {
// 	public class DCT
// 	{
// 		public static void DCT2D(float[,] input, float[,] coeffs)
// 		{
// 			var width = (byte)input.GetLength(1);
// 			var height = (byte)input.GetLength(0);
// 			var beta = Beta(height, width);
//
// 			if (width <= Environment.ProcessorCount)
// 			{
// 				Parallel.For(0, width, step =>
// 				{
// 					MathEx.LoopByTwoVariables(
// 						(byte)step, (byte)(step + 1),
// 						0, height,
// 						(u, v) =>
// 						{
// 							coeffs[u, v] = MathEx
// 								.SumByTwoVariables(
// 									0, width,
// 									0, height,
// 									(x, y) => BasisFunction(input[x, y], u, v, x, y, height, width) * Alpha(u) * Alpha(v)) * beta;
// 						});
// 				});
// 			}
// 			else
// 			{
// 				MathEx.LoopByTwoVariables(
// 					0, width,
// 					0, height,
// 					(u, v) =>
// 					{
// 						var sum = MathEx
// 							.SumByTwoVariables(
// 								0, width,
// 								0, height,
// 								(x, y) => BasisFunction(input[x, y], u, v, x, y, height, width));
//
// 						coeffs[u, v] = sum * beta * Alpha(u) * Alpha(v);
// 					});
// 			}
// 		}
//
// 		public static void IDCT2D(float[,] coeffs, float[,] output)
// 		{
// 			var height = (byte)coeffs.GetLength(0);
// 			var width = (byte)coeffs.GetLength(1);
// 			var beta = Beta(height, width);
// 			
// 			if (width <= Environment.ProcessorCount)
// 			{
// 				Parallel.For(0, width, step =>
// 				{
// 					MathEx.LoopByTwoVariables(
// 						(byte)step, (byte)(step + 1),
// 						0, height,
// 						(x, y) =>
// 						{
// 							output[x, y] = MathEx
// 								.SumByTwoVariables(
// 									0, width,
// 									0, height,
// 									(u, v) => BasisFunction(coeffs[u, v], u, v, x, y, height, width) * Alpha(u) * Alpha(v)) * beta + 128;
// 						});
// 				});
// 			}
// 			else
// 			{
// 				for(byte x = 0; x < width; x++)
// 				{
// 					for(byte y = 0; y < height; y++)
// 					{
// 						output[x, y] = MathEx
// 							.SumByTwoVariables(
// 								0, width,
// 								0, height,
// 								(u, v) => BasisFunction(coeffs[u, v], u, v, x, y, height, width) * Alpha(u) * Alpha(v)) * beta;
// 					}
// 				}
// 			}
// 		}
//
// 		public static float BasisFunction(float a, byte u, byte v, byte x, byte y, byte height, byte width)
// 		{
// 			return a 
// 			       * (float)Math.Cos(((2f * x + 1f) * u * (float)Math.PI) / (2f * width)) 
// 			       * (float)Math.Cos(((2f * y + 1f) * v * (float)Math.PI) / (2f * height));
// 		}
//
// 		private static float Alpha(byte u)
// 		{
// 			if(u == 0)
// 				return 1f / (float)Math.Sqrt(2);
// 			return 1f;
// 		}
//
// 		private static float Beta(byte height, byte width)
// 		{
// 			return 1f / width + 1f / height;
// 		}
// 	}
// }