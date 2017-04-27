using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EvaluationSystem.Entity
{
    class User
    {
        private long id;
        private String username;
        private String password;

        public long Id
        {
            get { return this.id; }
            set { this.id = value; }
        }

        public String UserName
        {
            get { return this.username; }
            set { this.username = value; }
        }

        public String Password
        {
            get { return this.password; }
            set { this.password = value; }
        }

        public User()
        {

        }
        public User(String username,String password)
        {
            this.username = username;
            this.password = password;
        }
    }
}
