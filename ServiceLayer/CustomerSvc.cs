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
        public getComponents getAllComponents()
        {
            return customer.getAllComponents();
        }
    }
}
