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
    public class CustomerDAO: ICustomerDAO
    {

        static DB db = new DB();

        public List<Dictionary<string, object>> getCustomerRecord()
        {
            string sql = "" +
            "select pis.id,document_status_map.description as [Status], pis.code as [Code], pis.last_name as [Last Name], pis.first_name as [First Name], pis.middle_name as [Middle Name], Convert(varchar,pis.date_of_birth,101) as [Date of Birth], " +
            "gender.description as [Gender], civil_status.description as [Civil Status], pis_address.street_address as [Address], city.description as [City] " +
            "from Final_Testing.dbo.pis inner join Final_Testing.dbo.document_status_map   on (pis.document_status_code = document_status_map.code)  inner join Final_Testing.dbo.user_account prepared_by   on (pis.prepared_by_id = prepared_by.id)  inner join Final_Testing.dbo.gender   on (pis.gender_id = gender.id)  inner join Final_Testing.dbo.civil_status   on (pis.civil_status_id = civil_status.id)  left join Final_Testing.dbo.pis_address   on (pis_address.pis_id = pis.id and pis_address.address_type_id = '0')  inner join Final_Testing.dbo.city on(pis_address.city_id = city.id)  inner join Final_Testing.dbo.province   on(city.province_id = province.id)  inner join Final_Testing.dbo.organization   on(pis.organization_id = organization.id)  where pis.permission > 0 ";            
            object[] parms = { };
            return db.ReadDictionary(sql, 0, parms);
        }        

        public IEnumerable<CustomerCharacter> getCustomerCharacterByID(string ID)
        {
            string sql = "SELECT [ID],[PIS_ID],[FIRST_NAME],[MIDDLE_NAME],[LAST_NAME],[RELATIONSHIP],[STREET_ADDRESS]," +
            "[CITY_ID],[City],[CONTACT_NO] FROM[FINAL_TESTING].[dbo].[uvw_PISCharacter] where PIS_ID = @ID";
            object[] parms = { "ID", ID };
            return db.Read(sql, PISCharacter, 0, parms).ToList();
        }

        static Func<IDataReader, CustomerCharacter> PISCharacter = reader =>
           new CustomerCharacter
           {
               ID = reader["ID"].AsString(),
               PISID = reader["PIS_ID"].AsString(),
               FirstName = reader["First_Name"].AsString(),
               MiddleName = reader["Middle_Name"].AsString(),
               LastName = reader["Last_Name"].AsString(),
               RelationShip = reader["RelationShip"].AsString(),
               StreetAddress = reader["Street_Address"].AsString(),
               CityID = reader["City_ID"].AsString(),
               City = reader["City"].AsString(),
               ContactNo = reader["Contact_No"].AsString()
           };

        public IEnumerable<CustomerEducation> getCustomerEducationByID(string ID)
        {
            string sql = "SELECT [ID],[PIS_ID],[EDUCATION_TYPE_ID],[EducationType],[SCHOOL_NAME]," +
                "Convert(varchar,[GRADUATION_DATE],101) as [GRADUATION_DATE] FROM [FINAL_TESTING].[dbo].[uvw_PISEducation] where PIS_ID = @ID";
            object[] parms = { "ID", ID };
            return db.Read(sql, PISEducation, 0, parms).ToList();
        }

        static Func<IDataReader, CustomerEducation> PISEducation = reader =>
           new CustomerEducation
           {
               ID = reader["ID"].AsString(),
               PISID = reader["PIS_ID"].AsString(),
               EducationTypeID = reader["Education_Type_ID"].AsString(),
               EducationType = reader["EducationType"].AsString(),
               SchoolName = reader["School_Name"].AsString(),
               GraduationDate = reader["Graduation_Date"].AsString()
           };


        public IEnumerable<CustomerDependents> getCustomerDependentsByID(string ID)
        {
            string sql = "SELECT [ID],[PIS_ID],[FIRST_NAME],[MIDDLE_NAME],[LAST_NAME],[GENDER_ID],[Gender],[STREET_ADDRESS],[CITY_ID],[City],[Province],[RELATIONSHIP_TYPE_ID],[RelationshipType], " +
        "Convert(varchar,[BIRTH_DATE], 101) as [BIRTH_DATE],[SCHOOL_ADDRESS],[CONTACT_NO] FROM[FINAL_TESTING].[dbo].[uvw_PISDependent] where PIS_ID = @ID";
            object[] parms = { "ID", ID };
            return db.Read(sql, PISDependents, 0, parms).ToList();
        }

        static Func<IDataReader, CustomerDependents> PISDependents = reader =>
           new CustomerDependents
           {
               ID = reader["ID"].AsString(),
               PISID = reader["PIS_ID"].AsString(),
               FirstName = reader["First_Name"].AsString(),
               MiddleName = reader["Middle_Name"].AsString(),
               LastName = reader["Last_Name"].AsString(),
               GenderID = reader["Gender_ID"].AsString(),
               Gender = reader["Gender"].AsString(),
               StreetAddress = reader["Street_Address"].AsString(),
               CityID = reader["City_ID"].AsString(),
               City = reader["City"].AsString(),
               Province = reader["Province"].AsString(),
               RelationshipTypeID = reader["Relationship_Type_ID"].AsString(),
               RelationshipType = reader["RelationshipType"].AsString(),
               BirthDate = reader["Birth_Date"].AsString(),
               SchoolAddress = reader["School_Address"].AsString(),
               ContactNo = reader["Contact_No"].AsString()
           };

        public IEnumerable<CustomerAddress> getCustomerAddressByID(string ID)
        {
            string sql = "SELECT [ID],[PIS_ID],[ADDRESS_TYPE_ID],[AddressType],[STREET_ADDRESS],[BARANGAY_NAME],[CITY_ID],CITY,PROVINCE,COUNTRY,[POSTAL_CODE],[PHONE_NUMBER] " +
            ",[MOBILE_NUMBER], Convert(varchar,[RESIDENT_DATE], 101) as [RESIDENT_DATE],[HOME_OWNERSHIP_ID],[HomeOwnership] " +
            "FROM[FINAL_TESTING].[dbo].[uvw_PISAddress] where PIS_ID = @ID";
            object[] parms = { "ID",ID};
            return db.Read(sql, PISAddress, 0, parms).ToList();
        }

        static Func<IDataReader, CustomerAddress> PISAddress = reader =>
           new CustomerAddress
           {
               ID = reader["ID"].AsString(),
               PISID = reader["PIS_ID"].AsString(),
               AddressTypeID = reader["Address_Type_ID"].AsString(),
               AddressType = reader["AddressType"].AsString(),
               StreetAddress = reader["Street_Address"].AsString(),
               BarangayName = reader["Barangay_Name"].AsString(),
               CityID = reader["City_ID"].AsString(),
               City = reader["City"].AsString(),
               Province = reader["Province"].AsString(),
               Country = reader["Country"].AsString(),
               PostalCode = reader["Postal_Code"].AsString(),
               PhoneNumber = reader["Phone_Number"].AsString(),
               MobileNumber = reader["Mobile_Number"].AsString(),
               ResidentDate = reader["Resident_Date"].AsString(),
               HomeOwnershipID = reader["Home_Ownership_ID"].AsString(),
               HomeOwnerShip = reader["HomeOwnership"].AsString()
           };

        public IEnumerable<CustomerEmployment> getCustomerEmploymentRecordByID (string ID)
        {
            string sql = "SELECT [ID],[PIS_ID],[BUSINESS_TYPE_ID],[BusinessType],[EMPLOYER_NAME],[INCOME],[CONTACT_NO]" +
                ",[FromDate],[ToDate],[IS_ACTIVE],[IS_SPOUSE],[NATURE_OF_BUSINESS_ID],[NATUREOFBUSINESS]" +
                "FROM[FINAL_TESTING].[dbo].[uvw_PISEmployment] where [PIS_ID] = @ID";
            object[] parms = { "ID", ID };            
            return db.Read(sql, PISEmployment, 0, parms).ToList();
        }

        static Func<IDataReader, CustomerEmployment> PISEmployment = reader =>
           new CustomerEmployment
           {
               ID = reader["ID"].AsString(),
               PISID = reader["PIS_ID"].AsString(),
               BusinessTypeID = reader["Business_Type_ID"].AsString(),
               BusinessType = reader["BusinessType"].AsString(),
               EmployerName = reader["Employer_Name"].AsString(),
               Income = reader["Income"].AsString(),
               Contact_No = reader["Contact_No"].AsString(),
               FromDate = reader["FromDate"].AsString(),
               ToDate = reader["ToDate"].AsString(),
               IsActive = reader["Is_Active"].AsString(),
               IsSpouse = reader["Is_Spouse"].AsString(),
               NatureOfBusinessID = reader["Nature_Of_Business_ID"].AsString(),
               NatureOfBusiness = reader["NatureOfBusiness"].AsString()
           };

        public IEnumerable<CustomerRecord> getCustomerRecordByCode(string Code)
        {
            string sql = "Select [ID],[CODE],Convert(varchar,[DATETIME_CREATED],101) + '|' + Convert(varchar,[DATETIME_CREATED],108) as [DATETIME_CREATED],[ORGANIZATION_ID],[Organization] "+
              ",[District],[FIRST_NAME],[LAST_NAME],[MIDDLE_NAME],[GENDER_ID],[Gender],[CIVIL_STATUS_ID],[CivilStatus], isnull(Convert(varchar,[DateOfMarriage], 101),'Not Applicable') as [DateOfMarriage] "+
	          ",[CITIZENSHIP_ID],[Citizenship],Convert(varchar, [DateOfBirth],101) as [DateOfBirth],[GSIS_NUMBER],[SSS_NUMBER],[TIN_NUMBER],[RCN] "+
              ",[RCN_PLACE_ISSUED],Convert(varchar, [RCN_DATE_ISSUED],101) as [RCN_DATE_ISSUED],[BORROWER_TYPE_ID],[BorrowerType],[BorrowerGroup],[LEAD_SOURCE_ID],[LeadSource],[AGENT_PROFILE_ID] "+
              ",[AgentCode],[AgentType],[AGENTLastName],[AGENTFirstName],[AGENTMiddleName],[DocumentationStatus],[APPLICATION_TYPE_ID],[ApplicationType],Isnull([SPOUSE_FIRST_NAME],'Not Applicable') as [SPOUSE_FIRST_NAME],Isnull([SPOUSE_MIDDLE_NAME],'Not Applicable') as [SPOUSE_MIDDLE_NAME],isnull([SPOUSE_LAST_NAME],'Not Applicable') as [SPOUSE_LAST_NAME],isnull(Convert(varchar, [SPOUSE_DATE_OF_BIRTH],101),'Not Applicable') as [SPOUSE_DATE_OF_BIRTH] " +
              ",isnull([SPOUSE_CONTACT_NUMBER],'Not Applicable') as [SPOUSE_CONTACT_NUMBER],[OWNER_CODE],[OWNER_ID],[PREPARED_BY_ID],Convert(varchar, [PREPARED_BY_DATETIME],101) + '|' + Convert(varchar, [PREPARED_BY_DATETIME],108) as [PREPARED_BY_DATETIME],[DOCUMENT_STATUS_CODE],[PERMISSION],[NOTES] " +
              " from FINAL_TESTING.dbo.uvw_PISData where Code = @Code";
            object[] parms = { "Code", Code };
            return db.Read(sql, PISRecord, 0, parms).ToList();
        }
        static Func<IDataReader, CustomerRecord> PISRecord = reader =>
           new CustomerRecord
           {
               ID = reader["ID"].AsString(),
               Code = reader["Code"].AsString(),
               DatetimeCreated = reader["DateTime_Created"].AsString(),
               OrganizationID = reader["Organization_ID"].AsString(),
               Organization = reader["Organization"].AsString(),
               District = reader["District"].AsString(),
               FirstName = reader["First_Name"].AsString(),
               LastName = reader["Last_Name"].AsString(),
               MiddleName = reader["Middle_Name"].AsString(),
               GenderID = reader["Gender_ID"].AsString(),
               Gender = reader["Gender"].AsString(),
               CivilStatusID = reader["Civil_Status_ID"].AsString(),
               CivilStatus = reader["CivilStatus"].AsString(),
               DateOfMarriage = reader["DateOfMarriage"].AsString(),
               CitizenshipID = reader["Citizenship_ID"].AsString(),
               Citizenship = reader["Citizenship"].AsString(),
               DateOfBirth = reader["DateofBirth"].AsString(),
               GSISNumber = reader["GSIS_Number"].AsString(),
               SSSNumber = reader["SSS_Number"].AsString(),
               TinNumber = reader["TIN_Number"].AsString(),
               RCN = reader["RCN"].AsString(),
               RCNPlaceIssued = reader["RCN_Place_Issued"].AsString(),
               RCNDateIssued = reader["RCN_Date_Issued"].AsString(),
               BorrowerTypeID = reader["Borrower_Type_ID"].AsString(),
               BorrowerType = reader["BorrowerType"].AsString(),
               BorrowerGroup = reader["BorrowerGroup"].AsString(),
               LeadSourceID = reader["Lead_Source_ID"].AsString(),
               LeadSource = reader["LeadSource"].AsString(),
               AgentProfileID = reader["Agent_Profile_ID"].AsString(),
               AgentCode = reader["AgentCode"].AsString(),
               AgentType = reader["AgentType"].AsString(),
               AgentLastName = reader["AgentLastName"].AsString(),
               AgentFirstName = reader["AgentFirstName"].AsString(),
               AgentMiddleName = reader["AgentMiddleName"].AsString(),    
               DocumentStatus = reader["DocumentationStatus"].AsString(),
               ApplicationTypeID = reader["Application_Type_ID"].AsString(),
               ApplicationType = reader["ApplicationType"].AsString(),
               SpouseFirstName = reader["Spouse_First_Name"].AsString(),
               SpouseMiddleName = reader["Spouse_Middle_Name"].AsString(),
               SpouseLastName = reader["Spouse_Last_Name"].AsString(),
               SpouseDateofBirth = reader["Spouse_Date_of_Birth"].AsString(),
               SpouseContactNumber = reader["Spouse_Contact_Number"].AsString(),
               OwnerCode = reader["Owner_Code"].AsString(),
               OwnerID = reader["Owner_ID"].AsString(),
               PreparedByID = reader["Prepared_By_ID"].AsString(),
               PreparedByDatetime = reader["Prepared_By_Datetime"].AsString(),
               DocumentStatusCode = reader["Document_Status_Code"].AsString(),
               Permission = reader["Permission"].AsString(),
               Notes = reader["Notes"].AsString()
           };

        public getComponents getAllComponents()
        {
            getComponents retComponent = new getComponents();
            retComponent.Gender = getGender();
            retComponent.Citizenship = getCitizenship();
            retComponent.District = getDistrict();
            retComponent.Organization = getOrganization();
            retComponent.ApplicationType = getApplicationType();
            retComponent.BorrowerType = getBorrowerType();
            retComponent.LeadSource = getLeadSource();
            retComponent.CivilStatus = getCivilStatus();
            retComponent.City = getCity();
            retComponent.Province = getProvince();
            retComponent.HomeOwnership = getHomeOwnership();
            retComponent.BusinessType = getBusinessType();
            retComponent.NatureofBusiness = getNatureofBusiness();
            retComponent.AddressType = getAddressType();
            retComponent.RelationshipType = getRelationshipType();
            retComponent.EducationType = getEducationType();

            return retComponent;
        }
        #region Gender
        public IEnumerable<Gender> getGender()
        {
            string sql = "Select ID,Code,Description from Gender";
            object[] parms = { };
            return db.Read(sql, selectGender, 0, parms);
        }

        static Func<IDataReader, Gender> selectGender = reader =>
           new Gender
           {
               ID = reader["ID"].AsString(),
               Code = reader["Code"].AsString(),
               Description = reader["Description"].AsString()
           };
        #endregion        
        #region Citizenship
        public IEnumerable<Citizenship> getCitizenship()
        {
            string sql = "Select ID,Code,Description from citizenship";
            object[] parms = { };
            return db.Read(sql, selectCitizenship, 0, parms);
        }

        static Func<IDataReader, Citizenship> selectCitizenship = reader =>
           new Citizenship
           {
               ID = reader["ID"].AsString(),
               Code = reader["Code"].AsString(),
               Description = reader["Description"].AsString()
           };
        #endregion
        #region District
        public IEnumerable<District> getDistrict()
        {
            string sql = "Select ID,CODE,DESCRIPTION,DISTRICT_GROUP_ID,REGIONAL_OFFICE_ID from district";
            object[] parms = { };
            return db.Read(sql, selectDistrict, 0, parms);
        }

        static Func<IDataReader, District> selectDistrict = reader =>
           new District
           {
               ID = reader["ID"].AsString(),
               Code = reader["Code"].AsString(),
               Description = reader["Description"].AsString(),
               DistrictGroupID = reader["DISTRICT_GROUP_ID"].AsString(),
               RegionalOfficeID = reader["REGIONAL_OFFICE_ID"].AsString()
           };
        #endregion
        #region Organization
        public IEnumerable<Organization> getOrganization()
        {
            string sql = "Select ID,CODE,DESCRIPTION,DISTRICT_ID,MOTHER_BRANCH_ID from organization";
            object[] parms = { };
            return db.Read(sql, selectOrganization, 0, parms);
        }

        static Func<IDataReader, Organization> selectOrganization = reader =>
           new Organization
           {
               ID = reader["ID"].AsString(),
               Code = reader["Code"].AsString(),
               Description = reader["Description"].AsString(),
               MotherBranchID = reader["MOTHER_BRANCH_ID"].AsString()
           };
        #endregion
        #region ApplicationType
        public IEnumerable<ApplicationType> getApplicationType()
        {
            string sql = "Select ID,Code,Description from application_type";
            object[] parms = { };
            return db.Read(sql, selectApplicationType, 0, parms);
        }

        static Func<IDataReader, ApplicationType> selectApplicationType = reader =>
           new ApplicationType
           {
               ID = reader["ID"].AsString(),
               Code = reader["Code"].AsString(),
               Description = reader["Description"].AsString()
           };
        #endregion
        #region BorrowerType
        public IEnumerable<BorrowerType> getBorrowerType()
        {
            string sql = "Select ID,CODE,DESCRIPTION,BORROWER_GROUP_ID from borrower_type";
            object[] parms = { };
            return db.Read(sql, selectBorrowerType, 0, parms);
        }

        static Func<IDataReader, BorrowerType> selectBorrowerType = reader =>
           new BorrowerType
           {
               ID = reader["ID"].AsString(),
               Code = reader["Code"].AsString(),
               Description = reader["Description"].AsString(),
               BorrowerGroupID = reader["BORROWER_GROUP_ID"].AsString()
           };
        #endregion
        #region LeadSource
        public IEnumerable<LeadSource> getLeadSource()
        {
            string sql = "Select ID,Code,Description from lead_source";
            object[] parms = { };
            return db.Read(sql, selectLeadSource, 0, parms);
        }

        static Func<IDataReader, LeadSource> selectLeadSource = reader =>
           new LeadSource
           {
               ID = reader["ID"].AsString(),
               Code = reader["Code"].AsString(),
               Description = reader["Description"].AsString()
           };
        #endregion
        #region CivilStatus
        public IEnumerable<CivilStatus> getCivilStatus()
        {
            string sql = "Select ID,Code,Description from civil_status";
            object[] parms = { };
            return db.Read(sql, selectCivilStatus, 0, parms);
        }

        static Func<IDataReader, CivilStatus> selectCivilStatus = reader =>
           new CivilStatus
           {
               ID = reader["ID"].AsString(),
               Code = reader["Code"].AsString(),
               Description = reader["Description"].AsString()
           };
        #endregion
        #region City
        public IEnumerable<City> getCity()
        {
            string sql = "Select ID,CODE,DESCRIPTION,PROVINCE_ID from city";
            object[] parms = { };
            return db.Read(sql, selectCity, 0, parms);
        }

        static Func<IDataReader, City> selectCity = reader =>
           new City
           {
               ID = reader["ID"].AsString(),
               Code = reader["Code"].AsString(),
               Description = reader["Description"].AsString(),
               ProvinceID = reader["PROVINCE_ID"].AsString()
           };
        #endregion
        #region Province
        public IEnumerable<Province> getProvince()
        {
            string sql = "Select ID,CODE,DESCRIPTION,COUNTRY_ID from province";
            object[] parms = { };
            return db.Read(sql, selectProvince, 0, parms);
        }

        static Func<IDataReader, Province> selectProvince = reader =>
           new Province
           {
               ID = reader["ID"].AsString(),
               Code = reader["Code"].AsString(),
               Description = reader["Description"].AsString(),
               CountryID = reader["COUNTRY_ID"].AsString()
           };
        #endregion
        #region HomeOwnership
        public IEnumerable<HomeOwnership> getHomeOwnership()
        {
            string sql = "Select ID,Code,Description from home_ownership";
            object[] parms = { };
            return db.Read(sql, selectHomeOwnership, 0, parms);
        }

        static Func<IDataReader, HomeOwnership> selectHomeOwnership = reader =>
           new HomeOwnership
           {
               ID = reader["ID"].AsString(),
               Code = reader["Code"].AsString(),
               Description = reader["Description"].AsString()
           };
        #endregion
        #region BusinessType
        public IEnumerable<BusinessType> getBusinessType()
        {
            string sql = "Select ID,Code,Description from business_type";
            object[] parms = { };
            return db.Read(sql, selectBusinessType, 0, parms);
        }

        static Func<IDataReader, BusinessType> selectBusinessType = reader =>
           new BusinessType
           {
               ID = reader["ID"].AsString(),
               Code = reader["Code"].AsString(),
               Description = reader["Description"].AsString()
           };
        #endregion
        #region NatureofBusiness
        public IEnumerable<NatureofBusiness> getNatureofBusiness()
        {
            string sql = "Select ID,Code,Description from nature_of_business";
            object[] parms = { };
            return db.Read(sql, selectNatureofBusiness, 0, parms);
        }

        static Func<IDataReader, NatureofBusiness> selectNatureofBusiness = reader =>
           new NatureofBusiness
           {
               ID = reader["ID"].AsString(),
               Code = reader["Code"].AsString(),
               Description = reader["Description"].AsString()
           };
        #endregion
        #region AddressType
        public IEnumerable<AddressType> getAddressType()
        {
            string sql = "Select ID,Code,Description from address_type";
            object[] parms = { };
            return db.Read(sql, selectAddressType, 0, parms);
        }

        static Func<IDataReader, AddressType> selectAddressType = reader =>
           new AddressType
           {
               ID = reader["ID"].AsString(),
               Code = reader["Code"].AsString(),
               Description = reader["Description"].AsString()
           };
        #endregion
        #region RelationshipType
        public IEnumerable<RelationshipType> getRelationshipType()
        {
            string sql = "Select ID,Code,Description from relationship_type";
            object[] parms = { };
            return db.Read(sql, selectRelationshipType, 0, parms);
        }

        static Func<IDataReader, RelationshipType> selectRelationshipType = reader =>
           new RelationshipType
           {
               ID = reader["ID"].AsString(),
               Code = reader["Code"].AsString(),
               Description = reader["Description"].AsString()
           };
        #endregion
        #region EducationType
        public IEnumerable<EducationType> getEducationType()
        {
            string sql = "Select ID,Code,Description from education_type";
            object[] parms = { };
            return db.Read(sql, selectEducationType, 0, parms);
        }

        static Func<IDataReader, EducationType> selectEducationType = reader =>
           new EducationType
           {
               ID = reader["ID"].AsString(),
               Code = reader["Code"].AsString(),
               Description = reader["Description"].AsString()
           };
        #endregion

    }
}
