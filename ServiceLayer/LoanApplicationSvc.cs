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


        public IEnumerable<BusinessObjects.newLoanBorrowerProfile> GetBorrowers(string searchkey)

        {
            return lnCtrl.GetBorrowers(searchkey);   
        }

        public IEnumerable<BusinessObjects.RequiredDocuments> getBorrowerRequiredDocuments(string borrowerCode)
        {
            return lnCtrl.getBorrowerRequiredDocuments(borrowerCode);
        }

        public IEnumerable<BusinessObjects.LoanType> GetLoanProducts()
        {
            return lnCtrl.getProducts();
        }

        public IEnumerable<BusinessObjects.LoanSet> GetLoanSet(string groupid, string loantype)
        {
            return lnCtrl.getLoanSet(groupid, loantype);
        }

        public IEnumerable<BusinessObjects.LoanTermsForLoanApplication> GetLoanTerms(string groupid, string loantype, string loanset)
        {
            return lnCtrl.getLoanTerms(groupid,loantype,loanset);

        }
        public BusinessObjects.BorrowerProfile GetBorrowerProfile(string code)
        {
            IEnumerable<BusinessObjects.BorrowerProfile> list = lnCtrl.getBorrowerProfile(code);
            return list.First();
        }

        public IEnumerable<BusinessObjects.Agent> GetAgents()
        {
             IEnumerable<BusinessObjects.Agent> list = lnCtrl.getAgent();
            return list;
        }


        public BusinessObjects.LoanApplicationModel  getLoanFormDetails(string AccountNo)
        {
            IEnumerable<BusinessObjects.LoanApplicationModel> list = lnCtrl.getLoanFormDetails(AccountNo);
            return list.First();
        }
        public IEnumerable<BusinessObjects.LoanList> getLoanApplicationListing(string status, string searchkey)
        {
            return lnCtrl.getLoanApplicationListing(status, searchkey);
        }

        public IEnumerable<BusinessObjects.DocumentStatus> GetDocumentStatus()
        {
            return lnCtrl.getDocumentStatus();
        }

        public IEnumerable<BusinessObjects.ComakerProfile> getComakers(string LoanCode)
        {
            return lnCtrl.getComakers(LoanCode);
        }

        public IEnumerable<BusinessObjects.CollateralProfile> getCollaterals(string loanCode)
        {
            return lnCtrl.getCollaterals(loanCode);
        }

        public IEnumerable<BusinessObjects.CollateralType> GetCollateralType()
        {
            return lnCtrl.getCollateralType();

        }

        public IEnumerable<BusinessObjects.CollateralUsage> GetCollateralUsage()
        {
            return lnCtrl.getCollateralUsage();
        }
        public IEnumerable<BusinessObjects.Color> GetColor()
        {
            return lnCtrl.getColor();
        }

        public IEnumerable<BusinessObjects.FuelType> GetFuelType()
        {
            return lnCtrl.getFuelType();
        }

        public string insertLoan(string AccountNo, string organizationid, string notes, string borrowerid, string loantype, string loanset, string loanterms, string ppd_rate_id, string handling_fee_id, string agent_incentive_type, string dealer_incentive_type, string loanamount, string userID, string loanpurpose, string addOnRate, string agent, string district, string assured)
        {
            return lnCtrl.insertLoan(AccountNo, organizationid, notes, borrowerid, loantype, loanset, loanterms, ppd_rate_id, handling_fee_id, agent_incentive_type, dealer_incentive_type, loanamount, userID, loanpurpose, addOnRate, agent, district, assured);
        }
        public IEnumerable<BusinessObjects.DocumentStatus> getHandlingFee()
        {
            return lnCtrl.getHandlingFee();
        }

        public IEnumerable<BusinessObjects.DocumentStatus> getPPDAmounts(string loantype)
        {
           return lnCtrl.getPPDAmounts(loantype);
        }

        public IEnumerable<BusinessObjects.DocumentStatus> getAgentIncentives(string loantype)
        {
            return lnCtrl.getAgentIncentives(loantype);
        }

        public IEnumerable<BusinessObjects.DocumentStatus> getDealerIncentives(string loantype)
        {
            return lnCtrl.getDealerIncentives(loantype);
        }

        public BusinessObjects.LoanAppLoanInfo getloanInfo(string loanCode)
        {
            return lnCtrl.getloanInfo(loanCode).First();
        }
        public BusinessObjects.LoanAppCreditInfo getloanCreditInfo(string loanCode)
        {
            return lnCtrl.getloanCreditInfo(loanCode).First();
        }


        public bool cancelLoanApplication(string loanCode)
        {
            return lnCtrl.cancelLoanApplication(loanCode);
        }

    }
}
