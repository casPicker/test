using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EvaluationSystem.Entity
{
    public class Index
    {
        private int indexid;
        private int pid;
        private string indexname;
        private int systemid;

        public int Indexid
        {
            get { return this.indexid;}
            set { this.indexid = value; }
        }

        public int Pid
        {
            get { return this.pid; }
            set { this.pid = value; }
        }

        public string Indexname
        {
            get { return this.indexname; }
            set { this.indexname = value; }
        }

        public int Systemid
        {
            get { return this.systemid; }
            set { this.systemid = value; }
        }
    }
}
