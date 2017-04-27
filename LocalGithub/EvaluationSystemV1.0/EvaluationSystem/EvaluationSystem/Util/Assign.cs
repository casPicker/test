using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EvaluationSystem.Util
{
    class Assign
    {
        public string OldValue;
        public string NewValue;

        public Assign()
        {

        }

        public Assign(string OldValue,string NewValue)
        {
            this.OldValue = OldValue;
            this.NewValue = NewValue;
        }
    }
}
