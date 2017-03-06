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
    public class MaintenanceAgentProfileDAO : IMaintenanceAgentProfileDAO
    {
        static DB db = new DB();

        public string getAgentCodebyID(string ID)
        {
            string sql = "Select Code from uvw_AgentProfile where ID = @ID";
            object[] parms = { "ID", ID };
            return db.Scalar(sql, 0, parms).AsString();
        }

        public List<Dictionary<string,object>> getAgentProfileList()
        {
            string sql = "" +
            "Select ID,DocumentStatus as Status,Code as AgentCode,Last_Name as LastName,First_Name as FirstName,Middle_Name as MiddleName, " +
            "Convert(varchar, DATE_OF_BIRTH, 101) as DateofBirth,Gender,CivilStatus,Organization as Branch from uvw_AgentProfile Order by AgentCode desc";
            object[] parms = { };
            return db.ReadDictionary(sql, 0, parms);
        }

        public IEnumerable<AgentAddress> getAgentAddressByID(string ID)
        {
            string sql = "SELECT [ID],[AGENT_PROFILE_ID],[ADDRESS_TYPE_ID],[AddressType],[STREET_ADDRESS],[BARANGAY_NAME],[CITY_ID],CITY,PROVINCEID,PROVINCE,COUNTRY,[POSTAL_CODE],[PHONE_NUMBER] " +
            ",[MOBILE_NUMBER], Convert(varchar,[RESIDENT_DATE], 101) as [RESIDENT_DATE],[HOME_OWNERSHIP_ID],[HomeOwnership] " +
            "FROM [FINAL_TESTING].[dbo].[uvw_AgentProfileAddress] where AGENT_PROFILE_ID = @ID";
            object[] parms = { "ID", ID };
            return db.Read(sql, AgentAddress, 0, parms).ToList();
        }

        static Func<IDataReader, AgentAddress> AgentAddress = reader =>
           new AgentAddress
           {
               ID = reader["ID"].AsString(),
               AgentProfileID = reader["AGENT_PROFILE_ID"].AsString(),
               AddressTypeID = reader["Address_Type_ID"].AsString(),
               AddressType = reader["AddressType"].AsString(),
               StreetAddress = reader["Street_Address"].AsString(),
               BarangayName = reader["Barangay_Name"].AsString(),
               CityID = reader["City_ID"].AsString(),
               City = reader["City"].AsString(),
               ProvinceID = reader["ProvinceID"].AsString(),
               Province = reader["Province"].AsString(),
               Country = reader["Country"].AsString(),
               PostalCode = reader["Postal_Code"].AsString(),
               PhoneNumber = reader["Phone_Number"].AsString(),
               MobileNumber = reader["Mobile_Number"].AsString(),
               ResidentDate = reader["Resident_Date"].AsString(),
               HomeOwnershipID = reader["Home_Ownership_ID"].AsString(),
               HomeOwnerShip = reader["HomeOwnership"].AsString()
           };

        public IEnumerable<AgentProfile> getAgentProfilebyCode(string Code)
        {
            string sql = "" +
                "Select ID,CODE,ORGANIZATION_ID,Organization,DistrictID,District,LAST_NAME,FIRST_NAME,MIDDLE_NAME,GENDER_ID,Gender, " +
                "CIVIL_STATUS_ID,CivilStatus,Convert(varchar,DATE_OF_BIRTH,101) as DATE_OF_BIRTH,AGENT_TYPE_ID,AgentType,WITH_CASH_CARD,PREPARED_BY_ID, " +
                "PreparedBy,DOCUMENT_STATUS_CODE,DocumentStatus,PERMISSION,NOTES from uvw_AgentProfile where Code = '"+ Code + "'";
            object[] parms = { };
            return db.Read(sql, AgentProfile, 0, parms);
        }
        static Func<IDataReader, AgentProfile> AgentProfile = reader =>
           new AgentProfile
           {
               ID = reader["ID"].AsString(),
               AGENTCode = reader["CODE"].AsString(),
               OrganizationID = reader["ORGANIZATION_ID"].AsString(),
               Organization = reader["Organization"].AsString(),
               DistrictID = reader["DistrictID"].AsString(),
               District = reader["District"].AsString(),
               LastName = reader["LAST_NAME"].AsString(),
               FirstName = reader["FIRST_NAME"].AsString(),
               MiddleName = reader["MIDDLE_NAME"].AsString(),
               GenderID = reader["GENDER_ID"].AsString(),
               Gender = reader["Gender"].AsString(),
               CivilStatusID = reader["CIVIL_STATUS_ID"].AsString(),
               CivilStatus = reader["CivilStatus"].AsString(),
               DateofBirth = reader["DATE_OF_BIRTH"].AsString(),
               AgentTypeID = reader["AGENT_TYPE_ID"].AsString(),
               AgentType = reader["AgentType"].AsString(),
               WithCashCard = reader["WITH_CASH_CARD"].AsString(),
               PreparedByID = reader["PREPARED_BY_ID"].AsString(),
               PreparedBy = reader["PreparedBy"].AsString(),
               DocumentStatusCode = reader["DOCUMENT_STATUS_CODE"].AsString(),
               DocumentStatus = reader["DocumentStatus"].AsString(),
               Permission = reader["PERMISSION"].AsString(),
               Notes = reader["NOTES"].AsString()
           };

        public int UpdateAgentData(string ProcessType, AgentModel AgentModel, string AgentProfileID)
        {
            int ret = 0;
            ret = updateAgentProfile(ProcessType, AgentModel.AgentProfile);
            if (ret != 1)
                return 0;
            ret = updateAgentAddress(AgentModel.AgentAddress, AgentProfileID);
            if (ret != 1)
                return 0;            
            if (ret != 1)
                return 0;
            return 1;
        }

        public int updateAgentProfile(string ProcessType, AgentProfile AgentProfile)
        {
            int ret = 1;
            if (ProcessType == "Update")
            {
                string sql = "[usp_UpdateAgentProfile]";
                object[] parms = {
                    "ID" , AgentProfile.ID.AsString(),
                    "Code" , AgentProfile.AGENTCode.AsString(),
                    "OrganizationID" , AgentProfile.OrganizationID.AsString(),
                    "LastName" , AgentProfile.LastName.AsString(),
                    "FirstName" , AgentProfile.FirstName.AsString(),
                    "MiddleName" , AgentProfile.MiddleName.AsString(),
                    "GenderID" , AgentProfile.GenderID.AsString(),
                    "CivilStatusID" , AgentProfile.CivilStatusID.AsString(),
                    "DateofBirth" , AgentProfile.DateofBirth.AsString(),
                    "AgentTypeID" , AgentProfile.AgentTypeID.AsString(),
                    "WithCashCard" , AgentProfile.WithCashCard.AsString(),
                    "PreparedBYID" , AgentProfile.PreparedByID.AsString(),
                    "DocumentStatusCode" , "7",
                    "Permission" , "65541",
                    "Notes" , ""
                };
                ret = db.Scalar(sql, 1, parms).AsInt();
            }
            else
            {
                string sql = "[usp_UpdateAgentProfile]";
                object[] parms = {
                    "ID" , AgentProfile.ID.AsString(),
                    "Code" , AgentProfile.AGENTCode.AsString(),
                    "OrganizationID" , AgentProfile.OrganizationID.AsString(),
                    "LastName" , AgentProfile.LastName.AsString(),
                    "FirstName" , AgentProfile.FirstName.AsString(),
                    "MiddleName" , AgentProfile.MiddleName.AsString(),
                    "GenderID" , AgentProfile.GenderID.AsString(),
                    "CivilStatusID" , AgentProfile.CivilStatusID.AsString(),
                    "DateofBirth" , AgentProfile.DateofBirth.AsString(),
                    "AgentTypeID" , AgentProfile.AgentTypeID.AsString(),
                    "WithCashCard" , AgentProfile.WithCashCard.AsString(),
                    "PreparedBYID" , AgentProfile.PreparedByID.AsString(),
                    "DocumentStatusCode" , "7",
                    "Permission" , "65541",                    
                    "Notes" , ""
                };
                ret = db.Scalar(sql, 1, parms).AsInt();
            }
            return ret;
        }
        public int updateAgentAddress(List<AgentAddress> AgentAddress, string AgentProfileID)
        {
            string sqlDel = "Delete from agent_profile_address where Agent_Profile_ID = '" + AgentProfileID + "'";
            object[] parmsDel = { };
            db.Scalar(sqlDel, 0, parmsDel).AsInt();
            int ret = 1;
            foreach (AgentAddress md in AgentAddress)
            {
                string sql = "[usp_UpdateAgentAddress]";
                object[] parms = {
                    "ID", md.ID.AsString(),
                    "AgentProfileID", md.AgentProfileID.AsString(),
                    "AddressTypeID", md.AddressTypeID.AsString(),
                    "StreetAddress", md.StreetAddress.AsString(),
                    "BarangayName", md.BarangayName.AsString(),
                    "CityID", md.CityID.AsString(),
                    "PostalCode", md.PostalCode.AsString(),
                    "PhoneNumber", md.PhoneNumber.AsString(),
                    "MobileNumber", md.MobileNumber.AsString(),
                    "ResidentDate", md.ResidentDate.AsString(),
                    "HomeOwnershipID", md.HomeOwnershipID.AsString()
                };
                ret = db.Scalar(sql, 1, parms).AsInt();
                if (ret != 1)
                    break;
            }
            return ret;
        }

        public IEnumerable<City> getCity(string AgentProfileID)
        {
            string sql = "Select ID,CODE,DESCRIPTION,PROVINCE_ID from city where Province_ID in (Select PROVINCEID from uvw_AgentProfileAddress where ADDRESS_TYPE_ID = 0 and Agent_Profile_ID = '" + AgentProfileID + "')";
            object[] parms = { };
            return db.Read(sql, selectCity, 0, parms);
        }

        static Func<IDataReader, City> selectCity = reader =>
           new City
           {
               CityID = reader["ID"].AsString(),
               Code = reader["Code"].AsString(),
               Description = reader["Description"].AsString(),
               ProvinceID = reader["PROVINCE_ID"].AsString()
           };
        public IEnumerable<AgentType> getAgentType()
        {
            string sql = "Select ID,Code,Description from Agent_Type";
            object[] parms = { };
            return db.Read(sql, selectAgentType, 0, parms);
        }

        static Func<IDataReader, AgentType> selectAgentType = reader =>
           new AgentType
           {
               AgentTypeID = reader["ID"].AsString(),
               Code = reader["Code"].AsString(),
               Description = reader["Description"].AsString()
           };
    }
}
