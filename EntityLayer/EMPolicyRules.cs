using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EntityManager
{
    public class EMPolicyRules
    {

        public int PolicyID { get; set; }
        public int Category { get; set; }
        public int CategoryType { get; set; }
        public string RuleValue { get; set; }
        public string Condition { get; set; }
    }
}
