﻿using BusinessObjects;
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


        public  BusinessObjects.LoanApplicationModel  getLoanFormDetails(string AccountNo)
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

        public string insertLoan(BusinessObjects.LoanApplicationModel loan, string userID)
        {
            return lnCtrl.insertLoan(loan, userID);
        }
    }
}
