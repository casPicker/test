namespace EvaluationSystem.ViewForm
{
    partial class ChartForm
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
            this.chartPicBox = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.chartPicBox)).BeginInit();
            this.SuspendLayout();
            // 
            // chartPicBox
            // 
            this.chartPicBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chartPicBox.Location = new System.Drawing.Point(0, 0);
            this.chartPicBox.Name = "chartPicBox";
            this.chartPicBox.Size = new System.Drawing.Size(1282, 775);
            this.chartPicBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.chartPicBox.TabIndex = 0;
            this.chartPicBox.TabStop = false;
            // 
            // ChartForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1282, 775);
            this.Controls.Add(this.chartPicBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "ChartForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "图表";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)(this.chartPicBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox chartPicBox;

    }
}