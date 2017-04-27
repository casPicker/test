using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Data.OleDb;
using DevExpress.XtraTreeList.Nodes;
namespace EvaluationSystem.IndexSys
{
    public partial class instanceAnalysis : Form
    {
        public instanceAnalysis()
        {
            InitializeComponent();
        }
        public DataTable tblDatas = new DataTable();

        private void instanceAnalysis_Load(object sender, EventArgs e)
        {
            //获取treelist2当前结点
            if (MainForm.pCurrentWin.treeList2.FocusedNode.Nodes.Count > 0)
            {
                #region 方法一:
                
                DataColumn dc = null;                              
                dc = tblDatas.Columns.Add("indexname", Type.GetType("System.String"));
                dc = tblDatas.Columns.Add("indexvalue", Type.GetType("System.String"));  
                //dc.AutoIncrement = true;//自动增加 
                //dc.AutoIncrementSeed = 1;//起始为1 
                //dc.AutoIncrementStep = 1;//步长为1 
                dc.AllowDBNull = false;
                #endregion
                foreach (TreeListNode node in MainForm.pCurrentWin.treeList2.FocusedNode.Nodes)
                {
                    DataRow newRow;
                    newRow = tblDatas.NewRow();
                    newRow["indexname"] = node.GetValue("indexname").ToString();
                    newRow["indexvalue"] = node.GetValue("indexvalue").ToString() ; 
                    tblDatas.Rows.Add(newRow);                   
                }

                if (rbtnPie.Checked)
                {
                    //OleDbConnection conn = DBHelper.getConn(); //得到连接对象
                    //conn.Open();
                    ////查找该指标体系的所有指标编号

                    //string newinst = "SELECT * FROM TB_INDEX_INSTANCE a inner join TB_INDEX b ON b.indexid=a.indexid  WHERE a.systemid =" + MainForm.pCurrentWin.treeList2.FocusedNode.GetValue("a.systemid") + "AND a.instanceid =" + MainForm.pCurrentWin.treeList2.FocusedNode.GetValue("instanceid");
                    //OleDbDataAdapter adp1 = new OleDbDataAdapter(newinst, conn);
                    //DataTable dtjoin1 = new DataTable();
                    //adp1.Fill(dtjoin1);
                    //conn.Close();
                    chartType.Series.Clear();
                    chartType.ChartAreas.Clear();

                    Series Series1 = new Series();
                    chartType.Series.Add(Series1);
                    chartType.Series["Series1"].ChartType = SeriesChartType.Pie;
                    chartType.Legends[0].Enabled = true;
                    chartType.Series["Series1"].LegendText = "#INDEX:#VALX";//开启图例
                    chartType.Series["Series1"].Label = "#INDEX:#PERCENT";
                    chartType.Series["Series1"].IsXValueIndexed = false;
                    chartType.Series["Series1"].IsValueShownAsLabel = false;
                    chartType.Series["Series1"]["PieLineColor"] = "Black";//连线颜色
                    chartType.Series["Series1"]["PieLabelStyle"] = "Outside";//标签位置
                    chartType.Series["Series1"].ToolTip = "#VALX";//显示提示用语
                    ChartArea ChartArea1 = new ChartArea();
                    chartType.ChartAreas.Add(ChartArea1);
                    //开启三维模式的原因是为了避免标签重叠
                    chartType.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = true;//开启三维模式;PointDepth:厚度BorderWidth:边框宽
                    chartType.ChartAreas["ChartArea1"].Area3DStyle.Rotation = 15;//起始角度
                    chartType.ChartAreas["ChartArea1"].Area3DStyle.Inclination = 45;//倾斜度(0～90)
                    chartType.ChartAreas["ChartArea1"].Area3DStyle.LightStyle = LightStyle.Realistic;//表面光泽度

                    List<string> xData = new List<string>();
                    List<string> yData = new List<string>();
                    for (int i = 0; i < tblDatas.Rows.Count; i++)
                    {
                        xData.Add(tblDatas.Rows[i][0].ToString());
                        yData.Add(tblDatas.Rows[i][1].ToString());
                    }
                    chartType.DataSource = tblDatas;
                    chartType.DataBind();
                    chartType.Series[0].XValueMember = "indexname";// xData.ToString(); ;
                    chartType.Series[0].YValueMembers = "indexvalue";// yData.ToString(); ;
                }

            }
        }
        private void rbtnPie_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtnPie.Checked)
            {
                //OleDbConnection conn = DBHelper.getConn(); //得到连接对象
                //conn.Open();
                ////查找该指标体系的所有指标编号

                //string newinst = "SELECT * FROM TB_INDEX_INSTANCE a inner join TB_INDEX b ON b.indexid=a.indexid  WHERE a.systemid =" + MainForm.pCurrentWin.treeList2.FocusedNode.GetValue("a.systemid") + "AND a.instanceid =" + MainForm.pCurrentWin.treeList2.FocusedNode.GetValue("instanceid");
                //OleDbDataAdapter adp1 = new OleDbDataAdapter(newinst, conn);
                //DataTable dtjoin1 = new DataTable();
                //adp1.Fill(dtjoin1);
                //conn.Close();
                chartType.Series.Clear();
                chartType.ChartAreas.Clear();

                Series Series1 = new Series();
                chartType.Series.Add(Series1);
                chartType.Series["Series1"].ChartType = SeriesChartType.Pie;
                chartType.Legends[0].Enabled = true;
                chartType.Series["Series1"].LegendText = "#INDEX:#VALX";//开启图例
                chartType.Series["Series1"].Label = "#INDEX:#PERCENT";
                chartType.Series["Series1"].IsXValueIndexed = false;
                chartType.Series["Series1"].IsValueShownAsLabel = false;
                chartType.Series["Series1"]["PieLineColor"] = "Black";//连线颜色
                chartType.Series["Series1"]["PieLabelStyle"] = "Outside";//标签位置
                chartType.Series["Series1"].ToolTip = "#VALX";//显示提示用语
                ChartArea ChartArea1 = new ChartArea();
                chartType.ChartAreas.Add(ChartArea1);
                //开启三维模式的原因是为了避免标签重叠
                chartType.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = true;//开启三维模式;PointDepth:厚度BorderWidth:边框宽
                chartType.ChartAreas["ChartArea1"].Area3DStyle.Rotation = 15;//起始角度
                chartType.ChartAreas["ChartArea1"].Area3DStyle.Inclination = 45;//倾斜度(0～90)
                chartType.ChartAreas["ChartArea1"].Area3DStyle.LightStyle = LightStyle.Realistic;//表面光泽度

                List<string> xData = new List<string>();
                List<string> yData = new List<string>();
                for (int i = 0; i < tblDatas.Rows.Count; i++)
                {
                    xData.Add(tblDatas.Rows[i][0].ToString());
                    yData.Add(tblDatas.Rows[i][1].ToString());
                }
                chartType.DataSource = tblDatas;
                chartType.DataBind();
                chartType.Series[0].XValueMember = "indexname";// xData.ToString(); ;
                chartType.Series[0].YValueMembers = "indexvalue";// yData.ToString(); ;
            }
        }

        private void rbtnBar_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtnBar.Checked)
            {
                //OleDbConnection conn = DBHelper.getConn(); //得到连接对象
                //conn.Open();
                ////查找该指标体系的所有指标编号

                //string newinst = "SELECT * FROM TB_INDEX_INSTANCE a inner join TB_INDEX b ON b.indexid=a.indexid  WHERE a.systemid =" + MainForm.pCurrentWin.treeList2.FocusedNode.GetValue("a.systemid") + "AND a.instanceid =" + MainForm.pCurrentWin.treeList2.FocusedNode.GetValue("instanceid");
                //OleDbDataAdapter adp1 = new OleDbDataAdapter(newinst, conn);
                //DataTable dtjoin1 = new DataTable();
                //adp1.Fill(dtjoin1);
                //conn.Close();

                chartType.Series.Clear();
                chartType.ChartAreas.Clear();
                Series Series1 = new Series();
                chartType.Series.Add(Series1);
                chartType.Series["Series1"].ChartType = SeriesChartType.Column;
                chartType.Legends[0].Enabled = false;
                chartType.Series["Series1"].LegendText = "";
                chartType.Series["Series1"].Label = "#VALY";
                chartType.Series["Series1"].ToolTip = "#VALX";
                chartType.Series["Series1"]["PointWidth"] = "0.5";
                ChartArea ChartArea1 = new ChartArea();
                chartType.ChartAreas.Add(ChartArea1);
                //开启三维模式的原因是为了避免标签重叠
                chartType.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = true;//开启三维模式;PointDepth:厚度BorderWidth:边框宽
                chartType.ChartAreas["ChartArea1"].Area3DStyle.Rotation = 15;//起始角度
                chartType.ChartAreas["ChartArea1"].Area3DStyle.Inclination = 30;//倾斜度(0～90)
                chartType.ChartAreas["ChartArea1"].Area3DStyle.LightStyle = LightStyle.Realistic;//表面光泽度
                chartType.ChartAreas["ChartArea1"].AxisX.Interval = 1; //决定x轴显示文本的间隔，1为强制每个柱状体都显示，3则间隔3个显示
                chartType.ChartAreas["ChartArea1"].AxisX.LabelStyle.Font = new Font("宋体", 9, FontStyle.Regular);
                chartType.ChartAreas["ChartArea1"].AxisX.MajorGrid.Enabled = false;

                List<string> xData = new List<string>();
                List<string> yData = new List<string>();
                for (int i = 0; i < tblDatas.Rows.Count; i++)
                {
                    xData.Add(tblDatas.Rows[i][0].ToString());
                    yData.Add(tblDatas.Rows[i][1].ToString());
                }
                chartType.DataSource = tblDatas;
                chartType.DataBind();
                chartType.Series[0].XValueMember = "indexname";// xData.ToString(); ;
                chartType.Series[0].YValueMembers = "indexvalue";// yData.ToString(); ;
            }
        }
        /*
        private void rbtnBar_CheckedChanged(object sender, EventArgs e)
        {
            OleDbConnection conn = DBHelper.getConn(); //得到连接对象
            conn.Open();
            //查找该指标体系的所有指标编号

            string newinst = "SELECT * FROM TB_INDEX_INSTANCE a inner join TB_INDEX b ON b.indexid=a.indexid  WHERE a.systemid =" + MainForm.pCurrentWin.treeList2.FocusedNode.GetValue("a.systemid") + "AND a.instanceid =" + MainForm.pCurrentWin.treeList2.FocusedNode.GetValue("instanceid");
            OleDbDataAdapter adp1 = new OleDbDataAdapter(newinst, conn);
            DataTable dtjoin1 = new DataTable();
            adp1.Fill(dtjoin1);
            conn.Close();
            List<int> pidcol = new List<int>();
            for (int i = 0; i < dtjoin1.Rows.Count; i++)
            {
                pidcol.Add(int.Parse(dtjoin1.Rows[i][7].ToString()));
            }
            IEnumerable<int> diff = pidcol.Distinct<int>();
            DataTable newdt1 = new DataTable();
            DataTable newdt2 = new DataTable();
            DataTable newdt3 = new DataTable();
            DataTable newdt4 = new DataTable();
            DataTable newdt5 = new DataTable();
            DataTable newdt6 = new DataTable();
            newdt1 = dtjoin1.Clone();
            newdt2 = dtjoin1.Clone();
            newdt3 = dtjoin1.Clone();
            newdt4 = dtjoin1.Clone();
            newdt5 = dtjoin1.Clone();
            newdt6 = dtjoin1.Clone();
            DataRow[] dr1 = dtjoin1.Select("pid =1");
            for (int i = 0; i < dr1.Length; i++)
            {
                newdt1.ImportRow((DataRow)dr1[i]);
            }
            DataRow[] dr2 = dtjoin1.Select("pid =2");
            for (int i = 0; i < dr2.Length; i++)
            {
                newdt2.ImportRow((DataRow)dr2[i]);
            }
            DataRow[] dr3 = dtjoin1.Select("pid =3");
            for (int i = 0; i < dr3.Length; i++)
            {
                newdt3.ImportRow((DataRow)dr3[i]);
            }
            DataRow[] dr4 = dtjoin1.Select("pid =4");
            for (int i = 0; i < dr4.Length; i++)
            {
                newdt4.ImportRow((DataRow)dr4[i]);
            }
            DataRow[] dr5 = dtjoin1.Select("pid =5");
            for (int i = 0; i < dr5.Length; i++)
            {
                newdt5.ImportRow((DataRow)dr5[i]);
            }
            DataRow[] dr6 = dtjoin1.Select("pid =23");
            for (int i = 0; i < dr6.Length; i++)
            {
                newdt6.ImportRow((DataRow)dr6[i]);
            }
            if (rbtnPie.Checked)
            {

                chart0.Series.Clear();
                chart0.ChartAreas.Clear();
                Series Series1 = new Series();
                chart0.Series.Add(Series1);
                chart0.Series["Series1"].ChartType = SeriesChartType.Pie;
                chart0.Legends[0].Enabled = true;
                chart0.Series["Series1"].LegendText = "#INDEX:#VALX";//开启图例
                chart0.Series["Series1"].Label = "#INDEX:#PERCENT";
                chart0.Series["Series1"].IsXValueIndexed = false;
                chart0.Series["Series1"].IsValueShownAsLabel = false;
                chart0.Series["Series1"]["PieLineColor"] = "Black";//连线颜色
                chart0.Series["Series1"]["PieLabelStyle"] = "Outside";//标签位置
                chart0.Series["Series1"].ToolTip = "#VALX";//显示提示用语
                ChartArea ChartArea1 = new ChartArea();
                chart0.ChartAreas.Add(ChartArea1);
                //开启三维模式的原因是为了避免标签重叠
                chart0.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = true;//开启三维模式;PointDepth:厚度BorderWidth:边框宽
                chart0.ChartAreas["ChartArea1"].Area3DStyle.Rotation = 15;//起始角度
                chart0.ChartAreas["ChartArea1"].Area3DStyle.Inclination = 45;//倾斜度(0～90)
                chart0.ChartAreas["ChartArea1"].Area3DStyle.LightStyle = LightStyle.Realistic;//表面光泽度
                chart0.DataSource = newdt1;
                chart0.DataBind();
                chart0.Series[0].XValueMember = "indexname";// xData.ToString(); ;
                chart0.Series[0].YValueMembers = "indexvalue";// yData.ToString(); ;

                chart1.Series.Clear();
                chart1.ChartAreas.Clear();
                Series Series2 = new Series();
                chart1.Series.Add(Series2);
                chart1.Series["Series2"].ChartType = SeriesChartType.Pie;
                chart1.Legends[0].Enabled = true;
                chart1.Series["Series2"].LegendText = "#INDEX:#VALX";//开启图例
                chart1.Series["Series2"].Label = "#INDEX:#PERCENT";
                chart1.Series["Series2"].IsXValueIndexed = false;
                chart1.Series["Series2"].IsValueShownAsLabel = false;
                chart1.Series["Series2"]["PieLineColor"] = "Black";//连线颜色
                chart1.Series["Series2"]["PieLabelStyle"] = "Outside";//标签位置
                chart1.Series["Series2"].ToolTip = "#VALX";//显示提示用语
                ChartArea ChartArea2 = new ChartArea();
                chart1.ChartAreas.Add(ChartArea2);
                //开启三维模式的原因是为了避免标签重叠
                chart1.ChartAreas["ChartArea2"].Area3DStyle.Enable3D = true;//开启三维模式;PointDepth:厚度BorderWidth:边框宽
                chart1.ChartAreas["ChartArea2"].Area3DStyle.Rotation = 15;//起始角度
                chart1.ChartAreas["ChartArea2"].Area3DStyle.Inclination = 45;//倾斜度(0～90)
                chart1.ChartAreas["ChartArea2"].Area3DStyle.LightStyle = LightStyle.Realistic;//表面光泽度
                chart1.DataSource = newdt2;
                chart1.DataBind();
                chart1.Series[0].XValueMember = "indexname";// xData.ToString(); ;
                chart1.Series[0].YValueMembers = "indexvalue";// yData.ToString(); ;

                chart2.Series.Clear();
                chart2.ChartAreas.Clear();
                Series Series3 = new Series();
                chart2.Series.Add(Series3);
                chart2.Series["Series3"].ChartType = SeriesChartType.Pie;
                chart2.Legends[0].Enabled = true;
                chart2.Series["Series3"].LegendText = "#INDEX:#VALX";//开启图例
                chart2.Series["Series3"].Label = "#INDEX:#PERCENT";
                chart2.Series["Series3"].IsXValueIndexed = false;
                chart2.Series["Series3"].IsValueShownAsLabel = false;
                chart2.Series["Series3"]["PieLineColor"] = "Black";//连线颜色
                chart2.Series["Series3"]["PieLabelStyle"] = "Outside";//标签位置
                chart2.Series["Series3"].ToolTip = "#VALX";//显示提示用语
                ChartArea ChartArea3 = new ChartArea();
                chart2.ChartAreas.Add(ChartArea3);
                //开启三维模式的原因是为了避免标签重叠
                chart2.ChartAreas["ChartArea3"].Area3DStyle.Enable3D = true;//开启三维模式;PointDepth:厚度BorderWidth:边框宽
                chart2.ChartAreas["ChartArea3"].Area3DStyle.Rotation = 15;//起始角度
                chart2.ChartAreas["ChartArea3"].Area3DStyle.Inclination = 45;//倾斜度(0～90)
                chart2.ChartAreas["ChartArea3"].Area3DStyle.LightStyle = LightStyle.Realistic;//表面光泽度
                chart2.DataSource = newdt3;
                chart2.DataBind();
                chart2.Series[0].XValueMember = "indexname";// xData.ToString(); ;
                chart2.Series[0].YValueMembers = "indexvalue";// yData.ToString(); ;

                chart3.Series.Clear();
                chart3.ChartAreas.Clear();
                Series Series4 = new Series();
                chart3.Series.Add(Series4);
                chart3.Series["Series4"].ChartType = SeriesChartType.Pie;
                chart3.Legends[0].Enabled = true;
                chart3.Series["Series4"].LegendText = "#INDEX:#VALX";//开启图例
                chart3.Series["Series4"].Label = "#INDEX:#PERCENT";
                chart3.Series["Series4"].IsXValueIndexed = false;
                chart3.Series["Series4"].IsValueShownAsLabel = false;
                chart3.Series["Series4"]["PieLineColor"] = "Black";//连线颜色
                chart3.Series["Series4"]["PieLabelStyle"] = "Outside";//标签位置
                chart3.Series["Series4"].ToolTip = "#VALX";//显示提示用语
                ChartArea ChartArea4 = new ChartArea();
                chart3.ChartAreas.Add(ChartArea4);
                //开启三维模式的原因是为了避免标签重叠
                chart3.ChartAreas["ChartArea4"].Area3DStyle.Enable3D = true;//开启三维模式;PointDepth:厚度BorderWidth:边框宽
                chart3.ChartAreas["ChartArea4"].Area3DStyle.Rotation = 15;//起始角度
                chart3.ChartAreas["ChartArea4"].Area3DStyle.Inclination = 45;//倾斜度(0～90)
                chart3.ChartAreas["ChartArea4"].Area3DStyle.LightStyle = LightStyle.Realistic;//表面光泽度
                chart3.DataSource = newdt4;
                chart3.DataBind();
                chart3.Series[0].XValueMember = "indexname";// xData.ToString(); ;
                chart3.Series[0].YValueMembers = "indexvalue";// yData.ToString(); ;

                chart4.Series.Clear();
                chart4.ChartAreas.Clear();
                Series Series5 = new Series();
                chart4.Series.Add(Series5);
                chart4.Series["Series5"].ChartType = SeriesChartType.Pie;
                chart4.Legends[0].Enabled = true;
                chart4.Series["Series5"].LegendText = "#INDEX:#VALX";//开启图例
                chart4.Series["Series5"].Label = "#INDEX:#PERCENT";
                chart4.Series["Series5"].IsXValueIndexed = false;
                chart4.Series["Series5"].IsValueShownAsLabel = false;
                chart4.Series["Series5"]["PieLineColor"] = "Black";//连线颜色
                chart4.Series["Series5"]["PieLabelStyle"] = "Outside";//标签位置
                chart4.Series["Series5"].ToolTip = "#VALX";//显示提示用语
                ChartArea ChartArea5 = new ChartArea();
                chart0.ChartAreas.Add(ChartArea5);
                //开启三维模式的原因是为了避免标签重叠
                chart4.ChartAreas["ChartArea5"].Area3DStyle.Enable3D = true;//开启三维模式;PointDepth:厚度BorderWidth:边框宽
                chart4.ChartAreas["ChartArea5"].Area3DStyle.Rotation = 15;//起始角度
                chart4.ChartAreas["ChartArea5"].Area3DStyle.Inclination = 45;//倾斜度(0～90)
                chart4.ChartAreas["ChartArea5"].Area3DStyle.LightStyle = LightStyle.Realistic;//表面光泽度
                chart4.DataSource = newdt5;
                chart4.DataBind();
                chart4.Series[0].XValueMember = "indexname";// xData.ToString(); ;
                chart4.Series[0].YValueMembers = "indexvalue";// yData.ToString(); ;

                chart5.Series.Clear();
                chart5.ChartAreas.Clear();
                Series Series6 = new Series();
                chart5.Series.Add(Series6);
                chart5.Series["Series6"].ChartType = SeriesChartType.Pie;
                chart5.Legends[0].Enabled = true;
                chart5.Series["Series6"].LegendText = "#INDEX:#VALX";//开启图例
                chart5.Series["Series6"].Label = "#INDEX:#PERCENT";
                chart5.Series["Series6"].IsXValueIndexed = false;
                chart5.Series["Series6"].IsValueShownAsLabel = false;
                chart5.Series["Series6"]["PieLineColor"] = "Black";//连线颜色
                chart5.Series["Series6"]["PieLabelStyle"] = "Outside";//标签位置
                chart5.Series["Series6"].ToolTip = "#VALX";//显示提示用语
                ChartArea ChartArea6 = new ChartArea();
                chart5.ChartAreas.Add(ChartArea6);
                //开启三维模式的原因是为了避免标签重叠
                chart5.ChartAreas["ChartArea6"].Area3DStyle.Enable3D = true;//开启三维模式;PointDepth:厚度BorderWidth:边框宽
                chart5.ChartAreas["ChartArea6"].Area3DStyle.Rotation = 15;//起始角度
                chart5.ChartAreas["ChartArea6"].Area3DStyle.Inclination = 45;//倾斜度(0～90)
                chart5.ChartAreas["ChartArea6"].Area3DStyle.LightStyle = LightStyle.Realistic;//表面光泽度
                chart5.DataSource = newdt6;
                chart5.DataBind();
                chart5.Series[0].XValueMember = "indexname";// xData.ToString(); 
                chart5.Series[0].YValueMembers = "indexvalue";// yData.ToString(); 


            }
        
        }
         */
    }
}
