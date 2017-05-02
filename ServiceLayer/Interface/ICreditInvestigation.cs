using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;
namespace ServiceLayer.Interface
{
    public interface ICreditInvestigation
    {
        IEnumerable<BusinessObjects.CRIncome> getIncome(string BorrowerID);
        BusinessObjects.CreditInvestigation getCRForm(string code);

        IEnumerable<BusinessObjects.LoanList> getLoanApplicationListing();

    }
}
