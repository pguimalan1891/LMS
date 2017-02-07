using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects
{
    public class Customer
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
        public string DocumentStatusID { get; set; }       
        public string Permission { get; set; }
        public string Notes { get; set; }
    }    
}
