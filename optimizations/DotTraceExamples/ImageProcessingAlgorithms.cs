using System;
using ImageProcessing;

namespace DotTraceExamples
{
	public static class ImageProcessingAlgorithms
	{
		#region EdgePreservingSmoothing
		// Multiplication factor for convolution mask in edge preserving smoothing
		private const int multiplicationFactor = 10;

		public static RGBImage EdgePreservingSmoothing(this RGBImage image)
		{
			var imageData = image.ImageData;
			var height = image.Height;
			var bytesPerLine = image.BytesPerLine;
			var bitesPerPixel = RGBImage.BytesPerPixel;
			var filteringData = new byte[imageData.Length];

			for (var y = 0; y < height; y++)
			{
				for (var x = 0; x < bytesPerLine; x += bitesPerPixel)
				{
					var i = y * bytesPerLine + x;

					if (x == 0 || y == 0 || x == bytesPerLine - bitesPerPixel || y == height - 1)
					{
						filteringData[i] = imageData[i];
						filteringData[i + 1] = imageData[i + 1];
						filteringData[i + 2] = imageData[i + 2];

						continue;
					}

					//Center pixel of convolution mask
					var centerRed = imageData[i + 2];
					var centerGreen = imageData[i + 1];
					var centerBlue = imageData[i];

					// Indexes of neighbor pixels of convolution mask
					var id1 = (y - 1) * bytesPerLine + (x - bitesPerPixel);
					var id2 = (y - 1) * bytesPerLine + x;
					var id3 = (y - 1) * bytesPerLine + x + bitesPerPixel;
					var id4 = y * bytesPerLine + (x - bitesPerPixel);
					var id5 = y * bytesPerLine + x + bitesPerPixel;
					var id6 = (y + 1) * bytesPerLine + (x - bitesPerPixel);
					var id7 = (y + 1) * bytesPerLine + x;
					var id8 = (y + 1) * bytesPerLine + x + bitesPerPixel;

					var c1 = GetCoefficient(centerRed, centerGreen, centerBlue, imageData[id1 + 2], imageData[id1 + 1],
						imageData[id1]);
					var c2 = GetCoefficient(centerRed, centerGreen, centerBlue, imageData[id2 + 2], imageData[id2 + 1],
						imageData[id2]);
					var c3 = GetCoefficient(centerRed, centerGreen, centerBlue, imageData[id3 + 2], imageData[id3 + 1],
						imageData[id3]);
					var c4 = GetCoefficient(centerRed, centerGreen, centerBlue, imageData[id4 + 2], imageData[id4 + 1],
						imageData[id4]);
					var c5 = GetCoefficient(centerRed, centerGreen, centerBlue, imageData[id5 + 2], imageData[id5 + 1],
						imageData[id5]);
					var c6 = GetCoefficient(centerRed, centerGreen, centerBlue, imageData[id6 + 2], imageData[id6 + 1],
						imageData[id6]);
					var c7 = GetCoefficient(centerRed, centerGreen, centerBlue, imageData[id7 + 2], imageData[id7 + 1],
						imageData[id7]);
					var c8 = GetCoefficient(centerRed, centerGreen, centerBlue, imageData[id8 + 2], imageData[id8 + 1],
						imageData[id8]);

					var csum = c1 + c2 + c3 + c4 + c5 + c6 + c7 + c8;

					var resultRed = (imageData[id1 + 2] * c8 +
									 imageData[id2 + 2] * c7 +
									 imageData[id3 + 2] * c6 +
									 imageData[id4 + 2] * c5 +
									 imageData[id5 + 2] * c4 +
									 imageData[id6 + 2] * c3 +
									 imageData[id7 + 2] * c2 +
									 imageData[id8 + 2] * c1) / csum;

					var resultGreen = (imageData[id1 + 1] * c8 +
									   imageData[id2 + 1] * c7 +
									   imageData[id3 + 1] * c6 +
									   imageData[id4 + 1] * c5 +
									   imageData[id5 + 1] * c4 +
									   imageData[id6 + 1] * c3 +
									   imageData[id7 + 1] * c2 +
									   imageData[id8 + 1] * c1) / csum;

					var resultBlue = (imageData[id1] * c8 +
									  imageData[id2] * c7 +
									  imageData[id3] * c6 +
									  imageData[id4] * c5 +
									  imageData[id5] * c4 +
									  imageData[id6] * c3 +
									  imageData[id7] * c2 +
									  imageData[id8] * c1) / csum;


					filteringData[i + 2] = (byte)resultRed;
					filteringData[i + 1] = (byte)resultGreen;
					filteringData[i] = (byte)resultBlue;
				}
			}

			return new RGBImage(image.Width, height, bytesPerLine, filteringData);
		}

