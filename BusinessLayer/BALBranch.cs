using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataManager;
using EntityManager;
using System.Data;

namespace BusinessManager
{
   public class BALBranch
    {
       private DALBranch _objDalBranch = new DALBranch();
        public int InsertUpdateBranch(EMBranch objEmuser)
        {
            return _objDalBranch.InsertUpdateBranch(objEmuser);
        }
        public DataSet GetBranchList(int BranchId)
        {
            return _objDalBranch.Branch_List(BranchId);
        }
    }
}
