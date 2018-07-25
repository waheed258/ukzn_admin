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
   public class BALUserUKZN
    {
       private DALUser _objDAlUser = new DALUser();
        public int InsertUpdateUser(EMUser objEmuser)
        {
            return _objDAlUser.InsertUpdateUser(objEmuser);
        }
        public DataSet GetUserList(int userId)
        {
            return _objDAlUser.User_GetList(userId);
        }
        public int ChangePassword(int userId,string password)
        {
            return _objDAlUser.Change_password(userId,password);
        }
        public DataSet GetUserListByEmail(string userEmail)
        {
            return _objDAlUser.UserDetailsByEmail(userEmail);
        }
        public DataSet GetRoles()
        {
            return _objDAlUser.GetRoles(); ;
        }
        public DataSet UserAuthentication(string UserName, string Password)
        {
            return _objDAlUser.UserAuthentication(UserName, Password);
        }

        public int UserLoginHistoryInsertUpdate(int LoginUserId, string IpAddress, string operation)
        {
            return _objDAlUser.UserLoginHistoryInsertUpdate(LoginUserId, IpAddress, operation);
        }
        public DataSet UserLoginHistoryGet()
        {
            return _objDAlUser.UserLoginHistoryGet();
        }
        public int EmpApprovalInsertUpdate(EMUser objEmUser)
        {
            return _objDAlUser.EmpApprovalInsertUpdate(objEmUser);
        }
        public DataSet EmpApprovalGet(int EmpId, int LevelId, int Status)
        {
            return _objDAlUser.EmpApprovalGet(EmpId, LevelId,Status);
        }
        public DataSet EmpApprovalCheckGet(int EmpId, int LevelId)
        {
            return _objDAlUser.EmpApprovalCheckGet(EmpId, LevelId);
        }
        public int EmpApprovalStatusZeroDelete(int status)
        {

            return _objDAlUser.EmpApprovalStatusZeroDelete(status);
        }

        public DataSet UserGetListByEmpApproval()
        {
            return _objDAlUser.User_GetListByEmpApproval();
        }
    }
}
