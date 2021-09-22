using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

namespace ImagePixelator
{
    public class Pixelator
    {
        private object _locker = new object();
        private CancellationTokenSource _cancellationTokenSource;

        public string ImagePath { get; private init; }
        public Bitmap OriginalImage { get; private init; }
        public Bitmap ModifiedImage { get; private set; }
        public int Width { get; private init; }
        public int Height { get; private init; }

        public Pixelator(string imagePath)
        {
            ImagePath = imagePath;
            OriginalImage = new Bitmap(ImagePath);
            Width = OriginalImage.Width;
            Height = OriginalImage.Height;

            Clear();
        }


        public async Task PixelateAsync(double compression, CancellationToken cancellation)
        {
            int maxBlockSize = Math.Max(Width, Height);
            int pixelBlockSize = (int)Math.Ceiling(maxBlockSize * MathPlus.Clamp(compression, 0, 1));

            await PixelateAsync(pixelBlockSize, cancellation);
        }

        public async Task PixelateAsync(int pixelBlockSize, CancellationToken cancellation)
        {
            Bitmap image = new Bitmap(OriginalImage);

            if (pixelBlockSize > 1)
            {
                await Task.Run(() => Pixelate(image, pixelBlockSize, cancellation));
            }

            if (!cancellation.IsCancellationRequested)
            {
                ModifiedImage = image;
            }
        }

        public void Clear()
        {
            ModifiedImage = new Bitmap(OriginalImage);
        }

        private void Pixelate(Bitmap image, int blockSize, CancellationToken cancellationToken)
        {
            int offsetX = CalculateOffsetToCenter(blockSize, Width);
            int offsetY = CalculateOffsetToCenter(blockSize, Height);
            int x = offsetX;
            int y = offsetY;

            while (x < Width && !cancellationToken.IsCancellationRequested)
            {
                int width = MathPlus.Min(blockSize + x, Width - x, blockSize); //Hay offset, No hay offset
                x = Math.Max(0, x);

                while (y < Height && !cancellationToken.IsCancellationRequested)
                {
                    int height = MathPlus.Min(blockSize + y, Height - y, blockSize);
                    y = Math.Max(0, y);

                    Rectangle rect = new Rectangle(x, y, width, height);
                    PixelateBlock(image, rect);

                    y += height;
                }

                x += width;
                y = offsetY;
            }
        }

        private int CalculateOffsetToCenter(int blockSize, int maxSize)
        {
            blockSize = Math.Min(blockSize, maxSize);
            int overSpace = maxSize - (blockSize * (maxSize / blockSize));

            return overSpace == 0 ? 0 : -(blockSize - (int)Math.Ceiling(overSpace / 2.0));
        }

        private void PixelateBlock(Bitmap image, Rectangle rect)
        {
            var blockData = image.LockBits(rect, ImageLockMode.ReadWrite, image.PixelFormat);

            int bytes = Math.Abs(blockData.Stride) * (blockData.Height - 1) + blockData.Width * 4;
            byte[] rgbaValues = new byte[bytes];
            Marshal.Copy(blockData.Scan0, rgbaValues, 0, rgbaValues.Length);

            Color[] colors = GetColors(rgbaValues, blockData.Width, blockData.Height, blockData.Stride);
            Color color = GetDominantColor(colors);
            SetColor(color, rgbaValues, blockData.Width, blockData.Height, blockData.Stride);

            Marshal.Copy(rgbaValues, 0, blockData.Scan0, rgbaValues.Length);
            image.UnlockBits(blockData);
        }

        private Color[] GetColors(byte[] rgbaValues, int width, int height, int stride)
        {
            List<Color> colors = new List<Color>(width * height);

            int offset = 0;

            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    int index = offset + j * 4;
                    Color color = Color.FromArgb(rgbaValues[index], rgbaValues[index + 1], rgbaValues[index + 2], rgbaValues[index + 3]);
                    colors.Add(color);
                }

                offset += stride;
            }

