
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
            this.ImagePathTextBox = new System.Windows.Forms.TextBox();
            this.LoadButton = new System.Windows.Forms.Button();
            this.SaveButton = new System.Windows.Forms.Button();
            this.PixelRatioLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.ImagePictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PixelRatioSlider)).BeginInit();
            this.SuspendLayout();
            // 
            // ImagePictureBox
            // 
            this.ImagePictureBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ImagePictureBox.Image = global::ImagePixelator.Properties.Resources.dragdropimageicon;
            this.ImagePictureBox.InitialImage = null;
            this.ImagePictureBox.Location = new System.Drawing.Point(40, 109);
            this.ImagePictureBox.Name = "ImagePictureBox";
            this.ImagePictureBox.Size = new System.Drawing.Size(500, 300);
            this.ImagePictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.ImagePictureBox.TabIndex = 0;
            this.ImagePictureBox.TabStop = false;
            this.ImagePictureBox.DragDrop += new System.Windows.Forms.DragEventHandler(this.ImagePictureBox_DragDrop);
            this.ImagePictureBox.DragEnter += new System.Windows.Forms.DragEventHandler(this.ImagePictureBox_DragEnter);
            // 
            // ExamineButton
            // 
            this.ExamineButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ExamineButton.Font = new System.Drawing.Font("Comic Sans MS", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.ExamineButton.Location = new System.Drawing.Point(465, 15);
            this.ExamineButton.Name = "ExamineButton";
            this.ExamineButton.Size = new System.Drawing.Size(80, 27);
            this.ExamineButton.TabIndex = 1;
            this.ExamineButton.Text = "Examinar";
            this.ExamineButton.UseVisualStyleBackColor = true;
            this.ExamineButton.Click += new System.EventHandler(this.OnExamineButtonClick);
            // 
            // PixelRatioSlider
            // 
            this.PixelRatioSlider.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PixelRatioSlider.Location = new System.Drawing.Point(40, 58);
            this.PixelRatioSlider.Maximum = 100;
            this.PixelRatioSlider.Name = "PixelRatioSlider";
            this.PixelRatioSlider.Size = new System.Drawing.Size(500, 45);
            this.PixelRatioSlider.TabIndex = 2;
            this.PixelRatioSlider.ValueChanged += new System.EventHandler(this.OnPixelRatioChanged);
            // 
            // ImagePathTextBox
            // 
            this.ImagePathTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ImagePathTextBox.Font = new System.Drawing.Font("Comic Sans MS", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.ImagePathTextBox.Location = new System.Drawing.Point(40, 15);
            this.ImagePathTextBox.Name = "ImagePathTextBox";
            this.ImagePathTextBox.PlaceholderText = "Introduce ruta de la imagen...";
            this.ImagePathTextBox.Size = new System.Drawing.Size(353, 26);
            this.ImagePathTextBox.TabIndex = 3;
            // 
            // LoadButton
            // 
            this.LoadButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.LoadButton.Font = new System.Drawing.Font("Comic Sans MS", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.LoadButton.Location = new System.Drawing.Point(399, 15);
            this.LoadButton.Name = "LoadButton";
            this.LoadButton.Size = new System.Drawing.Size(60, 27);
            this.LoadButton.TabIndex = 4;
            this.LoadButton.Text = "Cargar";
            this.LoadButton.UseVisualStyleBackColor = true;
            this.LoadButton.Click += new System.EventHandler(this.OnLoadButtonClick);
            // 
            // SaveButton
            // 
            this.SaveButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.SaveButton.Font = new System.Drawing.Font("Comic Sans MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.SaveButton.Location = new System.Drawing.Point(245, 459);
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.Size = new System.Drawing.Size(90, 40);
            this.SaveButton.TabIndex = 5;
            this.SaveButton.Text = "Guardar";
            this.SaveButton.UseVisualStyleBackColor = true;
            this.SaveButton.Click += new System.EventHandler(this.OnSaveButtonClick);
            // 
            // PixelRatioLabel
            // 
            this.PixelRatioLabel.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.PixelRatioLabel.AutoSize = true;
            this.PixelRatioLabel.Font = new System.Drawing.Font("Comic Sans MS", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.PixelRatioLabel.Location = new System.Drawing.Point(267, 417);
            this.PixelRatioLabel.Name = "PixelRatioLabel";
            this.PixelRatioLabel.Size = new System.Drawing.Size(33, 19);
            this.PixelRatioLabel.TabIndex = 6;
            this.PixelRatioLabel.Text = "1 : 1";
            this.PixelRatioLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // App
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 511);
            this.Controls.Add(this.PixelRatioLabel);
            this.Controls.Add(this.SaveButton);
            this.Controls.Add(this.LoadButton);
            this.Controls.Add(this.ImagePathTextBox);
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
        private System.Windows.Forms.TextBox ImagePathTextBox;
        private System.Windows.Forms.Button LoadButton;
        private System.Windows.Forms.Button SaveButton;
        private System.Windows.Forms.Label PixelRatioLabel;
    }
}

