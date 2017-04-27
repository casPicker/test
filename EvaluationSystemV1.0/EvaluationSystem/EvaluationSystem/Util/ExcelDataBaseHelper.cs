using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data.OleDb;
using System.Data;
using System.Windows.Forms;

namespace EvaluationSystem.Util
{
    class ExcelDataBaseHelper
    {
        public static object OpenFile(string fullFileName)
        {
            if (!File.Exists(fullFileName))
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("文件不存在！", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                return null;
            }
            var connectionString = string.Format("Provider=Microsoft.Jet.OLEDB.4.0; data source={0}; Extended Properties=Excel 8.0;", fullFileName);
            var adapter = new OleDbDataAdapter("select * from [原始数据$]", connectionString);
            var ds = new DataSet();
            string tableName = "RawData";
            adapter.Fill(ds, tableName);
            DataTable data = ds.Tables[tableName];
            return data;
        }

        public static object OpenFile(string FileName, string SheetName,string TableName, bool hasHeaders)
        {
            string HDR = hasHeaders ? "Yes" : "No";
            string strConn;
            if (FileName.Substring(FileName.LastIndexOf('.')).ToLower() == ".xlsx")
                strConn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + FileName + ";Extended Properties=\"Excel 12.0;HDR=" + HDR + ";IMEX=0\"";
            else
                strConn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + FileName + ";Extended Properties=\"Excel 8.0;HDR=" + HDR + ";IMEX=0\"";

            DataSet output = new DataSet();

            using (OleDbConnection conn = new OleDbConnection(strConn))
            {
                conn.Open();

                DataTable schemaTable = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[] { null, null, null, "TABLE" });
                try
                {
                    OleDbCommand cmd = new OleDbCommand("SELECT * FROM [" + SheetName + "]", conn);
                    cmd.CommandType = CommandType.Text;

                    DataTable outputTable = new DataTable(TableName);
                    output.Tables.Add(outputTable);
                    new OleDbDataAdapter(cmd).Fill(outputTable);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message + string.Format("Sheet:{0}.File:F{1}", SheetName, FileName), ex);
                }
                finally
                {
                    conn.Close();
                }
            }
            return output.Tables[TableName];
        }

        public static string[] GetSheetsNames(string FileName, bool hasHeaders)
        {
            string HDR = hasHeaders ? "Yes" : "No";
            string strConn;
            if (FileName.Substring(FileName.LastIndexOf('.')).ToLower() == ".xlsx")
                strConn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + FileName + ";Extended Properties=\"Excel 12.0;HDR=" + HDR + ";IMEX=0\"";
            else
                strConn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + FileName + ";Extended Properties=\"Excel 8.0;HDR=" + HDR + ";IMEX=0\"";

            DataSet output = new DataSet();
            string[] sheetNames;
            using (OleDbConnection conn = new OleDbConnection(strConn))
            {
                conn.Open();

                DataTable schemaTable = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[] { null, null, null, "TABLE" });
                sheetNames = new string[schemaTable.Rows.Count];
                for(int i=0;i<schemaTable.Rows.Count;i++)
                {
                    DataRow schemaRow = schemaTable.Rows[i];
                    string sheet = schemaRow["TABLE_NAME"].ToString();
                    
                    if (!sheet.EndsWith("_"))
                    {
                       sheetNames[i] = sheet;
                    }
                }
                conn.Close();
            }
            return sheetNames;
        }

        public static string[] GetColNames(DataTable dTable)
        {
            string[] colNames = new string[dTable.Columns.Count];
            for (int i = 0; i < dTable.Columns.Count; i++)
            {
                string colName = dTable.Columns[i].ColumnName;
                colNames[i] = colName;
            }
            return colNames;
        }

        public static string[] GetNumColNames(DataTable dTable)
        {
            string[] colNames = new string[dTable.Columns.Count];
            List<string> list = new List<String>();
            for (int i = 0; i < dTable.Columns.Count; i++)
            {
                DataColumn col = dTable.Columns[i];
                if (col.DataType == Type.GetType("System.String"))
                {
                    continue;
                }
                string colName = dTable.Columns[i].ColumnName;
                list.Add(colName);
            }
            return list.ToArray();
        }
    }
}
