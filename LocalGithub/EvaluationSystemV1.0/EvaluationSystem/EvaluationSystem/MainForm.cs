using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraBars;
using EvaluationSystem.Util;
using DevExpress.XtraBars.Navigation;
using DevExpress.XtraGrid;
using EvaluationSystem.Event;
using EvaluationSystem.ViewForm;
using System.Collections;
using EvaluationSystem.Entity;
using DevExpress.XtraCharts;
using DevExpress.XtraEditors.Controls;
using System.IO;
using DevExpress.Export;
using DevExpress.XtraEditors;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraBars;
using DevExpress.XtraTreeList.Nodes;
using DevExpress.XtraTreeList;
using System.Data.OleDb;
using System.Reflection;
using System.Collections;
using EvaluationSystem.IndexSys;
using System.Drawing.Imaging;
namespace EvaluationSystem
{
    /// <summary>
    /// 系统主界面
    /// </summary>
    public partial class MainForm : RibbonForm
    {
        private AssignmentForm assignmentForm;
        private SheetSelectForm sheetSelectForm;
        private StandardForm standardForm;
        private IndexSystemSelectForm indexSystemSelectForm;
        private CalculateForm calculateForm;
        private IndexSystem selectedIndexSystem;
        private DataTable rawDataTable;
        private DataTable assignTable;
        private DataTable standardTable;
        private DataTable resultTable;
        public MainForm()
        {
            InitializeComponent();
            this.rawDataPage.PageVisible = false;
            this.assignDataPage.PageVisible = false;
            this.standardPage.PageVisible = false;
            this.resultPage.PageVisible = false;
            pCurrentWin = this;
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();//主窗口关闭，退出程序
        }

        private void SheetSelectedEventHandler(object sender,SheetSelectedEventArgs e)
        {
            string sheetName = e.SheetName;
            string fullFileName = e.FullFileName;
            rawDataTable = (DataTable)ExcelDataBaseHelper.OpenFile(fullFileName, sheetName, "RawData", true);
            rawDataGrid.DataSource = rawDataTable;
            this.tabPane1.SelectedPageIndex = 0;
            this.rawDataPage.PageVisible = true;
            this.gridView1.BestFitColumns();

            //赋值按钮状态调整为可用
            barButtonItem66.Enabled = true;

            //其他按钮调整为不可用（重新导入情况）
            barButtonItem67.Enabled = false;
            barButtonItem41.Enabled = false;

            assignDataPage.PageVisible = false;
            standardPage.PageVisible = false;
            resultPage.PageVisible = false;

            //打印输出调整为可用
            barButtonItem68.Enabled = true;
            barButtonItem69.Enabled = true;
        }

        private void assignmentForm_AssignmentEvent(object sender, AssignmentEventArgs e)
        {
            /*动态创建DataTable*/
            if (assignTable == null)
            {
                assignTable = (DataTable)rawDataGrid.DataSource;
            }
            Hashtable assignDetail = e.AssignDetail;
            DataTable newTable = new DataTable();
            foreach (DataColumn col in assignTable.Columns)
            {
                newTable.Columns.Add(new DataColumn(col.ColumnName, col.DataType));
            }

            foreach (DataRow row in assignTable.Rows)
            {
                DataRow newRow = newTable.NewRow();
                //赋值处理 

                foreach (DataColumn col in row.Table.Columns)
                {
                    object rowValue = row[col];
                    foreach (string colName in assignDetail.Keys)
                    {
                        if (col.ColumnName.Equals(colName))
                        {
                            Assign[] tempAssigns = (Assign[])assignDetail[colName];
                            foreach(Assign assign in tempAssigns)
                            {
                                if (assign.OldValue.Equals(rowValue.ToString()))
                                {
                                    rowValue = assign.NewValue;
                                }
                            }
                        }
                    }
                    newRow[col.ColumnName] = rowValue;
                }
                newTable.Rows.Add(newRow);
            }
            assignTable = newTable;
            assignGrid.DataSource = newTable;

            this.tabPane1.SelectedPageIndex = 1;
            this.assignDataPage.PageVisible = true;

            this.gridView2.BestFitColumns();

            //数据标准化按钮调整为可用
            barButtonItem67.Enabled = true;
        }

        private void assignmentForm_FieldSelectEvent(object sender, EventArgs e)
        {
            ListBoxControl listBox = (ListBoxControl)sender;
            string colName = listBox.SelectedItem.ToString();
            DataView dv = new DataView(this.assignTable);
            DataTable tempTable = dv.ToTable(true,colName);
            string[] uniqValues = new string[tempTable.Rows.Count];
            for (int i = 0; i < tempTable.Rows.Count;i++ )
            {
                string curValue = tempTable.Rows[i][0].ToString();
                uniqValues[i] = curValue;
            }
            if (assignmentForm == null || assignmentForm.IsDisposed)
            {
                assignmentForm = new AssignmentForm();
            }
            assignmentForm.RefreshAssignGrid(uniqValues);
        }

