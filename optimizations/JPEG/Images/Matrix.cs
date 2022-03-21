using System.Drawing;
using System.Drawing.Imaging;

namespace JPEG.Images
{
    public struct Matrix
    {
        public readonly StructPixel[,] Pixels;
        public readonly int Height;
        public readonly int Width;
        
        public Matrix(int height, int width)
        {
            Height = height;
            Width = width;
            if (BufferManager.Pixels.GetLength(0) != height || BufferManager.Pixels.GetLength(1) != width) 
                BufferManager.Setup(width, height, 0);
            Pixels = BufferManager.Pixels;
        }

        public static explicit operator Matrix(Bitmap bmp)
        {
            var height = bmp.Height - bmp.Height % 8;
            var width = bmp.Width - bmp.Width % 8;
            var matrix = new Matrix(height, width);
            
            var data = bmp.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.ReadWrite, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
            unsafe
            {
                byte* curpos;
                for (int h = 0; h < height; h++)
                {
                    curpos = ((byte*)data.Scan0) + h * data.Stride;
                    for (int w = 0; w < width; w++)
                    {
                        var a1  = *(curpos++);
                        var a2  = *(curpos++);
                        var a3  = *(curpos++);
                        
                        matrix.Pixels[h, w] = new StructPixel(a3, a2, a1);
                    }
                }
            }
            bmp.UnlockBits(data);

            return matrix;
        }

        public static explicit operator Bitmap(Matrix matrix)
        {
            var bmp = new Bitmap(matrix.Width, matrix.Height);

            var data = bmp.LockBits(new Rectangle(0, 0, matrix.Width, matrix.Height), ImageLockMode.ReadWrite, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
            unsafe
            {
                byte* curpos;
                for (int h = 0; h < matrix.Height; h++)
                {
                    curpos = ((byte*)data.Scan0) + h * data.Stride;
                    for (int w = 0; w < matrix.Width; w++)
                    {
                        var pixel = matrix.Pixels[h, w];
                        *(curpos++) = pixel.B;
                        *(curpos++) = pixel.G;
                        *(curpos++) = pixel.R;
                    }
                }
            }
            bmp.UnlockBits(data);

            return bmp;
        }
    }
}