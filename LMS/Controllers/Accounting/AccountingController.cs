using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ServiceLayer;
using ServiceLayer.Interface;
using CommonClasses;
using BusinessObjects;

namespace LMS.Controllers.Accounting
{
    public class AccountingController : Controller
    {
        private IAccountingSvc service;

        public AccountingController()
            : this(new AccountingSvc())
        {

        }

        public AccountingController(IAccountingSvc service)
        {
            this.service = service;
        }

        public ActionResult RequestForPayment()
        {
            return View();
        }

        [HttpPost]
        public ActionResult RetrieveRequestforPayment(int status)
        {
            return Json(this.service.getRequestForPayment(status));
        }

        protected override JsonResult Json(object data, string contentType, System.Text.Encoding contentEncoding, JsonRequestBehavior behavior)
        {
            return new JsonResult()
            {
                Data = data,
                ContentType = contentType,
                ContentEncoding = contentEncoding,
                JsonRequestBehavior = behavior,
                MaxJsonLength = Int32.MaxValue
            };
        }
    }
}