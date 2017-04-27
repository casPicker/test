using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.XtraTreeList.Nodes.Operations;
using DevExpress.XtraTreeList.Nodes;

namespace EvaluationSystem.Util
{
    public class HasChildrenOperation:TreeListOperation
    {
        private List<TreeListNode> resultList;

        public List<TreeListNode> ResultList
        {
            get { return this.resultList; }
        }

        public HasChildrenOperation()
            : base()
        {
            resultList = new List<TreeListNode>();
        }

        public override void Execute(TreeListNode node)
        {
            bool hasChildren = node.HasChildren;
            if (hasChildren)
            {
                resultList.Add(node);
            }
        }
    }
}
