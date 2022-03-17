using System;

namespace JPEG.Images
{
    public readonly struct StructPixel
    {
        public byte R { get; }
        public byte G { get; }
        public byte B { get; }
        
        public float Y { get; }
        public float Cb { get; }
        public float Cr { get; }

        public StructPixel(byte r, byte g, byte b)
        {
            R = r;
            G = g;
            B = b;
            
            Y = 16.0f + 0.256789063f * R + 0.504128906f * G + 0.094f * B;
            Cb =  128.0f - 0.148222656f * R - 0.290992188f * G + 0.439214844f * B;
            Cr = 128.0f + 0.439214844f * R - 0.367789063f * G - 0.0714257813f * B;
        }
        
        public StructPixel(float y, float cb, float cr)
        {
            Y = y;
            Cb = cb;
            Cr = cr;
            
            R = ToByte(1.16438281f * Y + 1.59602734f * Cr - 222.921f);
            G =  ToByte(1.16438281f * Y - 0.391761719f * Cb - 0.81296875f * Cr + 135.576f);
            B = ToByte(1.16438281f * Y + 2.01723438f * Cb - 276.836f);
        }

        private static byte ToByte(float f)
        {
            var val = (int) f;
            if (val > byte.MaxValue)
                return byte.MaxValue;
            if (val < byte.MinValue)
                return byte.MinValue;
            return (byte)val;
        }
    }
}