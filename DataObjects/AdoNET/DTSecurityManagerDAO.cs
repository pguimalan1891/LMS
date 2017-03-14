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
    public class DTSecurityManagerDAO : IDTSecurityManagerDAO
    {
        static DB db = new DB();        
        
        public int updateUserRoles(List<Roles> userRoles,string userAccountID)
        {
            string sql = "Delete from user_role where USER_ID = @ID";
            object[] parms = { "ID", userAccountID };
            db.Scalar(sql, 0, parms);

            foreach(Roles Roles in userRoles)
            {
                sql = "Insert into user_role(ID,USER_ID,ROLE_ID) Select @ID,@UserID,@RoleID ";
                object[] parms2 = {
                    "ID", Guid.NewGuid().ToString(),
                    "UserID", userAccountID,
                    "RoleID", Roles.ID
                };
                db.Scalar(sql, 0, parms2);
            }
            return 1;
        }

        public IEnumerable<Roles> getGrantedRoles(string ID)
        {
            string sql = "Select ID,CODE,ROLE_NAME from user_account where type=1 and ID in (Select ROLE_ID from user_role where USER_ID = @ID)";
            object[] parms = { "ID", ID };
            return db.Read(sql, selectGrantedRole, 0, parms);
        }

        static Func<IDataReader, Roles> selectGrantedRole = reader =>
           new Roles
           {
               ID = reader["ID"].AsString(),
               Code = reader["CODE"].AsString(),
               RoleName = reader["ROLE_NAME"].AsString()

           };

        public IEnumerable<Roles> getNotGrantedRoles(string ID)
        {
            string sql = "Select ID,CODE,ROLE_NAME from user_account where type=1 and ID not in (Select ROLE_ID from user_role where USER_ID = @ID)";
            object[] parms = { "ID", ID };
            return db.Read(sql, selectNotGrantedRole, 0, parms);
        }

        static Func<IDataReader, Roles> selectNotGrantedRole = reader =>
           new Roles
           {
               ID = reader["ID"].AsString(),
               Code = reader["CODE"].AsString(),
               RoleName = reader["ROLE_NAME"].AsString()

           };

        public int UpdateStatusUserAccount(string ID, string Status)
        {
            string sql = "Update user_account set STATUS = @Status where ID = @ID; Select 1";
            object[] parms = { "ID", ID, "Status", Status };
            return db.Scalar(sql, 0, parms).AsInt();
        }

        public int ResetPasswordUserAccount(UserAccount UserAccount)
        {
            string sql = "spu_UpdatePassword";
            object[] parms = {
                "ID",UserAccount.ID,
                "PasswordID",UserAccount.PasswordID,
                "Password",UserAccount.Password
            };
            return db.Scalar(sql, 1, parms).AsInt();
        }

        public int UpdateUserAccounts(string ProcessType,UserAccount UserAccount)
        {
            string sql = "usp_UpdateUserAccounts";
            object[] parms = {
                "ProcessType",ProcessType,
                "ID",UserAccount.ID,
                "Code",UserAccount.Code,
                "LastName",UserAccount.LastName,
                "FirstName",UserAccount.FirstName,
                "MiddleName",UserAccount.MiddleName,
                "PostalAddress",UserAccount.PostalAddress,
                "EmailAddress",UserAccount.EmailAddress,
                "PositionID",UserAccount.PositionID,
                "OrganizationID",UserAccount.OrganizationID,
                "CompanyID",UserAccount.CompanyID,
                "PasswordNeverExpires",UserAccount.PasswordNeverExpires,
                "Status",UserAccount.Status,
                "RegisterByID",UserAccount.RegisteredByID,
                "PasswordID",UserAccount.PasswordID,
                "Password",UserAccount.Password
            };
            return db.Scalar(sql, 1, parms).AsInt();
        }

        public IEnumerable<UserAccount> getUserAccountbyID(string ID)
        {
            string sql = "Select ID,CODE,LAST_NAME,FIRST_NAME,MIDDLE_NAME,full_name,ROLE_NAME,POSTAL_ADDRESS,EMAIL_ADDRESS,POSITION_ID,position_code,position_desc," +
            "RANK_ID,Rank,ASSIGNED_OFFICE_ID,assigned_office_code,assigned_office_desc,COMPANY_ID,company_code,company_name,PASSWORD_NEVER_EXPIRES,STATUS,TYPE,uasm_desc from uvw_UserAccounts where ID=@ID";
            object[] parms = { "ID", ID };
            return db.Read(sql, selectUserAccount, 0, parms);
        }
        static Func<IDataReader, UserAccount> selectUserAccount = reader =>
           new UserAccount
           {
               ID = reader["ID"].AsString(),
               Code = reader["CODE"].AsString(),
               LastName = reader["LAST_NAME"].AsString(),
               FirstName = reader["FIRST_NAME"].AsString(),
               MiddleName = reader["MIDDLE_NAME"].AsString(),
               FullName = reader["full_name"].AsString(),
               RoleName = reader["ROLE_NAME"].AsString(),
               PostalAddress = reader["POSTAL_ADDRESS"].AsString(),
               EmailAddress = reader["EMAIL_ADDRESS"].AsString(),
               PositionID = reader["POSITION_ID"].AsString(),
               PositionCode = reader["position_code"].AsString(),
               Position = reader["position_desc"].AsString(),
               Rank = reader["Rank"].AsString(),
               RankID = reader["RANK_ID"].AsString(),
               OrganizationID = reader["ASSIGNED_OFFICE_ID"].AsString(),
               OrganizationCode = reader["assigned_office_code"].AsString(),
               Organization = reader["assigned_office_desc"].AsString(),
               CompanyID = reader["COMPANY_ID"].AsString(),
               CompanyCode = reader["company_code"].AsString(),
               CompanyName = reader["company_name"].AsString(),
               PasswordNeverExpires = reader["PASSWORD_NEVER_EXPIRES"].AsString(),
               Status = reader["STATUS"].AsString(),
               Type = reader["TYPE"].AsString(),
               UASMDesciption = reader["uasm_desc"].AsString()
           };

        public List<Dictionary<string, object>> getUserAccounts()
        {
            string sql = "Select ID,CODE,LAST_NAME as [Last Name],FIRST_NAME as [First Name],MIDDLE_NAME as [Middle Name],assigned_office_desc as [Organization]," +
            "uasm_desc as [Status],EXPIRY_DATE as [Expiry Date],POSTAL_ADDRESS as [Postal Address],EMAIL_ADDRESS as [Email Address] from uvw_UserAccounts Order by DATETIME_REGISTERED Desc";
            object[] parms = { };
            return db.ReadDictionary(sql, 0, parms);
        }

        public IEnumerable<UserAccountStatus> getUserAccountStatus()
        {
            string sql = "Select Code as Status,Description from user_account_status_map order by Status";
            object[] parms = { };
            return db.Read(sql, SelectUserAccountStatus, 0, parms);
        }

        static Func<IDataReader, UserAccountStatus> SelectUserAccountStatus = reader =>
           new UserAccountStatus
           {
               Status = reader["Status"].AsString(),               
               Description = reader["Description"].AsString()
           };

        public IEnumerable<Company> getListCompany()
        {
            string sql = "Select ID,CODE,NAME from uvw_Company";
            object[] parms = { };
            return db.Read(sql, selectCompany, 0, parms);
        }
        static Func<IDataReader, Company> selectCompany = reader =>
           new Company
           {
               CompanyID = reader["ID"].AsString(),
               Code = reader["CODE"].AsString(),
               Name = reader["NAME"].AsString()
           };

        public IEnumerable<Position> getPosition()
        {
            string sql = "Select ID,Code,Description from position";
            object[] parms = { };
            return db.Read(sql, selectPosition, 0, parms);
        }
        static Func<IDataReader, Position> selectPosition = reader =>
          new Position
          {
              PositionID = reader["ID"].AsString(),
              PositionCode = reader["CODE"].AsString(),
              Description = reader["Description"].AsString()
          };
    }
}
