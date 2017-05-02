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
                ln.NetMonthlyInstallment = Convert.ToDouble(d["net_monthly_installment"]).ToString("n2");
                mdl.Loan = ln;
                GIBCOModel gibco = new GIBCOModel();
                gibco.GIBCOBasic1 = Convert.ToDouble(d["gibco_basic_1"]).ToString("n2");
                gibco.GIBCOBasic2 = Convert.ToDouble(d["gibco_basic_2"]).ToString("n2");
                gibco.GIBCOBasic3 = Convert.ToDouble(d["gibco_basic_3"]).ToString("n2");
                gibco.GIBCOBasic4 = Convert.ToDouble(d["gibco_basic_4"]).ToString("n2");
                gibco.GIBCOBasic5 = Convert.ToDouble(d["gibco_basic_5"]).ToString("n2");
                
                gibco.GIBCOYear1 = Convert.ToDouble(d["gibco_first_year"]).ToString("n2");
                gibco.GIBCOYear2 = Convert.ToDouble(d["gibco_second_year"]).ToString("n2");
                gibco.GIBCOYear3 = Convert.ToDouble(d["gibco_third_year"]).ToString("n2");
                gibco.GIBCOYear4 = Convert.ToDouble(d["gibco_fourth_year"]).ToString("n2");
                gibco.GIBCOYear5 = Convert.ToDouble(d["gibco_fifth_year"]).ToString("n2");

                gibco.GMMU1 = Convert.ToDouble(d["gmmu_1"]).ToString("n2");
                gibco.GMMU2 = Convert.ToDouble(d["gmmu_2"]).ToString("n2");
                gibco.GMMU3 = Convert.ToDouble(d["gmmu_3"]).ToString("n2");
                gibco.GMMU4 = Convert.ToDouble(d["gmmu_4"]).ToString("n2");
                gibco.GMMU5 = Convert.ToDouble(d["gmmu_5"]).ToString("n2");

                gibco.GIBCODate1 = d["gibco_date_1"].ToString();
                gibco.GIBCODate2 = d["gibco_date_2"].ToString();
                gibco.GIBCODate3 = d["gibco_date_3"].ToString();
                gibco.GIBCODate4 = d["gibco_date_4"].ToString();
                gibco.GIBCODate5 = d["gibco_date_5"].ToString();

                mdl.GIBCO = gibco;

                OutrightPayments outpayment = new OutrightPayments();
                outpayment.HandlingORCode = d["or_handling_id"].ToString();
                if (outpayment.HandlingORCode != String.Empty)
                {
                    outpayment.HandlingORDate = d["datetime_created"].ToString();
                }
                outpayment.ProcessingORCode = d["or_processing_fee_id"].ToString();
                if (outpayment.ProcessingORCode != String.Empty)
                {
                    outpayment.ProcessingORDate = d["datetime_created"].ToString();
                }
                outpayment.DSTORCode = d["or_dst_id"].ToString();
                if (outpayment.DSTORCode != String.Empty)
                {
                    outpayment.DSTORDate = d["datetime_created"].ToString();
                }
                outpayment.PIPPVAOORCode = d["or_pip_pvao_id"].ToString();
                if (outpayment.PIPPVAOORCode != String.Empty)
                {
                    outpayment.PIPPVAOORDate = d["datetime_created"].ToString();
                }
                outpayment.RestructuringFeeORCode = d["or_restructuring_id"].ToString();
                if (outpayment.RestructuringFeeORCode != String.Empty)
                {
                    outpayment.RestructuringFeeORDate = d["datetime_created"].ToString();
                }

                outpayment.HandlingORAmount = d["or_amount_handling"].ToString();
                outpayment.ProcessingORAmount = d["or_amount_processing_fee"].ToString();
                outpayment.DSTORAmount = d["or_amount_dst"].ToString();
                outpayment.PIPPVAOORAmount = d["or_amount_pip_pvao"].ToString();
                outpayment.RestructuringFeeORAmount = d["or_amount_restructuring"].ToString();

                mdl.Outpayments = outpayment;
            }
            return mdl;
        }
    }
}
