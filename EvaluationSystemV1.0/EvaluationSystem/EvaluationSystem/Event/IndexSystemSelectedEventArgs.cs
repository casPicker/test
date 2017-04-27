using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EvaluationSystem.Entity;

namespace EvaluationSystem.Event
{
    public class IndexSystemSelectedEventArgs:EventArgs
    {
        private IndexSystem selectedIndexSystem;
        public IndexSystem SelectedIndexSystem
        {
            get { return this.selectedIndexSystem; }
            set { this.selectedIndexSystem = value; }
        }
    }
}
