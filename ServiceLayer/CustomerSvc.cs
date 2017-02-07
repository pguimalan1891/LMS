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

    }
}
