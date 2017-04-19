using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects
{
    public class Position
    {
        public string PositionID { get; set; }
        public string PositionCode { get; set; }
        public string Description { get; set; }
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

    public class PaymentMode
    {
        public string PaymentModeID { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
    }

    public class Bank
    {
        public string BankID { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
    }
}

