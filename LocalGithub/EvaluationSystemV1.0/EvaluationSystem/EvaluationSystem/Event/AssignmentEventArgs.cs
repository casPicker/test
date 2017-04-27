using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace EvaluationSystem.Event
{
    public class AssignmentEventArgs:EventArgs
    {
        private Hashtable assignDetail;
        public Hashtable AssignDetail
        {
            get { return this.assignDetail; }
            set { this.assignDetail = value; }
        }
    }
}
