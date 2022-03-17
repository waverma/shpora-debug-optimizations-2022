// using System;
// using System.Linq;
//
// namespace JPEG.Images
// {
//     public class RgbPixel : Pixel
//     {
//         public RgbPixel(byte r, byte g, byte b)
//         {
//             R = r;
//             G = g;
//             B = b;
//             
//             Y = 16.0f + 0.256789063f * R + 0.504128906f * G + 0.094f * B;
//             Cb =  128.0f - 0.148222656f * R - 0.290992188f * G + 0.439214844f * B;
//             Cr = 128.0f + 0.439214844f * R - 0.367789063f * G - 0.0714257813f * B;
//         }
//     }
//     
//     public class YCbCrPixel : Pixel
//     {
//         public YCbCrPixel(float y, float cb, float cr)
//         {
//             Y = y;
//             Cb = cb;
//             Cr = cr;
//             
//             R = ToByte(1.16438281f * Y + 1.59602734f * Cr - 222.921f);
//             G =  ToByte(1.16438281f * Y - 0.391761719f * Cb - 0.81296875f * Cr + 135.576f);
//             B = ToByte(1.16438281f * Y + 2.01723438f * Cb - 276.836f);
//         }
//     }
//     
//     public abstract class Pixel
//     {
//         // public Pixel(float firstComponent, float secondComponent, float thirdComponent, PixelFormat pixelFormat)
//         // {
//         //     if (PixelFormat.RGB != pixelFormat && PixelFormat.YCbCr != pixelFormat)
//         //         throw new FormatException("Unknown pixel format: " + pixelFormat);
//         //     if (pixelFormat == PixelFormat.RGB)
//         //     {
//         //         R = firstComponent;
//         //         G = secondComponent;
//         //         B = thirdComponent;
//         //     }
//         //     if (pixelFormat == PixelFormat.YCbCr)
//         //     {
//         //         Y = firstComponent;
//         //         Cb = secondComponent;
//         //         Cr = thirdComponent;
//         //     }
//         //
//         //     FillOppositeComponents(pixelFormat);
//         // }
//         //
//         // private void FillOppositeComponents(PixelFormat pixelFormat)
//         // {
//         //     if (pixelFormat == PixelFormat.RGB)
//         //     {
//         //         Y = 16.0f + (65.738f * R + 129.057f * G + 24.064f * B) / 256.0f;
//         //         Cb =  128.0f + (-37.945f * R - 74.494f * G + 112.439f * B) / 256.0f;
//         //         Cr = 128.0f + (112.439f * R - 94.154f * G - 18.285f * B) / 256.0f;
//         //         return;
//         //     }
//         //     R = (298.082f * Y + 408.583f * Cr) / 256.0f - 222.921f;
//         //     G =  (298.082f * Y - 100.291f * Cb - 208.120f * Cr) / 256.0f + 135.576f;
//         //     B = (298.082f * Y + 516.412f * Cb) / 256.0f - 276.836f;
//         // }
//
//         public byte R { get; protected set; }
//         public byte G { get; protected set; }
//         public byte B { get; protected set; }
//
//         public float Y { get; protected set; }
//         public float Cb { get; protected set; }
//         public float Cr { get; protected set; }
//         
//         public static byte ToByte(float f)
//         {
//             var val = (int) f;
//             if (val > byte.MaxValue)
//                 return byte.MaxValue;
//             if (val < byte.MinValue)
//                 return byte.MinValue;
//             return (byte)val;
//         }
//     }
// }