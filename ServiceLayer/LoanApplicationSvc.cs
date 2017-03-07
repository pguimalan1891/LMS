using BusinessObjects;
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
        static readonly ILoanApplicationDAO lnCtrl = factory.LoanApplicationDAO;
        static readonly ICustomerDAO customer = factory.CustomerDAO;


        public List<Dictionary<string, object>> GetBorrowers(string searchkey)
        {
            return null;   
        }

       
        public IEnumerable<BusinessObjects.LoanType> GetLoanProducts()
        {
            return lnCtrl.getProducts();
        }

        public IEnumerable<BusinessObjects.LoanSet> GetLoanSet()
        {
            return lnCtrl.getLoanSet();
        }

        public IEnumerable<BusinessObjects.LoanTerms> GetLoanTerms()
        {
            return lnCtrl.getLoanTerms();

        }
        public BusinessObjects.BorrowerProfile GetBorrowerProfile(string code)
        {
            IEnumerable<BusinessObjects.BorrowerProfile> list = lnCtrl.getBorrowerProfile(code);
            return list.First();
        }

        public IEnumerable<BusinessObjects.LoanList> GetLoanApplicationListing()
        {
            return lnCtrl.getLoanApplicationListing();
        }

        public IEnumerable<BusinessObjects.DocumentStatus> GetDocumentStatus()
        {
            return lnCtrl.getDocumentStatus();
        }
    }
}
