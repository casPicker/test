using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EvaluationSystem.Entity;
using System.Data.OleDb;
using System.Data;

namespace EvaluationSystem.DAO.DAOImpl
{
    class UserDAOImpl:UserDAO
    {
        public UserDAOImpl()
        {

        }
        public User FindUserByName(string username)
        {
            OleDbConnection conn = DBHelper.getConn(); //得到连接对象
            String strCommand = "SELECT * FROM TB_USER t WHERE t.username = '"+username+"'";
            OleDbCommand myCommand = new OleDbCommand(strCommand, conn);
            conn.Open();

            OleDbDataReader reader;
            User user = null;
            reader = myCommand.ExecuteReader(); //执行command并得到相应的DataReader
            if (reader.Read())
            {
                user = new User();
                user.Id = long.Parse(reader["ID"].ToString());
                user.UserName = reader["username"].ToString();
                user.Password = reader["pwd"].ToString();
            }
            reader.Close();
            conn.Close();
            return user;
        }


        public bool ValidateUser(string username, string password)
        {
            OleDbConnection conn = DBHelper.getConn(); //得到连接对象
            String strCommand = "SELECT * FROM TB_USER t WHERE t.username = '" + username + "' and t.pwd = '"+password+"'";
            OleDbCommand myCommand = new OleDbCommand(strCommand, conn);
            conn.Open();

            OleDbDataReader reader;
            bool isOk = false;
            reader = myCommand.ExecuteReader(); //执行command并得到相应的DataReader
            if (reader.Read())
            {
                isOk = true;
            }
            reader.Close();
            conn.Close();
            return isOk;
        }


        public bool AddUser(string username, string password)
        {
            bool isOk = false;
            OleDbConnection conn = DBHelper.getConn(); //得到连接对象
            conn.Open();
            try
            {
                String strCommand = "INSERT INTO TB_USER (username,pwd) VALUES (?,?)";
                OleDbCommand command = new OleDbCommand(strCommand, conn);
                OleDbParameterCollection paramCollection = command.Parameters;
                OleDbParameter param1 = paramCollection.Add("username", OleDbType.VarChar);
                param1.Value = username;
                OleDbParameter param2 = paramCollection.Add("pwd", OleDbType.VarChar);
                param2.Value = password;

                command.ExecuteNonQuery();
                conn.Close();
                isOk = true;
            }
            catch(Exception e)
            {
                conn.Close();
                throw new Exception(e.Message);
            }
            return isOk;
        }

        public bool ChangePwd(String username, String password)
        {
            bool isOk = false;
            OleDbConnection conn = DBHelper.getConn(); //得到连接对象
            conn.Open();
            try
            {
                String strCommand = "UPDATE TB_USER SET pwd = ? where username = ?";
                OleDbCommand command = new OleDbCommand(strCommand, conn);
                OleDbParameterCollection paramCollection = command.Parameters;
                OleDbParameter param1 = paramCollection.Add("pwd", OleDbType.VarChar);
                param1.Value = password;
                OleDbParameter param2 = paramCollection.Add("username", OleDbType.VarChar);
                param2.Value = username;

                command.ExecuteNonQuery();
                conn.Close();
                isOk = true;
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception(e.Message);
            }
            return isOk;
        }

        public DataSet FindAllUsers4Grid()
        {
            String strCommand = "SELECT * FROM TB_USER";
            OleDbConnection conn = DBHelper.getConn(); //得到连接对象
            conn.Open();
            try
            {
                OleDbDataAdapter adapter = new OleDbDataAdapter(strCommand, conn);

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

        public bool DeleteUser(string username)
        {
            bool isOk = false;
            OleDbConnection conn = DBHelper.getConn(); //得到连接对象
            conn.Open();
            try
            {
                String strCommand = "DELETE FROM TB_USER where username = ?";
                OleDbCommand command = new OleDbCommand(strCommand, conn);
                OleDbParameterCollection paramCollection = command.Parameters;
                OleDbParameter param1 = paramCollection.Add("username", OleDbType.VarChar);
                param1.Value = username;

                command.ExecuteNonQuery();
                conn.Close();
                isOk = true;
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception(e.Message);
            }
            return isOk;
        }
    }
}
