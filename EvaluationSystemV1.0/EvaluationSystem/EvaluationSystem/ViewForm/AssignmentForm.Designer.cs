namespace EvaluationSystem.ViewForm
{
    partial class AssignmentForm
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
            this.lsbColNames = new DevExpress.XtraEditors.ListBoxControl();
            this.grdAssign = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.btnConfirm = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.lsbColNames)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdAssign)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // lsbColNames
            // 
            this.lsbColNames.Location = new System.Drawing.Point(12, 46);
            this.lsbColNames.Name = "lsbColNames";
            this.lsbColNames.Size = new System.Drawing.Size(159, 273);
            this.lsbColNames.TabIndex = 0;
            this.lsbColNames.SelectedIndexChanged += new System.EventHandler(this.lsbColNames_SelectedIndexChanged);
            // 
            // grdAssign
            // 
            this.grdAssign.Location = new System.Drawing.Point(177, 46);
            this.grdAssign.MainView = this.gridView1;
            this.grdAssign.Name = "grdAssign";
            this.grdAssign.Size = new System.Drawing.Size(262, 273);
            this.grdAssign.TabIndex = 1;
            this.grdAssign.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.GridControl = this.grdAssign;
            this.gridView1.Name = "gridView1";
            this.gridView1.CellValueChanged += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.gridView1_CellValueChanged);
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(67, 26);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(24, 14);
            this.labelControl1.TabIndex = 2;
            this.labelControl1.Text = "列名";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(261, 26);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(48, 14);
            this.labelControl2.TabIndex = 3;
            this.labelControl2.Text = "赋值设置";
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(261, 336);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "取消";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnConfirm
            // 
            this.btnConfirm.Location = new System.Drawing.Point(350, 336);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(75, 23);
            this.btnConfirm.TabIndex = 5;
            this.btnConfirm.Text = "确定";
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // AssignmentForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(448, 371);
            this.Controls.Add(this.btnConfirm);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.grdAssign);
            this.Controls.Add(this.lsbColNames);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "AssignmentForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "数据赋值";
            ((System.ComponentModel.ISupportInitialize)(this.lsbColNames)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdAssign)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.ListBoxControl lsbColNames;
        private DevExpress.XtraGrid.GridControl grdAssign;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.SimpleButton btnConfirm;

    }
}