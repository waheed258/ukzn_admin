using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityManager;
using System.Collections;
using System.Data;

namespace DataManager
{
    public class DALUser : DataUtilities
    {
        public int InsertUpdateUser(EMUser objEmUser)
        {
            Hashtable htparams = new Hashtable
                                            {
                                               {"@userFirstName",objEmUser.UserFirstName},
	                                           {"@userLastName",objEmUser.UserLastName},
	                                           {"@userEmail",objEmUser.UserEmail},
	                                           {"@userPhone",objEmUser.UserPhone},
	                                           {"@userCompanyId",objEmUser.UserCompanyId},
	                                           {"@UserRole",objEmUser.UserRole},
	                                           {"@UserStatus",objEmUser.UserStatus},
	                                           {"@createdBy",objEmUser.CreatedBy},
	                                          
	                                           {"@userPassword",objEmUser.UserPassword},
	                                           {"@UserMasterId",objEmUser.UserMasterId},
                                               {"@UserBranchId",objEmUser.BranchId},
	                                          
	                                         
                                          };

            int IsSuccess = ExecuteNonQuery("User_InsertUpdate", htparams);
            return IsSuccess;
        }
        public DataSet User_GetList(int UserId)
        {

            Hashtable htparams = new Hashtable
                                            {
                                               {"@UserId",UserId}
                                            };

            return ExecuteDataSet("User_List", htparams);
        }
        public int Change_password(int UserId, string newpassword)
        {

            Hashtable htparams = new Hashtable
                                            {
                                               {"@userMasterId",UserId},
                                                {"@Password",newpassword}
                                            };

            return ExecuteNonQuery("Change_Password", htparams);
        }
        public DataSet UserDetailsByEmail(string UserEmail)
        {

            Hashtable htparams = new Hashtable
                                            {
                                               {"@userEmail",UserEmail}
                                               
                                            };

            return ExecuteDataSet("User_GetDeatilsByEmail", htparams);
        }

        public DataSet GetRoles()
        {



            return ExecuteDataSet("Roles_List");
        }
        public DataSet UserAuthentication(string UserName, string Password)
        {
            Hashtable htParams = new Hashtable
                                     {
                                         {"@userLoginId",UserName},
                                         {"@Password",Password},
                                     };
            return ExecuteDataSet("User_Login", htParams);
        }

        public int UserLoginHistoryInsertUpdate(int LoginUserId, string LoginIpAddress, string Operation)
        {
            Hashtable htParams = new Hashtable
                                     {
                                         {"@LoginUserId",LoginUserId},
                                         {"@UserIpAddress",LoginIpAddress},
                                         {"@Operation",Operation},
                                     };
            return ExecuteNonQuery("User_LoginHistory", htParams);
        }
        public DataSet UserLoginHistoryGet()
        {

            return ExecuteDataSet("UserLoginHistory_Get");
        }

        public int EmpApprovalInsertUpdate(EMUser ObjEmUser)
        {
            Hashtable htParams = new Hashtable
                                     {
                                         {"@EmpApproveId",ObjEmUser.EmApprovalId},
                                         {"@EmpId",ObjEmUser.EmpId},
                                         {"@LevelId",ObjEmUser.LevelId},
                                          {"@ApprovalIds",ObjEmUser.ApprovalsId},
                                           {"@PrimaryLevelId",ObjEmUser.PrimaryLevelId},
                                           {"@Status",ObjEmUser.ApprovalStatus},{"@Operation",ObjEmUser.Operation}
                                     };
            return ExecuteNonQuery("[EmpApprovalLevels_InsertUpdate]", htParams);
        }
        public DataSet EmpApprovalGet(int EmpId, int LevelId,int Status)
        {
            Hashtable htParams = new Hashtable
                                     {
                                         {"@EmpId",EmpId},
                                         {"@LevelId",LevelId},
                                         {"@Status",Status}
                                          
                                     };
            return ExecuteDataSet("[EmpApprovalLevels_Get]", htParams);
        }
        public DataSet EmpApprovalCheckGet(int EmpId, int LevelId)
        {
            Hashtable htParams = new Hashtable
                                     {
                                         {"@EmpId",EmpId},
                                         {"@LevelId",LevelId}
                                        
                                          
                                     };
            return ExecuteDataSet("[EmpApprovalCheck_Get]", htParams);
        }

        public int EmpApprovalStatusZeroDelete(int Status)
        {
            Hashtable htParams = new Hashtable
                                     {
                                         {"@status",Status},
                                        
                                     };
            return ExecuteNonQuery("EmpApprovalDelete", htParams);
        }

        public DataSet User_GetListByEmpApproval()
        {
            return ExecuteDataSet("EmployeeListGet_EmpApproval");
        }
    }
}
