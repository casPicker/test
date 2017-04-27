using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EvaluationSystem.DAO;
using EvaluationSystem.DAO.DAOImpl;
using EvaluationSystem.Commons;
using EvaluationSystem.Entity;
using System.Security.Cryptography;
using System.Data;

namespace EvaluationSystem.Service.ServiceImpl
{
    class LoginServiceImpl:LoginService
    {
        private UserDAO userDAO;

        public LoginServiceImpl()
        {
            this.userDAO = new UserDAOImpl();
        }

        public ValidateResult ValidateUser(string username, string password)
        {
            bool isExist = false;
            string msg = "";
            ValidateResult result = new ValidateResult(isExist, msg);
            if (username.Equals(""))
            {
                result.Message = "用户名不能为空！";
                return result;
            }
            if(password.Equals(""))
            {
                result.Message = "密码不能为空！";
                return result;
            }
        
            MD5 md = new MD5CryptoServiceProvider();
            password = Convert.ToBase64String(md.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password)));

            User user = this.userDAO.FindUserByName(username);
            if(null == user)
            {
                result.Message = "用户不存在！";
                return result;
            }
            isExist = user.Password.Equals(password);
            if(isExist)
            {
                result.IsOk = isExist;
                result.Message = username + "登录验证通过";
            }
            else
            {
                result.Message = "用户名或密码错误！";
            }
           
            return result;
        }


        public ValidateResult RegisterUser(string username, string password, String confirmpwd)
        {
            bool isExist = false;
            string msg = "";
            ValidateResult result = new ValidateResult(isExist, msg);
            if (username.Equals(""))
            {
                result.Message = "用户名不能为空！";
                return result;
            }
            if (password.Equals(""))
            {
                result.Message = "密码不能为空！";
                return result;
            }
            if (confirmpwd.Equals(""))
            {
                result.Message = "确认密码不能为空！";
                return result;
            }
            if(!password.Equals(confirmpwd))
            {
                result.Message = "密码与确认密码不一致！";
                return result;
            }
            User user = this.userDAO.FindUserByName(username);
            if (null != user)
            {
                result.Message = "用户已经存在，请使用其他用户名！";
                return result;
            }
            MD5 md = new MD5CryptoServiceProvider();
            password = Convert.ToBase64String(md.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password)));
            try
            {
                this.userDAO.AddUser(username, password);
                result.Message = "注册成功！";
                result.IsOk = true;
                return result;
            }
            catch (Exception e)
            {
                result.Message = "注册失败，" + e.Message;
                return result;
            }
        }

        public bool ChangePassword(string username, string password)
        {
            MD5 md = new MD5CryptoServiceProvider();
            password = Convert.ToBase64String(md.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password)));
            return this.userDAO.ChangePwd(username, password);
        }

        public DataSet GetUsers4Grid()
        {
            return this.userDAO.FindAllUsers4Grid();
        }

        public bool DeleteUser(string username)
        {
            return this.userDAO.DeleteUser(username);
        }
    }
}
