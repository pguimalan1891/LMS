using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace DataObjects.AdoNET
{
    public class BookingDAO: IBookingDAO
    {
        static DB db = new DB();

        public List<Dictionary<string,object>> getBookingRecords()
        {
            StringBuilder sql = new StringBuilder();

            sql.Append("select direct_loan_receipt.id,document_status_map.description as dsm_description, direct_loan_receipt.code, ");
            sql.Append("direct_loan_receipt.bis_code, direct_loan_receipt.datetime_created, organization.description branch_name,");
            sql.Append("loan_application.code loan_application_code,");
            sql.Append("--cast('~/Transactions/RFC/LoanApplication.aspx?state=4&code=' + loan_application.code as char(100)) as loan_application_hyperlink,");
            sql.Append("pis.first_name + ' ' + pis.middle_name + ' ' + pis.last_name as applicants_name,");
            sql.Append("--cast('~/Transactions/RFC/PIS.aspx?state=4&id=' + pis.id as char(100)) as pis_hyperlink,");
            sql.Append("loan_type.description as loan_type_description, loan_terms.description as loan_terms_description, loan_set.description as loan_set_description,");
            sql.Append("direct_loan_receipt.approved_mlv as approved_mlv,");
            sql.Append("direct_loan_receipt.prepared_by_datetime as prepared_by_datetime, prepared_by.last_name + ', ' + prepared_by.first_name as prepared_by_name");
            sql.Append("--, row_number() OVER");
            sql.Append("--(INSERT ORDER BY HERE) as row_num");
            sql.Append("from Final_Testing.dbo.direct_loan_receipt inner join Final_Testing.dbo.document_status_map");
            sql.Append("on (direct_loan_receipt.document_status_code = document_status_map.code)  inner");
            sql.Append("join Final_Testing.dbo.user_account prepared_by");
            sql.Append("on (direct_loan_receipt.prepared_by_id = prepared_by.id)  inner");
            sql.Append("join Final_Testing.dbo.loan_application");
            sql.Append("on (direct_loan_receipt.loan_application_id = loan_application.id)  inner");
            sql.Append("join Final_Testing.dbo.pis");
            sql.Append("on (loan_application.current_pis_id = pis.id)  inner");
            sql.Append("join Final_Testing.dbo.loan_type   on (loan_application.loan_type_id = loan_type.id)");
            sql.Append("inner");
            sql.Append("join Final_Testing.dbo.loan_terms   on (loan_application.loan_terms_id = loan_terms.id)  inner");
            sql.Append("join Final_Testing.dbo.loan_set");
            sql.Append("on (loan_application.loan_set_id = loan_set.id)  inner");
            sql.Append("join Final_Testing.dbo.organization   on (direct_loan_receipt.organization_id = organization.id)  where 1 = 1");

            sql.Append("and direct_loan_receipt.document_status_Code = 14");

            object[] parms = { };
            return db.ReadDictionary(sql.ToString(), 0, parms);
        }
    }
}
