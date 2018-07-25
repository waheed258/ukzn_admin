using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntityManager;
using System.Collections;
using System.Data;

namespace DataManager
{
    public class DALBranch : DataUtilities
    {
      public int InsertUpdateBranch(EMBranch objBranch)
      {
          Hashtable htparams = new Hashtable
                                            {
                                               {"@BranchId",objBranch.BranchId},
	                                           {"@Name",objBranch.BranchName},
	                                           {"@Address",objBranch.BranchAddress},
	                                           {"@MobileNo",objBranch.BranchMobileNo},
	                                           {"@Email",objBranch.BranchEmailId},
	                                           {"@ContactPerson",objBranch.BranchContactPerson},
	                                           {"@branchStatus",objBranch.branchStatus},
	                                           {"@CreatedBy",objBranch.CreatedBy},
	                                          
	                                          
	                                         
                                          };

          return ExecuteNonQuery("Branch_InsertUpdate", htparams);
          
      }
      public DataSet Branch_List(int BranchId)
      {

          Hashtable htparams = new Hashtable
                                            {
                                               {"@BranchId",BranchId}
                                            };

          return ExecuteDataSet("Branch_List", htparams);
      }
    }
}
