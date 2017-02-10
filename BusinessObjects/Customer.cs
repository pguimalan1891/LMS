using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects
{
    public class CustomerRecord
    {
        public string ID { get; set; }
        public string Code { get; set; }
        public string DatetimeCreated { get; set; }
        public string OrganizationID { get; set; }
        public string Organization { get; set; }
        public string District { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string GenderID { get; set; }
        public string Gender { get; set; }
        public string CivilStatusID { get; set; }
        public string CivilStatus { get; set; }
        public string DateOfMarriage { get; set; }
        public string CitizenshipID { get; set; }
        public string Citizenship { get; set; }
        public string DateOfBirth { get; set; }
        public string GSISNumber { get; set; }
        public string SSSNumber { get; set; }
        public string TinNumber { get; set; }
        public string RCN { get; set; }
        public string RCNPlaceIssued { get; set; }
        public string RCNDateIssued { get; set; }
        public string BorrowerTypeID { get; set; }
        public string BorrowerType { get; set; }
        public string BorrowerGroup { get; set; }
        public string LeadSourceID { get; set; }
        public string LeadSource { get; set; }
        public string AgentProfileID { get; set; }
        public string AgentCode { get; set; }
        public string AgentType { get; set; }
        public string AgentLastName { get; set; }
        public string AgentFirstName { get; set; }
        public string AgentMiddleName { get; set; }
        public string DocumentStatus { get; set; }
        public string ApplicationTypeID { get; set; }
        public string ApplicationType { get; set; }
        public string SpouseFirstName { get; set; }
        public string SpouseMiddleName { get; set; }
        public string SpouseLastName { get; set; }
        public string SpouseDateofBirth { get; set; }
        public string SpouseContactNumber { get; set; }
        public string OwnerCode { get; set; }
        public string OwnerID { get; set; }
        public string PreparedByID { get; set; }
        public string PreparedByDatetime { get; set; }
        public string DocumentStatusCode { get; set; }       
        public string Permission { get; set; }
        public string Notes { get; set; }

    }    

    public class CustomerEmployment
    {
        public string ID { get; set; }
        public string PISID { get; set; }
        public string BusinessTypeID { get; set; }
        public string BusinessType { get; set; }
        public string EmployerName { get; set; }
        public string Income { get; set; }
        public string Contact_No { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string IsActive { get; set; }
        public string IsSpouse { get; set; }
        public string NatureOfBusinessID { get; set; }
        public string NatureOfBusiness { get; set; }

    }

    public class CustomerAddress
    {
        public string ID { get; set; }
        public string PISID { get; set; }
        public string AddressTypeID { get; set; }
        public string AddressType { get; set; }
        public string StreetAddress { get; set; }
        public string BarangayName { get; set; }
        public string CityID { get; set; }
        public string City { get; set; }
        public string Province { get; set; }
        public string Country { get; set; }
        public string PostalCode { get; set; }
        public string PhoneNumber { get; set; }
        public string MobileNumber { get; set; }
        public string ResidentDate { get; set; }
        public string HomeOwnershipID { get; set; }
        public string HomeOwnerShip { get; set; }
    }

    public class CustomerDependents
    {
        public string ID { get; set; }
        public string PISID { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string GenderID { get; set; }
        public string Gender { get; set; }
        public string StreetAddress { get; set; }
        public string CityID { get; set; }
        public string City { get; set; }
        public string Province { get; set; }
        public string RelationshipTypeID { get; set; }
        public string RelationshipType { get; set; }
        public string BirthDate { get; set; }
        public string SchoolAddress { get; set; }
        public string ContactNo { get; set; }
    }

    public class CustomerEducation
    {
        public string ID { get; set; }
        public string PISID { get; set; }
        public string EducationTypeID { get; set; }
        public string EducationType { get; set; }
        public string SchoolName { get; set; }
        public string GraduationDate { get; set; }
    }

    public class CustomerCharacter
    {
        public string ID { get; set; }
        public string PISID { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string RelationShip { get; set; }
        public string StreetAddress { get; set; }
        public string CityID { get; set; }
        public string City { get; set; }
        public string ContactNo { get; set; }
    }

    public class getComponents
    {
        public IEnumerable<Gender> Gender { get; set; }
        public IEnumerable<Citizenship> Citizenship { get; set; }
        public IEnumerable<District> District { get; set; }
        public IEnumerable<Organization> Organization { get; set; }
        public IEnumerable<ApplicationType> ApplicationType { get; set; }
        public IEnumerable<BorrowerType> BorrowerType { get; set; }
        public IEnumerable<LeadSource> LeadSource { get; set; }
        public IEnumerable<CivilStatus> CivilStatus { get; set; }
        public IEnumerable<City> City { get; set; }
        public IEnumerable<Province> Province { get; set; }
        public IEnumerable<HomeOwnership> HomeOwnership { get; set; }
        public IEnumerable<BusinessType> BusinessType { get; set; }
        public IEnumerable<NatureofBusiness> NatureofBusiness { get; set; }
        public IEnumerable<AddressType> AddressType { get; set; }
        public IEnumerable<RelationshipType> RelationshipType { get; set; }
        public IEnumerable<EducationType> EducationType { get; set; }

    }

    public class Gender
    {
        public string ID { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }       
    }
    public class Citizenship
    {
        public string ID { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
    }
    public class District
    {
        public string ID { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public string DistrictGroupID { get; set; }
        public string RegionalOfficeID { get; set; }
    }
    public class Organization
    {
        public string ID { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public string DistrictID { get; set; }
        public string MotherBranchID { get; set; }
    }
    public class ApplicationType
    {
        public string ID { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
    }
    public class BorrowerType
    {
        public string ID { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public string BorrowerGroupID { get; set; }
    }
    public class LeadSource
    {
        public string ID { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }        
    }
    public class CivilStatus
    {
        public string ID { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
    }
    public class City
    {
        public string ID { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public string ProvinceID { get; set; }
    }
    public class Province
    {
        public string ID { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public string CountryID { get; set; }
    }
    public class HomeOwnership
    {
        public string ID { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
    }
    public class BusinessType
    {
        public string ID { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
    }
    public class NatureofBusiness
    {
        public string ID { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
    }
    public class AddressType
    {
        public string ID { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
    }
    public class RelationshipType
    {
        public string ID { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
    }
    public class EducationType
    {
        public string ID { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
    }    

}
