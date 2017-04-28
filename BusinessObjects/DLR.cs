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
        public string DLRDate { get; set; }
        public string Branch { get; set; }
        public string ApplicationTypeDesc { get; set; }
        public BorrowerInfoModel Borrower { get; set; }
        public LoanInfoModel Loan { get; set; }
    }

    public class LoanInfoModel
    {
        public string LoanApplicationNo { get; set; }
        public string LoanTypeDesc { get; set; }
        public string LoanSetDesc { get; set; }
        public string LoanTermDesc { get; set; }
        public string DesiredMLV { get; set; }
        public string ApprovedMLV { get; set; }
        public string FirstDueDate { get; set; }
        public string AddOnRate { get; set; }
    }

    public class BorrowerInfoModel
    {
        public string BorrowerCode { get; set; }
        public string FullName { get; set; }
        public string Province { get; set; }
        public string City { get; set; }
        public string Brgy { get; set; }
        public string StreetAddress { get; set; }
        public string PostalCode { get; set; }
        public string LandlineNo { get; set; }
        public string MobileNo { get; set; }
    }
}
