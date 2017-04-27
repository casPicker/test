using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using EvaluationSystem.Service;
using EvaluationSystem.Service.ServiceImpl;
using DevExpress.XtraEditors.Repository;

namespace EvaluationSystem.ViewForm
{
    public partial class UserListForm : Form
    {
        private LoginService loginService;
        public UserListForm()
        {
            InitializeComponent();
            this.loginService = new LoginServiceImpl();
            InitialList();
        }

        private void InitialList()
        {
            DataSet dataSet = this.loginService.GetUsers4Grid();
            DataTable tb = dataSet.Tables[0];
            DataColumn col = new DataColumn("check", System.Type.GetType("System.Boolean"));
            col.DefaultValue = false;
            tb.Columns.Add(col);

            tb.Columns.Remove("pwd");
            this.gridUsers.DataSource = tb;

            RepositoryItemCheckEdit riChEdit = new RepositoryItemCheckEdit();
            riChEdit.CheckedChanged +=new EventHandler(riChEdit_CheckedChanged);
            this.gridUsers.RepositoryItems.Add(riChEdit);
            this.gridView1.Columns.ColumnByFieldName("check").ColumnEdit = riChEdit;
            this.gridView1.Columns.ColumnByFieldName("check").OptionsColumn.ShowCaption = false;//不显示列标题
            this.gridView1.Columns.ColumnByFieldName("ID").Caption = "用户编号";
            this.gridView1.Columns.ColumnByFieldName("ID").OptionsColumn.AllowEdit= false;

            this.gridView1.Columns.ColumnByFieldName("username").Caption = "用户名";
            this.gridView1.Columns.ColumnByFieldName("username").OptionsColumn.AllowEdit = false;

            this.gridView1.OptionsSelection.MultiSelect = false;
            this.gridView1.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.RowSelect;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DataTable dt = gridUsers.DataSource as DataTable;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if ((bool)dt.Rows[i]["check"])
                {
                    string username = dt.Rows[i]["username"].ToString();
                    if (this.loginService.DeleteUser(username))
                    {
                        InitialList();
                    }
                    else 
                    {
                        DevExpress.XtraEditors.XtraMessageBox.Show("用户删除失败！", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                    }
                }
            }
        }

        private void riChEdit_CheckedChanged(object sender, EventArgs e)
        {
             DataTable dt = gridUsers.DataSource as DataTable;
             for (int i = 0; i < dt.Rows.Count; i++)
             {
                dt.Rows[i]["check"] = false;
             }
             gridUsers.RefreshDataSource();
        }
    }
}
