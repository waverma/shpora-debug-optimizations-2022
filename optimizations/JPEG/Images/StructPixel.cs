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
            
            Y = 16.0f + 0.256789063f * r + 0.504128906f * g + 0.094f * b;
            Cb =  128.0f - 0.148222656f * r - 0.290992188f * g + 0.439214844f * b;
            Cr = 128.0f + 0.439214844f * r - 0.367789063f * g - 0.0714257813f * b;
        }
        
        public StructPixel(float y, float cb, float cr)
        {
            Y = y;
            Cb = cb;
            Cr = cr;
            
            R = ToByte(1.16438281f * y + 1.59602734f * cr - 222.921f);
            G =  ToByte(1.16438281f * y - 0.391761719f * cb - 0.81296875f * cr + 135.576f);
            B = ToByte(1.16438281f * y + 2.01723438f * cb - 276.836f);
        }

        private static byte ToByte(float f)
        {
            var val = (int) f;
            if (val > byte.MaxValue)
                return byte.MaxValue;
            return val < byte.MinValue ? byte.MinValue : (byte)val;
        }
    }
}