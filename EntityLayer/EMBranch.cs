using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EntityManager
{
  public  class EMBranch
    {
        public int BranchId { get; set; }
        public string BranchName { get; set; }
        public string BranchAddress { get; set; }
        public string BranchMobileNo { get; set; }
        public string BranchEmailId { get; set; }
        public string BranchContactPerson { get; set; }
        public int branchStatus { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
        public int UpdatedBy { get; set; }
       
       
    }
}