		/// <summary>
		/// Calculate coefficient for the convolution mask of edge preserving smoothing
		/// </summary>
		/// <param name="centerRed"></param>
		/// <param name="centerGreen"></param>
		/// <param name="centerBlue"></param>
		/// <param name="neighborRed"></param>
		/// <param name="neighborGreen"></param>
		/// <param name="neighborBlue"></param>
		/// <returns></returns>
		private static double GetCoefficient(byte centerRed, byte centerGreen, byte centerBlue, byte neighborRed,
			byte neighborGreen, byte neighborBlue)
		{
			var d = Math.Abs(centerRed - neighborRed) + Math.Abs(centerGreen - neighborGreen) +
					Math.Abs(centerBlue - neighborBlue);

			var r = 1 - d;
			for (int i = 0; i < multiplicationFactor; i++)
			{
				r *= r;
			}
			return r;
		}

		#endregion

		#region MeanShift
		private const int rad = 2;
		private const int radCol = 20;
		private const int radCol2 = radCol * radCol;

		private static byte[] bc = new byte[3];
		private static double[] dc = new double[3];
		
		public static RGBImage MeanShift(this RGBImage image)
		{
			var width = image.Width;
			var height = image.Height;
			var imageData = image.ImageData;
			var bytesPerLine = image.BytesPerLine;
			var bytesPerPixel = RGBImage.BytesPerPixel;
			var pixelsf = new double[width, height][];
			var filteringData = new byte[imageData.Length];
			
			

			for (var y = 0; y < height; y++)
			{
				for (var x = 0; x < width; x++)
				{
					var curElem = y * bytesPerLine + x * bytesPerPixel;
					var r = image.ImageData[curElem + 2];
					var g = image.ImageData[curElem + 1];
					var b = image.ImageData[curElem];
					dc[0] = r;
					dc[2] = b;
					dc[1] = g;
					RgbToXyz();
					XyzToLuv();

					pixelsf[x, y] = dc;
				}
			}

			var numOfIterations = 0;

			for (var y = 0; y < height; y++)
			{
				for (int x = 0, resX = 0; x < width; x++, resX += bytesPerPixel)
				{
					var i = y * bytesPerLine + resX;

					var xCenter = x;
					var yCenter = y;
					var lCenter = pixelsf[x, y][0];
					var uCenter = pixelsf[x, y][1];
					var vCenter = pixelsf[x, y][2];
					double shift;

					do
					{
						var xCenterOld = xCenter;
						var yCenterOld = yCenter;
						var lCenterOld = lCenter;
						var uCenterOld = uCenter;
						var vCenterOld = vCenter;

						float mx = 0;
						float my = 0;
						float mY = 0;
						float mI = 0;
						float mQ = 0;
						var num = 0;

						for (var ry = -rad; ry <= rad; ry++)
						{
							var y2 = yCenter + ry;

							if (y2 >= 0 && y2 < height)
							{
								for (var rx = -rad; rx <= rad; rx++)
								{
									var x2 = xCenter + rx;
									if (x2 >= 0 && x2 < width)
									{
										var l2 = pixelsf[x2, y2][0];
										var u2 = pixelsf[x2, y2][1];
										var v2 = pixelsf[x2, y2][2];

										var dYinner = lCenter - l2;
										var dIinner = uCenter - u2;
										var dQinner = vCenter - v2;

										if (dYinner * dYinner + dIinner * dIinner + dQinner * dQinner <= radCol2)
										{
											mx += x2;
											my += y2;
											mY += (float) l2;
											mI += (float) u2;
											mQ += (float) v2;
											num++;
										}
									}
								}
							}
						}

						var numResult = 1.0 / num;
						lCenter = mY * numResult;
						uCenter = mI * numResult;
						vCenter = mQ * numResult;
						xCenter = (int) (mx * numResult + 0.5);
						yCenter = (int) (my * numResult + 0.5);
						var dx = xCenter - xCenterOld;
						var dy = yCenter - yCenterOld;
						var dY = lCenter - lCenterOld;
						var dI = uCenter - uCenterOld;
						var dQ = vCenter - vCenterOld;

						shift = dx * dx + dy * dy + dY * dY + dI * dI + dQ * dQ;
						numOfIterations++;
					} while (shift > 1 && numOfIterations < 100);

					var ar = new[] { lCenter, uCenter, vCenter };
					dc[0] = lCenter;
						dc[1] = uCenter;
							dc[2] = vCenter;
					LuvToXyz();
					XyzToRgb();

					filteringData[i + 2] = bc[0];
					filteringData[i + 1] = bc[1];
					filteringData[i] = bc[2];
				}
			}

			return new RGBImage(image.Width, height, image.BytesPerLine, filteringData);
		}
		
