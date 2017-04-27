using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EvaluationSystem.Entity;
using System.Data;

namespace EvaluationSystem.DAO
{
    interface IndexDAO
    {
        //查找所有指标系统
        List<IndexSystem> FindAllIndexSystem();

        //根据指标系统id查找所有指标项
        DataSet FindAllIndex(int systemid);

        //获取唯一值的实例
        List<IndexInstance> FindDistinctIndexInstance(int systemid);

        //获取指定指标系统实例
        DataSet FindAllInstance(int systemid,int instanceid);
    }
}
