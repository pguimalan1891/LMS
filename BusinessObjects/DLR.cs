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
        public GIBCOModel GIBCO { get; set; }
        public OutrightPayments Outpayments { get; set; }
        public LessHandlingFeeRebatablePaymentDiscount LessHandling { get; set; }
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
        public string NetMonthlyInstallment { get; set; }
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

    public class GIBCOModel
    {
        public string GIBCOBasic1 { get; set; }
        public string GIBCOBasic2 { get; set; }

        public string GIBCOBasic3 { get; set; }
        public string GIBCOBasic4 { get; set; }
        public string GIBCOBasic5 { get; set; }
        public string GIBCOYear1 { get; set; }
        public string GIBCOYear2 { get; set; }
        public string GIBCOYear3 { get; set; }
        public string GIBCOYear4 { get; set; }
        public string GIBCOYear5 { get; set; }
        public string GMMU1 { get; set; }
        public string GMMU2 { get; set; }
        public string GMMU3 { get; set; }
        public string GMMU4 { get; set; }
        public string GMMU5 { get; set; }

        public string GIBCODate1 { get; set; }

        public string GIBCODate2 { get; set; }

        public string GIBCODate3 { get; set; }

        public string GIBCODate4 { get; set; }
        public string GIBCODate5 { get; set; }
    }

    public class OutrightPayments
    {
        public string HandlingORCode { get; set; }
        public string HandlingORDate { get; set; }
        public string HandlingORAmount { get; set; }
        public string DSTORCode { get; set; }
        public string DSTORDate { get; set; }
        public string DSTORAmount { get; set; }
        public string ProcessingORCode { get; set; }
        public string ProcessingORDate { get; set; }
        public string ProcessingORAmount { get; set; }
        public string PIPPVAOORCode { get; set; }
        public string PIPPVAOORDate { get; set; }
        public string PIPPVAOORAmount { get; set; }
        public string RestructuringFeeORCode { get; set; }
        public string RestructuringFeeORDate { get; set; }
        public string RestructuringFeeORAmount { get; set; }
    }

    public class LessHandlingFeeRebatablePaymentDiscount
    {
        public string LessFinanceCharges { get; set; }
        public string TotalLessHandlingFee { get; set; }
        public string LessHandlingFee { get; set; }
        public string LessHandlingWOPDC { get; set; }
        public string LessHandlingWOInsurance { get; set; }
        public string TotalLessHandlingWOPDC { get; set; }
        public string LessRppd { get; set; }
        public string TotalPpd { get; set; }
    }
}
