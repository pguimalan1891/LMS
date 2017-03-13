using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    public interface ILoanApplicationDAO
    {
        IEnumerable<BusinessObjects.newLoanBorrowerProfile> GetBorrowers(string searchkey);

        IEnumerable<BusinessObjects.LoanType> getProducts();
        IEnumerable<BusinessObjects.LoanSet> getLoanSet();

        IEnumerable<BusinessObjects.LoanTerms> getLoanTerms();
        IEnumerable<BusinessObjects.BorrowerProfile> getBorrowerProfile(string borrowerCode);

        IEnumerable<BusinessObjects.RequiredDocuments> getBorrowerRequiredDocuments(string borrowerCode);
        IEnumerable<BusinessObjects.LoanList> getLoanApplicationListing(string status, string searchkey);
        IEnumerable<BusinessObjects.DocumentStatus> getDocumentStatus();
        IEnumerable<BusinessObjects.ComakerProfile> getComakers(string loanCode);
        IEnumerable<BusinessObjects.LoanApplicationModel> getLoanFormDetails(string AccountNo);
    }
}
