using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceLayer.Interface;
using DataObjects;


namespace ServiceLayer
{

    

    public class CreditInvestigationSvc : ICreditInvestigation
    {
        static readonly IDAOFactory factory = DAOFactories.GetFactory();
        static readonly ICreditInvestigationDAO lnCtrl = factory.CreditInvestigationDAO;
        static readonly ICustomerDAO customer = factory.CustomerDAO;
        public IEnumerable<BusinessObjects.CRIncome> getIncome(string BorrowerID)
        {
           return lnCtrl.getIncome(BorrowerID);
        }

        public BusinessObjects.CreditInvestigation getCRForm(string code)
        {
            return lnCtrl.getCRForm(code).First();
        }
    }
}
