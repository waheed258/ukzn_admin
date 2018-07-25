using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityManager
{
  public  class EMUser
    {
        public int UserMasterId { get; set; }
        public string UserFirstName { get; set; }
        public string UserLastName { get; set; }
        public string UserEmail { get; set; }
        public string UserPhone { get; set; }
        public string UserPassword { get; set; }
        public int UserCompanyId { get; set; }
        public int UserRole { get; set; }
        public int UserStatus { get; set; }
        public DateTime UserLastLoginDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
        public int UpdatedBy { get; set; }
        public string UserLoginId { get; set; }
        public int BranchId { get; set; }
        public int EmApprovalId { get; set; }
        public int EmpId { get; set; }
        public int LevelId { get; set; }
        public String ApprovalsId { get; set; }
        public int PrimaryLevelId { get; set; }
        public int ApprovalStatus { get; set; }
        public string Operation { get; set; }   
       
    }

  
}
