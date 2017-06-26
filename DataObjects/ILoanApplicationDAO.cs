using BusinessObjects;
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
        IEnumerable<BusinessObjects.LoanSet> getLoanSet(string groupid, string loantype);

        IEnumerable<BusinessObjects.LoanTermsForLoanApplication> getLoanTerms(string groupid, string loantype, string loanset);
        IEnumerable<BusinessObjects.BorrowerProfile> getBorrowerProfile(string borrowerCode);

        IEnumerable<BusinessObjects.RequiredDocuments> getBorrowerRequiredDocuments(string borrowerCode);
        IEnumerable<BusinessObjects.LoanList> getLoanApplicationListing(string status, string searchkey);
        IEnumerable<BusinessObjects.DocumentStatus> getDocumentStatus();
        IEnumerable<BusinessObjects.ComakerProfile> getComakers(string loanCode);
        IEnumerable<BusinessObjects.LoanApplicationModel> getLoanFormDetails(string AccountNo);

        IEnumerable<BusinessObjects.CollateralProfile> getCollaterals(string loanCode);

        IEnumerable<BusinessObjects.CollateralType> getCollateralType();

        IEnumerable<BusinessObjects.CollateralUsage> getCollateralUsage();
        IEnumerable<BusinessObjects.Color> getColor();

        IEnumerable<BusinessObjects.FuelType> getFuelType();
        IEnumerable<Agent> getAgent();
        string insertLoan(string AccountNo, string organizationid, string notes, string borrowerid, string loantype, string loanset, string loanterms, string ppd_rate_id, string handling_fee_id, string agent_incentive_type, string dealer_incentive_type, string loanamount, string userID, string loanpurpose, string addOnRate, string agent, string district, string assured);

        IEnumerable<BusinessObjects.DocumentStatus> getHandlingFee();

        IEnumerable<BusinessObjects.DocumentStatus> getPPDAmounts(string loantype);

        IEnumerable<BusinessObjects.DocumentStatus> getAgentIncentives(string loantype);

        IEnumerable<BusinessObjects.DocumentStatus> getDealerIncentives(string loantype);

        bool cancelLoanApplication(string loanCode);

        IEnumerable<BusinessObjects.LoanAppCreditInfo> getloanCreditInfo(string loanCode);

        IEnumerable<BusinessObjects.LoanAppLoanInfo> getloanInfo(string loanCode);


    }
}
