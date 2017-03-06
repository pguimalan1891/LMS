using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects
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
    public class AgentProfile
    {
        public string ID { get; set; }
        public string AGENTCode { get; set; }
        public string OrganizationID { get; set; }
        public string Organization { get; set; }
        public string DistrictID { get; set; }
        public string District { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string GenderID { get; set; }
        public string Gender { get; set; }
        public string CivilStatusID { get; set; }
        public string CivilStatus { get; set; }
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
        public string StreetAddress { get; set; }
        public string BarangayName { get; set; }
        public string CityID { get; set; }
        public string City { get; set; }
        public string ProvinceID { get; set; }
        public string Province { get; set; }
        public string Country { get; set; }
        public string PostalCode { get; set; }
        public string PhoneNumber { get; set; }
        public string MobileNumber { get; set; }
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
