using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects
{
    public class OfficialReceipt
    {
        public string ORNumber { get; set; }
        public string ORDate { get; set; }
        public string OrganizationID { get; set; }
        public string PaymentModeID { get; set; }
        public string AmountDue { get; set; }
        public string AmountReceived { get; set; }
        public string BankID { get; set; }
        public string CheckNo { get; set; }
        public string AccelerationDiscount { get; set; }
        public string PenaltyWaived { get; set; }
        public string PromptPaymentDiscount { get; set; }
        public string TotalDiscount { get; set; }
        public string PIP { get; set; }
        public string GIBCO { get; set; }
        public string RFC { get; set; }
        public string TotalRFC { get; set; }
        public string DateDue { get; set; }
        public string PIPDue { get; set; }
        public string GIBCODue { get; set; }
        public string RFCDue { get; set; }
        public string PPD { get; set; }
        public string LoanAccountNo { get; set; }        
        public string DirectLoanReceiptNo { get; set; }
        public string CustomerName { get; set; }
        public string CreditInvestigator { get; set; }
        public string UserID { get; set; }
        public string Notes { get; set; }

    }

    public class Sundry
    {
        public string ID { get; set; }
        public string CMDMAccountTypeID { get; set; }
        public string SundryAmount { get; set; }
    }

    public class CMDMAccountType
    {
        public string CMDMAccountTypeID { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
    }
}
