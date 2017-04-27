using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Columns;
using DevExpress.XtraTreeList.Nodes;
using DevExpress.XtraEditors.Repository;
using EvaluationSystem.Entity;
using EvaluationSystem.Service;
using EvaluationSystem.Service.ServiceImpl;
using System.Collections;
using EvaluationSystem.Util;
using EvaluationSystem.Event;

namespace EvaluationSystem.ViewForm
{
    public partial class CalculateForm : Form
    {
        private IndexSystem indexSystem;
        private IndexService indexService;
        private Hashtable hashTable;
        private string[] colNames;
        private DataTable stdTable;
        private DataTable resultTable;
        public event CalcCompleteEventHandler CalcCompleteEvent;

        public IndexSystem IndexSystem
        {
            get { return this.indexSystem; }
            set { this.indexSystem = value;}
        }

        public string[] ColNames
        {
            get { return this.colNames; }
            set 
            { 
                this.colNames = value; 
            }
        }

        public CalculateForm(string[] colNames, IndexSystem indexSystem,DataTable dataTable)
        {
            InitializeComponent();
            this.indexService = new IndexServiceImpl();
            this.colNames = colNames;
            this.indexSystem = indexSystem;
            this.stdTable = dataTable;
            hashTable = FillHashTable();
            FillIndexInstanceList();

            CreateColumns(tlMatch);
        }

        private Hashtable FillHashTable()
        {
            Hashtable hashTable = new Hashtable();
            foreach (string colName in this.colNames)
            {
                if (!hashTable.ContainsKey(colName))
                {
                    hashTable.Add(colName, colName);
                }
            }
            return hashTable;
        }

        public void FillIndexInstanceList()
        {
            this.lsbIndexInstance.DataSource = this.indexService.FindDistinctIndexInstance(this.indexSystem.Systemid);
        }

        public void InitializeTreeList()
        {
            tlMatch.KeyFieldName = "indexid";
            tlMatch.ParentFieldName = "pid";

            DataSet dataSet = this.indexService.FindAllIndex(this.indexSystem.Systemid);
            DataTable tb = dataSet.Tables[0];
            DataColumn col1 = new DataColumn("indexvalue", System.Type.GetType("System.Double"));
            tb.Columns.Add(col1);

            DataColumn col2 = new DataColumn("colname", System.Type.GetType("System.String"));
            tb.Columns.Add(col2);
            col2.DefaultValue = "";

            //字段列名自动匹配处理
            for (int i = 0; i < tb.Rows.Count; i++)
            {
                if (hashTable.ContainsKey(tb.Rows[i]["indexname"]))
                {
                    tb.Rows[i]["colname"] = hashTable[tb.Rows[i]["indexname"]].ToString();
                }
            }

            tlMatch.DataSource = tb;
            tlMatch.ExpandAll();
            tlMatch.BestFitColumns();
        }

