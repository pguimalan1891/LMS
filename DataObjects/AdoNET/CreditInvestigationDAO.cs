using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects.AdoNET
{
    public class CreditInvestigationDAO: ICreditInvestigationDAO
    {
        static DB db = new DB();
        public IEnumerable<BusinessObjects.CRIncome> getIncome(string BorrowerID)
        {
            string sql = "SELECT 'EMPLOYED_INCOME' as IncomeType,ISNULL(SUM(INCOME),0) as Income "
                      + " FROM[dbo].[pis_employment]  where PIS_ID = @pis_id and BUSINESS_TYPE_ID = '2'"
                      + " UNION ALL"
                      + " SELECT 'SPOUSE_INCOME',ISNULL(SUM(INCOME), 0)"
                      + " FROM[dbo].[pis_employment]  where PIS_ID = @pis_id and IS_SPOUSE = 'Y'"
                        + " UNION ALL "
                      + " SELECT 'BUSINESS_INCOME',ISNULL(SUM(INCOME), 0) "
                     + "  FROM[dbo].[pis_employment] where PIS_ID = @pis_id and BUSINESS_TYPE_ID = '1' ";
            object[] parms = { "pis_id", BorrowerID };
            return db.Read(sql, selectIncome, 0, parms);
        }

        static Func<IDataReader, BusinessObjects.CRIncome> selectIncome = reader =>
          new BusinessObjects.CRIncome
          {
              IncomeType = reader["IncomeType"].AsString(),
              IncomeAmount = reader["Income"].AsString()
          };


        public IEnumerable<BusinessObjects.LoanList> getLoanApplicationListing( string searchkey)
        {
            string status ="31";
            if (status == "-1")
            {
                status = "[All]";
            }
            string sql = "select top 1000 " +
                " loan_application.ID, " +
                " dm.DESCRIPTION as [Status], " +
                " loan_application.code as LA_No,  " +
                " convert(varchar, loan_application.DATETIME_CREATED, 101) as [Date], " +
                " org.DESCRIPTION as Branch, " +
                " pis.LAST_NAME + ' ,' + pis.FIRST_NAME + ' ' + pis.MIDDLE_NAME as Customer, " +
                " city.DESCRIPTION + ', ' + province.DESCRIPTION as [CustomerAddress], " +
                " loan_type.DESCRIPTION as Product, " +
                " loan_application.ORIGINAL_MLV as Desired, " +
                " loan_application.RECOMMENDED_MLV as Recommended, " +
                " loan_application.APPROVED_MLV as Approved, " +
                " loan_set.DESCRIPTION as [Set], " +
                " loan_terms.DESCRIPTION as [Terms], " +
                " loan_application.PURPOSE_OF_LOAN as Purpose, " +
                " user_account.LAST_NAME + ', ' + user_account.FIRST_NAME as CCI, " +
                " ISNULL((select document_status_map.description from document_status_map where document_status_map.code = ci.document_status_code), 'Not Available') as CiStatus " +
                " from loan_application " +
                " inner join document_status_map dm on dm.CODE = loan_application.DOCUMENT_STATUS_CODE " +
                " inner " +
                " join organization org on org.ID = loan_application.ORGANIZATION_ID " +
                " inner " +
                " join pis on pis.ID = loan_application.PIS_ID " +

                " inner " +
                " join pis_address on pis_address.PIS_ID = loan_application.PIS_ID " +
                " inner " +
                " join city on city.ID = pis_address.CITY_ID " +
                " inner " +
                " join province on province.ID = city.PROVINCE_ID " +
                " inner " +
                " join loan_type on loan_type.ID = loan_application.LOAN_TYPE_ID " +
                " inner " +
                " join loan_set on loan_set.ID = loan_application.LOAN_SET_ID " +
                " inner " +
                " join loan_terms on loan_terms.ID = loan_application.LOAN_TERMS_ID " +
                " inner " +
                " join user_account on user_account.ID = loan_application.REQUESTED_BY_ID " +
                " inner " +
                " join credit_investigation ci on ci.LOAN_APPLICATION_ID = loan_application.id " +
                " inner " +
                " join document_status_map ciStat on ciStat.CODE = ci.DOCUMENT_STATUS_CODE";

            if (status == "[Approval]" && searchkey == "[All]")
            {
                object[] parms = { "stat", status, "search", searchkey };
                sql += "  where dm.Description like '% Approval' ";
                return db.Read(sql, selectLoanList, 0, parms);
            }
            else if (status == "[Approval]" && searchkey != "[All]")
            {
                object[] parms = { "stat", status, "search", searchkey };
                sql += "  where  dm.Description like '% Approval'  and (pis.LAST_NAME like '%'+@search+'%' OR pis.FIRST_NAME like '%'+@search+'%' OR pis.CODE like '%'+@search+'%') ";
                return db.Read(sql, selectLoanList, 0, parms);
            }
            else
            if (status != "[All]" && searchkey == "[All]")
            {
                sql += " where loan_application.DOCUMENT_STATUS_CODE = @stat ";
                object[] parms = { "stat", status };
                return db.Read(sql, selectLoanList, 0, parms);
            }
            else if (status != "[All]" && searchkey != "[All]")
            {
                object[] parms = { "stat", status, "search", searchkey };
                sql += "  where loan_application.DOCUMENT_STATUS_CODE = @stat  and (pis.LAST_NAME like '%'+@search+'%' OR pis.FIRST_NAME like '%'+@search+'%' OR pis.CODE like '%'+@search+'%') ";
                return db.Read(sql, selectLoanList, 0, parms);
            }
            else if (status == "[All]" && searchkey != "[All]")
            {
                object[] parms = { "search", searchkey };
                sql += " where (pis.LAST_NAME like '%'+@search+'%' OR pis.FIRST_NAME like '%'+@search+'%' OR pis.CODE like '%'+@search+'%') ";
                return db.Read(sql, selectLoanList, 0, parms);
            }
            else
            {
                object[] parms = { };

                return db.Read(sql, selectLoanList, 0, parms);
            }

        }

        static Func<IDataReader, BusinessObjects.LoanList> selectLoanList = reader =>
           new BusinessObjects.LoanList
           {

               ID = reader["ID"].ToString(),
               LA_No = "<a href='/" + reader["LA_No"].AsString() + "'>" + reader["LA_No"].AsString() + "</a>",
               Status = reader["Status"].AsString(),
               Date = reader["Date"].AsString(),
               Branch = reader["Branch"].AsString(),
               Customer = reader["Customer"].AsString(),
               CustomerAddress = reader["CustomerAddress"].AsString(),
               Product = reader["Product"].AsString(),
               Desired = reader["Desired"].AsString(),
               Recommended = reader["Recommended"].AsString(),
               Approved = reader["Approved"].AsString(),
               Set = reader["Set"].AsString(),
               Terms = reader["Terms"].AsString(),
               Purpose = reader["Purpose"].AsString(),
               CCI = reader["CCI"].AsString(),
               CiStatus = reader["CiStatus"].AsString()
           };

        public IEnumerable<BusinessObjects.CreditInvestigation> getCRForm(string code)
        {
            string sql = "SELECT ID "
      + "  ,  CODE  "
      + "  ,  DATETIME_CREATED "
      + "  ,  ORGANIZATION_ID "
      + "  ,  LOAN_APPLICATION_ID "
      + "  ,  NEIGHBORHOOD_NOTES "
      + "  ,  ENVIRONMENT_NOTES "
      + "  ,  INCOME "
      + "  ,  DEDUCTION "
      + "  ,  NET_INCOME "
      + "  ,  SPOUSE_INCOME "
      + "  ,  SPOUSE_DEDUCTION "
      + "  ,  SPOUSE_NET_INCOME "
      + "  ,  BUSINESS_INCOME "
      + "  ,  OTHER_INCOME "
      + "  ,  TOTAL_INCOME "
      + "  ,  LIVING_EXPENSES "
      + "  ,  RENTALS "
      + "  ,  UTILITY "
      + "  ,  EDUCATION "
      + "  ,  AMORTIZATION "
      + "  ,  TRANSPORTATION "
      + "  ,  OTHER_EXPENSES "
      + "  ,  TOTAL_EXPENSES "
      + "  ,  GROSS_DISPOSABLE_INCOME "
      + "  ,  CLASS_AMOUNT "
      + "  ,  NET_DISPOSABLE_INCOME "
      + "  ,  MI_RESULT "
      + "  ,  EXCESS_AMOUNT "
      + "  ,  NOTES "
    + " FROM dbo.credit_investigation "
    + " where CODE = @CI_CODE";
            object[] parms = { "CI_CODE", code };
            return db.Read(sql, selectCRForm, 0, parms);
        }

        static Func<IDataReader, BusinessObjects.CreditInvestigation> selectCRForm = reader =>
          new BusinessObjects.CreditInvestigation
          {
              ID = reader["ID"].AsString(),
              CINumber = reader["CODE"].AsString(),
              Date = reader["DATETIME_CREATED"].AsString(),
              Neighborhood = reader["NEIGHBORHOOD_NOTES"].AsString(),
              LivingConditions = reader["ENVIRONMENT_NOTES"].AsString(),
              Income = reader["INCOME"].AsString(),
              Deduction = reader["DEDUCTION"].AsString(),
              NetIncome = reader["NET_INCOME"].AsString(),
              spouseIncome = reader["SPOUSE_INCOME"].AsString(),
              spouseDeduction = reader["SPOUSE_DEDUCTION"].AsString(),
              spouseNetIncome = reader["SPOUSE_NET_INCOME"].AsString(),
              BusinessIncome = reader["BUSINESS_INCOME"].AsString(),
              OtherIncome = reader["OTHER_INCOME"].AsString(),
              TotalIncome = reader["TOTAL_INCOME"].AsString(),
              LivingExpenses = reader["LIVING_EXPENSES"].AsString(),
              Rentals = reader["RENTALS"].AsString(),
              LightWater = reader["UTILITY"].AsString(),
              Education = reader["EDUCATION"].AsString(),
              Amortization = reader["AMORTIZATION"].AsString(),
              Transporatation = reader["TRANSPORTATION"].AsString(),
              OtherExpense = reader["OTHER_EXPENSES"].AsString(),
              TotalExpense = reader["TOTAL_EXPENSES"].AsString(),
              GDI = reader["GROSS_DISPOSABLE_INCOME"].AsString(),
              SummaryClass = reader["CLASS_AMOUNT"].AsString(),
              NDI = reader["NET_DISPOSABLE_INCOME"].AsString(),
              SummaryMonthlyInstallment = reader["MI_RESULT"].AsString(),
              Excess = reader["EXCESS_AMOUNT"].AsString(),
              Notes = reader["NOTES"].AsString()
          };
    }


}
