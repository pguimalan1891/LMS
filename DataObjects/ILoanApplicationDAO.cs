using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    public interface ILoanApplicationDAO
    {
        List<Dictionary<string, object>> GetBorrowers(string searchkey);

        IEnumerable<BusinessObjects.LoanType> getProducts();
        IEnumerable<BusinessObjects.LoanSet> getLoanSet();

        IEnumerable<BusinessObjects.LoanTerms> getLoanTerms();
        IEnumerable<BusinessObjects.BorrowerProfile> getBorrowerProfile(string borrowerCode);
    }
}
