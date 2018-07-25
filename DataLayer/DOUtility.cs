using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using EntityManager;


namespace DataManager
{
    public class DOUtility : DataUtilities
    {
        public DataSet GetMenus(string RoleId, int CompanyId)
        {
            Hashtable htParams = new Hashtable
                                     {
                                          
                                         {"@id_role",RoleId},
                                         {"@CompanyId",CompanyId}
                                     };
            return ExecuteDataSet("get_menu_byrole", htParams);
        }

        public DataSet BindGenVATPer()
        {

            return ExecuteDataSet("Vat_GetData");
        }
        public DataSet BindPaymentType()
        {

            return ExecuteDataSet("PaymentTypesMaster_GetData");
        }
        public DataSet BindCreditCardType()
        {

            return ExecuteDataSet("CreditCardTypeMaster_GetData");
        }

       
        public DataSet get_Type()
        {
            return ExecuteDataSet("[Type_Get]");
        }

        public object getVatByType(int typeId)
        {
            Hashtable htParams = new Hashtable
                                     {
                                          
                                         {"@TypeId",typeId},
                                       
                                     };
            return ExecuteScalar("get_VatRateByType", htParams);
        }
    }
}
