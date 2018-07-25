using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace DataManager
{
    public class DALogin : DataUtilities
    {
        public DataSet UserAuthentication(string UserName, string Password)
        {
            Hashtable htParams = new Hashtable
                                     {
                                         {"@userLoginId",UserName},
                                         {"@Password",Password},
                                     };
            return ExecuteDataSet("User_Login", htParams);
        }
    }
}
