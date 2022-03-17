using System;
using System.Linq;

namespace JPEG.Utilities
{
    public static class MathEx
    {
        public static float Sum(byte from, byte to, Func<int, float> function)
        {
            var sum = 0f;
            
            for (int i = from; i < to; i++)
                sum += function(i);
            
            return sum;
            // return Enumerable.Range(@from, to - @from).Sum(function);
        }

        public static float SumByTwoVariables(byte from1, byte to1, byte from2, byte to2, Func<byte, byte, float> function)
        {
            return Sum(from1, to1, x => Sum(from2, to2, y => function((byte) x, (byte) y)));
        }

        public static float LoopByTwoVariables(byte from1, byte to1, byte from2, byte to2, Action<byte, byte> function)
            => Sum(from1, to1, x => Sum(from2, to2, y =>
            {
                function((byte)x, (byte)y);
                return 0f;
            }));
    }
}