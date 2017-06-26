using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Interface
{
    public interface ILoanApplicationSvc
    {
        IEnumerable<BusinessObjects.newLoanBorrowerProfile> GetBorrowers(string searchkey);

        IEnumerable<BusinessObjects.LoanType> GetLoanProducts();

        IEnumerable<BusinessObjects.LoanSet> GetLoanSet(string groupid, string loantype);

        IEnumerable<BusinessObjects.LoanTermsForLoanApplication> GetLoanTerms(string groupid, string loantype, string loanset);

        BusinessObjects.BorrowerProfile GetBorrowerProfile(string code);

        IEnumerable<BusinessObjects.LoanList> getLoanApplicationListing(string status, string searchkey);

        IEnumerable<BusinessObjects.DocumentStatus> GetDocumentStatus();

        IEnumerable<BusinessObjects.RequiredDocuments> getBorrowerRequiredDocuments(string borrowerCode);

        BusinessObjects.LoanApplicationModel getLoanFormDetails(string AccountNo);

        IEnumerable<BusinessObjects.ComakerProfile> getComakers(string LoanCode);

        IEnumerable<BusinessObjects.CollateralProfile> getCollaterals(string loanCode);

        IEnumerable<BusinessObjects.CollateralType> GetCollateralType();

        IEnumerable<BusinessObjects.CollateralUsage> GetCollateralUsage();

        IEnumerable<BusinessObjects.Color> GetColor();

        IEnumerable<BusinessObjects.FuelType> GetFuelType();

        IEnumerable<BusinessObjects.Agent> GetAgents();
    
        IEnumerable<BusinessObjects.DocumentStatus> getHandlingFee();

        IEnumerable<BusinessObjects.DocumentStatus> getPPDAmounts(string loantype);

        IEnumerable<BusinessObjects.DocumentStatus> getAgentIncentives(string loantype);

        IEnumerable<BusinessObjects.DocumentStatus> getDealerIncentives(string loantype);
        string insertLoan(string AccountNo, string organizationid, string notes, string borrowerid, string loantype, string loanset, string loanterms, string ppd_rate_id, string handling_fee_id, string agent_incentive_type, string dealer_incentive_type, string loanamount, string userID, string loanpurpose);
        bool cancelLoanApplication(string loanCode);
    }
}
