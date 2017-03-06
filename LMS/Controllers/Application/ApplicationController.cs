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
        // GET: Application
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult LoanApplication(LoanApplicationModel loan)
        {

            return View();
        }

        [HttpPost]
        public ActionResult getBorrowers(string searchkey)
        {
            return Json(this.service.GetBorrowers(searchkey), JsonRequestBehavior.DenyGet);
        }

        //getDistrict, getBranch, getApplicationType, getLoanProduct, getSetPerProduct, getTerms, getCollateralType
    }
}