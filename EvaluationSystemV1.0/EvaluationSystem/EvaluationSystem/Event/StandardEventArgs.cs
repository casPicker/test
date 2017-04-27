using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EvaluationSystem.Util;

namespace EvaluationSystem.Event
{
    public class StandardEventArgs:EventArgs
    {
        private Standard[] standards;
        public Standard[] Standards
        {
            get { return this.standards; }
            set { this.standards = value; }
        }
    }
}