		private static void RgbToXyz()
		{
			var red = (double) bc[0] / 255;
			var green = (double) bc[1] / 255;
			var blue = (double) bc[2] / 255;

			if (red > 0.04045)
			{
				red = Math.Pow((red + 0.055) / 1.055, 2.4);
			}
			else
			{
				red /= 12.92;
			}

			if (green > 0.04045)
			{
				green = Math.Pow((green + 0.055) / 1.055, 2.4);
			}
			else
			{
				green /= 12.92;
			}

			if (blue > 0.04045)
			{
				blue = Math.Pow((blue + 0.055) / 1.055, 2.4);
			}
			else
			{
				blue /= 12.92;
			}

			red *= 100;
			green *= 100;
			blue *= 100;

			dc[0] = red * 0.4124 + green * 0.3576 + blue * 0.1805;
			dc[1] = red * 0.2126 + green * 0.7152 + blue * 0.0722;
			dc[2] = red * 0.0193 + green * 0.1192 + blue * 0.9505;
		}

		private static void XyzToRgb()
		{
			var x = dc[0] / 100;
			var y = dc[1] / 100;
			var z = dc[2] / 100;

			var red = x * 3.2406 + y * -1.5372 + z * -0.4986;
			var green = x * -0.9689 + y * 1.8758 + z * 0.0415;
			var blue = x * 0.0557 + y * -0.2040 + z * 1.0570;

			if (red > 0.0031308)
			{
				red = 1.055 * Math.Pow(red, 1 / 2.4) - 0.055;
			}
			else
			{
				red = 12.92 * red;
			}

			if (green > 0.0031308)
			{
				green = 1.055 * Math.Pow(green, 1 / 2.4) - 0.055;
			}
			else
			{
				green = 12.92 * green;
			}

			if (blue > 0.0031308)
			{
				blue = 1.055 * Math.Pow(blue, 1 / 2.4) - 0.055;
			}
			else
			{
				blue = 12.92 * blue;
			}
			
			bc[0] = (byte) (red * 255);
			bc[1] = (byte) (green * 255);
			bc[2] = (byte) (blue * 255);
		}

		private static void XyzToLuv()
		{
			var l = dc[1] / 100;
			var u = 4 * dc[0] / (dc[0] + 15 * dc[1] + 3 * dc[2]);
			var v = 9 * dc[1] / (dc[0] + 15 * dc[1] + 3 * dc[2]);

			if (l > 0.008856)
			{
				l = Math.Pow(l, 1.0 / 3.0);
			}
			else
			{
				l = 7.787 * l + 16.0 / 116;
			}

			const double x = 95.047;
			const double y = 100.0;
			const double z = 108.883;

			var u2 = 4 * x / (x + 15 * y + 3 * z);
			var v2 = 9 * y / (x + 15 * y + 3 * z);

			dc[0] = 116 * l - 16;
			dc[1] = 13 * dc[0] * (u - u2);
			dc[2] = 13 * dc[0] * (v - v2);
		}

		private static void LuvToXyz()
		{
			var y = (dc[0] + 16) / 116;

			if (Math.Pow(y, 3) > 0.008856)
			{
				y = Math.Pow(y, 3);
			}
			else
			{
				y = (y - 16.0 / 116) / 7.787;
			}

			const double localX = 95.047;
			const double localY = 100.0;
			const double localZ = 108.883;

			const double localU = 4 * localX / (localX + 15 * localY + 3 * localZ);
			const double localV = 9 * localY / (localX + 15 * localY + 3 * localZ);

			var u = dc[1] / (13 * dc[0]) + localU;
			var v = dc[2] / (13 * dc[0]) + localV;

			dc[1] = y * 100;
			dc[0] = -(9 * dc[1] * u) / ((u - 4) * v - u * v);
			dc[2] = (9 * dc[1] - 15 * v * dc[1] - v * dc[0]) / (3 * v);

			//return xyz;
		}

		#endregion
	}
}