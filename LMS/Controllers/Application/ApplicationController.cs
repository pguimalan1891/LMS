using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ServiceLayer;
using ServiceLayer.Interface;
using LMS.Models.LoanApplication;
using System.Web.Script.Serialization;
using System.Reflection;

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
            //    loan.sets = service.GetLoanSet();
             //   loan.terms = service.GetLoanTerms();
                loan.borrowerProfile = service.GetBorrowerProfile(loan.BorrowerCode);
                loan.reqDocs = service.getBorrowerRequiredDocuments(loan.BorrowerCode);
            }
           
           // loan.AccountNo = loanID;
           
            return View("LoanApplication", loan);
        }

        [AuthorizationFilter]
        [HttpPost]
        public ActionResult getComakers(string loanApplicationNo)
        {
            return PartialView("_LoanComakers", null);
        }

        [AuthorizationFilter]
        [HttpPost]
        public ActionResult getCollaterals(string loanApplicationNo)
        {
            return PartialView("_LoanCollaterals", null);
        }

    
        [AuthorizationFilter]
        public ActionResult NewLoanApplication(string borrower)
        {
            LoanApplicationModel loan = new LoanApplicationModel();
            loan.AccountNo = "LA-" + DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Second.ToString();
            loan.TransactionDate = DateTime.Now;
            loan.orgs = customerComp.getOrganization();
            loan.districts = customerComp.getDistrict();
            loan.applicationTypes = customerComp.getApplicationType();
            loan.products = service.GetLoanProducts();
            loan.DesiredMLV = "0.00";
            
          //  loan.sets = service.GetLoanSet();
          //  loan.terms = service.GetLoanTerms();
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
        [Route("Application/LoanSet/{groupid}/{loantype}")]
        public ActionResult LoanSet(string groupid, string loantype)
        {
            return Json(service.GetLoanSet(groupid, loantype));
        }

        [HttpPost]
        [AuthorizationFilter]
        [Route("Application/LoanTerms/{groupid}/{loantype}/{loanset}")]
        public ActionResult LoanTerms(string groupid, string loantype, string loanset)
        {
            return Json(service.GetLoanTerms(groupid, loantype, loanset));
        }

        //[HttpPost]
        //[AuthorizationFilter]
        //[Route("Application/HandlingFee")]
        //public ActionResult HandlingFee()
        //{
        //    return Json();
        //}



        [HttpPost]
        [AuthorizationFilter]
        [Route("Application/ListAgent")]
        public ActionResult ListAgents()
        {
            return Json(service.GetAgents());
        }





        [HttpPost]
        [AuthorizationFilter]
        [Route("Application/ListDocumentStatus")]
        public ActionResult ListDocumentStatus()
        {
            return Json(service.GetDocumentStatus());
        }

        [HttpPost]
        [AuthorizationFilter]
        [Route("Application/ListCollateralUsage")]
        public ActionResult ListCollateralUsage()
        {
            return Json(service.GetCollateralUsage());
        }


        [HttpPost]
        [AuthorizationFilter]
        [Route("Application/ListFuelType")]
        public ActionResult ListFuelType()
        {
            return Json(service.GetFuelType());
        }

        [HttpPost]
        [AuthorizationFilter]
        [Route("Application/ListColor")]
        public ActionResult ListColor()
        {
            return Json(service.GetColor());
        }


        [HttpPost]
        [AuthorizationFilter]
        [Route("Application/ListCollateralType")]
        public ActionResult ListCollateralType()
        {
            return Json(service.GetCollateralType());
        }



        [HttpPost]
        [AuthorizationFilter]
        [Route("Application/ListComakers")]
        [Route("Application/ListComakers/{loanCode}")]
        public ActionResult ListComakers(string loanCode)
        {
            return Json(service.getComakers(loanCode));
        }

        [HttpPost]
        [AuthorizationFilter]
        [Route("Application/ListCollaterals")]
        [Route("Application/ListCollaterals/{loanCode}")]
        public ActionResult ListCollaterals(string loanCode)
        {
            return Json(service.getCollaterals(loanCode));
        }

       

        [HttpPost]
        public ActionResult InsertNewLoan(string AccountNo, string organizationid, string notes, string borrowerid, string loantype, string loanset, string loanterms, string ppd_rate_id, string handling_fee_id, string agent_incentive_type, string dealer_incentive_type, string loanamount, string userID, string loanpurpose)
        {
              List<Dictionary<string, object>> session = (List<Dictionary<string, object>>)Session["loginDetails"];
            
            string user = session[0]["ID"].ToString();
            return Json(service.insertLoan(AccountNo, organizationid, notes, borrowerid, loantype, loanset, loanterms, ppd_rate_id, handling_fee_id, agent_incentive_type, dealer_incentive_type, loanamount, user, loanpurpose));
        }

        //getDistrict, getBranch, getApplicationType, getLoanProduct, getSetPerProduct, getTerms, getCollateralType
    }
}
