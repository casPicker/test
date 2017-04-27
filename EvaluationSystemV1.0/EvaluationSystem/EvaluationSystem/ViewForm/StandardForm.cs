using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors.Repository;
using EvaluationSystem.Event;
using EvaluationSystem.Util;

namespace EvaluationSystem.ViewForm
{
    public partial class StandardForm : Form
    {
        private string[] colNames;
        public string[] ColNames
        {
            get { return this.colNames; }
            set{ 
                this.colNames = value;
                FillDataGrid();
            }
        }

        public event StandardEventHandler StandardEvent;

        public StandardForm()
        {
            InitializeComponent();
            this.imageSlider1.Images.Add(Image.FromFile(Application.StartupPath+"\\Resources\\Min_Max.jpg"));

            DataTable table = new DataTable();
            table.Columns.Add(new DataColumn("列名", Type.GetType("System.String")));
            table.Columns.Add(new DataColumn("是否标准化", Type.GetType("System.Boolean")));
            table.Columns.Add(new DataColumn("是否取补", Type.GetType("System.Boolean")));
            table.Columns.Add(new DataColumn("标准化方法", Type.GetType("System.String")));
            this.grdStandard.DataSource = table;

            RepositoryItemCheckEdit riChEdit = new RepositoryItemCheckEdit();
            this.grdStandard.RepositoryItems.Add(riChEdit);

            RepositoryItemComboBox riCombo = new RepositoryItemComboBox();
            riCombo.Items.AddRange(new string[] { "Min-Max标准化" });
            this.grdStandard.RepositoryItems.Add(riCombo);

            this.gridView1.Columns.ColumnByFieldName("是否标准化").ColumnEdit = riChEdit;
            this.gridView1.Columns.ColumnByFieldName("是否取补").ColumnEdit = riChEdit;
            this.gridView1.Columns.ColumnByFieldName("标准化方法").ColumnEdit = riCombo;
        }

        private void FillDataGrid()
        {
           DataTable table = (DataTable)this.grdStandard.DataSource;
           foreach(string colName in colNames)
           {
               DataRow row = table.NewRow();
               row["列名"] = colName;
               row["是否标准化"] = true;
               row["是否取补"] = false;
               row["标准化方法"] = "Min-Max标准化";
               table.Rows.Add(row);
           }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            if (this.StandardEvent != null)
            {
                StandardEventArgs args = new StandardEventArgs();
                DataTable curTable = (DataTable)this.grdStandard.DataSource;

                Standard[] standards = new Standard[curTable.Rows.Count];
                for (int i = 0; i < curTable.Rows.Count; i++)
                {
                    string colName = curTable.Rows[i]["列名"].ToString();
                    bool isStandard = (bool)curTable.Rows[i]["是否标准化"];
                    bool isComplementray = (bool)curTable.Rows[i]["是否取补"];
                    string normalFunName = curTable.Rows[i]["标准化方法"].ToString();

                    standards[i] = new Standard();
                    standards[i].ColName = colName;
                    standards[i].IsStandard = isStandard;
                    standards[i].IsComplementary = isComplementray;
                    standards[i].NormalFunName = normalFunName;
                }
                args.Standards = standards;
                this.StandardEvent(this,args);
            }
            this.Close();
        }
    }
}
