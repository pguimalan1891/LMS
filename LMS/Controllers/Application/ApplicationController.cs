using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ServiceLayer;
using ServiceLayer.Interface;
using LMS.Models.LoanApplication;

namespace LMS.Controllers
{
    public class ApplicationController : Controller
    {
        private ILoanApplicationSvc service;
        private ICustomerSvc customerComp;
        private BusinessObjects.getComponents allComponenents;
       
        // GET: Application

        public ApplicationController()
            :this(new LoanApplicationSvc(), new CustomerSvc())
        {

        }

        public ApplicationController(ILoanApplicationSvc loanApplicationSvc, ICustomerSvc customerSvc)
        {
            this.service = loanApplicationSvc;
            this.customerComp = customerSvc;
        }

        [Route("Application")]
        public ActionResult Application()
        {
            return View("ApplicationList");
        }

        [Route("LoanApplication")]
        
        [HttpGet]
        public ActionResult LoanApplication(string code)
        {
            LoanApplicationModel loan = new LoanApplicationModel();
            loan.AccountNo = code;
            loan.orgs = customerComp.getOrganization();
            loan.districts = customerComp.getDistrict();
            loan.applicationTypes = customerComp.getApplicationType();
            loan.products = service.GetLoanProducts();
            loan.sets = service.GetLoanSet();
            loan.terms = service.GetLoanTerms();
            
            return View(loan);
        }

        [HttpPost]
        public ActionResult getComakers(string loanApplicationNo)
        {
            return PartialView("_LoanComakers", null);
        }

        [HttpPost]
        public ActionResult getCollaterals(string loanApplicationNo)
        {
            return PartialView("_LoanCollaterals", null);
        }

        public ActionResult NewLoanApplication(string borrower)
        {
            LoanApplicationModel loan = new LoanApplicationModel();
            loan.BorrowerCode = borrower;
            loan.borrowerProfile = service.GetBorrowerProfile(borrower);
            loan.orgs = customerComp.getOrganization();
            loan.districts = customerComp.getDistrict();
            loan.applicationTypes = customerComp.getApplicationType();
            loan.products = service.GetLoanProducts();
            loan.sets = service.GetLoanSet();
            loan.terms = service.GetLoanTerms();
            return View("LoanApplication", loan);
       
        }


        [HttpPost]
        public ActionResult ListApplications()
        {
            return Json(service.GetLoanApplicationListing());
        }


        [HttpPost]
        public ActionResult ListDocumentStatus()
        {
            return Json(service.GetDocumentStatus());
        }



        [HttpPost]
        public ActionResult getBorrowers(string searchkey)
        {
            return Json(this.service.GetBorrowers(searchkey), JsonRequestBehavior.DenyGet);
        }


        
        //getDistrict, getBranch, getApplicationType, getLoanProduct, getSetPerProduct, getTerms, getCollateralType
    }
}