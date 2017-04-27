using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EvaluationSystem.Util
{
    public class EntryPort
    {
        public int X;
        public int Y;
        public string Name;
        public EntryPort(string name,int x,int y)
        {
            this.Name = name;
            this.X = x;
            this.Y = y;
        }
    }
}