        private void standardForm_StandardEvent(object sender,StandardEventArgs e)
        {
            Standard[] standards = e.Standards;
            //动态创建DataTable
            if (standardTable == null)
            {
                if (assignTable == null)
                {
                    standardTable = (DataTable)rawDataGrid.DataSource;
                }
                else 
                {
                    standardTable = (DataTable)assignGrid.DataSource;
                }
            }

            DataTable newTable = new DataTable();
            foreach (DataColumn col in standardTable.Columns)
            {
                bool flag = false;
                foreach(Standard standard in standards)
                {
                    if (col.ColumnName.Equals(standard.ColName) && standard.IsStandard)
                    {
                        newTable.Columns.Add(new DataColumn(col.ColumnName, Type.GetType("System.Double")));
                        flag = true;
                        break;
                    }
                }
                if (!flag)
                {
                    newTable.Columns.Add(new DataColumn(col.ColumnName, col.DataType));
                }
            }

            foreach (DataRow row in standardTable.Rows)
            {
                DataRow newRow = newTable.NewRow();
                foreach (DataColumn col in row.Table.Columns)
                {
                    object rowValue = row[col];
                    foreach (Standard standard in standards)
                    {
                        if (col.ColumnName.Equals(standard.ColName)&&standard.IsStandard)
                        {
                            double max = Double.Parse(standardTable.Compute("MAX([" + col.ColumnName + "])", "true").ToString());
                            double min = Double.Parse(standardTable.Compute("MIN([" + col.ColumnName + "])", "true").ToString());

                            double curValue = Double.Parse(rowValue.ToString());
                            if (standard.IsComplementary)
                            {
                                rowValue = 1 - (curValue - min) / (max - min);
                            }
                            else
                            {
                                rowValue = (curValue - min) / (max - min);
                            }

                            rowValue = Double.Parse(rowValue.ToString());// Math.Round(Double.Parse(rowValue.ToString()), 2);
                            break;
                        }
                    }
                    newRow[col.ColumnName] = rowValue;
                }
                newTable.Rows.Add(newRow);
            }

            standardTable = newTable;
            standardGrid.DataSource = newTable;

            this.tabPane1.SelectedPageIndex = 2;
            this.standardPage.PageVisible = true;

            this.gridView3.BestFitColumns();       
 
            //计算按钮调整为可用
            barButtonItem41.Enabled = true;
        }

        private void barButtonItem41_ItemClick(object sender, ItemClickEventArgs e)
        {
            if(this.rawDataTable == null)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("请先导入原始数据！", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                return;
            }
            /*
            if (indexSystemSelectForm == null || indexSystemSelectForm.IsDisposed)
            {
                indexSystemSelectForm = new IndexSystemSelectForm();
                indexSystemSelectForm.IndexSystemSelectedEvent += new IndexSystemSelectedEventHandler(indexSystemSelectForm_IndexSystemSelectedEvent);
            }*/
            indexSystemSelectForm = new IndexSystemSelectForm();
            indexSystemSelectForm.IndexSystemSelectedEvent += new IndexSystemSelectedEventHandler(indexSystemSelectForm_IndexSystemSelectedEvent);
            indexSystemSelectForm.ShowDialog();
        }

        private void indexSystemSelectForm_IndexSystemSelectedEvent(object sender, IndexSystemSelectedEventArgs e)
        {
            IndexSystem indexSystem = e.SelectedIndexSystem;
            this.selectedIndexSystem = indexSystem;
            if (indexSystem != null)
            {
                /*
                if (calculateForm == null || calculateForm.IsDisposed)
                {
                    calculateForm = new CalculateForm(ExcelDataBaseHelper.GetColNames(this.rawDataTable), indexSystem, this.standardTable);
                    calculateForm.CalcCompleteEvent +=new CalcCompleteEventHandler(calculateForm_CalcCompleteEvent);
                }
                */
                calculateForm = new CalculateForm(ExcelDataBaseHelper.GetColNames(this.rawDataTable), indexSystem, this.standardTable/*this.rawDataTable*/);
                calculateForm.CalcCompleteEvent += new CalcCompleteEventHandler(calculateForm_CalcCompleteEvent);
                calculateForm.InitializeTreeList();
                calculateForm.ShowDialog();
            }
        }

        private void calculateForm_CalcCompleteEvent(object sender,CalcCompleteEventArgs e)
        {
            this.resultTable = e.ResultTable;
            resultGrid.DataSource = this.resultTable;

            this.tabPane1.SelectedPageIndex = 3;
            this.resultPage.PageVisible = true;
            this.splitContainerControl1.Visible = true;

            this.gridView3.BestFitColumns();

            InitializeChart(this.resultTable);

            //可视化、专题图按钮调整为可用
            barButtonItem71.Enabled = true;
            barButtonItem72.Enabled = true;
        }

        private void InitializeChart( DataTable table)
        {
            foreach(DataColumn col in table.Columns)
            {
                if (col.DataType != Type.GetType("System.String"))
                {
                    clbShowFilds.Items.Add(new CheckedListBoxItem(col.ColumnName, false));
                }
                else
                {
                    cmbXField.Properties.Items.Add(col.ColumnName);
                }
            }
            
        }



        private void ribbonControl_SelectedPageChanged(object sender, EventArgs e)
        {
            //if (this.ribbonControl.SelectedPage == ribbonPage3)
            //{
            //    dataComputePanel.Visible = true;
            //}
        }

        private void barButtonItem57_ItemClick(object sender, ItemClickEventArgs e)
        {
            RegisterForm registerForm = new RegisterForm();
            registerForm.ShowDialog();
        }

        private void barButtonItem58_ItemClick(object sender, ItemClickEventArgs e)
        {
            PwdChangeForm pwdChangeForm = new PwdChangeForm();
            pwdChangeForm.ShowDialog();
        }

        private void barBtnUserList_ItemClick(object sender, ItemClickEventArgs e)
        {
            UserListForm userListForm = new UserListForm();
            userListForm.ShowDialog();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (cmbXField.SelectedItem == null)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("请选择横轴字段！", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                return;
            }
            if(clbShowFilds.CheckedItems.Count == 0)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("请选择展示字段！", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                return;
            }
            //先清除
            this.resultChart.Series.Clear();
            this.resultChart.Titles.Clear();

            foreach (object item in clbShowFilds.CheckedItems)
            {
                Series series = new Series(item.ToString(),ViewType.Bar);
                series.ArgumentDataMember = cmbXField.SelectedItem.ToString();
                series.ValueDataMembers.AddRange(new string[]{item.ToString()});
                this.resultChart.Series.Add(series);
            }
            ChartTitle chartTitle = new ChartTitle();
            chartTitle.Text = (this.selectedIndexSystem !=null)?this.selectedIndexSystem.Systemname:"可视化图表";
            this.resultChart.Titles.Add(chartTitle);

            //设置缩放和平移
            XYDiagram diagram = this.resultChart.Diagram as XYDiagram;
            if (diagram != null)
            {
                diagram.EnableAxisXScrolling = true;
                //diagram.EnableAxisYScrolling = true;
                diagram.EnableAxisXZooming = true;
                //diagram.EnableAxisYZooming = true;
            }
            this.resultChart.DataSource = this.resultTable;

            //导出图片按钮调整为可用
            barButtonItem77.Enabled = true;
        }

