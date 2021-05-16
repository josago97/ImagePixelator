using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImagePixelator
{
    public partial class App : Form
    {
        private Pixelator _pixelator;

        public App()
        {
            InitializeComponent();
        }

        private void OnExamineButtonClick(object sender, EventArgs e)
        {
            string ruta = string.Empty;
            using (var openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = ruta;
                openFileDialog.Filter = "Imagen|*.jpg;*.png;*.ico:*.jpeg";
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    LoadImage(openFileDialog.FileName);
                }
            }
        }

        private void LoadImage(string imagePath)
        {
            _pixelator = new Pixelator(imagePath);
            _pixelator.Pixelate(0.25);
            imagePictureBox.Image = _pixelator.ModifiedImage;
        }
    }
}
