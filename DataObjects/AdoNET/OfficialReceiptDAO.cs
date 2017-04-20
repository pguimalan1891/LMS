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

        public List<Dictionary<string,object>> getOfficialReceiptListing(string Status)
        {
            string sql = "Select top(100) b.DESCRIPTION as Status,a.CODE as [OR No],Convert(varchar,a.DATETIME_CREATED,101) as Date,c.DESCRIPTION as Branch,d.DESCRIPTION as Type, " +
            "Case when a.DIRECT_LOAN_RECEIPT_ID is null then '' else e.CODE end as [DLR No], Case when a.CUSTOMER_NAME is null then g.FIRST_NAME + ' ' + g.MIDDLE_NAME + ' ' + g.LAST_NAME else a.CUSTOMER_NAME end as Customer, " +
            "h.DESCRIPTION as [Payment Mode],a.AMOUNT_RECEIVED as [Amount Received],a.BANK_NAME as [Bank Name],a.CHECK_NO as [Check No] " +
            "from official_receipt a  " +
            "inner join document_status_map b on a.DOCUMENT_STATUS_CODE = b.CODE " +
            "inner join organization c on a.ORGANIZATION_ID = c.ID " +
            "left join official_receipt_type d on a.OFFICIAL_RECEIPT_TYPE_ID = d.ID " +
            "left join direct_loan_receipt e on a.DIRECT_LOAN_RECEIPT_ID = e.ID " +
            "left join loan_application f on e.LOAN_APPLICATION_ID = f.ID " +
            "left join uvw_PISData g on f.CURRENT_PIS_ID = g.ID " +
            "left join payment_mode h on a.PAYMENT_MODE_ID = h.ID " +
            "where a.DOCUMENT_STATUS_CODE = " + Status + " " +
            "Order by a.DATETIME_CREATED desc";
            object[] parms = { };
            return db.ReadDictionary(sql, 0, parms);
        }

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
                "AmountReceived",OfficialReceiptModel.AmountReceived,
                "PaymentModeID",OfficialReceiptModel.PaymentModeID,
                "BankName",OfficialReceiptModel.BankID,
                "CheckNo",OfficialReceiptModel.CheckNo,
                "ARAcceleratedDiscount",OfficialReceiptModel.AccelerationDiscount,
                "ARInterestWaived",OfficialReceiptModel.PenaltyWaived,
                "ARPPD",OfficialReceiptModel.PPD,
                "ARTotalDiscount",OfficialReceiptModel.TotalDiscount,
                "SundryTotal", "0.00",
                "BillingPIP",OfficialReceiptModel.PIPDue,
                "BillingGIBCO",OfficialReceiptModel.GIBCODue,
                "BillingRFC",OfficialReceiptModel.RFCDue,
                "BillingPPD",OfficialReceiptModel.PPD,
                "DateDue",OfficialReceiptModel.DateDue,
                "OfficialReviewerID",Guid.NewGuid().ToString(),
                "Notes",OfficialReceiptModel.Notes
            };
            return db.Scalar(sql, 1, parms).AsInt();            
        }

        public int SubmitSundry(OfficialReceipt OfficialReceiptModel,IEnumerable<Sundry> SundryAccount)
        {
            string OfficialReceiptID = Guid.NewGuid().ToString();
            int ret = 0;
            string sql = "usp_SubmitOfficialReceipt";
            object[] parms = {
                "OfficialReceiptID", OfficialReceiptID,
                "ORNumber",OfficialReceiptModel.ORNumber,
                "PreparedByID",OfficialReceiptModel.UserID,
                "RequestedByID",OfficialReceiptModel.UserID,
                "OrganizationID",OfficialReceiptModel.OrganizationID,
                "DirectLoanReceiptNumber",OfficialReceiptModel.DirectLoanReceiptNo,
                "OfficialReceiptType","1",
                "AmountReceived",OfficialReceiptModel.AmountReceived,
                "PaymentModeID",OfficialReceiptModel.PaymentModeID,
                "BankName",OfficialReceiptModel.BankID,
                "CheckNo",OfficialReceiptModel.CheckNo,
                "ARAcceleratedDiscount",OfficialReceiptModel.AccelerationDiscount,
                "ARInterestWaived",OfficialReceiptModel.PenaltyWaived,
                "ARPPD",OfficialReceiptModel.PPD,
                "ARTotalDiscount",OfficialReceiptModel.TotalDiscount,
                "SundryTotal", "0.00",
                "BillingPIP",OfficialReceiptModel.PIPDue,
                "BillingGIBCO",OfficialReceiptModel.GIBCODue,
                "BillingRFC",OfficialReceiptModel.RFCDue,
                "BillingPPD",OfficialReceiptModel.PPD,
                "DateDue",OfficialReceiptModel.DateDue,
                "OfficialReviewerID",Guid.NewGuid().ToString(),
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
    }
}
