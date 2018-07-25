using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;


namespace DataManager
{
    public class DALQuotation : DOUtility
    {
        public DataSet Quotation_List()
        {

            return ExecuteDataSet("QuotationMaster_Get");
        }
    }
}
