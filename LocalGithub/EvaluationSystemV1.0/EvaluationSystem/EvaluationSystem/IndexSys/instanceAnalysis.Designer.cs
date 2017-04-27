namespace EvaluationSystem.IndexSys
{
    partial class instanceAnalysis
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.chartType = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.rbtnBar = new System.Windows.Forms.RadioButton();
            this.rbtnPie = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.chartType)).BeginInit();
            this.SuspendLayout();
            // 
            // chartType
            // 
            chartArea2.Name = "ChartArea1";
            this.chartType.ChartAreas.Add(chartArea2);
            legend2.Name = "Legend1";
            this.chartType.Legends.Add(legend2);
            this.chartType.Location = new System.Drawing.Point(12, 34);
            this.chartType.Name = "chartType";
            series2.ChartArea = "ChartArea1";
            series2.Legend = "Legend1";
            series2.Name = "Series1";
            this.chartType.Series.Add(series2);
            this.chartType.Size = new System.Drawing.Size(489, 380);
            this.chartType.TabIndex = 0;
            this.chartType.Text = "chart1";
            // 
            // rbtnBar
            // 
            this.rbtnBar.AutoSize = true;
            this.rbtnBar.Location = new System.Drawing.Point(80, 12);
            this.rbtnBar.Name = "rbtnBar";
            this.rbtnBar.Size = new System.Drawing.Size(59, 16);
            this.rbtnBar.TabIndex = 3;
            this.rbtnBar.Text = "柱状图";
            this.rbtnBar.UseVisualStyleBackColor = true;
            this.rbtnBar.CheckedChanged += new System.EventHandler(this.rbtnBar_CheckedChanged);
            // 
            // rbtnPie
            // 
            this.rbtnPie.AutoSize = true;
            this.rbtnPie.Checked = true;
            this.rbtnPie.Location = new System.Drawing.Point(12, 12);
            this.rbtnPie.Name = "rbtnPie";
            this.rbtnPie.Size = new System.Drawing.Size(59, 16);
            this.rbtnPie.TabIndex = 2;
            this.rbtnPie.TabStop = true;
            this.rbtnPie.Text = "饼状图";
            this.rbtnPie.UseVisualStyleBackColor = true;
            this.rbtnPie.CheckedChanged += new System.EventHandler(this.rbtnPie_CheckedChanged);
            // 
            // instanceAnalysis
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(508, 418);
            this.Controls.Add(this.rbtnBar);
            this.Controls.Add(this.rbtnPie);
            this.Controls.Add(this.chartType);
            this.Name = "instanceAnalysis";
            this.Text = "实例分析";
            this.Load += new System.EventHandler(this.instanceAnalysis_Load);
            ((System.ComponentModel.ISupportInitialize)(this.chartType)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart chartType;
        private System.Windows.Forms.RadioButton rbtnBar;
        private System.Windows.Forms.RadioButton rbtnPie;
    }
}