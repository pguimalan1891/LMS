using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using BusinessObjects;

namespace DataObjects.AdoNET
{
    public class BookingDAO: IBookingDAO
    {
        static DB db = new DB();

        public List<Dictionary<string,object>> getBookingRecords(int status)
        {
            string sql = "RetrieveBookingRecords";

            object[] parms = { "@statuscode", status };
            return db.ReadDictionary(sql, 1, parms);
        }

        public List<Dictionary<string, object>> getCheckVoucher(int status)
        {
            string sql = "usp_getCheckVoucher";

            object[] parms = { "@statuscode", status };
            return db.ReadDictionary(sql, 1, parms);
        }

        public List<Dictionary<string, object>> getCIRForm(int status)
        {
            string sql = "usp_getCIRForm";

            object[] parms = { "@statuscode", status };
            return db.ReadDictionary(sql, 1, parms);
        }

        public List<Dictionary<string, object>> getDisbursementVoucher(int status)
        {
            string sql = "usp_getDisbursementVoucher";

            object[] parms = { "@statuscode", status };
            return db.ReadDictionary(sql, 1, parms);
        }

        public List<Dictionary<string, object>> getChangeCCIForm(int status)
        {
            string sql = "usp_getChangeCCIForm";

            object[] parms = { "@statuscode", status };
            return db.ReadDictionary(sql, 1, parms);
        }

        public DLRModel getDLR(string lmscode)
        {
            string sql = "usp_getDLR";

            object[] parms = { "@lmsno", lmscode };
            
            var data = db.ReadDictionary(sql, 1, parms);
            DLRModel mdl = new DLRModel();
            foreach (var d in data)
            {
                mdl.LMSCode = d["code"].ToString();
                mdl.DLRNo = d["bis_code"].ToString();
                mdl.DLRDate = d["datetime_created"].ToString();
                mdl.Branch = d["branch"].ToString();
                mdl.ApplicationTypeDesc = d["ApplicationTypeDesc"].ToString();
                BorrowerInfoModel brw = new BorrowerInfoModel();
                brw.FullName = d["LAST_NAME"].ToString() + ", " + d["FIRST_NAME"].ToString() + " " + d["MIDDLE_NAME"].ToString() ;
                brw.BorrowerCode = d["OWNER_CODE"].ToString();
                brw.Province = d["province_name"].ToString();
                brw.City = d["city_name"].ToString();
                brw.StreetAddress = d["street_address"].ToString();
                brw.PostalCode = d["postal_code"].ToString();
                brw.LandlineNo = d["phone_number"].ToString();
                brw.MobileNo = d["mobile_number"].ToString();
                brw.Brgy = d["barangay_name"].ToString();
                mdl.Borrower = brw;
                LoanInfoModel ln = new LoanInfoModel();
                ln.LoanApplicationNo= d["loan_application_code"].ToString();
                ln.LoanTypeDesc = d["loan_type_desc"].ToString();
                ln.LoanSetDesc = d["loan_set_desc"].ToString();
                ln.LoanTermDesc = d["loan_term_desc"].ToString();
                ln.DesiredMLV = Convert.ToDouble(d["LOAN_AMOUNT"]).ToString("n2");
                ln.ApprovedMLV = Convert.ToDouble(d["approved_mlv"]).ToString("n2");
                ln.FirstDueDate = d["first_due_date"].ToString();
                ln.AddOnRate = d["add_on_rate"].ToString();
                mdl.Loan = ln;
            }
            return mdl;
        }
    }
}
