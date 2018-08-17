using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataManager;
using System.Data;
using EntityManager;

namespace BusinessManager
{
    public class BAPolicyRules
    {
        public DALPolicyRules objDALPolicy = new DALPolicyRules();
        public int InsUpdateCategoryType(EMPolicyRules objEMPolicy, char Operation)
        {
            return objDALPolicy.InsertUpdatePolicyRules(objEMPolicy, Operation);
        }


        public DataSet Get_Category(int CategoryId)
        {
            return objDALPolicy.Get_Category(CategoryId);
        }

        //Get_Category_type

        public DataSet Get_Category_type(int CategoryId)
        {
            return objDALPolicy.Get_Category_type(CategoryId);
        }
        //GetPolicys

        public DataSet GetPolicys(int CategoryId)
        {
            return objDALPolicy.GetPolicys(CategoryId);
        }

        public int DeletePolicy(int CategoryId)
        {
            return objDALPolicy.DeletePolicy(CategoryId);
        }

    }
}
