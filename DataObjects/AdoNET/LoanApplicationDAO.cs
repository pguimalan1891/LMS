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

        public IEnumerable<BusinessObjects.newLoanBorrowerProfile> GetBorrowers(string searchkey)
        {
            string sql = "select '../../Application/NewLoanApplication/?borrower='+pis.CODE as newLoanLink, pis.id,document_status_map.description as [Status], pis.code as [Code], pis.last_name as [LastName], pis.first_name as [FirstName], pis.middle_name as [MiddleName], Convert(varchar,pis.date_of_birth,101) as [DateofBirth],  " +
            "gender.description as [Gender], civil_status.description as [CivilStatus], pis_address.street_address as [Address], city.description as [City] " +
            "from Final_Testing.dbo.pis inner join Final_Testing.dbo.document_status_map   on (pis.document_status_code = document_status_map.code)  inner join Final_Testing.dbo.user_account prepared_by   on (pis.prepared_by_id = prepared_by.id)  inner join Final_Testing.dbo.gender   on (pis.gender_id = gender.id)  inner join Final_Testing.dbo.civil_status   on (pis.civil_status_id = civil_status.id)  left join Final_Testing.dbo.pis_address   on (pis_address.pis_id = pis.id and pis_address.address_type_id = '0')  inner join Final_Testing.dbo.city on(pis_address.city_id = city.id)  inner join Final_Testing.dbo.province   on(city.province_id = province.id)  inner join Final_Testing.dbo.organization   on(pis.organization_id = organization.id)  where pis.permission > 0  ";
            object[] parms = { "search", searchkey };
            sql += " and (pis.LAST_NAME like '%'+@search+'%' OR pis.FIRST_NAME like '%'+@search+'%' OR pis.CODE like '%'+@search+'%') ";
            return db.Read(sql, selectGetBorrowers, 0, parms);
          

        }

        static Func<IDataReader, BusinessObjects.newLoanBorrowerProfile> selectGetBorrowers = reader =>
          new BusinessObjects.newLoanBorrowerProfile
          {
              newLoanLink = "<div class='btn btn-warning btn-sm'><a style='color:white;' href='"+ reader["newLoanLink"].AsString()+"'>New Loan</a></div>",
              id = reader["id"].AsString(),
              Status = reader["Status"].AsString(),
              Code = reader["Code"].AsString(),
              LastName = reader["LastName"].AsString(),
              FirstName = reader["FirstName"].AsString(),
              MiddleName = reader["MiddleName"].AsString(),
              DateofBirth = reader["DateofBirth"].AsString(),
              Gender = reader["Gender"].AsString(),
              CivilStatus = reader["CivilStatus"].AsString(),
              Address = reader["Address"].AsString(),
              City = reader["City"].AsString(),
          };

        public string insertCoMaker(string id, string  loan_application_id, string  first_name, string middle_name, string last_name, string date_of_birth, string phone_number, string address, string notes)
        {
            string sql = "INSERT INTO loan_application_pis (id,loan_application_id,first_name,middle_name,last_name,date_of_birth,phone_number,address,notes)  VALUES (@id,@loan_application_id,@first_name,@middle_name,@last_name,@date_of_birth,@phone_number,@address,@notes )";
            return db.Insert(sql, 2, null).ToString();

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


        public IEnumerable<BusinessObjects.DocumentStatus> getDocumentStatus()
        {
            string sql = "SELECT '-1' as [CODE], 'All' as [DESCRIPTION] UNION ALL SELECT" +
                         "[CODE] " +
                         ",[DESCRIPTION] " +
                         "FROM[FINAL_TESTING].[dbo].[document_status_map]";
            object[] parms = {};
            return db.Read(sql, selectDocumentStatus, 0, parms);
        }

        static Func<IDataReader, BusinessObjects.DocumentStatus> selectDocumentStatus = reader =>
           new BusinessObjects.DocumentStatus
           {
                CODE = reader["CODE"].AsString(),
               DESCRIPTION = reader["DESCRIPTION"].AsString(),
            };


        public IEnumerable<BusinessObjects.BorrowerProfile> getBorrowerProfile(string borrowerCode)
        {
            string sql = "select pis.CODE,(pis.LAST_NAME+', '+pis.FIRST_NAME) as Fullname, pa.PHONE_NUMBER as LandLine, pa.MOBILE_NUMBER as MobileNumber, " +
                         "(city.DESCRIPTION+', '+province.DESCRIPTION) as ProvinceCity, " +
                         " pa.BARANGAY_NAME as Barangay, pa.STREET_ADDRESS, pa.POSTAL_CODE " +
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

        public IEnumerable<BusinessObjects.LoanApplicationModel> getLoanFormDetails(string AccountNo)
        {
            string sql = "select loan_application.ID as reference_id, "+
            "loan_application.CODE as AccountNo, "+
            "CONVERT(varchar, loan_application.DATETIME_CREATED, 101) as TransactionDate, " +
            "pis.CODE as BorroweCode, " +
            "loan_application.PURPOSE_OF_LOAN as LoanPurpose , " +
            "district.CODE as DistrictCode, " +
            "organization.CODE as BranchCode, " +
            "APPLICATION_TYPE_ID as ApplicationType, " +
            "loan_application.REQUESTED_BY_ID as AgentId, " +
            "loan_application.LOAN_TYPE_ID as ProductId, " +
            "loan_application.LOAN_SET_ID as SetId, " +
            "loan_application.LOAN_TERMS_ID as TermsId, " +
            "loan_application.FACTOR_SETUP_ID as FactorRate, " +
            "loan_application.ORIGINAL_MLV as DesiredMLV, " +
            "loan_application.NOTES as Notes " +
            "from loan_application inner join " +
            "pis on pis.id = loan_application.PIS_ID " +
            "inner " +
            "join application_type on application_type.ID = pis.APPLICATION_TYPE_ID " +
            "inner " +
            "join organization on organization.ID = loan_application.ORGANIZATION_ID " +
            "inner " +
            "join district on organization.DISTRICT_ID = district.ID " +
            "where loan_application.CODE = @loanAccount";
            object[] parms = { "loanAccount", AccountNo };
            return db.Read(sql, selectLoanFormDetails, 0, parms);
        }

        static Func<IDataReader, BusinessObjects.LoanApplicationModel> selectLoanFormDetails = reader =>
           new BusinessObjects.LoanApplicationModel
           {


               reference_id = reader["reference_id"].AsString(),

               AccountNo = reader["AccountNo"].AsString(),

                TransactionDate = DateTime.Parse(reader["TransactionDate"].AsString()),

                 BorrowerCode = reader["BorroweCode"].AsString(),

                 LoanPurpose = reader["LoanPurpose"].AsString(),

                 DistrictCode = reader["DistrictCode"].AsString(),

                 BranchCode = reader["BranchCode"].AsString(),

                 ApplicationType = reader["ApplicationType"].AsString(),

                 AgentId = reader["AgentId"].AsString(),

                 ProductId = reader["ProductId"].AsString(),

                 SetId = reader["SetId"].AsString(),

                 TermsId = reader["TermsId"].AsString(),

                 FactorRate = reader["FactorRate"].AsString(),

                 DesiredMLV = reader["DesiredMLV"].AsString()
           };

        public IEnumerable<BusinessObjects.RequiredDocuments> getBorrowerRequiredDocuments(string borrowerCode)
        {
            string sql = "SELECT "+
            "required_document_line.id, "+
            "required_document_line.required_document_id, "+
            "required_document_line.support_document_id, "+
            "support_document.description sd_description "+
            "FROM "+
            "required_document_line "+
            "inner join support_document "+
            "ON (required_document_line.support_document_id = support_document.id) "+
            "inner join "+
            "required_document rd on rd.ID = required_document_line.REQUIRED_DOCUMENT_ID "+
            "inner join pis on rd.BORROWER_TYPE_ID = pis.BORROWER_TYPE_ID "+
            "where pis.CODE = @BorrowerCode";
            object[] parms = { "BorrowerCode", borrowerCode };
            return db.Read(sql, selectBorrowerRequiredDocuments, 0, parms);
        }

        static Func<IDataReader, BusinessObjects.RequiredDocuments> selectBorrowerRequiredDocuments = reader =>
           new BusinessObjects.RequiredDocuments
           {
               id = reader["id"].AsString(),
               required_document_id = reader["required_document_id"].AsString(),
               support_document_id = reader["support_document_id"].AsString(),
               description = reader["sd_description"].AsString()
           };




        public IEnumerable<BusinessObjects.LoanList> getLoanApplicationListing(string status, string searchkey)
        {
            if(status=="-1")
            {
                status = "[All]";
            }
            string sql = "select top 1000 "+
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
            else if(status == "[Approval]" && searchkey != "[All]")
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
            }else if (status != "[All]" && searchkey != "[All]")
            {
                object[] parms = { "stat", status , "search", searchkey };
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
                LA_No = "<a href='../Application/LoanApplication?code="+reader["LA_No"].AsString()+"'>"+ reader["LA_No"].AsString() + "</a>",
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


    }
}
