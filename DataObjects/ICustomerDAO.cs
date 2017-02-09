using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects.AdoNET;
using BusinessObjects;

namespace DataObjects
{
    public interface ICustomerDAO
    {
        List<Dictionary<string, object>> getCustomerRecord();
        IEnumerable<CustomerCharacter> getCustomerCharacterByID(string ID);
        IEnumerable<CustomerEducation> getCustomerEducationByID(string ID);
        IEnumerable<CustomerDependents> getCustomerDependentsByID(string ID);
        IEnumerable<CustomerAddress> getCustomerAddressByID(string ID);
        IEnumerable<CustomerEmployment> getCustomerEmploymentRecordByID(string ID);
        IEnumerable<CustomerRecord> getCustomerRecordByCode(string Code);


    }
}
