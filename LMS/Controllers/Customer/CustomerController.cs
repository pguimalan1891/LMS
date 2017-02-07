using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Configuration;
using System.Net;
using ServiceLayer;
using ServiceLayer.Interface;
using CommonClasses;
using BusinessObjects;


namespace LMS.Controllers
{
    public class CustomerController : Controller
    {
        private ICustomerSvc service;

        // GET: Customer
        public CustomerController()
            : this(new CustomerSvc())
        {
        }
        public CustomerController(ICustomerSvc service)
        {
            this.service = service;
        }

        public ActionResult Customer()
        {
            return View();
        }

        [HttpGet]
        public ActionResult FetchCustomerRecord()
        {
            return Json(this.service.getCustomerRecord(), JsonRequestBehavior.AllowGet);
        }
    }
}