            return colors.ToArray();
        }

        private Color GetDominantColor(Color[] colors)
        {
            var colorsCounter = new Dictionary<Color, int>();
            Color dominantColor = Color.White;
            int dominantRepetitions = 0;

            foreach (Color color in colors)
            {
                int repetitions = 1;

                if (colorsCounter.ContainsKey(color))
                {
                    repetitions = ++colorsCounter[color];
                }
                else
                {
                    colorsCounter.Add(color, 1);
                }

                if (repetitions > dominantRepetitions)
                {
                    dominantColor = color;
                    dominantRepetitions = repetitions;
                }
            }

            return dominantColor;
        }

        private void SetColor(Color color, byte[] rgbaValues, int width, int height, int stride)
        {
            int offset = 0;

            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    int index = offset + j * 4;

                    rgbaValues[index] = color.A;
                    rgbaValues[index + 1] = color.R;
                    rgbaValues[index + 2] = color.G;
                    rgbaValues[index + 3] = color.B;
                }

                offset += stride;
            }
        }



        /*
        private void PixelateBlock(Bitmap image, Rectangle rect)
        {
            var blockData = image.LockBits(rect, ImageLockMode.ReadWrite, image.PixelFormat);
            Debug.WriteLine($"A {rect.Width * rect.Height * 4} | {blockData.Stride * blockData.Height}");

            if (blockData.Height == 1)
            {
                var t = 0;
            }

            int bytes = Math.Abs(blockData.Stride) * blockData.Height;
            byte[] rgbValues = new byte[bytes];

            Marshal.Copy(blockData.Scan0, rgbValues, 0, rgbValues.Length); //Se sale del limite de pixeles
            int bytesPerPixel = Bitmap.GetPixelFormatSize(image.PixelFormat);
            try
            {
                //Color color = GetDominantColor(blockData);
                //SetColor(blockData, color);
            }
            finally
            {
                image.UnlockBits(blockData);
            }

            /*unsafe
            {
                var blockData = image.LockBits(rect, ImageLockMode.ReadWrite, image.PixelFormat);

                int bytesPerPixel = System.Drawing.Bitmap.GetPixelFormatSize(processedBitmap.PixelFormat) / 8;
                int heightInPixels = bitmapData.Height;
                int widthInBytes = bitmapData.Width * bytesPerPixel;
                byte* PtrFirstPixel = (byte*)bitmapData.Scan0;

                Parallel.For(0, heightInPixels, y =>
                {
                    byte* currentLine = PtrFirstPixel + (y * bitmapData.Stride);
                    for (int x = 0; x < widthInBytes; x = x + bytesPerPixel)
                    {
                        int oldBlue = currentLine[x];
                        int oldGreen = currentLine[x + 1];
                        int oldRed = currentLine[x + 2];

                        currentLine[x] = (byte)oldBlue;
                        currentLine[x + 1] = (byte)oldGreen;
                        currentLine[x + 2] = (byte)oldRed;
                    }
                });
                processedBitmap.UnlockBits(bitmapData);
            }*/

        /*blockData = image.LockBits(rect, ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);
        //Debug.WriteLine($"B {rect.Width * rect.Height * 4} | {blockData.Stride * blockData.Height}");
        SetColor(blockData, color);
        image.UnlockBits(blockData);*/
    /*}

        private Color GetDominantColor(BitmapData bitmapData)
        {
            var colors = new Dictionary<Color, int>();
            Color dominantColor = Color.White;
            int dominantRepetitions = 0;

            foreach (Color color in GetColors(bitmapData))
            {
                int repetitions = 1;

                if (colors.ContainsKey(color))
                {
                    repetitions = ++colors[color];
                }
                else
                {
                    colors.Add(color, 1);
                }

                if (repetitions > dominantRepetitions)
                {
                    dominantColor = color;
                    dominantRepetitions = repetitions;
                }
            }

            return dominantColor;
        }

        private Color[] GetColors(BitmapData bitmapData)
        {
            List<Color> colors = new List<Color>();
            int bytes = Math.Abs(bitmapData.Stride) * bitmapData.Height;
            byte[] rgbValues = new byte[bytes];

            Marshal.Copy(bitmapData.Scan0, rgbValues, 0, bytes);

            int offset = 0;

            for(int i = 0; i < bitmapData.Height; i++)
            {
                for (int j = 0; j < bitmapData.Width; j++)
                {
                    int index = offset + j * 4;
                    Color color = Color.FromArgb(rgbValues[index], rgbValues[index + 1], rgbValues[index + 2], rgbValues[index + 3]);
                    colors.Add(color);
                }

                offset += Math.Abs(bitmapData.Stride);
            }
            /*for (int i = 0; i < rgbValues.Length; i += 4)
            {
                Color color = Color.FromArgb(rgbValues[i], rgbValues[i + 1], rgbValues[i + 2], rgbValues[i + 3]);
                colors.Add(color);
            }*/

         /*   return colors.ToArray();
        }

        //Sacado a partir de https://social.msdn.microsoft.com/Forums/vstudio/en-US/7892deff-affa-4ec4-b161-6a8259cb7308/bitmap-lockbits-results-in-wrong-stride?forum=csharpgeneral
        private void SetColor(BitmapData bitmapData, Color color)
        {
            IntPtr scan0 = bitmapData.Scan0;
            byte[] rgbValues = new byte[bitmapData.Width * 4];

            for (int i = 0; i < bitmapData.Height; i++)
            {
                for(int j = 0; j < bitmapData.Width; j++)
                {
                    int index = j * 4;

                    rgbValues[index] = color.A;
                    rgbValues[index + 1] = color.R;
                    rgbValues[index + 2] = color.G;
                    rgbValues[index + 3] = color.B;
                }

                Marshal.Copy(rgbValues, 0, scan0, rgbValues.Length);
                scan0 += bitmapData.Stride;
            }
        }

        /*
        private void A(int blockSize, CancellationToken cancellationToken)
        {
            int offsetX = CalculateOffsetToCenter(blockSize, Width);
            int offsetY = CalculateOffsetToCenter(blockSize, Height);
            int x = offsetX;
            int y = offsetY;

            var tasks = new List<Task>();

            while (x < Width && !cancellationToken.IsCancellationRequested)
            {
                int xPos = x;
                int yPos = y;
                var task = Task.Run(() => PixelateBlock(xPos, yPos, blockSize, default), cancellationToken);
                tasks.Add(task);
                //PixelateBlock(x, y, blockSize, cancellationToken);

                //Task.Delay(200).Wait();

                y += blockSize;

                if (y >= Height)
                {
                    y = offsetY;
                    x += blockSize;
                }
                
            }

            //Task.Delay(2000).Wait();
            Task.WaitAll(tasks.ToArray());
        }

        private int CalculateOffsetToCenter(int blockSize, int maxSize)
        {
            return -(int)Math.Ceiling((maxSize - (blockSize * (maxSize / blockSize))) / 1.0);
        }

        private void PixelateBlock(int x, int y, int size, CancellationToken cancellationToken)
        {
            int width = MathPlus.Min(size + x, Width - x, size); //Hay offset, No hay offset
            int height = MathPlus.Min(size + y, Height - y, size);
            x = Math.Max(0, x);
            y = Math.Max(0, y);
            Color color = CalculateDominantColor(x, y, width, height);

            //Debug.WriteLine($"{x}, {y}");

            for (int i = 0; i < width; i++)
                for (int j = 0; j < height; j++)
                {
                    //if (cancellationToken.IsCancellationRequested) return;

                    lock (_locker) ModifiedImage.SetPixel(x + i, y + j, color);
                    
                }
        }

        private Color CalculateDominantColor(int x, int y, int width, int height)
        {
            var colors = new Dictionary<Color, int>();
            Color dominantColor = Color.White;
            int dominantRepetitions = 0;

            for (int i = 0; i < width; i++)
                for (int j = 0; j < height; j++)
                {
                    Color color = OriginalImageColors[x + i, y + j];
                    int repetitions = 1;

                    if (colors.ContainsKey(color))
                    {
                        repetitions = ++colors[color];
                    }
                    else
                    {
                        colors.Add(color, 1);
                    }

                    if (repetitions > dominantRepetitions)
                    {
                        dominantColor = color;
                        dominantRepetitions = repetitions;
                    }
                }

            return dominantColor;
        }

        private void A(int blockSize, CancellationToken cancellationToken)
        {
            int offsetX = CalculateOffsetToCenter(blockSize, Width);
            int offsetY = CalculateOffsetToCenter(blockSize, Height);
            int x = offsetX;
            int y = offsetY;

            var tasks = new List<Task>();

            while (x < Width && !cancellationToken.IsCancellationRequested)
            {
                int xPos = x;
                int yPos = y;
                var task = Task.Run(() => PixelateBlock(xPos, yPos, blockSize, default), cancellationToken);
                tasks.Add(task);
                //PixelateBlock(x, y, blockSize, cancellationToken);

                //Task.Delay(200).Wait();

                y += blockSize;

                if (y >= Height)
                {
                    y = offsetY;
                    x += blockSize;
                }

            }

            //Task.Delay(2000).Wait();
            Task.WaitAll(tasks.ToArray());
        }


        /*private void PixelateBlock(int x, int y, int size, CancellationToken cancellationToken)
        {
            int width = Math.Min(Width - (x + size), size);
            int height = Math.Min(Height - (y + size), size);
            Color color = CalculateDominantColor(x, y, width, height);

            BitmapData data;

            lock (ModifiedImage)
            {
                var rect = new Rectangle(x, y, width, height);
                data = ModifiedImage.LockBits(rect, ImageLockMode.ReadWrite, ModifiedImage.PixelFormat);
            }

            data.

            /*for (int i = 0; i < width; i++)
                for (int j = 0; j < height; j++)
                {
                    if (cancellationToken.IsCancellationRequested) return;

                    lock (ModifiedImage)
                    {
                        var rect = new Rectangle(0, 0, bmp.Width, bmp.Height);
                        ModifiedImage.LockBits();
                    }
                        ModifiedImage.SetPixel(x + i, y + j, Color.White);
                }
        }*/
    }
}
