using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects
{
    public class UserAccount
    {
        public string ID { get; set; }
        public string Code { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string FullName { get; set; }
        public string RoleName { get; set; }
        public string PostalAddress { get; set; }
        public string EmailAddress { get; set; }
        public string PositionID { get; set; }
        public string PositionCode { get; set; }
        public string Position { get; set; }
        public string RankID { get; set; }
        public string Rank { get; set; }
        public string OrganizationID { get; set; }
        public string OrganizationCode { get; set; }   
        public string Organization { get; set; }
        public string CompanyID { get; set; }
        public string CompanyCode { get; set; }
        public string CompanyName { get; set; }
        public string PasswordNeverExpires { get; set; }
        public string Status { get; set; }
        public string Type { get; set; }
        public string RegisteredByID { get; set; }
        public string DatetimeRegistered { get; set; }
        public string LockoutCounter { get; set; }
        public string DatetimeLogonFailed { get; set; }
        public string DatetimeLockOut { get; set; }
        public string ExpiryDate { get; set; }
        public string UASMDesciption { get; set; }
        public string PasswordID { get; set; }
        public string Password { get; set; }

    }    

    public class Roles
    {
        public string ID { get; set; }
        public string Code { get; set; }
        public string RoleName { get; set; }
    }
}
