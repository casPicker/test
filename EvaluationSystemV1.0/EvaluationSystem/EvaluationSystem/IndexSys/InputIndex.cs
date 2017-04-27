using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using EvaluationSystem;
using System.Data.OleDb;

namespace EvaluationSystem.IndexSys
{
    public partial class InputIndex : Form
    {
        public InputIndex()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textEdit1_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {

            if (textEdit1.Text.Trim() == "")
            {
                MessageBox.Show("请输入对应名称", "错误", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            //添加进数据库
            OleDbConnection conn = DBHelper.getConn(); //得到连接对象
            conn.Open();
            int indexid1;
            try
            {

                int tmp = int.Parse(MainForm.pCurrentWin.treeList1.FocusedNode.GetValue("indexid").ToString());
                //根据指标体编号
                String strCommand1 = "SELECT * FROM TB_INDEX t WHERE t.indexid = " + tmp;
                OleDbCommand myCommand1 = new OleDbCommand(strCommand1, conn);
                OleDbDataReader reader;
                reader = myCommand1.ExecuteReader(); //执行command并得到相应的DataReader
                reader.Read();
                //int indexid = int.Parse(reader["indexid"].ToString());//指标编号
                int indexsys = int.Parse(reader["systemid"].ToString());//指标体系编号
                reader.Close();
                //加入指标表
                String strCommand = "INSERT INTO TB_INDEX (pid,indexname,systemid) VALUES (?,?,?)";
                OleDbCommand command = new OleDbCommand(strCommand, conn);
                OleDbParameterCollection paramCollection = command.Parameters;
                OleDbParameter param1 = paramCollection.Add("pid", OleDbType.VarChar);
                param1.Value = tmp;
                OleDbParameter param2 = paramCollection.Add("indexname", OleDbType.VarChar);
                param2.Value = textEdit1.Text;
                OleDbParameter param3 = paramCollection.Add("systemid", OleDbType.VarChar);
                param3.Value = indexsys;
                command.ExecuteNonQuery();
                //根据指标体系名查找指标体系编号
                string strCommand2 = "select max(indexid) from TB_INDEX";
                OleDbCommand myCommand2 = new OleDbCommand(strCommand2, conn);
                object o = myCommand2.ExecuteScalar();
                indexid1 = int.Parse(o.ToString());
            }
            catch (System.Exception ex)
            {
                conn.Close();
                throw new Exception("[ERROR] 数据库操作出现异常：" + ex.Message);
            }
            MainForm.pCurrentWin.treeList1.BeginUnboundLoad();
            MainForm.pCurrentWin.treeList1.AppendNode(new object[] { indexid1, MainForm.pCurrentWin.nodeid, textEdit1.Text }, MainForm.pCurrentWin.nodeid);
            MainForm.pCurrentWin.treeList1.EndUnboundLoad();
            MainForm.pCurrentWin.SetImageIndex(MainForm.pCurrentWin.treeList1, null, 20, 21);
            conn.Close();
            this.Close();
        }

    }
}
