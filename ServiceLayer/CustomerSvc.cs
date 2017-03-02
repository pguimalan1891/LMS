using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;
using BusinessObjects;
using ServiceLayer.Interface;

namespace ServiceLayer
{
    public class CustomerSvc : ICustomerSvc
    {
        static readonly IDAOFactory factory = DAOFactories.GetFactory();
        static readonly ICustomerDAO customer = factory.CustomerDAO;

        public int UpdateCustomerData(string ProcessType, CustomerModel custModel, string PISID)
        {
            return customer.UpdateCustomerData(ProcessType, custModel, PISID);
        }
        public List<Dictionary<string, object>> getCustomerRecord()
        {
            return customer.getCustomerRecord();
        }

        public BusinessObjects.CustomerRecord getCustomerRecordByCode(string Code)
        {
            var CustRecord = customer.getCustomerRecordByCode(Code);

            return (BusinessObjects.CustomerRecord)CustRecord.First();
        }

        public IEnumerable<CustomerCharacter> getCustomerCharacterByID(string ID)
        {
            return customer.getCustomerCharacterByID(ID);
        }
        public IEnumerable<CustomerEducation> getCustomerEducationByID(string ID)
        {
            return customer.getCustomerEducationByID(ID);
        }

        public IEnumerable<CustomerDependents> getCustomerDependentsByID(string ID)
        {
            return customer.getCustomerDependentsByID(ID);
        }

        public IEnumerable<CustomerAddress> getCustomerAddressByID(string ID)
        {
            return customer.getCustomerAddressByID(ID);
        }

        public IEnumerable<CustomerEmployment> getCustomerEmploymentRecordByID(string ID)
        {
            return customer.getCustomerEmploymentRecordByID(ID);
        }
        public getComponents getAllComponents(BusinessObjects.CustomerRecord custRecord)
        {
            return customer.getAllComponents(custRecord);
        }
        public IEnumerable<Gender> getGender()
        {
            return customer.getGender();
        }
        public IEnumerable<Citizenship> getCitizenship()
        {
            return customer.getCitizenship();
        }
        public IEnumerable<District> getDistrict()
        {
            return customer.getDistrict();
        }
        public IEnumerable<Organization> getOrganization()
        {
            return customer.getOrganization();
        }
        public IEnumerable<ApplicationType> getApplicationType()
        {
            return customer.getApplicationType();
        }
        public IEnumerable<BorrowerType> getBorrowerType()
        {
            return customer.getBorrowerType();
        }
        public IEnumerable<LeadSource> getLeadSource()
        {
            return customer.getLeadSource();
        }
        public IEnumerable<CivilStatus> getCivilStatus()
        {
            return customer.getCivilStatus();
        }
        public IEnumerable<City> getCity(string PISID)
        {
            return customer.getCity(PISID);
        }
        public IEnumerable<City> updateCity(string ProvinceID)
        {
            return customer.updateCity(ProvinceID);
        }

        public IEnumerable<Province> getProvince()
        {
            return customer.getProvince();
        }
        public IEnumerable<HomeOwnership> getHomeOwnership()
        {
            return customer.getHomeOwnership();
        }
        public IEnumerable<BusinessType> getBusinessType()
        {
            return customer.getBusinessType();
        }
        public IEnumerable<NatureofBusiness> getNatureofBusiness()
        {
            return customer.getNatureofBusiness();
        }
        public IEnumerable<AddressType> getAddressType(bool includecurrentAddress)
        {
            return customer.getAddressType(includecurrentAddress);
        }
        public IEnumerable<RelationshipType> getRelationshipType()
        {
            return customer.getRelationshipType();
        }
        public IEnumerable<EducationType> getEducationType()
        {
            return customer.getEducationType();
        }
        public IEnumerable<Agent> getAgent(string PISID)
        {
            return customer.getAgent(PISID);
        }
        public IEnumerable<Agent> updateAgent(string applicationTypeID, string OrganizationID)
        {
            return customer.updateAgent(applicationTypeID, OrganizationID);
        }
    }
}
