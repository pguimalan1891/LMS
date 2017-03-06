using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects.AdoNET
{
    public class LoanApplicationDAO : ILoanApplicationDAO
    {
        static DB db = new DB();

        public List<Dictionary<string, object>> GetBorrowers(string searchkey)
        {
            string sql = "" +
            "select pis.id,document_status_map.description as [Status], pis.code as [Code], pis.last_name as [Last Name], pis.first_name as [First Name], pis.middle_name as [Middle Name], Convert(varchar,pis.date_of_birth,101) as [Date of Birth], " +
            "gender.description as [Gender], civil_status.description as [Civil Status], pis_address.street_address as [Address], city.description as [City] " +
            "from Final_Testing.dbo.pis inner join Final_Testing.dbo.document_status_map   on (pis.document_status_code = document_status_map.code)  inner join Final_Testing.dbo.user_account prepared_by   on (pis.prepared_by_id = prepared_by.id)  inner join Final_Testing.dbo.gender   on (pis.gender_id = gender.id)  inner join Final_Testing.dbo.civil_status   on (pis.civil_status_id = civil_status.id)  left join Final_Testing.dbo.pis_address   on (pis_address.pis_id = pis.id and pis_address.address_type_id = '0')  inner join Final_Testing.dbo.city on(pis_address.city_id = city.id)  inner join Final_Testing.dbo.province   on(city.province_id = province.id)  inner join Final_Testing.dbo.organization   on(pis.organization_id = organization.id)  where pis.permission > 0 ";
            object[] parms = { };
            return null;
        }
        public IEnumerable<BusinessObjects.LoanType> getProducts()
        {
            string sql = "SELECT [ID] " +
                         ",[CODE] " +
                         ",[DESCRIPTION] " +
                         ",[REQUIRE_COLLATERAL] " +
                         ",[WITH_DST] " +
                         ",[WITH_CV] " +
                         ",[BALLOON_PAYMENT] " +
                         ",[MIGID] " +
                       "FROM[FINAL_TESTING].[dbo].[loan_type]"; object[] parms = { };
            return db.Read(sql, selectLoanType, 0, parms);
        }

        static Func<IDataReader, BusinessObjects.LoanType> selectLoanType = reader =>
           new BusinessObjects.LoanType
           {
               ID = reader["ID"].AsString(),
               CODE = reader["CODE"].AsString(),
               DESCRIPTION = reader["DESCRIPTION"].AsString(),
               REQUIRE_COLLATERAL = reader["REQUIRE_COLLATERAL"].AsString(),
               WITH_DST = reader["WITH_DST"].AsString(),
               WITH_CV = reader["WITH_CV"].AsString(),
               BALLOON_PAYMENT =  reader["BALLOON_PAYMENT"].AsString(),
               MIGID = reader["MIGID"].AsString()
           };

        public IEnumerable<BusinessObjects.LoanSet> getLoanSet()
        {
            string sql = "SELECT [ID] " +
                         ",[CODE] " +
                         ",[DESCRIPTION] " +
                         ",[MIGID] " +
                       "FROM[FINAL_TESTING].[dbo].[loan_set]"; object[] parms = { };
            return db.Read(sql, selectLoanSet, 0, parms);
        }

        static Func<IDataReader, BusinessObjects.LoanSet> selectLoanSet = reader =>
           new BusinessObjects.LoanSet
           {
               ID = reader["ID"].AsString(),
               CODE = reader["CODE"].AsString(),
               DESCRIPTION = reader["DESCRIPTION"].AsString(),
               MIGID = reader["MIGID"].AsString()
           };

        public IEnumerable<BusinessObjects.LoanTerms> getLoanTerms()
        {
            string sql = "SELECT [ID] " +
                         ",[CODE] " +
                         ",[DESCRIPTION] " +
                         ",[MONTHS] " +
                         ",[MIGID] " +
                       "FROM[FINAL_TESTING].[dbo].[loan_terms]"; object[] parms = { };
            return db.Read(sql, selectLoanTerms, 0, parms);
        }

        static Func<IDataReader, BusinessObjects.LoanTerms> selectLoanTerms = reader =>
           new BusinessObjects.LoanTerms
           {
               ID = reader["ID"].AsString(),
               CODE = reader["CODE"].AsString(),
               DESCRIPTION = reader["DESCRIPTION"].AsString(),
               MONTHS = reader["MONTHS"].AsString(),
               MIGID = reader["MIGID"].AsString()
           };


        public IEnumerable<BusinessObjects.BorrowerProfile> getBorrowerProfile(string borrowerCode)
        {
            string sql = "select pis.CODE,(pis.LAST_NAME+', '+pis.FIRST_NAME) as Fullname, pa.PHONE_NUMBER as LandLine, pa.MOBILE_NUMBER as MobileNumber, " +
                         "(city.DESCRIPTION+', '+province.DESCRIPTION) as ProvinceCity, " +
                         " '' as Barangay, pa.STREET_ADDRESS, pa.POSTAL_CODE " +
                         " from pis inner join pis_address pa on pa.PIS_ID = pis.ID " +
                         " inner join city on city.ID = pa.CITY_ID  " +
                         " inner join province on province.ID = city.PROVINCE_ID where pis.CODE = @BorrowerCode";
                object[] parms = { "BorrowerCode", borrowerCode };
            return db.Read(sql, selectBorrowerProfile, 0, parms);
        }

        static Func<IDataReader, BusinessObjects.BorrowerProfile> selectBorrowerProfile = reader =>
           new BusinessObjects.BorrowerProfile
           {
               CODE = reader["CODE"].AsString(),
               Fullname = reader["Fullname"].AsString(),
               LandLine = reader["LandLine"].AsString(),
               MobileNumber = reader["MobileNumber"].AsString(),
               ProvinceCity = reader["ProvinceCity"].AsString(),
               Barangay = reader["Barangay"].AsString(),
               STREET_ADDRESS = reader["STREET_ADDRESS"].AsString(),
               POSTAL_CODE = reader["POSTAL_CODE"].AsString(),
           };


    }
}
