using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;
using ServiceLayer.Interface;

namespace ServiceLayer
{
    public class AccountingSvc: IAccountingSvc
    {
        static readonly IDAOFactory factory = DAOFactories.GetFactory();
        static readonly IAccountingDAO accounting = factory.AccountingDAO;

        public List<Dictionary<string, object>> getRequestForPayment(int status)
        {
            return accounting.getRequestForPayment(status);
        }
    }
}
