using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace LMS.Models.DevelopmentTools
{
    public class UserAccountModel
    {
        public UserAccount userAccount { get; set; }
        public IEnumerable<UserAccountStatus> userAccountStatus { get; set; }
        public IEnumerable<Company> Company { get; set; }
        public IEnumerable<Position> Position { get; set; }
        public IEnumerable<Customer.Organization> Organization { get; set; }
        public IEnumerable<PasswordNeverExpiresYesNo> PasswordNeverExpires { get; set; }
    }

    public class UserRoleModel
    {
        public UserAccount userAccount { get; set; }
        public IEnumerable<Roles> GrantedRoles { get; set; }
        public IEnumerable<Roles> NotGrantedRoles { get; set; }
    }

    public class UserAccount
    {
        public string ID { get; set; }
        [Required(ErrorMessage = "User Code is required.")]
        public string Code { get; set; }
        [Required(ErrorMessage = "Last Name is required.")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "First Name is required.")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Middle Name is required.")]
        public string MiddleName { get; set; }        
        public string FullName { get; set; }
        public string RoleName { get; set; }        
        public string PostalAddress { get; set; }
        [Required(ErrorMessage = "Email Address is required.")]
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
        [Required(ErrorMessage = "Password is required.")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Confirm Password is required.")]
        public string ConfirmPassword { get; set; }
    }

    public class UserAccountStatus
    {
        public string Status { get; set; }
        public string Description { get; set; }
    }

    public class Company
    {
        public string CompanyID { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string ShortDescription { get; set; }
        public string OrganizationID { get; set; }
        public string Organization { get; set; }
        public string PhoneNumber { get; set; }
        public string MobileNumber { get; set; }
        public string EmailAddress { get; set; }
        public string ContactPerson { get; set; }
        public string ContactPosition { get; set; }
        public string ContactPhoneNumber { get; set; }
        public string ContactMobileNumber { get; set; }
        public string ContactEmailAddress { get; set; }
        public string TINNumber { get; set; }
        public string AccountNumber { get; set; }
        public string StreetName { get; set; }
        public string CityID { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string POBox { get; set; }
        public string WebSite { get; set; }
        public string IndustryID { get; set; }
        public string Industry { get; set; }
        public string LeadSourceID { get; set; }
        public string LeadSource { get; set; }
        public string CompanyTypeID { get; set; }
        public string CompanyType { get; set; }
        public string PreparedByID { get; set; }
        public string PreparedByDateTime { get; set; }
        public string DocumentStatusCode { get; set; }
        public string DocumentStatus { get; set; }
        public string Permission { get; set; }
        public string Notes { get; set; }
    }

    public class Position
    {
        public string PositionID { get; set; }
        public string PositionCode { get; set; }
        public string Description { get; set; }
    }

    public class PasswordNeverExpiresYesNo
    {
        public string PasswordNeverExpires { get; set; }
        public string text { get; set; }
    }

    public class Roles
    {
        public string ID { get; set; }
        public string Code { get; set; }
        public string RoleName { get; set; }
    }
}