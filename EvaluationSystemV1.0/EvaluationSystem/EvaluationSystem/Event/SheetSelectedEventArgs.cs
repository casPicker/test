using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EvaluationSystem.Event
{
    public class SheetSelectedEventArgs:EventArgs
    {
        private string sheetName;
        private string fullFileName;

        public SheetSelectedEventArgs(string fullFileName,string sheetName)
        {
            this.fullFileName = fullFileName;
            this.sheetName = sheetName;
        }

        public string SheetName
        {
            get { return this.sheetName; }
            set { this.sheetName = value; }
        }

        public string FullFileName
        {
            get { return this.fullFileName; }
            set { this.fullFileName = value; }
        }
    }
}
