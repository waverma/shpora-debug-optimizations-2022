using JPEG.Images;

namespace JPEG
{
    public static class BufferManager
    {
        public static StructPixel[,] Pixels { get; set; }
        
        public static void Setup(int w, int h, int matrixSize)
        {
            Pixels = new StructPixel[h, w];
        }
    }
}