using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace ImagePixelator
{
    public partial class App : Form
    {
        private Pixelator _pixelator;
        private CancellationTokenSource _pixelateCancellationSource;

        public App()
        {
            InitializeComponent();

            /*PixelRatioSlider.Enabled = false;
            SaveButton.Enabled = false;
            ImagePictureBox.AllowDrop = true;

            SetPixelRatio(0);*/
        }
        /*
        private void SetPixelRatio(int value)
        {
            PixelRatioSlider.Value = value;
            PixelRatioLabel.Text = $"1 : {Math.Max(1, value)}";
        }

        private void OnLoadButtonClick(object sender, EventArgs e)
        {
            string path = ImagePathTextBox.Text;

            LoadImage(path);
        }

        private void OnExamineButtonClick(object sender, EventArgs e)
        {
            using (var openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Imagen|*.jpg;*.png;*.ico;*.jpeg";
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    LoadImage(openFileDialog.FileName);
                }
            }
        }

        private void LoadImage(string imagePath)
        {
            try
            {
                ImagePathTextBox.Text = imagePath;
                _pixelator = new Pixelator(imagePath);
                PixelRatioSlider.Maximum = Math.Max(_pixelator.Width, _pixelator.Height);
                SetPixelRatio(0);
                ImagePictureBox.Image = _pixelator.ModifiedImage;

                PixelRatioSlider.Enabled = true;
                SaveButton.Enabled = true;
            }
            catch
            {
                MessageBox.Show(this, "No se ha podido cargar la imagen", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void Pixelate()
        {
            _pixelateCancellationSource?.Cancel();
            _pixelateCancellationSource = new CancellationTokenSource();

            int blockSize = PixelRatioSlider.Value;
            await _pixelator.PixelateAsync(blockSize, _pixelateCancellationSource.Token);

            if (!_pixelateCancellationSource.IsCancellationRequested)
            {
                ImagePictureBox.Image = _pixelator.ModifiedImage;
            }
        }

        private void OnPixelRatioChanged(object sender, EventArgs e)
        {
            SetPixelRatio(PixelRatioSlider.Value);
            Pixelate();
        }

        private void OnSaveButtonClick(object sender, EventArgs e)
        {
            using (var saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "Imagen|*.png";
                saveFileDialog.RestoreDirectory = true;

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    ImagePictureBox.Image.Save(saveFileDialog.FileName, ImageFormat.Png);
                }
            }
        }

        private void ImagePictureBox_DragDrop(object sender, DragEventArgs e)
        {
            string path = ((string[])e.Data.GetData(DataFormats.FileDrop)).FirstOrDefault();

            LoadImage(path);
        }

        private void ImagePictureBox_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }*/
    }
}
