using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LMS.Models.Customer;
using System.ComponentModel.DataAnnotations;

namespace LMS.Models.MaintenanceAgentProfile
{
    public class AgentModel
    {
        public AgentProfile AgentProfile { get; set; }
        public List<AgentAddress> AgentAddress { get; set; }
        public IEnumerable<Gender> Gender { get; set; }
        public IEnumerable<CivilStatus> CivilStatus { get; set; }
        public IEnumerable<District> District { get; set; }
        public IEnumerable<Organization> Organization { get; set; }
        public IEnumerable<AgentType> AgentType { get; set; }
        public IEnumerable<Province> Province { get; set; }
        public IEnumerable<City> City { get; set; }
        public IEnumerable<HomeOwnership> HomeOwnership { get; set; }
        public List<WithCashCardYesNo> WithCashCard { get; set; }
    }
    public class AgentAddressModel
    {
        public AgentAddress AgentAddress { get; set; }
        public IEnumerable<Province> Province { get; set; }
        public IEnumerable<City> City { get; set; }
        public IEnumerable<HomeOwnership> HomeOwnership { get; set; }
        public IEnumerable<AddressType> AddressType { get; set; }
    }    
    public class AgentProfile
    {
        public string ID { get; set; }
        public string AGENTCode { get; set; }
        public string OrganizationID { get; set; }
        public string Organization { get; set; }
        public string DistrictID { get; set; }
        public string District { get; set; }
        [Required(ErrorMessage = "First Name is required.")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Middle Name is required.")]
        public string MiddleName { get; set; }
        [Required(ErrorMessage = "Last Name is required.")]
        public string LastName { get; set; }
        public string GenderID { get; set; }
        public string Gender { get; set; }
        public string CivilStatusID { get; set; }
        public string CivilStatus { get; set; }
        [Required(ErrorMessage = "Date of Birth is required.")]
        public string DateofBirth { get; set; }
        public string AgentTypeID { get; set; }
        public string AgentType { get; set; }
        public string WithCashCard { get; set; }
        public string PreparedByID { get; set; }
        public string PreparedBy { get; set; }
        public string DocumentStatusCode { get; set; }
        public string DocumentStatus { get; set; }
        public string Permission { get; set; }
        public string Notes { get; set; }
    }

    public class AgentAddress
    {
        public string ID { get; set; }
        public string AgentProfileID { get; set; }
        public string AddressTypeID { get; set; }
        public string AddressType { get; set; }
        [Required(ErrorMessage = "Street Address is required.")]
        public string StreetAddress { get; set; }
        [Required(ErrorMessage = "Barangay Name is required.")]
        public string BarangayName { get; set; }
        public string CityID { get; set; }
        public string City { get; set; }
        public string ProvinceID { get; set; }
        public string Province { get; set; }
        public string Country { get; set; }
        public string PostalCode { get; set; }
        [Required(ErrorMessage = "Phone Number is required.")]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "Mobile Number is required.")]
        public string MobileNumber { get; set; }
        [Required(ErrorMessage = "Date of Residence is required.")]
        public string ResidentDate { get; set; }
        public string HomeOwnershipID { get; set; }
        public string HomeOwnerShip { get; set; }
    }

    public class AgentType
    {
        public string AgentTypeID { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
    }

    public class WithCashCardYesNo
    {
        public string WithCashCard { get; set; }
        public string text { get; set; }
    }
}