using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects
{
    public class DLRModel
    {
        public string LMSCode { get; set; }
        public string DLRNo { get; set; }
        public string Branch { get; set; }
        public string FirstDueDate { get; set; }
        public string ApprovedMLV { get; set; }
        public string AddOnRate { get; set; }
        public string NetMonthlyInstallment { get; set; }
        public string LessRppd { get; set; }
    }
}
