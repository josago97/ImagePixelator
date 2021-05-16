using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;

namespace ImagePixelator
{
    public class Pixelator
    {

        private CancellationTokenSource _cancellationTokenSource;

        public string ImagePath { get; private set; }
        public Bitmap OriginalImage { get; private set; }
        public Bitmap ModifiedImage { get; private set; }

        public Pixelator(string imagePath)
        {
            ImagePath = imagePath;
            OriginalImage = new Bitmap(ImagePath);
            ModifiedImage = (Bitmap)OriginalImage.Clone();
        }

        public void Pixelate(double compression)
        {
            Stop();

            compression = MathPlus.Clamp(compression, 0, 1);

            int maxBlockSize = Math.Max(OriginalImage.Width, OriginalImage.Height);
            int pixelBlockSize = (int)MathPlus.Lerp(1, maxBlockSize, compression);

            if (pixelBlockSize == 1)
            {
                ModifiedImage = (Bitmap)OriginalImage.Clone();
            }
            else
            {
                _cancellationTokenSource = new CancellationTokenSource();
                CancellationToken cancellationToken = _cancellationTokenSource.Token;

                A(pixelBlockSize, cancellationToken);
            }
        }

        public void Stop()
        {
            _cancellationTokenSource?.Cancel();
        }

        private void A(int blockSize,  CancellationToken cancellationToken)
        {
            int x = 0;
            int y = 0;
            int width = OriginalImage.Width;
            int height = OriginalImage.Height;

            while (x < width && !cancellationToken.IsCancellationRequested)
            {
                PixelateBlock(x, y, blockSize, cancellationToken);
        
                y += blockSize;

                if (y >= height)
                {
                    y = 0;
                    x += blockSize;
                }
            }
        }

        private void PixelateBlock(int x, int y, int size, CancellationToken cancellationToken)
        {
            var color = CalculateDominantColor(x, y, size, size);

            for (int i = 0; i < size; i++)
                for (int j = 0; j < size; j++)
                {
                    if (cancellationToken.IsCancellationRequested) return;

                    ModifiedImage.SetPixel(x + i, y + j, color);
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
                    Color color = OriginalImage.GetPixel(x + i, y + j);
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
    }
}
