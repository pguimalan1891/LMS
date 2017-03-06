using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Interface
{
    public interface ILoanApplicationSvc
    {
        List<Dictionary<string, object>> GetBorrowers(string searchkey);
        IEnumerable<BusinessObjects.LoanType> GetLoanProducts();

        IEnumerable<BusinessObjects.LoanSet> GetLoanSet();

        IEnumerable<BusinessObjects.LoanTerms> GetLoanTerms();

        BusinessObjects.BorrowerProfile GetBorrowerProfile(string code);
    }
}
