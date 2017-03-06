using DataObjects;
using ServiceLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer
{
    public class LoanApplicationSvc : ILoanApplicationSvc
    {
        static readonly IDAOFactory factory = DAOFactories.GetFactory();
        static readonly IAccountDAO acctCtrl = factory.AccountDAO;

        public List<Dictionary<string, object>> GetBorrowers(string searchkey)
        {
            return null;   
        }

    }
}
