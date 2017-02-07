using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LMS.Models
{
    public class OfficialReceiptViewModel
    {

        public string OR_Number { get; set; }
        public string OR_Date { get; set; }
        public string Branch { get; set; }
        public string PaymentMode { get; set; }
        public string AmountDue { get; set; }
        public string AmountPaid { get; set;}
        public string BankName { get; set; }
        public string CheckNo { get; set; }
        public string AccelerationDiscount { get; set; }
        public string PenaltyWaived { get; set; }
        public string PromptPaymentDiscount { get; set; }
        public string TotalDiscount { get; set; }
        public string PIP { get; set; }
        public string GIBCO { get; set; }
        public string RFC { get; set; }
        public string TotalRFC { get; set; }
        public string LA_No { get; set; }
        public string DLR_No { get; set; }
        public string CustomerName { get; set; }
        public string DateDue { get; set; }
        public string PIPDue { get; set; }
        public string GIBCODue { get; set; }
        public string RFCDue { get; set; }
        public string PPD { get; set; }
        public string Notes { get; set; }


        public bool isSundry { get; set; }
        public string Sundry_handlingFee { get; set; }
        public string Sundry_DSTAmount { get; set; }
        public string Sundry_RestructuringFee { get; set; }
        public string Sundry_GIBCOPremium { get; set; }
        public string Sundry_ProcessingFee { get; set; }
        public string Sundry_PIP { get; set; }
        public string Sundry_Downpayment { get; set; }
        public string Sundry_OtherAmount { get; set; }
        public string Sundry_OtherDescription { get; set; }
        public string Sundry_SalesOfRepossessed { get; set; }
        public string Sundry_Total { get; set; }

        public static OfficialReceiptViewModel insert(OfficialReceiptViewModel mdl)
        {
            return null;
        }

        public static OfficialReceiptViewModel delete(OfficialReceiptViewModel mdl)
        {
            return null;
        }

        public static OfficialReceiptViewModel update(OfficialReceiptViewModel mdl)
        {
            return null;
        }

        public static OfficialReceiptViewModel get(String ReferenceID)
        {
            return null;
        }

        public static IEnumerable<OfficialReceiptViewModel> find()
        {
            return null;
        }

    }

    
}