using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects.AdoNET
{
    public class OfficialReceiptDAO : IOfficialReceiptDAO
    {
        static DB db = new DB();
        public List<Dictionary<string, object>> getDLRActiveAccounts()
        {
            string sql = "Select doc.Description,dlr.Code as LMSDLRNo,BIS_CODE as DLRNo,Convert(varchar,dlr.REQUESTED_BY_DATETIME,101) as Date,org.DESCRIPTION as Branch,lapp.CODE, " +
            "dlr.ASSIGNED_PROCEEDS,lty.DESCRIPTION as [Loan Type],ltm.DESCRIPTION as Terms,ls.DESCRIPTION as [Loan Set],Convert(varchar,Cast(dlr.APPROVED_MLV as money),1) as MLV from direct_loan_receipt dlr " +
            "left join organization org on dlr.ORGANIZATION_ID = org.ID " +
            "left join loan_application lapp on dlr.LOAN_APPLICATION_ID = lapp.ID " +
            "left join loan_type lty on lapp.LOAN_TYPE_ID = lty.ID " +
            "left join loan_terms ltm on lapp.LOAN_TERMS_ID = ltm.ID " +
            "left join loan_set ls on lapp.LOAN_SET_ID = ls.ID " +
            "left join document_status_map doc on dlr.DOCUMENT_STATUS_CODE = doc.CODE " +
            "where dlr.DOCUMENT_STATUS_CODE = 7 Order by dlr.DATETIME_CREATED Desc";
            object[] parms = { };
            return db.ReadDictionary(sql, 0, parms);
        }

        public List<Dictionary<string, object>> getOfficialReceiptListing(string Status, string CustomerName)
        {
            string sql = "usp_getOfficialReceiptListing";
            object[] parms = {
                "Status",Status,
                "CustomerName", CustomerName
            };
            return db.ReadDictionary(sql, 1, parms);
        }

        public IEnumerable<OfficialReceipt> getOfficialReceipt(string ORNumber)
        {
            string sql = "Select top(100) a.ID,a.CODE as [OR No],Convert(varchar,a.DATETIME_CREATED,101) as ORDate,a.ORGANIZATION_ID,c.DESCRIPTION as Branch,a.OFFICIAL_RECEIPT_TYPE_ID,d.DESCRIPTION as Type,  " +
            "Case when a.DIRECT_LOAN_RECEIPT_ID is null then '' else e.CODE end as [DLR No],f.CODE as LoanApplicationNumber, Case when a.CUSTOMER_NAME is null then g.FIRST_NAME + ' ' + g.MIDDLE_NAME + ' ' + g.LAST_NAME else a.CUSTOMER_NAME end as Customer,  " +
            "a.PAYMENT_MODE_ID,h.DESCRIPTION as PaymentMode,Convert(varchar, cast(a.AMOUNT_RECEIVED as money), 1) as [Amount Received],Isnull(Convert(varchar, Cast((a.BILLING_GIBCO + a.BILLING_PIP + a.BILLING_PPD + a.BILLING_RFC) as money), 1),0.00) as AmountDue,a.BANK_NAME as [Bank Name],a.CHECK_NO as [Check No], " +
            "i.full_name as CreditInvestigator,a.NOTES,a.DATE_DUE,Convert(varchar,Cast(a.BILLING_PIP as money),1) as PIPDue,Convert(varchar,Cast(a.BILLING_GIBCO as money),1) as GIBCODue,Convert(varchar,Cast(a.BILLING_RFC as money),1) as RFCDue,Convert(varchar,Cast(a.BILLING_PPD as money),1) as PPDDue, " +
            "Convert(varchar,Cast(a.AR_ACCELERATED_DISCOUNT as money),1) as ARAcceleratedDiscount,Convert(varchar,Cast(a.AR_INTEREST_WAIVED as money),1) as ARPenaltyWaived,Convert(varchar,Cast(a.AR_PPD as money),1) as ARPPD,Convert(varchar,Cast(a.AR_TOTAL_DISCOUNT as money),1) as ARTotalDiscount, " +
            "Convert(varchar,Cast(a.PIP_MI as money),1) as AR_PIP,Convert(varchar,Cast(a.AR_GIBCO_MI as money),1) as AR_GIBCO,Convert(varchar,Cast(a.AR_RFC_MI as money),1) as AR_RFC,Convert(varchar,Cast(a.AR_TOTAL_RFC as money),1) as AR_TOTAL_RFC,a.OFFICIAL_RECEIPT_TYPE_ID " +
            "from official_receipt a " +
            "inner join document_status_map b on a.DOCUMENT_STATUS_CODE = b.CODE " +
            "inner join organization c on a.ORGANIZATION_ID = c.ID " +
            "left join official_receipt_type d on a.OFFICIAL_RECEIPT_TYPE_ID = d.ID " +
            "left join direct_loan_receipt e on a.DIRECT_LOAN_RECEIPT_ID = e.ID " +
            "left join loan_application f on e.LOAN_APPLICATION_ID = f.ID " +
            "left join uvw_PISData g on f.CURRENT_PIS_ID = g.ID " +
            "left join payment_mode h on a.PAYMENT_MODE_ID = h.ID " +
            "left join user_account i on a.PREPARED_BY_ID = i.ID " +
            "where a.Code = @ORNumber " +
            "Order by a.DATETIME_CREATED desc";
            object[] parms = { "ORNumber", ORNumber };
            return db.Read(sql, selectOfficialReceipt, 0, parms);
        }
        static Func<IDataReader, OfficialReceipt> selectOfficialReceipt = reader =>
           new OfficialReceipt
           {
               ORNumber = reader["OR No"].AsString(),
               ORDate = reader["ORDate"].AsString(),
               Organization = reader["Branch"].AsString(),
               PaymentMode = reader["PaymentMode"].AsString(),
               AmountDue = reader["AmountDue"].AsString(),
               AmountReceived = reader["Amount Received"].AsString(),
               Bank = reader["Bank Name"].AsString(),
               CheckNo = reader["Check No"].AsString(),
               DateDue = reader["DATE_DUE"].AsString(),
               PIPDue = reader["PIPDue"].AsString(),
               GIBCODue = reader["GIBCODue"].AsString(),
               RFCDue = reader["RFCDue"].AsString(),
               PPD = reader["PPDDue"].AsString(),
               LoanAccountNo = reader["LoanApplicationNumber"].AsString(),
               DirectLoanReceiptNo = reader["DLR No"].AsString(),
               CustomerName = reader["Customer"].AsString(),
               CreditInvestigator = reader["CreditInvestigator"].AsString(),
               AccelerationDiscount = reader["ARAcceleratedDiscount"].AsString(),
               PenaltyWaived = reader["ARPenaltyWaived"].AsString(),
               PromptPaymentDiscount = reader["ARPPD"].AsString(),
               TotalDiscount = reader["ARTotalDiscount"].AsString(),
               PIP = reader["AR_PIP"].AsString(),
               GIBCO = reader["AR_GIBCO"].AsString(),
               RFC = reader["AR_RFC"].AsString(),
               TotalRFC = reader["AR_TOTAL_RFC"].AsString(),
               OfficialReceiptType = reader["OFFICIAL_RECEIPT_TYPE_ID"].AsString()               
           };
        public IEnumerable<Sundry> getSundry(string ORNumber)
        {
            string sql = "Select a.ID,a.OFFICIAL_RECEIPT_ID,a.ACCOUNT_TYPE_ID,b.Description as ACCOUNT_TYPE,Convert(varchar,Cast(a.Amount as money),1) as Amount from official_receipt_account a " +
            "inner join cmdm_account_type b on a.ACCOUNT_TYPE_ID = b.ID " +
            "inner join official_receipt c on a.OFFICIAL_RECEIPT_ID = c.ID " +
            "where c.CODE = @ORNumber";
            object[] parms = { "ORNumber", ORNumber };
            return db.Read(sql, selectSundryAccount, 0, parms);
        }
        static Func<IDataReader, Sundry> selectSundryAccount = reader =>
           new Sundry
           {
               ID = reader["ID"].AsString(),
               CMDMAccountTypeID = reader["ACCOUNT_TYPE_ID"].AsString(),
               CMDMAccountType = reader["ACCOUNT_TYPE"].AsString(),
               SundryAmount = reader["Amount"].AsString()
           };
        public IEnumerable<PaymentMode> getPaymentMode()
        {
            string sql = "Select ID,Code,Description from payment_mode";
            object[] parms = { };
            return db.Read(sql, selectPaymentMode, 0, parms);
        }

        static Func<IDataReader, PaymentMode> selectPaymentMode = reader =>
           new PaymentMode
           {
               PaymentModeID = reader["ID"].AsString(),
               Code = reader["Code"].AsString(),
               Description = reader["Description"].AsString()
           };
        public IEnumerable<Bank> getBank()
        {
            string sql = "Select ID,Code,Description into #Bank from bank; Insert into #Bank Select '0','',''; Select * from #Bank Order by ID";
            object[] parms = { };
            return db.Read(sql, selectBank, 0, parms);
        }

        static Func<IDataReader, Bank> selectBank = reader =>
           new Bank
           {
               BankID = reader["ID"].AsString(),
               Code = reader["Code"].AsString(),
               Description = reader["Description"].AsString()
           };

        public IEnumerable<CMDMAccountType> getCMDMAccountType()
        {
            string sql = "Select ID,CODE,CODE+'-'+DESCRIPTION AS DESCRIPTION from cmdm_account_type order by CODE";
            object[] parms = { };
            return db.Read(sql, selectCMDMAccountType, 0, parms);
        }

        static Func<IDataReader, CMDMAccountType> selectCMDMAccountType = reader =>
           new CMDMAccountType
           {
               CMDMAccountTypeID = reader["ID"].AsString(),
               Code = reader["Code"].AsString(),
               Description = reader["Description"].AsString()
           };

        public List<Dictionary<string, object>> getCollectionDues(string DLRNumber)
        {
            string sql = "usp_getCollectionDues";
            object[] parms = { "DLRNumber", DLRNumber };
            return db.ReadDictionary(sql, 1, parms);
        }

        public string getServerDate()
        {
            string sql = "Select Convert(varchar,Getdate(),101)";
            object[] parms = { };
            return db.Scalar(sql, 0, parms).AsString();
        }

        public int SubmitOfficialReceipt(OfficialReceipt OfficialReceiptModel)
        {
            string sql = "usp_SubmitOfficialReceipt";
            object[] parms = {
                "OfficialReceiptID", Guid.NewGuid().ToString(),
                "ORNumber",OfficialReceiptModel.ORNumber,
                "PreparedByID",OfficialReceiptModel.UserID,
                "RequestedByID",OfficialReceiptModel.UserID,
                "OrganizationID",OfficialReceiptModel.OrganizationID,
                "DirectLoanReceiptNumber",OfficialReceiptModel.DirectLoanReceiptNo,
                "OfficialReceiptType","1",
                "AmountReceived",OfficialReceiptModel.AmountReceived.Replace(",",""),
                "PaymentModeID",OfficialReceiptModel.PaymentModeID,
                "BankName",OfficialReceiptModel.BankID,
                "CheckNo",OfficialReceiptModel.CheckNo,
                "ARAcceleratedDiscount",OfficialReceiptModel.AccelerationDiscount.Replace(",",""),
                "ARInterestWaived",OfficialReceiptModel.PenaltyWaived.Replace(",",""),
                "ARPPD",OfficialReceiptModel.PromptPaymentDiscount.Replace(",",""),
                "ARTotalDiscount",OfficialReceiptModel.TotalDiscount.Replace(",",""),
                "ARRFC",OfficialReceiptModel.RFC.Replace(",",""),
                "ARTOTALRFC",OfficialReceiptModel.TotalRFC.Replace(",",""),
                "ARGIBCO",OfficialReceiptModel.GIBCO.Replace(",",""),
                "ARPIP",OfficialReceiptModel.PIP.Replace(",",""),
                "SundryTotal", "0.00",
                "BillingPIP",OfficialReceiptModel.PIPDue.Replace(",",""),
                "BillingGIBCO",OfficialReceiptModel.GIBCODue.Replace(",",""),
                "BillingRFC",OfficialReceiptModel.RFCDue.Replace(",",""),
                "BillingPPD",OfficialReceiptModel.PPD.Replace(",",""),
                "DateDue",OfficialReceiptModel.DateDue,
                "OfficialReviewerID",Guid.NewGuid().ToString(),
                "CustomerName",OfficialReceiptModel.CustomerName,
                "Notes",OfficialReceiptModel.Notes
            };
            return db.Scalar(sql, 1, parms).AsInt();            
        }

        public int SubmitSundry(OfficialReceipt OfficialReceiptModel,IEnumerable<Sundry> SundryAccount)
        {
            string OfficialReceiptID = Guid.NewGuid().ToString();
            int ret = 0;
            string sql = "[usp_SubmitOfficialReceipt]";
            object[] parms = {
                "OfficialReceiptID", OfficialReceiptID,
                "ORNumber",OfficialReceiptModel.ORNumber,
                "PreparedByID",OfficialReceiptModel.UserID,
                "RequestedByID",OfficialReceiptModel.UserID,
                "OrganizationID",OfficialReceiptModel.OrganizationID,
                "DirectLoanReceiptNumber",OfficialReceiptModel.DirectLoanReceiptNo,
                "OfficialReceiptType","2",
                "AmountReceived",OfficialReceiptModel.AmountReceived.Replace(",",""),
                "PaymentModeID",OfficialReceiptModel.PaymentModeID,
                "BankName",OfficialReceiptModel.BankID,
                "CheckNo",OfficialReceiptModel.CheckNo,
                "ARAcceleratedDiscount","0.00",
                "ARInterestWaived","0.00",
                "ARPPD","0.00",
                "ARTotalDiscount","0.00",
                "ARRFC","0.00",
                "ARTOTALRFC","0.00",
                "ARGIBCO","0.00",
                "ARPIP","0.00",
                "SundryTotal", "0.00",
                "BillingPIP","0.00",
                "BillingGIBCO","0.00",
                "BillingRFC","0.00",
                "BillingPPD","0.00",
                "DateDue",OfficialReceiptModel.DateDue,
                "OfficialReviewerID",Guid.NewGuid().ToString(),
                "CustomerName",OfficialReceiptModel.CustomerName,
                "Notes",OfficialReceiptModel.Notes
            };
            ret = db.Scalar(sql, 1, parms).AsInt();
            if (ret != 0)
            {
                return ret;
            }            
            foreach(Sundry md in SundryAccount)
            {
                sql = "INSERT INTO Final_Testing.dbo.official_receipt_account (id,official_receipt_id,account_type_id,amount) " +
                    "VALUES ('" + md.ID + "','" + OfficialReceiptID + "','" + md.CMDMAccountTypeID + "'," + md.SundryAmount + ");Select 0 as Message ";
                object[] parmsSundry = { };
                ret = db.Scalar(sql, 0, parmsSundry).AsInt();
                if (ret != 0)
                {
                    break;
                }
            }
            return ret;
        }

        public int UpdateOfficialReceipt(OfficialReceipt OfficialReceiptModel, string isFinalize)
        {            
            string sql = "usp_UpdateOfficialReceipt";
            object[] parms = {
                "@ORNumber", OfficialReceiptModel.ORNumber,
                "@isFinalize",isFinalize,
                "@UserID",OfficialReceiptModel.UserID,
                "@GLID",Guid.NewGuid().ToString(),
                "@GLAccountID20",Guid.NewGuid().ToString(),
                "@GLAccountID24",Guid.NewGuid().ToString(),
                "@GLAccountID03",Guid.NewGuid().ToString(),
                "@GLAccountID25",Guid.NewGuid().ToString(),
                "@GLAccountID21",Guid.NewGuid().ToString(),
                "@GLAccountID22",Guid.NewGuid().ToString(),
                "@GLAccountID23",Guid.NewGuid().ToString(),
                "@GLReviewerID",Guid.NewGuid().ToString(),
                "@DCCRID",Guid.NewGuid().ToString(),
                "@LedgerLine1",Guid.NewGuid().ToString(),
                "@LedgerLine2",Guid.NewGuid().ToString(),
                "@LedgerLine3",Guid.NewGuid().ToString(),
                "@EffectiveYieldID",Guid.NewGuid().ToString()                
            };
            return db.Scalar(sql, 1, parms).AsInt();             
        }
    }
}
