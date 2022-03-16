using System;
using System.Linq;

namespace JPEG.Images
{
    public class Pixel
    {
        public Pixel(float firstComponent, float secondComponent, float thirdComponent, PixelFormat pixelFormat)
        {
            if (!new[]{PixelFormat.RGB, PixelFormat.YCbCr}.Contains(pixelFormat))
                throw new FormatException("Unknown pixel format: " + pixelFormat);
            if (pixelFormat == PixelFormat.RGB)
            {
                R = firstComponent;
                G = secondComponent;
                B = thirdComponent;
            }
            if (pixelFormat == PixelFormat.YCbCr)
            {
                Y = firstComponent;
                Cb = secondComponent;
                Cr = thirdComponent;
            }

            FillOppositeComponents(pixelFormat);
        }

        private void FillOppositeComponents(PixelFormat pixelFormat)
        {
            if (pixelFormat == PixelFormat.RGB)
            {
                Y = 16.0f + (65.738f * R + 129.057f * G + 24.064f * B) / 256.0f;
                Cb =  128.0f + (-37.945f * R - 74.494f * G + 112.439f * B) / 256.0f;
                Cr = 128.0f + (112.439f * R - 94.154f * G - 18.285f * B) / 256.0f;
                return;
            }
            R = (298.082f * Y + 408.583f * Cr) / 256.0f - 222.921f;
            G =  (298.082f * Y - 100.291f * Cb - 208.120f * Cr) / 256.0f + 135.576f;
            B = (298.082f * Y + 516.412f * Cb) / 256.0f - 276.836f;
        }

        public float R { get; private set; }
        public float G { get; private set; }
        public float B { get; private set; }

        public float Y { get; private set; }
        public float Cb { get; private set; }
        public float Cr { get; private set; }
    }
}