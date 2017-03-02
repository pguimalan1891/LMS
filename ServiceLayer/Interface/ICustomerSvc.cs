using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;

namespace ServiceLayer.Interface
{
    public interface ICustomerSvc
    {
        List<Dictionary<string, object>> getCustomerRecord();
        CustomerRecord getCustomerRecordByCode(string Code);
        IEnumerable<CustomerCharacter> getCustomerCharacterByID(string ID);
        IEnumerable<CustomerEducation> getCustomerEducationByID(string ID);
        IEnumerable<CustomerDependents> getCustomerDependentsByID(string ID);
        IEnumerable<CustomerAddress> getCustomerAddressByID(string ID);
        IEnumerable<CustomerEmployment> getCustomerEmploymentRecordByID(string ID);
        int UpdateCustomerData(string ProcessType, CustomerModel custModel, string PISID);
        getComponents getAllComponents(BusinessObjects.CustomerRecord custRecord);    
        IEnumerable<Gender> getGender();
        IEnumerable<Citizenship> getCitizenship();
        IEnumerable<District> getDistrict();
        IEnumerable<Organization> getOrganization();
        IEnumerable<ApplicationType> getApplicationType();
        IEnumerable<BorrowerType> getBorrowerType();
        IEnumerable<LeadSource> getLeadSource();
        IEnumerable<CivilStatus> getCivilStatus();
        IEnumerable<City> getCity(string PISID);
        IEnumerable<City> updateCity(string ProvinceID);
        IEnumerable<Province> getProvince();
        IEnumerable<HomeOwnership> getHomeOwnership();
        IEnumerable<BusinessType> getBusinessType();
        IEnumerable<NatureofBusiness> getNatureofBusiness();
        IEnumerable<AddressType> getAddressType(bool includecurrentAddress);
        IEnumerable<RelationshipType> getRelationshipType();
        IEnumerable<EducationType> getEducationType();

    }
}
