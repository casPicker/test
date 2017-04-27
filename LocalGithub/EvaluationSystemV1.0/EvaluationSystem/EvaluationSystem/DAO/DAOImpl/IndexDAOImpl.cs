using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EvaluationSystem.Entity;
using System.Data.OleDb;
using System.Collections;
using System.Data;

namespace EvaluationSystem.DAO.DAOImpl
{
    class IndexDAOImpl:IndexDAO
    {
        public List<IndexSystem> FindAllIndexSystem()
        {
            OleDbConnection conn = DBHelper.getConn(); //得到连接对象
            String strCommand = "SELECT * FROM TB_INDEX_SYSTEM";
            OleDbCommand myCommand = new OleDbCommand(strCommand, conn);
            conn.Open();

            OleDbDataReader reader;
            reader = myCommand.ExecuteReader(); //执行command并得到相应的DataReader
            List<IndexSystem> list = new List<IndexSystem>();
            while (reader.Read())
            {
                IndexSystem indexSystem = new IndexSystem();
                indexSystem.Systemid = int.Parse(reader["systemid"].ToString());
                indexSystem.Systemname = reader["systemname"].ToString();
                list.Add(indexSystem);
            }
            reader.Close();
            conn.Close();
            return list;
        }

        public DataSet FindAllIndex(int systemid)
        {
            OleDbConnection conn = DBHelper.getConn(); //得到连接对象
            String strCommand = "SELECT * FROM TB_INDEX where systemid = ?";
            OleDbCommand command = new OleDbCommand(strCommand, conn);
            OleDbParameterCollection paramCollection = command.Parameters;
            OleDbParameter param1 = paramCollection.Add("systemid", OleDbType.Integer);
            param1.Value = systemid;
            conn.Open();

            try
            {
                OleDbDataAdapter adapter = new OleDbDataAdapter(command);

                DataSet sourceDataSet = new DataSet();
                adapter.Fill(sourceDataSet);
                conn.Close();
                return sourceDataSet;
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception(e.Message);
            }
        }

        public List<IndexInstance> FindDistinctIndexInstance(int systemid)
        {
            OleDbConnection conn = DBHelper.getConn(); //得到连接对象
            String strCommand = "SELECT DISTINCT(instanceid),instancename FROM TB_INDEX_INSTANCE where systemid = ?";
            OleDbCommand command = new OleDbCommand(strCommand, conn);
            OleDbParameterCollection paramCollection = command.Parameters;
            OleDbParameter param1 = paramCollection.Add("systemid", OleDbType.Integer);
            param1.Value = systemid;
            conn.Open();

            OleDbDataReader reader;
            reader = command.ExecuteReader(); //执行command并得到相应的DataReader
            List<IndexInstance> list = new List<IndexInstance>();
            while (reader.Read())
            {
                IndexInstance indexInstance = new IndexInstance();
                //indexInstance.Indexid = int.Parse(reader["indexid"].ToString());
                indexInstance.Instanceid = int.Parse(reader["instanceid"].ToString());
                //indexInstance.Indexvalue = double.Parse(reader["indexvalue"].ToString());
                indexInstance.Instancename = reader["instancename"].ToString();
                list.Add(indexInstance);
            }
            reader.Close();
            conn.Close();
            return list;
        }

        public DataSet FindAllInstance(int systemid, int instanceid)
        {
            OleDbConnection conn = DBHelper.getConn(); //得到连接对象
            String strCommand = "SELECT a.indexid,a.pid,a.indexname,b.indexvalue FROM TB_INDEX a LEFT JOIN TB_INDEX_INSTANCE b ON a.systemid = b.systemid AND a.indexid = b.indexid WHERE a.systemid = ? AND b.instanceid =?";
            OleDbCommand command = new OleDbCommand(strCommand, conn);
            OleDbParameterCollection paramCollection = command.Parameters;
            OleDbParameter param1 = paramCollection.Add("systemid", OleDbType.Integer);
            param1.Value = systemid;
            OleDbParameter param2 = paramCollection.Add("instanceid", OleDbType.Integer);
            param2.Value = instanceid;
            conn.Open();

            try
            {
                OleDbDataAdapter adapter = new OleDbDataAdapter(command);

                DataSet sourceDataSet = new DataSet();
                adapter.Fill(sourceDataSet);
                conn.Close();
                return sourceDataSet;
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception(e.Message);
            }
        }
    }
}
