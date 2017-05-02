using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    public interface ICreditInvestigationDAO
    {
        IEnumerable<BusinessObjects.CRIncome> getIncome(string BorrowerID);
        IEnumerable<BusinessObjects.CreditInvestigation> getCRForm(string code);
        IEnumerable<BusinessObjects.LoanList> getLoanApplicationListing(string searchkey);
    }
}
