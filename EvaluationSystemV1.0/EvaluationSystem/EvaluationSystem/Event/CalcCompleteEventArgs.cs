using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace EvaluationSystem.Event
{
    public class CalcCompleteEventArgs:EventArgs
    {
        private DataTable resultTable;
        public DataTable ResultTable
        {
            get { return this.resultTable; }
            set { this.resultTable = value; }
        }
    }
}
