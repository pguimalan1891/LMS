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
        //    var qryResult = customer.getCustomerRecord();
        //    List<Dictionary<string, object>> lstInfo = new List<Dictionary<string, object>>();
        //    foreach (var rec in qryResult)
        //    {
        //        Dictionary<string, object> d = new Dictionary<string, object>();
        //        foreach (var x in rec)
        //        {
        //            d.Add(x.Key, x.Value);
        //        }
        //        lstInfo.Add(d);
        //    }

        //    return lstInfo;
            return customer.getCustomerRecord();
        }

    }
}
