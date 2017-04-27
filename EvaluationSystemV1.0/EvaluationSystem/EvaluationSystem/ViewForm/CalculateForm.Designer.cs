namespace EvaluationSystem.ViewForm
{
    partial class CalculateForm
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
            this.tlMatch = new DevExpress.XtraTreeList.TreeList();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.btnStartCalc = new DevExpress.XtraEditors.SimpleButton();
            this.lsbIndexInstance = new DevExpress.XtraEditors.ListBoxControl();
            this.btnImport = new DevExpress.XtraEditors.SimpleButton();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            ((System.ComponentModel.ISupportInitialize)(this.tlMatch)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lsbIndexInstance)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlMatch
            // 
            this.tlMatch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlMatch.Location = new System.Drawing.Point(2, 27);
            this.tlMatch.Margin = new System.Windows.Forms.Padding(4);
            this.tlMatch.Name = "tlMatch";
            this.tlMatch.OptionsClipboard.AllowCopy = DevExpress.Utils.DefaultBoolean.True;
            this.tlMatch.OptionsClipboard.CopyNodeHierarchy = DevExpress.Utils.DefaultBoolean.True;
            this.tlMatch.Size = new System.Drawing.Size(700, 398);
            this.tlMatch.TabIndex = 0;
            this.tlMatch.ShowingEditor += new System.ComponentModel.CancelEventHandler(this.tlMatch_ShowingEditor);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(744, 457);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 29);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "取消";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnStartCalc
            // 
            this.btnStartCalc.Location = new System.Drawing.Point(863, 457);
            this.btnStartCalc.Margin = new System.Windows.Forms.Padding(4);
            this.btnStartCalc.Name = "btnStartCalc";
            this.btnStartCalc.Size = new System.Drawing.Size(100, 29);
            this.btnStartCalc.TabIndex = 2;
            this.btnStartCalc.Text = "开始计算";
            this.btnStartCalc.Click += new System.EventHandler(this.btnStartCalc_Click);
            // 
            // lsbIndexInstance
            // 
            this.lsbIndexInstance.HorizontalScrollbar = true;
            this.lsbIndexInstance.Location = new System.Drawing.Point(5, 30);
            this.lsbIndexInstance.Name = "lsbIndexInstance";
            this.lsbIndexInstance.Size = new System.Drawing.Size(250, 392);
            this.lsbIndexInstance.TabIndex = 3;
            // 
            // btnImport
            // 
            this.btnImport.Location = new System.Drawing.Point(167, 457);
            this.btnImport.Margin = new System.Windows.Forms.Padding(4);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(100, 29);
            this.btnImport.TabIndex = 4;
            this.btnImport.Text = "导入权重";
            this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.lsbIndexInstance);
            this.groupControl1.Location = new System.Drawing.Point(12, 12);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(260, 427);
            this.groupControl1.TabIndex = 6;
            this.groupControl1.Text = "指标实例";
            // 
            // groupControl2
            // 
            this.groupControl2.Controls.Add(this.tlMatch);
            this.groupControl2.Location = new System.Drawing.Point(278, 12);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.Size = new System.Drawing.Size(704, 427);
            this.groupControl2.TabIndex = 4;
            this.groupControl2.Text = "指标-数据配置";
            // 
            // CalculateForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(994, 514);
            this.Controls.Add(this.groupControl2);
            this.Controls.Add(this.groupControl1);
            this.Controls.Add(this.btnImport);
            this.Controls.Add(this.btnStartCalc);
            this.Controls.Add(this.btnCancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "CalculateForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "配置";
            ((System.ComponentModel.ISupportInitialize)(this.tlMatch)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lsbIndexInstance)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraTreeList.TreeList tlMatch;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.SimpleButton btnStartCalc;
        private DevExpress.XtraEditors.ListBoxControl lsbIndexInstance;
        private DevExpress.XtraEditors.SimpleButton btnImport;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.GroupControl groupControl2;
    }
}