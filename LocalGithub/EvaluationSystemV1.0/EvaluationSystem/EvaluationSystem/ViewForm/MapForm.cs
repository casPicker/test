using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using EvaluationSystem.Util;

namespace EvaluationSystem.ViewForm
{
    public partial class MapForm : Form
    {
        public DataTable table;
        private AttributeForm attriForm;
        public MapForm()
        {
            InitializeComponent();
            this.mapPicBox.Image = Image.FromFile(Application.StartupPath + "\\Resources\\map.jpg");
        }

        private void mapPicBox_MouseClick(object sender, MouseEventArgs e)
        {
            if (attriForm == null || attriForm.IsDisposed)
            {
                attriForm = new AttributeForm();
            }
            attriForm.Hide();
            
            if (table == null)
            {
                return;
            }
            EntryPort nearest = new EntryPort("", 10000, 10000);
            foreach (EntryPort port in PortList.EntryPorts)
            {
                int minDistance = Math.Abs(e.X - nearest.X) + Math.Abs(e.Y - nearest.Y);
                int curDistance = Math.Abs(e.X - port.X) + Math.Abs(e.Y - port.Y);

                nearest = (minDistance < curDistance) ? nearest : port;
            }
            if ((Math.Abs(e.X - nearest.X) + Math.Abs(e.Y - nearest.Y)) <= 30 && !nearest.Name.Equals(""))//匹配上
            {
                DataTable attriTable = new DataTable();

                attriTable.Columns.Add(new DataColumn("属性名", Type.GetType("System.String")));
                attriTable.Columns.Add(new DataColumn("属性值", Type.GetType("System.String")));
                DataRow targetRow = null;
                foreach (DataRow row in table.Rows)
                {
                    bool flag = false;
                    foreach (DataColumn col in table.Columns)
                    {
                        if (row[col].ToString().Trim().Equals(nearest.Name.Trim()))
                        {
                            targetRow = row;
                            flag = true;
                            break;
                        }
                    }
                    if (flag)
                    {
                        break;
                    }
                }
                if (targetRow != null)
                {
                    foreach (DataColumn col in table.Columns)
                    {
                        DataRow newRow = attriTable.NewRow();
                        newRow["属性名"] = col.ColumnName;
                        newRow["属性值"] = targetRow[col];
                        attriTable.Rows.Add(newRow);
                    }
                    
                    attriForm.SetDataSource(attriTable);
                    attriForm.Show();
                }
            }
        }
    }
}
