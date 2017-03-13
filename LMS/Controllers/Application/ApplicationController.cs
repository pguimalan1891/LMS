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

        [AuthorizationFilter]
        [Route("Application")]
        [Route("Application/ApplicationMainMenu")]
        public ActionResult Application()
        {
            return View("ApplicationMainMenu");
        }

        [AuthorizationFilter]
        [Route("Application/List/{status}")]
        public ActionResult ApplicationList(string status)
        {
            ViewBag.getStatus = status;
            return View("ApplicationList");
        }



        [AuthorizationFilter]
        [Route("Application/CustomerList")]
        public ActionResult BorrowerList()
        {
            return View("CustomerSearch");
        }


        [AuthorizationFilter]
        [Route("Application/LoanApplication")]
        public ActionResult wdLoanApplication(string code)
        {
            BusinessObjects.LoanApplicationModel loan = new BusinessObjects.LoanApplicationModel();

            if (code.Length > 3)
            {
                loan = new BusinessObjects.LoanApplicationModel();
                loan = service.getLoanFormDetails(code);
                loan.orgs = customerComp.getOrganization();
                loan.districts = customerComp.getDistrict();
                loan.applicationTypes = customerComp.getApplicationType();
                loan.products = service.GetLoanProducts();
                loan.sets = service.GetLoanSet();
                loan.terms = service.GetLoanTerms();
                loan.borrowerProfile = service.GetBorrowerProfile(loan.BorrowerCode);
                loan.reqDocs = service.getBorrowerRequiredDocuments(loan.BorrowerCode);
            }
           
           // loan.AccountNo = loanID;
           
            return View("LoanApplication", loan);
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
            loan.orgs = customerComp.getOrganization();
            loan.districts = customerComp.getDistrict();
            loan.applicationTypes = customerComp.getApplicationType();
            loan.products = service.GetLoanProducts();
            loan.sets = service.GetLoanSet();
            loan.terms = service.GetLoanTerms();
            loan.borrowerProfile = service.GetBorrowerProfile(borrower);
            loan.reqDocs = service.getBorrowerRequiredDocuments(borrower);
            return View("LoanApplication", loan);
       
        }


        [HttpPost]
        [AuthorizationFilter]
        [Route("Application/ListApplications/{status}/{filterKey}")]
        public ActionResult ListApplications(string status, string filterKey)
        {
            if(status == "undefined")
            {
                status = "[All]";
            }
            return Json(service.getLoanApplicationListing(status,filterKey));
        }

        [HttpPost]
        [AuthorizationFilter]
        [Route("Application/ListBorrowers/{filterKey}")]
        public ActionResult ListBorrowers(string filterKey)
        {
            return Json(service.GetBorrowers(filterKey));
        }


      

        [HttpPost]
        [AuthorizationFilter]
        [Route("Application/ListDocumentStatus")]
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