        private void CreateColumns(TreeList tl)
        {
            // 创建列
            tl.BeginUpdate();
            TreeListColumn col1 = tl.Columns.Add();
            col1.OptionsColumn.AllowEdit = false;
            col1.Caption = "指标名称";
            col1.FieldName = "indexname";
            col1.VisibleIndex = 0;

            TreeListColumn col2 = tl.Columns.Add();
            col2.Caption = "对应列名";
            col2.FieldName = "colname";
            col2.VisibleIndex = 1;

            TreeListColumn col3 = tl.Columns.Add();
            col3.Caption = "权重";
            col3.FieldName = "indexvalue";
            col3.VisibleIndex = 2;

            RepositoryItemComboBox riCombo = new RepositoryItemComboBox();
            if(this.colNames!=null && this.colNames.Length>0)
            {
                riCombo.Items.AddRange(this.colNames);
                tl.RepositoryItems.Add(riCombo);
                col2.ColumnEdit = riCombo;
            }
            tl.EndUpdate();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnStartCalc_Click(object sender, EventArgs e)
        {
            //先检查是否填写权重和匹配字段
            DataTable tb = (DataTable)tlMatch.DataSource;
            for (int i = 0; i < tb.Rows.Count; i++)
            {
                if (tb.Rows[i]["indexvalue"].ToString().Equals(""))
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show("请先完成所有指标权重设置！", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                    return;
                }
            }

            //检查列名是否全部匹配
            MatchValidateOperation matchValidateOperation = new MatchValidateOperation();
            tlMatch.NodesIterator.DoOperation(matchValidateOperation);
            if (!matchValidateOperation.Result)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("请先完成所有列名匹配！", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                return;
            }

            //检查列名是否重复匹配
            Hashtable hashTable = new Hashtable();
            for (int i = 0; i < tb.Rows.Count; i++)
            {
                string colname = tb.Rows[i]["colname"].ToString();
                if(colname.Equals(""))continue;
                if (hashTable.ContainsKey(colname))
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show(colname+"列,重复匹配！", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                    return;
                }
                else 
                {
                    hashTable.Add(colname,colname);
                }
            }

            if(this.stdTable != null)
            {
                this.resultTable = new DataTable();
                foreach(DataColumn col in this.stdTable.Columns)
                {
                    if (!hashTable.ContainsKey(col.ColumnName))
                    {
                        this.resultTable.Columns.Add(new DataColumn(col.ColumnName, col.DataType));
                    }
                }

                //查找有子节点的列
                HasChildrenOperation hasChildrenOperation = new HasChildrenOperation();
                tlMatch.NodesIterator.DoOperation(hasChildrenOperation);
                foreach (TreeListNode node in hasChildrenOperation.ResultList)
                {
                    this.resultTable.Columns.Add(new DataColumn(node["indexname"].ToString(), Type.GetType("System.Double")));
                    hashTable.Add(node["indexname"].ToString(), node["indexname"].ToString());
                }

                //遍历赋值
                for (int i = 0; i < this.stdTable.Rows.Count; i++)
                {
                    DataRow row = this.resultTable.NewRow();
                    foreach (DataColumn col in this.resultTable.Columns)
                    {
                        if (!hashTable.ContainsKey(col.ColumnName))
                        {
                            row[col] = this.stdTable.Rows[i][col.ColumnName].ToString();
                        }
                        else 
                        {
                            row[col] = CalValueRecursive(col.ColumnName, this.stdTable.Rows[i]);
                        }
                    }
                    this.resultTable.Rows.Add(row);
                }

                if (CalcCompleteEvent != null)
                {
                    CalcCompleteEventArgs args = new CalcCompleteEventArgs();
                    args.ResultTable = this.resultTable;
                    CalcCompleteEvent(this,args);
                }
            }

            this.Close();
        }


        //递归计算汇总指标值
        private double CalValueRecursive(string indexname,DataRow row)
        {
            TreeListNode node = tlMatch.FindNodeByFieldValue("indexname",indexname);
            double result = 0;
            if (!node.HasChildren)
            {
                result += double.Parse(node["indexvalue"].ToString()) * double.Parse(row[node["colname"].ToString()].ToString());
            }
            else 
            {
                foreach (TreeListNode n in node.Nodes)
                {
                    result += CalValueRecursive(n["indexname"].ToString(), row);
                }
                result = double.Parse(node["indexvalue"].ToString()) * result;
            }
            return result;
        }

        private void tlMatch_ShowingEditor(object sender, CancelEventArgs e)
        {
            if (tlMatch.FocusedColumn.AbsoluteIndex == 1)
            {
                TreeListNode node = tlMatch.FocusedNode;
                if (node.HasChildren)
                {
                    e.Cancel = true;
                    DevExpress.XtraEditors.XtraMessageBox.Show("该指标无需选择对应列名！", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                }
                else
                {
                    e.Cancel = false;
                }
            }
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            if (this.lsbIndexInstance.SelectedItem == null)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("请先选择指标实例！", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            }
            else 
            {
                tlMatch.KeyFieldName = "indexid";
                tlMatch.ParentFieldName = "pid";
                IndexInstance indexInstance = (IndexInstance)this.lsbIndexInstance.SelectedItem;
                DataSet dataSet = this.indexService.FindAllInstance(this.indexSystem.Systemid,indexInstance.Instanceid);
                DataTable tb = dataSet.Tables[0];

                DataColumn col1 = new DataColumn("colname", System.Type.GetType("System.String"));
                tb.Columns.Add(col1);
                col1.DefaultValue = "";

                DataTable oldtb = (DataTable)tlMatch.DataSource;
                //字段列名自动匹配处理
                for (int i = 0; i < tb.Rows.Count; i++)
                {
                    tb.Rows[i]["colname"] = oldtb.Rows[i]["colname"];
                }

                tlMatch.DataSource = tb;
                tlMatch.ExpandAll();
                tlMatch.BestFitColumns();
            }
        }
    }
}
