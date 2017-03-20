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
    public class HomeSvc : IHomeSvc
    {
        static readonly IDAOFactory factory = DAOFactories.GetFactory();
        static readonly IHomeDAO homeDao = factory.HomeDAO;

        public List<Dictionary<string, object>> GetCustomerList() {
            return homeDao.GetCustomerList();
        }

    }
}
