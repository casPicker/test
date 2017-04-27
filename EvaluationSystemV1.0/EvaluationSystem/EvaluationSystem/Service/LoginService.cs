using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EvaluationSystem.Commons;
using System.Data;

namespace EvaluationSystem.Service
{
    interface LoginService
    {
        ValidateResult ValidateUser(String username, String password);
        ValidateResult RegisterUser(String username, String password,String confirmpwd);
        bool ChangePassword(string username, string password);
        DataSet GetUsers4Grid();
        bool DeleteUser(string username);
    }
}
