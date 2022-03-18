namespace JPEG
{
    public class ZigZager
    {
        private static long Pointer2;
        public static unsafe void ZigZagUnScan(byte[] quantizedBytes, byte[,] list)
        {
            fixed (byte* cursor = quantizedBytes)
            {
                byte* target = cursor;
                target += Pointer2;
                list[0, 0] = *(target++); 
                list[0, 1] = *(target++); 
                list[1, 0] = *(target++); 
                list[2, 0] = *(target++); 
                list[1, 1] = *(target++); 
                list[0, 2] = *(target++); 
                list[0, 3] = *(target++); 
                list[1, 2] = *(target++);
                list[2, 1] = *(target++); 
                list[3, 0] = *(target++); 
                list[4, 0] = *(target++); 
                list[3, 1] = *(target++); 
                list[2, 2] = *(target++); 
                list[1, 3] = *(target++); 
                list[0, 4] = *(target++); 
                list[0, 5] = *(target++);
                list[1, 4] = *(target++); 
                list[2, 3] = *(target++); 
                list[3, 2] = *(target++); 
                list[4, 1] = *(target++); 
                list[5, 0] = *(target++); 
                list[6, 0] = *(target++); 
                list[5, 1] = *(target++); 
                list[4, 2] = *(target++);
                list[3, 3] = *(target++); 
                list[2, 4] = *(target++); 
                list[1, 5] = *(target++); 
                list[0, 6] = *(target++); 
                list[0, 7] = *(target++); 
                list[1, 6] = *(target++); 
                list[2, 5] = *(target++); 
                list[3, 4] = *(target++);
                list[4, 3] = *(target++); 
                list[5, 2] = *(target++); 
                list[6, 1] = *(target++); 
                list[7, 0] = *(target++); 
                list[7, 1] = *(target++); 
                list[6, 2] = *(target++); 
                list[5, 3] = *(target++); 
                list[4, 4] = *(target++);
                list[3, 5] = *(target++); 
                list[2, 6] = *(target++);
                list[1, 7] = *(target++); 
                list[2, 7] = *(target++); 
                list[3, 6] = *(target++); 
                list[4, 5] = *(target++); 
                list[5, 4] = *(target++); 
                list[6, 3] = *(target++);
                list[7, 2] = *(target++); 
                list[7, 3] = *(target++); 
                list[6, 4] = *(target++); 
                list[5, 5] = *(target++); 
                list[4, 6] = *(target++); 
                list[3, 7] = *(target++); 
                list[4, 7] = *(target++); 
                list[5, 6] = *(target++);
                list[6, 5] = *(target++); 
                list[7, 4] = *(target++); 
                list[7, 5] = *(target++); 
                list[6, 6] = *(target++); 
                list[5, 7] = *(target++); 
                list[6, 7] = *(target++); 
                list[7, 6] = *(target++); 
                list[7, 7] = *(target);

                Pointer += 8 * 8;
            }
        }
        
        private static long Pointer;
        public static unsafe void FillZigZagScan(byte[,] channelFreqs, byte[] list)
        {
            fixed (byte* cursor = list)
            {
                byte* target = cursor;
                target += Pointer;
                *(target++) = channelFreqs[0, 0]; 
                *(target++) = channelFreqs[0, 1]; 
                *(target++) = channelFreqs[1, 0]; 
                *(target++) = channelFreqs[2, 0]; 
                *(target++) = channelFreqs[1, 1]; 
                *(target++) = channelFreqs[0, 2]; 
                *(target++) = channelFreqs[0, 3]; 
                *(target++) = channelFreqs[1, 2];
                *(target++) = channelFreqs[2, 1]; 
                *(target++) = channelFreqs[3, 0]; 
                *(target++) = channelFreqs[4, 0]; 
                *(target++) = channelFreqs[3, 1]; 
                *(target++) = channelFreqs[2, 2]; 
                *(target++) = channelFreqs[1, 3]; 
                *(target++) = channelFreqs[0, 4]; 
                *(target++) = channelFreqs[0, 5];
                *(target++) = channelFreqs[1, 4]; 
                *(target++) = channelFreqs[2, 3]; 
                *(target++) = channelFreqs[3, 2]; 
                *(target++) = channelFreqs[4, 1]; 
                *(target++) = channelFreqs[5, 0]; 
                *(target++) = channelFreqs[6, 0]; 
                *(target++) = channelFreqs[5, 1]; 
                *(target++) = channelFreqs[4, 2];
                *(target++) = channelFreqs[3, 3]; 
                *(target++) = channelFreqs[2, 4]; 
                *(target++) = channelFreqs[1, 5]; 
                *(target++) = channelFreqs[0, 6]; 
                *(target++) = channelFreqs[0, 7]; 
                *(target++) = channelFreqs[1, 6]; 
                *(target++) = channelFreqs[2, 5]; 
                *(target++) = channelFreqs[3, 4];
                *(target++) = channelFreqs[4, 3]; 
                *(target++) = channelFreqs[5, 2]; 
                *(target++) = channelFreqs[6, 1]; 
                *(target++) = channelFreqs[7, 0]; 
                *(target++) = channelFreqs[7, 1]; 
                *(target++) = channelFreqs[6, 2]; 
                *(target++) = channelFreqs[5, 3]; 
                *(target++) = channelFreqs[4, 4];
                *(target++) = channelFreqs[3, 5]; 
                *(target++) = channelFreqs[2, 6];
                *(target++) = channelFreqs[1, 7]; 
                *(target++) = channelFreqs[2, 7]; 
                *(target++) = channelFreqs[3, 6]; 
                *(target++) = channelFreqs[4, 5]; 
                *(target++) = channelFreqs[5, 4]; 
                *(target++) = channelFreqs[6, 3];
                *(target++) = channelFreqs[7, 2]; 
                *(target++) = channelFreqs[7, 3]; 
                *(target++) = channelFreqs[6, 4]; 
                *(target++) = channelFreqs[5, 5]; 
                *(target++) = channelFreqs[4, 6]; 
                *(target++) = channelFreqs[3, 7]; 
                *(target++) = channelFreqs[4, 7]; 
                *(target++) = channelFreqs[5, 6];
                *(target++) = channelFreqs[6, 5]; 
                *(target++) = channelFreqs[7, 4]; 
                *(target++) = channelFreqs[7, 5]; 
                *(target++) = channelFreqs[6, 6]; 
                *(target++) = channelFreqs[5, 7]; 
                *(target++) = channelFreqs[6, 7]; 
                *(target++) = channelFreqs[7, 6]; 
                *(target) = channelFreqs[7, 7];

                Pointer += 8 * 8;
            }
        }
    }
}