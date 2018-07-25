using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityManager;
using System.Data;
using DataManager;

namespace BusinessManager
{
    public class BALogin
    {
        private DALogin _objDALogin = new DALogin();
        public DataSet UserAuthentication(string UserName, string Password)
        {
            return _objDALogin.UserAuthentication(UserName, Password);
        }
    }
}
