using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;
using System.Windows.Forms;

namespace EvaluationSystem
{
    class DBHelper
    {
        private static OleDbConnection conn = null;

        public static OleDbConnection getConn()
        {
            if (null == conn)
            {
                String connStr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + Application.StartupPath + "\\DB\\SystemDB.accdb";
                conn = new OleDbConnection(connStr);
            }
            return conn;
        }

    }
}
