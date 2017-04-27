using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EvaluationSystem.DAO;
using EvaluationSystem.DAO.DAOImpl;
using EvaluationSystem.Entity;
using System.Data;

namespace EvaluationSystem.Service.ServiceImpl
{
    public class IndexServiceImpl:IndexService
    {
        private IndexDAO indexDAO;
        public IndexServiceImpl()
        {
            this.indexDAO = new IndexDAOImpl();
        }

        public List<IndexSystem> FindAllIndexSystem()
        {
            return this.indexDAO.FindAllIndexSystem();
        }

        public DataSet FindAllIndex(int systemid)
        {
            return this.indexDAO.FindAllIndex(systemid);
        }

        public List<IndexInstance> FindDistinctIndexInstance(int systemid)
        {
            return this.indexDAO.FindDistinctIndexInstance(systemid);
        }

        public DataSet FindAllInstance(int systemid, int instanceid)
        {
            return this.indexDAO.FindAllInstance(systemid,instanceid);
        }
    }
}
