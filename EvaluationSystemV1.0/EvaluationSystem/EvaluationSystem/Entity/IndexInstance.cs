using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EvaluationSystem.Entity
{
    public class IndexInstance
    {
        private int instanceid;
        private int indexid;
        private double indexvalue;
        private string instancename;

        public int Instanceid
        {
            get { return this.instanceid; }
            set { this.instanceid = value; }
        }

        public int Indexid
        {
            get { return this.indexid; }
            set { this.indexid = value; }
        }

        public double Indexvalue
        {
            get { return this.indexvalue; }
            set { this.indexvalue = value; }
        }

        public string Instancename
        {
            get { return this.instancename; }
            set { this.instancename = value; }
        }

        public override string ToString()
        {
            return this.instanceid+". "+this.instancename;
        }
    }
}
