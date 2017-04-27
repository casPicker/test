using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EvaluationSystem.Entity
{
    public class IndexSystem
    {
        private int systemid;
        private string systemname;

        public int Systemid
        {
            get { return this.systemid; }
            set { this.systemid = value; }
        }

        public string Systemname
        {
            get { return this.systemname; }
            set { this.systemname = value; }
        }

        public override string ToString()
        {
            return this.systemname;
        }
    }
}
