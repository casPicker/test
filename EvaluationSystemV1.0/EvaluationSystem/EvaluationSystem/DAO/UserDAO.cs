using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EvaluationSystem.Entity;
using System.Data;

namespace EvaluationSystem.DAO
{
    interface UserDAO
    {
        //通过用户名查找用户
        User FindUserByName(String username);

        //验证用户
        bool ValidateUser(String username, String password);

        //添加用户
        bool AddUser(String username, String password);

        //修改用户密码
        bool ChangePwd(String username,String password);

        //获取全部用户，用于填充Grid
        DataSet FindAllUsers4Grid();

        bool DeleteUser(string username);
    }
}
