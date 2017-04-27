using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.XtraTreeList.Nodes.Operations;
using DevExpress.XtraTreeList.Nodes;

namespace EvaluationSystem.Util
{
    public class MatchValidateOperation:TreeListOperation
    {
        private bool result;

        public bool Result
        {
            get { return this.result; }
        }

        public MatchValidateOperation()
            : base()
        {
            this.result = true;
        }

        public override void Execute(TreeListNode node)
        {
            bool hasChildren = node.HasChildren;
            string colname = node.GetValue("colname").ToString();
            if (!hasChildren && colname.Equals(""))
            {
                this.result = false;
            }
                
        }
    }
}