        #region 指标体系管理
        public int nodeid = 0;
        public static MainForm pCurrentWin = null;
        TreeListNode deletenode;
        /// <summary>
        /// 新建指标体系右键事件
        /// </summary>
        private void treeList1_Load(object sender, EventArgs e)
        {

            OleDbConnection conn = DBHelper.getConn(); //得到连接对象
            OleDbDataAdapter adp = new OleDbDataAdapter("select * from TB_INDEX", conn);
            DataTable dt = new DataTable();
            conn.Open();
            adp.Fill(dt);
            conn.Close();
            // 清空数据源
            treeList1.DataSource = null;
            dt.Columns.Remove("systemid");
            dt.Columns["indexid"].SetOrdinal(0);
            dt.Columns["pid"].SetOrdinal(1);
            dt.Columns["indexname"].SetOrdinal(2);
            treeList1.DataSource = dt;
            treeList1.Columns[2].Caption = "指标体系管理";
            treeList1.KeyFieldName = "indexid";
            treeList1.ParentFieldName = "pid";
            treeList1.ExpandAll();
            //把焦点设置在第一个NODE，这样防止树形结构会展开
            //treeList1.FocusedNode = treeList1.Nodes[0];
            //设置各级有线条连接
            treeList1.LookAndFeel.UseDefaultLookAndFeel = false;
            treeList1.LookAndFeel.UseWindowsXPTheme = true;
            //递归显示图标
            //递归设置图标             
            SetImageIndex(treeList1, null, 20, 21);
        }
        //获取根节点所有节点数

        private int GetChildNodes(TreeListNode parentNode, List<TreeListNode> list)
        {

            if (parentNode.Nodes.Count > 0)
            {
                foreach (TreeListNode node in parentNode.Nodes)
                {
                    list.Add(node);
                    if (node.Nodes.Count > 0)
                    {
                        GetChildNodes(node, list);
                    }
                }
            }
            int allnodes = list.Count + 1;
            return allnodes;
        }

        //获取节点编号
        private List<TreeListNode> GetNodesindex(TreeListNode parentNode, List<TreeListNode> list)
        {

            if (parentNode.ParentNode == null)
            { list.Add(parentNode); }
            if (parentNode.Nodes.Count > 0)
            {

                foreach (TreeListNode node in parentNode.Nodes)
                {
                    list.Add(node);
                    if (node.Nodes.Count > 0)
                    {
                        GetNodesindex(node, list);
                    }
                }
            }

            return list;
        }
        private void treeList1_MouseClick(object sender, MouseEventArgs e)
        {
            treeList1.ContextMenuStrip = null;
            if (e.Button == MouseButtons.Right)
            {

                TreeListHitInfo hInfo = treeList1.CalcHitInfo(new Point(e.X, e.Y));
                TreeListNode node = hInfo.Node;
                treeList1.ContextMenuStrip = contextMenuStrip1;

                if (node == null)
                {

                    添加指标ToolStripMenuItem.Enabled = false;
                    删除指标ToolStripMenuItem.Enabled = false;
                    完成创建ToolStripMenuItem.Enabled = false;
                    新建指标体系ToolStripMenuItem.Enabled = true;
                    刷新列表ToolStripMenuItem.Enabled = true;
                    指标实例ToolStripMenuItem.Enabled = false;
                }
                else if (node != null && node.ParentNode == null)
                {
                    指标实例ToolStripMenuItem.Enabled = true;
                    添加指标ToolStripMenuItem.Enabled = true;
                    删除指标ToolStripMenuItem.Enabled = true;
                    完成创建ToolStripMenuItem.Enabled = true;
                    新建指标体系ToolStripMenuItem.Enabled = false;
                    刷新列表ToolStripMenuItem.Enabled = false;
                    node.TreeList.FocusedNode = node;
                }
                else
                {
                    指标实例ToolStripMenuItem.Enabled = false;
                    添加指标ToolStripMenuItem.Enabled = true;
                    删除指标ToolStripMenuItem.Enabled = true;
                    完成创建ToolStripMenuItem.Enabled = true;
                    新建指标体系ToolStripMenuItem.Enabled = false;
                    刷新列表ToolStripMenuItem.Enabled = false;
                    node.TreeList.FocusedNode = node;
                }
            }

        }
        public int indexidcounts;
        private void 新建目标层ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<string> indexid = new List<string>();
            List<TreeListNode> list = new List<TreeListNode>();
            InputDesti inputdesti = new InputDesti();
            inputdesti.Show();
            inputdesti.Focus();
            inputdesti.TopMost = true;

