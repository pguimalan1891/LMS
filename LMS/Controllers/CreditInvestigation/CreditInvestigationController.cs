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
          //  ci.loanModel.terms = loanService.GetLoanTerms(ci.loanModel.DistrictCode, ci.loanModel.ProductId, ci.loanModel.SetId);
          //  ci.loanModel.terms = loanService.GetLoanTerms();
            ci.RecommendedMLV = "0";
            ci.MonthlyInstallment = "0";
            string BorrowerCode = ci.loanModel.BorrowerCode;
            ci.borrowProf = loanService.GetBorrowers(BorrowerCode).First();
            ci.incomes = crdService.getIncome(ci.borrowProf.id);
            ci.Date = DateTime.Now.ToShortDateString();
            return View("CreditInvestigationFrm",ci);
        }

        [Route("CreditInvestigation/New")]
        public ActionResult viewListCreditInvestigation()
        {
            return View("ListOfLoanForCR");
            
        }

        [Route("CreditInvestigation/New/List")]
        public ActionResult ListCreditInvestigation()
        {

            return Json(crdService.getLoanApplicationListing());
        }



        [Route("CreditInvestigation/Save")]
        public JsonResult SaveCreditInvestigation(string income, string deduction, string net_income, string spouse_income, string spouse_deduction, string spouse_net_income, string business_income, string other_income, string total_income, string living_expenses, string rentals, string utility, string education, string amortization, string transportation, string other_expenses, string total_expenses, string gross_disposable_income, string class_amount, string net_disposable_income, string mi_result, string excess_amount, string document_status_code, string notes, string loan_code, string recommended_mlv)
        {

            //string income,string deduction,string net_income,string spouse_income,string spouse_deduction,string spouse_net_income,string business_income,string other_income,string total_income,string living_expenses,string rentals,string utility,string education,string amortization,string transportation,string other_expenses,string total_expenses,string gross_disposable_income,string class_amount,string net_disposable_income,string mi_result,string excess_amount,string document_status_code,string notes,string loan_code,string recommended_mlv,prepared_by
            List<Dictionary<string, object>> session = (List<Dictionary<string, object>>)Session["loginDetails"];

            string prepared_by = session[0]["ID"].ToString();
            
            return Json(crdService.updateCI(income, deduction, net_income, spouse_income, spouse_deduction, spouse_net_income, business_income, other_income, total_income, living_expenses, rentals, utility, education, amortization, transportation, other_expenses, total_expenses, gross_disposable_income, class_amount, net_disposable_income, mi_result, excess_amount, document_status_code, notes, loan_code, recommended_mlv, prepared_by));
        }

    }
}