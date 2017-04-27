using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using EvaluationSystem.Util;
using EvaluationSystem.Event;

namespace EvaluationSystem.ViewForm
{
    public partial class AssignmentForm : Form
    {
        public event EventHandler FieldSelectEvent;
        public event AssignmentEventHandler AssignmentEvent;

        private Hashtable assignDetail;
        private string[] colNames;
        public string[] ColNames
        {
            get { return this.colNames; }
            set
            {
                this.colNames = value;
                this.lsbColNames.Items.Clear();
                this.lsbColNames.Items.AddRange(this.colNames);

                assignDetail = new Hashtable();
            }
        }
        public AssignmentForm()
        {
            InitializeComponent();
        }

        private void lsbColNames_SelectedIndexChanged(object sender, EventArgs e)
        {
            string colName = this.lsbColNames.SelectedItem.ToString();
            if (assignDetail.Contains(colName))
            {
                Assign[] assigns = (Assign[])assignDetail[colName];
                DataTable table = new DataTable();
                table.Columns.Add(new DataColumn("原始值", Type.GetType("System.String")));
                table.Columns.Add(new DataColumn("替换值", Type.GetType("System.String")));

                foreach (Assign assign in assigns)
                {
                    DataRow row = table.NewRow();
                    row["原始值"] = assign.OldValue;
                    row["替换值"] = assign.NewValue;
                    table.Rows.Add(row);
                }
                this.grdAssign.DataSource = table;
            }
            else 
            {
                if (FieldSelectEvent != null)
                {
                    FieldSelectEvent(sender, e);
                }
            }
        }

        public void RefreshAssignGrid(string[] uniqValues)
        {
            DataTable table = new DataTable();
            table.Columns.Add(new DataColumn("原始值", Type.GetType("System.String")));
            table.Columns.Add(new DataColumn("替换值", Type.GetType("System.String")));

            foreach(string uniqValue in uniqValues)
            {
                DataRow row = table.NewRow();
                row["原始值"] = uniqValue;
                table.Rows.Add(row);
            }
            this.grdAssign.DataSource = table;
        }

        private void gridView1_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            string colName = this.lsbColNames.SelectedItem.ToString();
            DataTable curTable = (DataTable)this.grdAssign.DataSource;
            Assign[] assigns = new Assign[curTable.Rows.Count];
            for (int i = 0; i < curTable.Rows.Count;i++ )
            {
                string oldValue = curTable.Rows[i]["原始值"].ToString();
                string newValue = curTable.Rows[i]["替换值"].ToString();
                assigns[i] = new Assign(oldValue,newValue);
            }
            if (assignDetail.ContainsKey(colName))
            {
                assignDetail[colName] = assigns;
            }
            else
            {
                assignDetail.Add(colName, assigns);
            }
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            if (AssignmentEvent != null)
            {
                AssignmentEventArgs args = new AssignmentEventArgs();
                args.AssignDetail = this.assignDetail;
                AssignmentEvent(this,args);
            }
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
