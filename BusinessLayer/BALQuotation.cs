using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using DataManager;
using System.Data;

namespace BusinessManager
{

    public class BALQuotation
    {
        DALQuotation _objDalQuotation = new DALQuotation();
        public DataSet GetQuotationList()
        {
            return _objDalQuotation.Quotation_List();
        }
    }
}
