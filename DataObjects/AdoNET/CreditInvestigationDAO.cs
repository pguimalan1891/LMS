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
