using BusinessObjects;
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



        public IEnumerable<Agent> getAgent()
        {
            string sql = "Select ID,Code,LAST_NAME + ', ' + FIRST_NAME + ' ' + MIDDLE_NAME as Description from agent_profile  where  agent_type_id = '2' order by last_name,first_name";
            object[] parms = { };
            return db.Read(sql, selectAgent, 0, parms);
        }
        static Func<IDataReader, Agent> selectAgent = reader =>
           new Agent
           {
               AgentProfileID = reader["ID"].AsString(),
               Code = reader["Code"].AsString(),
               Description = reader["Description"].AsString()
           };

        public IEnumerable<BusinessObjects.newLoanBorrowerProfile> GetBorrowers(string searchkey)
        {
            string sql = "select 'NewLoanApplication?borrower='+pis.CODE as newLoanLink, pis.id,document_status_map.description as [Status], pis.code as [Code], pis.last_name as [LastName], pis.first_name as [FirstName], pis.middle_name as [MiddleName], Convert(varchar,pis.date_of_birth,101) as [DateofBirth],  " +
            "gender.description as [Gender], civil_status.description as [CivilStatus], pis_address.street_address as [Address], city.description as [City] " +
            "from pis inner join document_status_map   on (pis.document_status_code = document_status_map.code)  inner join user_account prepared_by   on (pis.prepared_by_id = prepared_by.id)  inner join gender   on (pis.gender_id = gender.id)  inner join civil_status   on (pis.civil_status_id = civil_status.id)  left join pis_address   on (pis_address.pis_id = pis.id and pis_address.address_type_id = '0')  inner join city on(pis_address.city_id = city.id)  inner join province   on(city.province_id = province.id)  inner join organization   on(pis.organization_id = organization.id)  where pis.permission > 0  ";
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
                       "FROM [dbo].[loan_type]"; object[] parms = { };
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

        public IEnumerable<BusinessObjects.LoanSet> getLoanSet(string groupid, string loantype)
        {
            string sql = "select distinct loan_set.id loan_set_id, loan_set.CODE, loan_set.description, loan_Set.MIGID from Final_Testing.dbo.factor_setup inner join Final_Testing.dbo.district_group  on (factor_setup.district_group_id = district_group.id) inner join Final_Testing.dbo.loan_type  on (factor_setup.loan_type_id = loan_type.id) inner join Final_Testing.dbo.loan_set  on (factor_setup.loan_set_id = loan_set.id)  inner join district on district_group.ID=district.DISTRICT_GROUP_ID where factor_setup.document_status_code = 7 and "
                           + "district.id = @groupid" 
                            +" and loan_type.id = @loantype order by loan_set.description ";
            object[] parms = { "groupid",groupid,"loantype", loantype  };
            return db.Read(sql, selectLoanSet, 0, parms);
        }

        static Func<IDataReader, BusinessObjects.LoanSet> selectLoanSet = reader =>
           new BusinessObjects.LoanSet
           {
               ID = reader["loan_set_id"].AsString(),
               CODE = reader["CODE"].AsString(),
               DESCRIPTION = reader["description"].AsString(),
               MIGID = reader["MIGID"].AsString()
           };

       

        public IEnumerable<BusinessObjects.FuelType> getFuelType()
        {
            string sql = "SELECT [ID] " +
                         ",[CODE] " +
                         ",[DESCRIPTION] " +
                         ",[MIGID] " +
                       "FROM [dbo].[fuel_type]"; object[] parms = { };
            return db.Read(sql, selectFuelType, 0, parms);
        }

        static Func<IDataReader, BusinessObjects.FuelType> selectFuelType = reader =>
           new BusinessObjects.FuelType
           {
               ID = reader["ID"].AsString(),
               CODE = reader["CODE"].AsString(),
               DESCRIPTION = reader["DESCRIPTION"].AsString(),
               MIGID = reader["MIGID"].AsString()
           };

        public IEnumerable<BusinessObjects.CollateralUsage> getCollateralUsage()
        {
            string sql = "SELECT [ID] " +
                         ",[CODE] " +
                         ",[DESCRIPTION] " +
                         ",[MIGID] " +
                       "FROM [dbo].[collateral_usage]"; object[] parms = { };
            return db.Read(sql, selectCollateralUsage, 0, parms);
        }

        static Func<IDataReader, BusinessObjects.CollateralUsage> selectCollateralUsage = reader =>
           new BusinessObjects.CollateralUsage
           {
               ID = reader["ID"].AsString(),
               CODE = reader["CODE"].AsString(),
               DESCRIPTION = reader["DESCRIPTION"].AsString(),
               MIGID = reader["MIGID"].AsString()
           };

        public IEnumerable<BusinessObjects.Color> getColor()
        {
            string sql = "SELECT [ID] " +
                         ",[CODE] " +
                         ",[DESCRIPTION] " +
                         ",[MIGID] " +
                       "FROM [dbo].[color]"; object[] parms = { };
            return db.Read(sql, selectColor, 0, parms);
        }

        static Func<IDataReader, BusinessObjects.Color> selectColor = reader =>
           new BusinessObjects.Color
           {
               ID = reader["ID"].AsString(),
               CODE = reader["CODE"].AsString(),
               DESCRIPTION = reader["DESCRIPTION"].AsString(),
               MIGID = reader["MIGID"].AsString()
           };



        public IEnumerable<BusinessObjects.CollateralType> getCollateralType()
        {
            string sql = "SELECT [ID] " +
                         ",[CODE] " +
                         ",[DESCRIPTION] " +
                         ",[COLLATERAL_GROUP_ID] "+
                         ",[MIGID] " +
                       "FROM [dbo].[collateral_type]"; object[] parms = { };
            return db.Read(sql, selectCollateralType, 0, parms);
        }

        static Func<IDataReader, BusinessObjects.CollateralType> selectCollateralType = reader =>
           new BusinessObjects.CollateralType
           {
               ID = reader["ID"].AsString(),
               CODE = reader["CODE"].AsString(),
               DESCRIPTION = reader["DESCRIPTION"].AsString(),
               COLLATERAL_GROUP_ID = reader["COLLATERAL_GROUP_ID"].AsString(),
               MIGID = reader["MIGID"].AsString()
           };

        public IEnumerable<BusinessObjects.LoanTermsForLoanApplication> getLoanTerms(string groupid, string loantype, string loanset)
        {
            string sql = "select distinct loan_terms.id loan_terms_id,loan_terms.CODE ,loan_terms.description, loan_terms.months, factor_setup_term.add_on_rate, factor_setup_term.factor_setup_id from Final_Testing.dbo.factor_setup inner join Final_Testing.dbo.district_group  on (factor_setup.district_group_id = district_group.id) inner join Final_Testing.dbo.loan_type  on (factor_setup.loan_type_id = loan_type.id) inner join Final_Testing.dbo.loan_set  on (factor_setup.loan_set_id = loan_set.id) inner join Final_Testing.dbo.factor_setup_term  on (factor_setup_term.factor_setup_id = factor_setup.id) inner join Final_Testing.dbo.loan_terms  on (factor_setup_term.loan_terms_id = loan_terms.id) inner join district on district.district_group_id = district_group.id where factor_setup.document_status_code = 7 and "
                         +"district.id = @groupid and loan_type.id = @loantype and loan_set.id = @loanset "
                         +"order by loan_terms.description ";
            object[] parms = { "groupid", groupid, "loantype", loantype, "loanset", loanset };
            return db.Read(sql, selectLoanTermsForLoanApplication, 0, parms);
        }

        static Func<IDataReader, BusinessObjects.LoanTermsForLoanApplication> selectLoanTermsForLoanApplication = reader =>
           new BusinessObjects.LoanTermsForLoanApplication
           {
               ID = reader["loan_terms_id"].AsString(),
               CODE = reader["CODE"].AsString(),
               DESCRIPTION = reader["description"].AsString(),
               MONTHS = reader["months"].AsString(),
               FACTOR_ADDON_RATE = reader["add_on_rate"].AsString(),
               FACTOR_ID = reader["factor_setup_id"].AsString()
           };


        public IEnumerable<BusinessObjects.DocumentStatus> getDocumentStatus()
        {
            string sql = "SELECT '-1' as [CODE], 'All' as [DESCRIPTION] UNION ALL SELECT" +
                         "[CODE] " +
                         ",[DESCRIPTION] " +
                         "FROM [dbo].[document_status_map]";
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
                LA_No = "<a href='../LoanApplication?code="+reader["LA_No"].AsString()+"'>"+ reader["LA_No"].AsString() + "</a>",
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

        public IEnumerable<BusinessObjects.ComakerProfile> getComakers(string loanCode)
        {
            string sql = " SELECT [loan_application_pis].[ID] "+
     " ,[LOAN_APPLICATION_ID] "+
     " ,[FIRST_NAME] " +
     " ,[MIDDLE_NAME] " +
     " ,[LAST_NAME] " +
     " ,[DATE_OF_BIRTH] " +
     " ,[PHONE_NUMBER] " +
     " ,[ADDRESS] " +
     " ,[loan_application_pis].NOTES " +
        "FROM[loan_application_pis] " +
         " inner join loan_application la on la.ID=loan_application_pis.LOAN_APPLICATION_ID " +
        "  where la.CODE = @loanCode";
            object[] parms = { "loanCode", loanCode };
            return db.Read(sql, selectComakers, 0, parms);
        }

        static Func<IDataReader, BusinessObjects.ComakerProfile> selectComakers = reader =>
           new BusinessObjects.ComakerProfile
           {
               ID = reader["ID"].AsString(),
               LOAN_APPLICATION_ID = reader["LOAN_APPLICATION_ID"].AsString(),
               FIRST_NAME = reader["FIRST_NAME"].AsString(),
               MIDDLE_NAME = reader["MIDDLE_NAME"].AsString(),
               LAST_NAME = reader["LAST_NAME"].AsString(),
               DATE_OF_BIRTH = reader["DATE_OF_BIRTH"].AsString(),
               PHONE_NUMBER = reader["PHONE_NUMBER"].AsString(),
               ADDRESS = reader["ADDRESS"].AsString(),
               NOTES = reader["NOTES"].AsString()
           };


        public string insertLoan(BusinessObjects.LoanApplicationModel loan, string userID)
        {
            string guid = Guid.NewGuid().ToString();
            string guid_reviewer = Guid.NewGuid().ToString();
            string sql = "exec usp_insertLoan @guid,@code,@datetime_created,@prepared_by_id,@prepared_by_datetime  , @requested_by_id  ,@requested_by_datetime  ,@organization_id  ,@document_status_code  ,@permission  ,@notes  ,@pis_id  ,@history_pis_id  ,@current_pis_id  ,@factor_setup_id  ,@authority_setup_id  ,@ppd_rate_id  ,@handling_fee_id  ,@pp_discount_id  ,@agent_incentive_id  ,@agent_incentive_type_id  ,@dealer_incentive_id  ,@dealer_incentive_type_id  ,@loan_type_id  ,@add_on_rate  ,@loan_amount  ,@recommended_mlv  ,@original_mlv  ,@approved_mlv  ,@loan_set_id  ,@loan_terms_id  ,@purpose_of_loan  ,@required_document_id  ,@character_notes  ,@capacity_notes  ,@collateral_notes  ,@capital_notes  ,@business_environment_notes  ,@restructure_count  ,@direct_loan_receipt_id  ,@pip_balance  ,@gibco_balance  ,@rfc_balance  ,@total_balance  ,@pip_due  ,@gibco_due  ,@rfc_due  ,@total_due  ,@restructure_fee  ,@restructure_income  ,@acceleration_form_id  ,@effective_yield_id  ,@tag  ,@tag_amount  ,@assured  ,@remedial_type_id  ,@reviewer_id  ";
            object[] parms = { "guid", guid , "reviewer_id", userID,"code", guid_reviewer, "datetime_created" ,  DateTime.Now.ToShortDateString(),
        "prepared_by_id" , userID ,
        "prepared_by_datetime" , DateTime.Now.ToShortDateString() ,
        "requested_by_id" ,userID ,
        "requested_by_datetime" , DateTime.Now.ToShortDateString() ,
        "organization_id" , loan.BranchCode,
        "document_status_code" , "31" ,
        "permission" , "" ,
        "notes" , loan.Notes,
        "pis_id" , loan.BorrowerCode ,
        "history_pis_id" , loan.BorrowerCode,
        "current_pis_id" , loan.BorrowerCode ,
        "factor_setup_id" , loan.FactorRate,
        "authority_setup_id" , "0" ,
        "ppd_rate_id" ,"0" ,
        "handling_fee_id" , "0" ,
        "pp_discount_id" , "0" ,
        "agent_incentive_id" , "0" ,
        "agent_incentive_type_id" , "0" ,
        "dealer_incentive_id" , "0" ,
        "dealer_incentive_type_id" , "0" ,
        "loan_type_id" , loan.ApplicationType ,
        "add_on_rate" , "0" ,
        "loan_amount" , loan.DesiredMLV,
        "recommended_mlv" , "0" ,
        "original_mlv" , loan.DesiredMLV ,
        "approved_mlv" , "0" ,
        "loan_set_id" , loan.SetId ,
        "loan_terms_id" , loan.TermsId ,
        "purpose_of_loan" , loan.LoanPurpose ,
        "required_document_id" , "0" ,
        "character_notes" , "0",
        "capacity_notes" , "0",
        "collateral_notes" , "0",
        "capital_notes" ,"0" ,
        "business_environment_notes" ,"0" ,
        "restructure_count" , "0",
        "direct_loan_receipt_id" , "0",
        "pip_balance" , "0",
        "gibco_balance" , "0",
        "rfc_balance" , "0",
        "total_balance" , "0",
        "pip_due" , "0" ,
        "gibco_due" , "0",
        "rfc_due" , "0",
        "total_due" , "0",
        "restructure_fee" , "0",
        "restructure_income" ,"0" ,
        "acceleration_form_id", "0", 
        "effective_yield_id" ,"0" ,
        "tag" ,"0" ,
        "tag_amount" ,"0" ,
        "assured" ,"0" ,
        "remedial_type_id" ,"0" };
            db.RetValue(sql, 0, parms);

            insertLoanCollateral(guid, loan.ListOfCollaterals);
            insertLoanComaker(guid, loan.ListOfComakers);

            return "Success";

        }
        public string insertLoanCollateral(string loan_application_id, IEnumerable<BusinessObjects.CollateralProfile> collaterals)
        {
            foreach(BusinessObjects.CollateralProfile col in collaterals)
            {
                string guid = Guid.NewGuid().ToString();
                col.ID = guid;
                col.LOAN_APPLICATION_ID = loan_application_id;

                string sql = "usp_insertLoanCollateral";
                object[] parms = { "loan_application_id", loan_application_id ,"id" , guid, "collateral_type_id" , col.ColType  ,  "serial_number" , col.SERIAL_NUMBER ,  "description" , col.DESCRIPTION ,  "collateral_usage_id" , col.ColUsage ,  "year" , col.YEAR ,  "model" , col.MODEL ,  "color_id" , col.Color ,  "fuel_type_id" , col.FuelType ,  "chassis_number" , col.CHASSIS_NUMBER ,  "engine_number" ,  col.ENGINE_NUMBER,  "plate_number" , col.PLATE_NUMBER ,  "tct_number" , col.TCT_NUMBER ,  "odo_reading" , col.ODO_READING ,  "cr_number" , col.CR_NUMBER ,  "cr_name" ,  col.CR_NUMBER,  "or_number" , col.OR_NUMBER ,  "or_expiration_date" , col.OR_EXPIRATION_DATE ,  "cr_expiration_date" , "" ,  "insurance_name" , col.INSURANCE_NAME,  "insurance_expiration_date" , col.INDSURANCE_EXPIRATION_DATE ,  "mlv" ,  col.MLV ,  "appraised_value" , col.APPRAISED_VALUE,  "loan_value" , col.LOAN_VALUE  ,  "direct_loan_receipt_id","" };
                db.RetValue(sql, 4, parms);
            }
           
            return "Success";

        }
        public string insertLoanComaker(string loan_application_id, IEnumerable<BusinessObjects.ComakerProfile> comakers)
        {

            foreach (BusinessObjects.ComakerProfile com in comakers)
            {
                string guid = Guid.NewGuid().ToString();

                string sql = "usp_insertLoanComaker";
                object[] parms = { "loan_application_id", loan_application_id , "id",  guid , "first_name", com.FIRST_NAME , "middle_name", com.MIDDLE_NAME , "last_name", com.MIDDLE_NAME, "date_of_birth", com.DATE_OF_BIRTH, "phone_number", com.PHONE_NUMBER , "address", com.ADDRESS, "notes", com.NOTES };
                db.RetValue(sql, 4, parms);
            }
          
            return "Success";
        }
        public IEnumerable<BusinessObjects.CollateralProfile> getCollaterals(string loanCode)
        {
            string sql = " SELECT  lp.[ID] " +
          ",[LOAN_APPLICATION_ID] "+
          ", ct.DESCRIPTION as ColType " +
          ",cg.DESCRIPTION as ColGroup " +
          ",[SERIAL_NUMBER] " +
          ",lp.[DESCRIPTION] " +
          ",cu.DESCRIPTION as ColUsage " +
          ",[YEAR] " +
          ",[MODEL] " +
          ",col.DESCRIPTION as Color " +
          ",ft.DESCRIPTION as FuelType " +
          ",[CHASSIS_NUMBER] " +
          ",[ENGINE_NUMBER] " +
          ",[PLATE_NUMBER] " +
          ",[TCT_NUMBER] " +
          ",[ODO_READING] " +
          ",[CR_NUMBER] " +
          ",[CR_NAME] " +
          ",[OR_NUMBER] " +
           ", ISNULL(convert(varchar,[OR_EXPIRATION_DATE], 101), 'Not Available') as [OR_EXPIRATION_DATE] "+
        ",ISNULL(convert(varchar,[CR_EXPIRATION_DATE], 101), 'Not Available') as [CR_EXPIRATION_DATE] "+
        ",[INSURANCE_NAME] "+
        ",ISNULL(convert(varchar, [INSURANCE_EXPIRATION_DATE],101),'Not Available') as [INSURANCE_EXPIRATION_DATE] "+
        ",[MLV] "+
        ",[APPRAISED_VALUE] "+
        ",[LOAN_VALUE] "+
        ",ISNULL(lp.[DIRECT_LOAN_RECEIPT_ID],'Not Available') as [DIRECT_LOAN_RECEIPT_ID] "+
        ", CASE " +
        "WHEN cg.ID = 3 " +
        "    then 'TCT No. : ' + lp.[TCT_NUMBER] " +
        "WHEN cg.ID = 2 " +
        "    then 'Year: ' + lp.YEAR + '<br/>' " +
        "            + 'Model: ' + lp.MODEL + '<br/>' " +
        "            + 'Color: ' + col.DESCRIPTION + '<br/>' " +
        "            + 'Serial No.: ' + lp.SERIAL_NUMBER + '<br/>' " +
        "WHEN cg.ID = 1 " +
        "    then 'Year: ' + lp.[YEAR] + '<br/>' " +
        "            + 'Model: ' + lp.MODEL + '<br/>' " +
        "            + 'Color: ' + col.DESCRIPTION + '<br/>' " +
        "            + 'Fuel: ' + ft.DESCRIPTION + '<br/>' " +
        "            + 'Chassis No.: ' + lp.CHASSIS_NUMBER + '<br/>' " +
        "            + 'Engine No.: ' + lp.ENGINE_NUMBER + '<br/>' " +
        "            + 'ODO Reading: ' + convert(varchar, ODO_READING) + '<br/>' " +
        "            + 'CRE No.: ' + lp.CR_NUMBER + '<br/>' " +
        "            + 'CR Expiry Date: ' + convert(varchar, CR_EXPIRATION_DATE, 101) + '<br/>' " +
        "            + 'OR No.: ' + OR_NUMBER + '<br/>' " +
        "            + 'Insurance Name: ' + INSURANCE_NAME + '<br/>' " +
        "            + 'Insurance Expiry Date: ' + convert(varchar, INSURANCE_EXPIRATION_DATE, 101) + '<br/>' "+

        "END as ADDITIONAL_INFO "+
          "FROM[loan_application_collateral] lp " +
        " inner join loan_application la on la.ID = lp.LOAN_APPLICATION_ID " +
        " inner join collateral_type ct on ct.ID =lp.COLLATERAL_TYPE_ID " +
        " inner join collateral_usage cu on cu.ID = lp.COLLATERAL_USAGE_ID " +
        " inner join collateral_group cg on cg.ID = ct.COLLATERAL_GROUP_ID " +
        " inner join fuel_type ft on ft.ID = lp.FUEL_TYPE_ID " +
        " inner join color col on col.ID = lp.COLOR_ID " +
        " where la.CODE = @loanCode";
            object[] parms = { "loanCode", loanCode };
            return db.Read(sql, selectCollaterals, 0, parms);
        }

        static Func<IDataReader, BusinessObjects.CollateralProfile> selectCollaterals = reader =>
           new BusinessObjects.CollateralProfile
           {
               ID = reader["ID"].AsString(),
               LOAN_APPLICATION_ID = reader["LOAN_APPLICATION_ID"].AsString(),
               ColType = reader["ColType"].AsString(),
               ColGroup = reader["ColGroup"].AsString(),
               SERIAL_NUMBER = reader["SERIAL_NUMBER"].AsString(),
               DESCRIPTION = reader["DESCRIPTION"].AsString(),
               ColUsage = reader["ColUsage"].AsString(),
               YEAR = reader["YEAR"].AsString(),
               MODEL = reader["MODEL"].AsString(),
               Color = reader["Color"].AsString(),
               FuelType = reader["FuelType"].AsString(),
               CHASSIS_NUMBER = reader["CHASSIS_NUMBER"].AsString(),
               ENGINE_NUMBER = reader["ENGINE_NUMBER"].AsString(),
               PLATE_NUMBER = reader["PLATE_NUMBER"].AsString(),
               TCT_NUMBER = reader["TCT_NUMBER"].AsString(),
               ODO_READING = reader["ODO_READING"].AsString(),
               CR_NUMBER = reader["CR_NUMBER"].AsString(),
               OR_NUMBER = reader["OR_NUMBER"].AsString(),
               OR_EXPIRATION_DATE = reader["OR_EXPIRATION_DATE"].AsString(),
               INSURANCE_NAME = reader["INSURANCE_NAME"].AsString(),
               INDSURANCE_EXPIRATION_DATE = reader["INSURANCE_EXPIRATION_DATE"].AsString(),
               MLV = reader["MLV"].AsString(),
               APPRAISED_VALUE = reader["APPRAISED_VALUE"].AsString(),
               LOAN_VALUE = reader["LOAN_VALUE"].AsString(),
               DIRECT_LOAN_RECEIPT_ID = reader["DIRECT_LOAN_RECEIPT_ID"].AsString(),
               ADDITIONAL_INFO = reader["ADDITIONAL_INFO"].AsString(),
           };
    }

    
}
