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

    }
}
