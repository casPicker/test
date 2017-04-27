using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using EvaluationSystem.DAO;
using System.Data.OleDb;

namespace EvaluationSystem.IndexSys
{
    public partial class InputDesti : Form
    {
        public InputDesti()
        {
            InitializeComponent();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (textEdit1.Text.Trim() == "")
            {
                MessageBox.Show("请输入对应名称!", "错误", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            //添加进数据库
            OleDbConnection conn = DBHelper.getConn(); //得到连接对象
            conn.Open();
            int indexid1;
            try
            {

                //加入指标体系表
                String strCommand0 = "INSERT INTO TB_INDEX_SYSTEM (systemname) VALUES (?)";
                OleDbCommand command0 = new OleDbCommand(strCommand0, conn);
                OleDbParameterCollection paramCollection0 = command0.Parameters;
                OleDbParameter param0 = paramCollection0.Add("systemname", OleDbType.VarChar);
                param0.Value = textEdit1.Text;
                //command0.ExecuteNonQuery();
                if (command0.ExecuteNonQuery() > 0)//判断插入数据是否成功
                {

                    //执行要操作的语句
                    MessageBox.Show("指标体系创建成功,请添加指标");

                }
                //根据指标体系名查找指标体系编号
                String strCommand1 = "SELECT * FROM TB_INDEX_SYSTEM t WHERE t.systemname = '" + textEdit1.Text + "'";
                OleDbCommand myCommand1 = new OleDbCommand(strCommand1, conn);
                OleDbDataReader reader;
                reader = myCommand1.ExecuteReader(); //执行command并得到相应的DataReader
                reader.Read();
                int indexsys = int.Parse(reader["systemid"].ToString());
                reader.Close();
                //加入指标表
                String strCommand = "INSERT INTO TB_INDEX (pid,indexname,systemid) VALUES (?,?,?)";
                OleDbCommand command = new OleDbCommand(strCommand, conn);
                OleDbParameterCollection paramCollection = command.Parameters;
                OleDbParameter param1 = paramCollection.Add("pid", OleDbType.VarChar);
                param1.Value = 0;
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
            MainForm.pCurrentWin.treeList1.AppendNode(new object[] { indexid1, 0, textEdit1.Text }, -1);
            MainForm.pCurrentWin.treeList1.EndUnboundLoad();
            MainForm.pCurrentWin.SetImageIndex(MainForm.pCurrentWin.treeList1, null, 20, 21);
            conn.Close();
            this.Close();
        }
    }
}
