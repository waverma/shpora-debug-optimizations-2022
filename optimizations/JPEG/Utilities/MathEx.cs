// using System;
// using System.Linq;
//
// namespace JPEG.Utilities
// {
//     public static class MathEx
//     {
//         public static float SumByTwoVariables(byte from1, byte to1, byte from2, byte to2, Func<byte, byte, float> function)
//         {
//             var sum = 0f;
//             
//             for (byte x = from1; x < to1; x++)
//             {
//                 for (byte y = from2; y < to2; y++)
//                 {
//                     sum += function(x, y);
//                 }
//             }
//
//             return sum;
//         }
//
//         public static float LoopByTwoVariables(byte from1, byte to1, byte from2, byte to2, Action<byte, byte> function)
//         {
//             for (byte x = from1; x < to1; x++)
//             {
//                 for (byte y = from2; y < to2; y++)
//                 {
//                     function(x, y);
//                 }
//             }
//             return 0f;
//         }
//     }
// }