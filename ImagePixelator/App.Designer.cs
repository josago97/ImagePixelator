
namespace ImagePixelator
{
    partial class App
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(App));
            this.ImagePictureBox = new System.Windows.Forms.PictureBox();
            this.ExamineButton = new System.Windows.Forms.Button();
            this.PixelRatioSlider = new System.Windows.Forms.TrackBar();
            ((System.ComponentModel.ISupportInitialize)(this.ImagePictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PixelRatioSlider)).BeginInit();
            this.SuspendLayout();
            // 
            // ImagePictureBox
            // 
            this.ImagePictureBox.Location = new System.Drawing.Point(40, 130);
            this.ImagePictureBox.Name = "ImagePictureBox";
            this.ImagePictureBox.Size = new System.Drawing.Size(500, 300);
            this.ImagePictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.ImagePictureBox.TabIndex = 0;
            this.ImagePictureBox.TabStop = false;
            // 
            // ExamineButton
            // 
            this.ExamineButton.Location = new System.Drawing.Point(465, 32);
            this.ExamineButton.Name = "ExamineButton";
            this.ExamineButton.Size = new System.Drawing.Size(75, 23);
            this.ExamineButton.TabIndex = 1;
            this.ExamineButton.Text = "Examinar";
            this.ExamineButton.UseVisualStyleBackColor = true;
            // 
            // PixelRatioSlider
            // 
            this.PixelRatioSlider.Location = new System.Drawing.Point(40, 79);
            this.PixelRatioSlider.Maximum = 100;
            this.PixelRatioSlider.Name = "PixelRatioSlider";
            this.PixelRatioSlider.Size = new System.Drawing.Size(500, 45);
            this.PixelRatioSlider.TabIndex = 2;
            // 
            // App
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(584, 511);
            this.Controls.Add(this.PixelRatioSlider);
            this.Controls.Add(this.ExamineButton);
            this.Controls.Add(this.ImagePictureBox);
            this.Font = new System.Drawing.Font("Comic Sans MS", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "App";
            this.Text = "Pixelador de imágenes";
            ((System.ComponentModel.ISupportInitialize)(this.ImagePictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PixelRatioSlider)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button ExamineButton;
        private System.Windows.Forms.PictureBox ImagePictureBox;
        private System.Windows.Forms.TrackBar PixelRatioSlider;
    }
}

