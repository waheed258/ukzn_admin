using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityManager;
using System.Collections;
using System.Data;

namespace DataManager
{
    public class DALPolicyRules : DataUtilities
    {
        public int InsertUpdatePolicyRules(EMPolicyRules objPolicyRules, char Operation)
        {
            Hashtable htparams = new Hashtable{
                                            {"@PolicyID",objPolicyRules.PolicyID},
                                            {"@Category",objPolicyRules.Category},
                                             {"@CategoryType",objPolicyRules.CategoryType},
                                            {"@RuleValue",objPolicyRules.RuleValue},
                                            {"@Condition",objPolicyRules.Condition},
                                            {"@Operation",Operation},

        };

            int Result = ExecuteNonQuery("InsUpdatePolicyRules", htparams);
            return Result;
        }



        public DataSet Get_Category(int CategoryId)
        {
            Hashtable htparams = new Hashtable{
                                     {"@CategoryID",CategoryId},
          };
            return ExecuteDataSet("GetCategorymaster", htparams);
        }
        public DataSet Get_Category_type(int TypeId)
        {
            Hashtable htparams = new Hashtable{
                                     {"@TypeID",TypeId},
          };
            return ExecuteDataSet("GetCategoryType", htparams);
        }

        //GetPolicys
        public DataSet GetPolicys(int PolicyID)
        {
            Hashtable htparams = new Hashtable{
                                     {"@PolicyID",PolicyID},
          };
            return ExecuteDataSet("GetPolicyRules", htparams);
        }

        public int DeletePolicy(int PolicyID)
        {
            Hashtable htpParams = new Hashtable{
                                  {"@PolicyID",PolicyID},
           };
            return ExecuteNonQuery("Delete_PolicyRules", htpParams);
        }

    }
}
