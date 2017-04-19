using ServiceLayer;
using ServiceLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace LMS.Controllers
{
    public class CreditInvestigationController : Controller
    {
        private ILoanApplicationSvc loanService;
        private ICustomerSvc customerService;
        private ICreditInvestigation crdService;

        // GET: Application

        public CreditInvestigationController()
            :this(new LoanApplicationSvc(), new CustomerSvc(), new CreditInvestigationSvc())
        {

        }

        public CreditInvestigationController(ILoanApplicationSvc loanApplicationSvc, ICustomerSvc customerSvc, ICreditInvestigation crdService)
        {
            this.loanService = loanApplicationSvc;
            this.customerService = customerSvc;
            this.crdService = crdService;
        }

        // GET: CreditInvestigation
        [Route("CreditInvestigation")]
        public ActionResult Index()
        {
            return View();
        }

        [Route("CreditInvestigation/New/{LoanApplicationNo}")]
        public ActionResult NewCreditInvestigation(string LoanApplicationNo)
        {
            BusinessObjects.CreditInvestigation ci = new BusinessObjects.CreditInvestigation();
            ci = crdService.getCRForm(LoanApplicationNo.Replace("LA-", ""));
            ci.CINumber = LoanApplicationNo.Replace("LA-","");

            ci.loanModel = loanService.getLoanFormDetails(LoanApplicationNo);
            ci.loanModel.products = loanService.GetLoanProducts();
            ci.loanModel.terms = loanService.GetLoanTerms();
            ci.RecommendedMLV = "0";
            ci.MonthlyInstallment = "0";
            string BorrowerCode = ci.loanModel.BorrowerCode;
            ci.borrowProf = loanService.GetBorrowers(BorrowerCode).First();
            ci.incomes = crdService.getIncome(ci.borrowProf.id);
            ci.Date = DateTime.Now.ToShortDateString();
            return View("CreditInvestigationFrm",ci);
        }

        [Route("CreditInvestigation/Save")]
        public JsonResult SaveCreditInvestigation(string ci)
        {
            JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
            BusinessObjects.CreditInvestigation objCustomer = jsonSerializer.Deserialize<BusinessObjects.CreditInvestigation>(ci);

            string a = "b";
            return Json(null);
        }

    }
}