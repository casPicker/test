using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EvaluationSystem.ViewForm
{
    public partial class AttributeForm : Form
    {
        public AttributeForm()
        {
            InitializeComponent();
        }

        public void SetDataSource(DataTable table)
        {
            this.gridControl1.DataSource = table;
            foreach (DataColumn col in table.Columns)
            {
                this.gridView1.Columns.ColumnByFieldName(col.ColumnName).OptionsColumn.AllowEdit = false;
            }
            this.gridControl1.RefreshDataSource();
        }
    }
}
