namespace EvaluationSystem.ViewForm
{
    partial class MapForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.mapPicBox = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.mapPicBox)).BeginInit();
            this.SuspendLayout();
            // 
            // mapPicBox
            // 
            this.mapPicBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mapPicBox.Location = new System.Drawing.Point(0, 0);
            this.mapPicBox.Name = "mapPicBox";
            this.mapPicBox.Size = new System.Drawing.Size(1106, 751);
            this.mapPicBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.mapPicBox.TabIndex = 0;
            this.mapPicBox.TabStop = false;
            this.mapPicBox.MouseClick += new System.Windows.Forms.MouseEventHandler(this.mapPicBox_MouseClick);
            // 
            // MapForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1106, 751);
            this.Controls.Add(this.mapPicBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "MapForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "专题图";
            ((System.ComponentModel.ISupportInitialize)(this.mapPicBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox mapPicBox;
    }
}