            foreach (TreeListNode item in treeList1.Nodes)//遍历Treeview的所有节点
            {
                list = GetNodesindex(item, list);
            }
            for (int i = 0; i < list.Count; i++)
            {
                indexid.Add(list[i].GetValue("indexid").ToString());

            }
            List<int> tempNum = indexid.Select(x => int.Parse(x)).ToList();
            if (tempNum.Count != 0)
            {
                indexidcounts = tempNum.Max();
            }
            else
            { indexidcounts = 0; }
           // MessageBox.Show(indexidcounts.ToString());
            新建指标体系ToolStripMenuItem.Enabled = false;

        }
        private void 添加指标ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<TreeListNode> list = new List<TreeListNode>();
            List<string> indexid = new List<string>();
            InputIndex inputindex = new InputIndex();
            inputindex.Show();
            inputindex.Focus();
            inputindex.TopMost = true;
            foreach (TreeListNode item in treeList1.Nodes)//遍历Treeview的所有节点
            {
                list = GetNodesindex(item, list);
            }
            for (int i = 0; i < list.Count; i++)
            {
                indexid.Add(list[i].GetValue("indexid").ToString());


            }
            List<int> tempNum = indexid.Select(x => int.Parse(x)).ToList();
            indexidcounts = tempNum.Max();
           // MessageBox.Show(indexidcounts.ToString());
        }

        private void 删除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OleDbConnection conn = DBHelper.getConn(); //得到连接对象
            conn.Open();
            string sql1 = "delete from TB_INDEX where indexid=" + int.Parse(treeList1.FocusedNode.GetValue("indexid").ToString());
            OleDbCommand cmd1 = new OleDbCommand(sql1, conn);
            cmd1.ExecuteNonQuery();
            //删除节点下（包括节点）所有节点
            foreach (TreeListNode node in treeList1.FocusedNode.Nodes)
            {

                string sql = "delete from TB_INDEX where indexid=" + int.Parse(node.GetValue("indexid").ToString());
                OleDbCommand cmd = new OleDbCommand(sql, conn);
                cmd.ExecuteNonQuery();

            }
            conn.Close();
            treeList1.DeleteNode(deletenode);
        }

        private void treeList1_AfterFocusNode(object sender, NodeEventArgs e)
        {
            nodeid = e.Node.Id;
            deletenode = e.Node;

            //MessageBox.Show(treeList1.FocusedNode.GetValue("indexid").ToString());
        }
        private void 刷新列表ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var ids = new List<object>();
            treeList1.GetNodeList().FindAll(n => n.Expanded).ForEach(n => { ids.Add(n.GetValue("indexid")); });
            ids.ForEach(id => { treeList1.FindNodeByKeyID(id).Expanded = true; });
        }

        private void 完成创建ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            新建指标体系ToolStripMenuItem.Enabled = true;
        }

        private void iExit_ItemClick(object sender, ItemClickEventArgs e)
        {
            
        }
        private void treeList1_FocusedNodeChanged(object sender, FocusedNodeChangedEventArgs e)
        {
            //MessageBox.Show(e.Node.Level.ToString());
            //MessageBox.Show(e.Node.Id.ToString());
            //MessageBox.Show(e.Node.GetDisplayText(0));
        }
        int tag = 0;
        private void 指标实例ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            xtraTabControl2.SelectedTabPage = xtraTabPage3;
            //添加进数据库
            OleDbConnection conn = DBHelper.getConn(); //得到连接对象
            labelControl3.Visible = false;
            conn.Open();
            try
            {
                string tmp = treeList1.FocusedNode.GetDisplayText("indexname");
                //根据指标体系名查找指标体系编号
                String strCommand1 = "SELECT * FROM TB_INDEX t WHERE t.indexname = '" + tmp + "'";
                OleDbCommand myCommand1 = new OleDbCommand(strCommand1, conn);
                OleDbDataReader reader1;
                reader1 = myCommand1.ExecuteReader(); //执行command并得到相应的DataReader
                reader1.Read();
                int indexid = int.Parse(reader1["indexid"].ToString());//指标编号
                int indexsys = int.Parse(reader1["systemid"].ToString());//指标体系编号
                reader1.Close();
                //连接表
                string join = "SELECT * FROM TB_INDEX_INSTANCE a inner join TB_INDEX b ON b.indexid=a.indexid  WHERE a.systemid =" + indexsys;
                OleDbDataAdapter adp0 = new OleDbDataAdapter(join, conn);
                DataTable dtjoin = new DataTable();
                adp0.Fill(dtjoin);
                if (dtjoin.Rows.Count == 0)
                {
                    tag = 1;
                    treeList2.DataSource = null;
                    labelControl3.Visible = true;
                    conn.Close();
                    return;
                }
                //取出实例编号这列
                int[] num = dtjoin.AsEnumerable().Select(d => d.Field<int>("instanceid")).ToArray();
                IEnumerable<int> diff = num.Distinct<int>();
                //获取该指标体系的最大节点编号
                int[] instanceindexid = dtjoin.AsEnumerable().Select(d => d.Field<int>("a.indexid")).ToArray();
                IEnumerable<int> instanceindexidnum = instanceindexid.Distinct<int>();
                int maxindexid = instanceindexidnum.Max();
                //不同实力编号不一样，但是节点编号一样，要加载到同一个treelist必须节点编号不一样
                foreach (int i in diff)
                {
                    for (int j = 0; j < dtjoin.Rows.Count; j++)
                    {
                        if (dtjoin.Rows[j]["instanceid"].ToString() == i.ToString() && dtjoin.Rows[j]["pid"].ToString() != "0")//查询条件 
                        {
                            dtjoin.Rows[j]["a.indexid"] = (int.Parse(dtjoin.Rows[j]["a.indexid"].ToString()) + (i - 1) * maxindexid).ToString();
                            dtjoin.Rows[j]["pid"] = (int.Parse(dtjoin.Rows[j]["pid"].ToString()) + (i - 1) * maxindexid).ToString();
                        }
                        else if (dtjoin.Rows[j]["instanceid"].ToString() == i.ToString() && dtjoin.Rows[j]["pid"].ToString() == "0")
                        {
                            dtjoin.Rows[j]["a.indexid"] = (int.Parse(dtjoin.Rows[j]["a.indexid"].ToString()) + (i - 1) * maxindexid).ToString();
                        }
                    }
                }
                //清空数据源
                treeList2.DataSource = null;
                //dtjoin.Columns.Remove("a.systemid");
                //dtjoin.Columns.Remove("instanceid");
                dtjoin.Columns.Remove("b.indexid");
                dtjoin.Columns.Remove("b.systemid");
                dtjoin.Columns.Remove("ID");
                dtjoin.Columns.Remove("instanceid");
                dtjoin.Columns["indexname"].SetOrdinal(0);
                dtjoin.Columns["indexvalue"].SetOrdinal(1);
                dtjoin.Columns["instancename"].SetOrdinal(2);
                treeList2.DataSource = dtjoin;
                treeList2.Columns[0].Caption = "指标体系";
                treeList2.Columns[1].Caption = "指标权重";
                treeList2.Columns[2].Caption = "实例名称";
                treeList2.Columns[3].Visible = false;
                //treeList2.Columns[4].Visible = false;
                //treeList2.Columns[5].Visible = false;
                treeList2.KeyFieldName = "a.indexid";
                treeList2.ParentFieldName = "pid";

                conn.Close();
                tag = 1;
            }
            catch (System.Exception ex)
            {
                conn.Close();
                throw new Exception("[ERROR] 数据库操作出现异常：" + ex.Message);
            }
        }
        private void 新建指标实例toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            labelControl3.Visible = false;
            完成创建simpleButton1.Visible = true;
            取消创建simpleButton2.Visible = true;
            //在当前treelist的基础上，添加新的实例
            //添加进数据库
            OleDbConnection conn = DBHelper.getConn(); //得到连接对象
            labelControl3.Visible = false;
            conn.Open();
            try
            {
                string tmp = treeList1.FocusedNode.GetDisplayText("indexname");
                //根据指标体系名查找指标体系编号
                String strCommand1 = "SELECT * FROM TB_INDEX t WHERE t.indexname = '" + tmp + "'";
                OleDbCommand myCommand1 = new OleDbCommand(strCommand1, conn);
                OleDbDataReader reader1;
                reader1 = myCommand1.ExecuteReader(); //执行command并得到相应的DataReader
                reader1.Read();
                int indexid = int.Parse(reader1["indexid"].ToString());//指标编号
                int indexsys = int.Parse(reader1["systemid"].ToString());//指标体系编号
                reader1.Close();
                //连接表
                string join = "SELECT * FROM TB_INDEX_INSTANCE a inner join TB_INDEX b ON b.indexid=a.indexid  WHERE a.systemid =" + indexsys;
                OleDbDataAdapter adp0 = new OleDbDataAdapter(join, conn);
                DataTable dtjoin = new DataTable();
                adp0.Fill(dtjoin);
                dtjoin.Columns.Remove("a.systemid");
                dtjoin.Columns.Remove("a.indexid");
                dtjoin.Columns.Remove("ID");

                //查找该指标体系的所有指标编号
                string newinst = "SELECT * FROM TB_INDEX f WHERE f.systemid =" + indexsys;
                OleDbDataAdapter adp1 = new OleDbDataAdapter(newinst, conn);
                DataTable dtjoin1 = new DataTable();
                adp1.Fill(dtjoin1);

                //添加DataTable2的数据                
                if (dtjoin.Rows.Count == 0)
                {
                    for (int i = 0; i < dtjoin1.Rows.Count; i++)
                    {
                        dtjoin.Rows.Add(new object[] { null, 1, null, int.Parse(dtjoin1.Rows[i]["indexid"].ToString()), int.Parse(dtjoin1.Rows[i]["pid"].ToString()), dtjoin1.Rows[i]["indexname"].ToString(), indexsys });
                    }
                }
                else
                {
                    //取出实例编号这列
                    int[] num = dtjoin.AsEnumerable().Select(d => d.Field<int>("instanceid")).ToArray();
                    IEnumerable<int> diff = num.Distinct<int>();
                    for (int i = 0; i < dtjoin1.Rows.Count; i++)
                    {

                        dtjoin.Rows.Add(new object[] { null, diff.Max() + 1, null, int.Parse(dtjoin1.Rows[i]["indexid"].ToString()), int.Parse(dtjoin1.Rows[i]["pid"].ToString()), dtjoin1.Rows[i]["indexname"].ToString(), indexsys });
                    }
                }
                //取出实例编号这列
                int[] num1 = dtjoin.AsEnumerable().Select(d => d.Field<int>("instanceid")).ToArray();
                IEnumerable<int> diff1 = num1.Distinct<int>();
                //获取该指标体系的最大节点编号
                int[] instanceindexid = dtjoin.AsEnumerable().Select(d => d.Field<int>("b.indexid")).ToArray();
                IEnumerable<int> instanceindexidnum = instanceindexid.Distinct<int>();
                int maxindexid = instanceindexidnum.Max();
                //不同实力编号不一样，但是节点编号一样，要加载到同一个treelist必须节点编号不一样
                foreach (int i in diff1)
                {
                    for (int j = 0; j < dtjoin.Rows.Count; j++)
                    {
                        if (dtjoin.Rows[j]["instanceid"].ToString() == i.ToString() && dtjoin.Rows[j]["pid"].ToString() != "0")//查询条件 
                        {
                            dtjoin.Rows[j]["b.indexid"] = (int.Parse(dtjoin.Rows[j]["b.indexid"].ToString()) + (i - 1) * maxindexid).ToString();
                            dtjoin.Rows[j]["pid"] = (int.Parse(dtjoin.Rows[j]["pid"].ToString()) + (i - 1) * maxindexid).ToString();
                        }
                        else if (dtjoin.Rows[j]["instanceid"].ToString() == i.ToString() && dtjoin.Rows[j]["pid"].ToString() == "0")
                        {
                            dtjoin.Rows[j]["b.indexid"] = (int.Parse(dtjoin.Rows[j]["b.indexid"].ToString()) + (i - 1) * maxindexid).ToString();
                        }
                    }
                }
                //清空数据源
                treeList2.DataSource = null;
                dtjoin.Columns.Remove("instanceid");
                dtjoin.Columns.Remove("b.systemid");
                dtjoin.Columns["indexname"].SetOrdinal(0);
                dtjoin.Columns["indexvalue"].SetOrdinal(1);
                dtjoin.Columns["instancename"].SetOrdinal(2);
                treeList2.DataSource = dtjoin;
                treeList2.Columns[0].Caption = "指标体系";
                treeList2.Columns[1].Caption = "指标权重";
                treeList2.Columns[2].Caption = "实例名称";
                treeList2.KeyFieldName = "b.indexid";
                treeList2.ParentFieldName = "pid";

                conn.Close();
            }
            catch (System.Exception ex)
            {
                conn.Close();
                throw new Exception("[ERROR] 数据库操作出现异常：" + ex.Message);
            }
        }
        private void treeList2_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (treeList1.FocusedNode != null)
                {
                    TreeListHitInfo hInfo = treeList2.CalcHitInfo(new Point(e.X, e.Y));
                    TreeListNode node = hInfo.Node;

                    if (tag == 1)
                    {
                        treeList2.ContextMenuStrip = contextMenuStrip2;
                    }
                }
            }
        }
        private void simpleButton2_Click(object sender, EventArgs e)
        {
            //添加进数据库
            OleDbConnection conn = DBHelper.getConn(); //得到连接对象
            labelControl3.Visible = false;
            conn.Open();
            try
            {

                string tmp = treeList1.FocusedNode.GetDisplayText("indexname");
                //根据指标体系名查找指标体系编号
                String strCommand1 = "SELECT * FROM TB_INDEX t WHERE t.indexname = '" + tmp + "'";
                OleDbCommand myCommand1 = new OleDbCommand(strCommand1, conn);
                OleDbDataReader reader1;
                reader1 = myCommand1.ExecuteReader(); //执行command并得到相应的DataReader
                reader1.Read();
                int indexid = int.Parse(reader1["indexid"].ToString());//指标编号
                int indexsys = int.Parse(reader1["systemid"].ToString());//指标体系编号
                reader1.Close();
                //连接表
                string join = "SELECT * FROM TB_INDEX_INSTANCE a inner join TB_INDEX b ON b.indexid=a.indexid  WHERE a.systemid =" + indexsys;
                OleDbDataAdapter adp0 = new OleDbDataAdapter(join, conn);
                DataTable dtjoin = new DataTable();
                adp0.Fill(dtjoin);
                if (dtjoin.Rows.Count == 0)
                {
                    treeList2.DataSource = null;
                    labelControl3.Visible = true;
                    conn.Close();
                    完成创建simpleButton1.Visible = false;
                    取消创建simpleButton2.Visible = false;
                    return;
                }
                //取出实例编号这列
                int[] num = dtjoin.AsEnumerable().Select(d => d.Field<int>("instanceid")).ToArray();
                IEnumerable<int> diff = num.Distinct<int>();
                //获取该指标体系的最大节点编号
                int[] instanceindexid = dtjoin.AsEnumerable().Select(d => d.Field<int>("a.indexid")).ToArray();
                IEnumerable<int> instanceindexidnum = instanceindexid.Distinct<int>();
                int maxindexid = instanceindexidnum.Max();
                //不同实力编号不一样，但是节点编号一样，要加载到同一个treelist必须节点编号不一样
                foreach (int i in diff)
                {
                    for (int j = 0; j < dtjoin.Rows.Count; j++)
                    {
                        if (dtjoin.Rows[j]["instanceid"].ToString() == i.ToString() && dtjoin.Rows[j]["pid"].ToString() != "0")//查询条件 
                        {
                            dtjoin.Rows[j]["a.indexid"] = (int.Parse(dtjoin.Rows[j]["a.indexid"].ToString()) + (i - 1) * maxindexid).ToString();
                            dtjoin.Rows[j]["pid"] = (int.Parse(dtjoin.Rows[j]["pid"].ToString()) + (i - 1) * maxindexid).ToString();
                        }
                        else if (dtjoin.Rows[j]["instanceid"].ToString() == i.ToString() && dtjoin.Rows[j]["pid"].ToString() == "0")
                        {
                            dtjoin.Rows[j]["a.indexid"] = (int.Parse(dtjoin.Rows[j]["a.indexid"].ToString()) + (i - 1) * maxindexid).ToString();
                        }
                    }
                }
                //清空数据源
                treeList2.DataSource = null;
                dtjoin.Columns.Remove("a.systemid");
                dtjoin.Columns.Remove("instanceid");
                dtjoin.Columns.Remove("b.indexid");
                dtjoin.Columns.Remove("b.systemid");
                dtjoin.Columns.Remove("ID");
                dtjoin.Columns["indexname"].SetOrdinal(0);
                dtjoin.Columns["indexvalue"].SetOrdinal(1);
                dtjoin.Columns["instancename"].SetOrdinal(2);
                dtjoin.Columns["a.indexid"].SetOrdinal(3);
                dtjoin.Columns["pid"].SetOrdinal(4);
                treeList2.DataSource = dtjoin;
                treeList2.Columns[0].Caption = "指标体系";
                treeList2.Columns[1].Caption = "指标权重";
                treeList2.Columns[2].Caption = "实例名称";
                treeList2.KeyFieldName = "a.indexid";
                treeList2.ParentFieldName = "pid";

                conn.Close();
            }
            catch (System.Exception ex)
            {
                conn.Close();
                throw new Exception("[ERROR] 数据库操作出现异常：" + ex.Message);
            }
            完成创建simpleButton1.Visible = false;
            取消创建simpleButton2.Visible = false;
        }
        private void 完成创建simpleButton1_Click(object sender, EventArgs e)
        {
            //新加的部分填进数据库，首先要判断，原实例表中有n行，从treelist的n+1行开始填入
            OleDbConnection conn = DBHelper.getConn(); //得到连接对象
            conn.Open();
            try
            {
                string tmp = treeList1.FocusedNode.GetDisplayText("indexname");
                //根据指标体系名查找指标体系编号
                String strCommand1 = "SELECT * FROM TB_INDEX t WHERE t.indexname = '" + tmp + "'";
                OleDbCommand myCommand1 = new OleDbCommand(strCommand1, conn);
                OleDbDataReader reader1;
                reader1 = myCommand1.ExecuteReader(); //执行command并得到相应的DataReader
                reader1.Read();
                int indexsys = int.Parse(reader1["systemid"].ToString());//指标体系编号
                reader1.Close();
                //连接表
                string join = "SELECT * FROM TB_INDEX_INSTANCE a inner join TB_INDEX b ON b.indexid=a.indexid  WHERE a.systemid =" + indexsys;
                OleDbDataAdapter adp0 = new OleDbDataAdapter(join, conn);
                DataTable dtjoin = new DataTable();
                adp0.Fill(dtjoin);
                int rows = dtjoin.Rows.Count;
                //取出实例编号这列
                int[] num = dtjoin.AsEnumerable().Select(d => d.Field<int>("instanceid")).ToArray();
                IEnumerable<int> diff = num.Distinct<int>();
                //查找该指标体系的所有指标编号
                string newinst = "SELECT * FROM TB_INDEX f WHERE f.systemid =" + indexsys;
                OleDbDataAdapter adp1 = new OleDbDataAdapter(newinst, conn);
                DataTable dtjoin1 = new DataTable();
                adp1.Fill(dtjoin1);
                //获取treelist第rows+1行填入access。
                int j = 0;
                for (int i = rows; i < treeList2.AllNodesCount; i++)
                {
                    //查询 定位 根据行序号  
                    TreeListNode node3 = treeList2.FindNodeByID(i);
                    //int a=node3.Id;
                    //int b = node3.Nodes.Count;
                    //定位后 把节点设置为焦点  
                    //treeList2.FocusedNode = node3;
                    DataRowView rov = treeList2.GetDataRecordByNode(node3) as DataRowView;
                    DataRow dr = rov.Row;
                    //string test = dr["pid"].ToString();
                    // newta.Rows.Add(dr);
                    //加入实例表
                    String strCommand = "INSERT INTO TB_INDEX_INSTANCE (systemid,indexid,indexvalue,instanceid,instancename) VALUES (?,?,?,?,?)";
                    OleDbCommand command = new OleDbCommand(strCommand, conn);
                    OleDbParameterCollection paramCollection = command.Parameters;
                    OleDbParameter param1 = paramCollection.Add("systemid", OleDbType.VarChar);
                    param1.Value = indexsys;
                    OleDbParameter param2 = paramCollection.Add("indexid", OleDbType.VarChar);
                    param2.Value = int.Parse(dtjoin1.Rows[j]["indexid"].ToString()); j++;
                    OleDbParameter param3 = paramCollection.Add("indexvalue", OleDbType.VarChar);
                    if (dr["indexvalue"].ToString() == "")
                    {
                        MessageBox.Show("指标权重信息未填充完整，确认后不能修改！");
                        conn.Close();
                        return;
                    }
                    else
                    {
                        param3.Value = dr["indexvalue"];
                    }

                    OleDbParameter param4 = paramCollection.Add("instanceid", OleDbType.VarChar);
                    if (num.Length == 0)
                    { param4.Value = 1; }
                    else
                    {
                        param4.Value = diff.Max() + 1;
                    }
                    OleDbParameter param5 = paramCollection.Add("instancename", OleDbType.VarChar);
                    //只需写第一个实例名。自动补齐后面的。
                    TreeListNode node4 = treeList2.FindNodeByID(rows);
                    DataRowView rov1 = treeList2.GetDataRecordByNode(node4) as DataRowView;
                    DataRow dr1 = rov1.Row;
                    if (dr1["instancename"].ToString() == "")
                    {
                        MessageBox.Show("实例名称信息未填充完整，确认后不能修改！");
                        conn.Close();
                        return;
                    }
                    else
                    {
                        param5.Value = dr1["instancename"];
                    }
                    command.ExecuteNonQuery();

                }
                conn.Close();
            }
            catch (System.Exception ex)
            {
                conn.Close();
                throw new Exception("[ERROR] 数据库操作出现异常：" + ex.Message);
            }
            完成创建simpleButton1.Visible = false;
            取消创建simpleButton2.Visible = false;
        }
        /// <summary> 
        /// 设置TreeList显示的图标 
        /// </summary> 
        /// <param name="tl">TreeList组件</param> 
        /// <param name="node">当前结点，从根结构递归时此值必须=null</param> 
        /// <param name="nodeIndex">根结点图标(无子结点)</param> 
        /// <param name="parentIndex">有子结点的图标</param> 
        public void SetImageIndex(TreeList tl, TreeListNode node, int nodeIndex, int parentIndex)
        {
            if (node == null)
            {
                foreach (TreeListNode N in tl.Nodes)
                    SetImageIndex(tl, N, nodeIndex, parentIndex);
            }
            else
            {
                if (node.HasChildren || node.ParentNode == null)
                {
                    //node.SelectImageIndex = parentIndex; 
                    node.StateImageIndex = parentIndex;
                    node.ImageIndex = parentIndex;
                }
                else
                {
                    //node.SelectImageIndex = nodeIndex; 
                    node.StateImageIndex = nodeIndex;
                    node.ImageIndex = nodeIndex;
                }

                foreach (TreeListNode N in node.Nodes)
                {
                    SetImageIndex(tl, N, nodeIndex, parentIndex);
                }
            }
        }
        private void 实例分析ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (treeList2.FocusedNode == null)
            {
                MessageBox.Show("请选择实例！");
                return;
            }
            if (treeList2.FocusedNode.Nodes.Count > 0)
            {
                instanceAnalysis instan = new instanceAnalysis();
                instan.Show();
            }
        }
        private void MainForm_Load(object sender, EventArgs e)
        {
            xtraTabControl2.Region = new Region(new RectangleF(xtraTabPage3.Left, xtraTabPage3.Top, xtraTabPage4.Width, xtraTabPage4.Height));
        }
        #endregion

        private void barButtonItem65_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            xtraTabControl2.SelectedTabPage = xtraTabPage4;
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "(*.xls,*.xlsx)|*.xls;*.xlsx";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string fullFileName = openFileDialog.FileName;
                string[] sheetNames = ExcelDataBaseHelper.GetSheetsNames(fullFileName, true);
                /*
                if (sheetSelectForm == null || sheetSelectForm.IsDisposed)
                {
                    sheetSelectForm = new SheetSelectForm();
                    sheetSelectForm.SheetSelectedEvent += new SheetSelectedEventHandler(SheetSelectedEventHandler);
                }*/
                SheetSelectForm sheetSelectForm = new SheetSelectForm();
                sheetSelectForm.SheetSelectedEvent += new SheetSelectedEventHandler(SheetSelectedEventHandler);
                sheetSelectForm.SheetNames = sheetNames;
                sheetSelectForm.FullFileName = fullFileName;
                sheetSelectForm.ShowDialog();
            }
        }

        private void barButtonItem66_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (this.rawDataTable == null)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("请先导入原始数据！", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            }

            this.assignTable = this.rawDataTable;

            if (assignmentForm == null || assignmentForm.IsDisposed)
            {
                assignmentForm = new AssignmentForm();
            }

            if (this.assignTable != null)
            {
                string[] colNames = ExcelDataBaseHelper.GetColNames(this.assignTable);
                assignmentForm.ColNames = colNames;
            }
            assignmentForm.FieldSelectEvent += new EventHandler(assignmentForm_FieldSelectEvent);
            assignmentForm.AssignmentEvent += new AssignmentEventHandler(assignmentForm_AssignmentEvent);
            //assignmentForm.TopMost = true;
            assignmentForm.Show();
        }

        private void barButtonItem67_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (this.rawDataTable == null)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("请先导入原始数据！", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                return;
            }
            /*
            if (standardForm == null || standardForm.IsDisposed)
            {
                standardForm = new StandardForm();
                standardForm.StandardEvent +=new StandardEventHandler(standardForm_StandardEvent);
            }
            */
            StandardForm standardForm = new StandardForm();
            standardForm.StandardEvent += new StandardEventHandler(standardForm_StandardEvent);

            if (this.rawDataTable != null)
            {
                string[] colNames = ExcelDataBaseHelper.GetColNames(this.rawDataTable);
                standardForm.ColNames = colNames;
            }
            standardForm.Show();
        }

        private void barButtonItem68_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "(*.xls,*.xlsx)|*.xls;*.xlsx";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                ExportSettings.DefaultExportType = ExportType.WYSIWYG;

                FileInfo fileInfo = new FileInfo(saveFileDialog.FileName);
                if (fileInfo.Extension.Equals(".xls"))
                {
                    switch (tabPane1.SelectedPageIndex)
                    {
                        case 0:
                            rawDataGrid.ExportToXls(saveFileDialog.FileName);
                            break;
                        case 1:
                            assignGrid.ExportToXls(saveFileDialog.FileName);
                            break;
                        case 2:
                            standardGrid.ExportToXls(saveFileDialog.FileName);
                            break;
                        case 3:
                            resultGrid.ExportToXls(saveFileDialog.FileName);
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    switch (tabPane1.SelectedPageIndex)
                    {
                        case 0:
                            rawDataGrid.ExportToXlsx(saveFileDialog.FileName);
                            break;
                        case 1:
                            assignGrid.ExportToXlsx(saveFileDialog.FileName);
                            break;
                        case 2:
                            standardGrid.ExportToXlsx(saveFileDialog.FileName);
                            break;
                        case 3:
                            resultGrid.ExportToXlsx(saveFileDialog.FileName);
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        private void barButtonItem69_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            switch (tabPane1.SelectedPageIndex)
            {
                case 0:
                    if (!rawDataGrid.IsPrintingAvailable)
                    {
                        DevExpress.XtraEditors.XtraMessageBox.Show("打印功能出错！", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                        return;
                    }
                    rawDataGrid.ShowPrintPreview();
                    break;
                case 1:
                    if (!assignGrid.IsPrintingAvailable)
                    {
                        DevExpress.XtraEditors.XtraMessageBox.Show("打印功能出错！", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                        return;
                    }
                    assignGrid.ShowPrintPreview();
                    break;
                case 2:
                    if (!standardGrid.IsPrintingAvailable)
                    {
                        DevExpress.XtraEditors.XtraMessageBox.Show("打印功能出错！", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                        return;
                    }
                    standardGrid.ShowPrintPreview();
                    break;
                case 3:
                    if (!resultGrid.IsPrintingAvailable)
                    {
                        DevExpress.XtraEditors.XtraMessageBox.Show("打印功能出错！", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                        return;
                    }
                    resultGrid.ShowPrintPreview();
                    break;
                default:
                    break;
            }
        }

        private void barButtonItem70_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Application.Exit();//主窗口关闭，退出程序
        }

        private void ribbonControl_Click(object sender, EventArgs e)
        {
            xtraTabControl2.SelectedTabPage = xtraTabPage4;
        }

        private void barButtonItem72_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            MapForm mapForm = new MapForm();
            if(this.resultTable != null)
            {
                mapForm.table = this.resultTable;
            }
            mapForm.ShowDialog();
        }

        private void barButtonItem74_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            string path = Application.StartupPath + "\\Resources\\chart1.jpg";
            ChartForm chartForm = new ChartForm(path);
            chartForm.ShowDialog();
        }

        private void barButtonItem76_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            string path = Application.StartupPath + "\\Resources\\chart2.jpg";
            ChartForm chartForm = new ChartForm(path);
            chartForm.ShowDialog();
        }

        private void barButtonItem71_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.splitContainerControl1.PanelVisibility = DevExpress.XtraEditors.SplitPanelVisibility.Both;
        }

        private void barButtonItem53_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            string path = Application.StartupPath + "\\Resources\\aph.pdf";
            PDFViewer pdfViewer = new PDFViewer(path);
            pdfViewer.ShowDialog();
        }

        private void barButtonItem73_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            string path = Application.StartupPath + "\\Resources\\zhzsf.pdf";
            PDFViewer pdfViewer = new PDFViewer(path);
            pdfViewer.ShowDialog();
        }

        private void barButtonItem54_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            string path = Application.StartupPath + "\\Resources\\help.pdf";
            PDFViewer pdfViewer = new PDFViewer(path);
            pdfViewer.ShowDialog();
        }

        private void barButtonItem77_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "(*.jpg,*.png)|*.jpg;*.png";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                FileInfo fileInfo = new FileInfo(saveFileDialog.FileName);
                if (fileInfo.Extension.Equals(".jpg"))
                {
                    SaveChartImageToFile(this.resultChart, ImageFormat.Jpeg, saveFileDialog.FileName);
                }
                else if (fileInfo.Extension.Equals(".png"))
                {
                    SaveChartImageToFile(this.resultChart, ImageFormat.Png, saveFileDialog.FileName);
                }
            }
        }
        private void SaveChartImageToFile(ChartControl chart, ImageFormat format, String fileName)
        {
            chart.ExportToImage(fileName, format);
        }
    }
}

