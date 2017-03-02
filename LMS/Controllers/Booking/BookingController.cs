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
    public class BookingController : Controller
    {
        private IBookingSvc service;

        public BookingController()
            : this(new BookingSvc())
        {

        }

        public BookingController(IBookingSvc service)
        {
            this.service = service;
        }

        // GET: Booking
        public ActionResult Booking()
        {
            return View();
        }

        public ActionResult DirectLoanReceipt()
        {
            return View();
        }

        public ActionResult CheckVoucher()
        {
            return View();
        }

        public ActionResult CIRForm()
        {
            return View();
        }

        public ActionResult DisbursementVoucher()
        {
            return View();
        }

        public ActionResult ChangeCCI()
        {
            return View();
        }

        public ActionResult BankAssignment()
        {
            return View();
        }

        public ActionResult PromissoryNoteAllocation()
        {
            return View();
        }

        [HttpPost]
        public ActionResult RetrieveBookingRecords(int status)
        {
            return Json(this.service.getBookingRecords(status));
        }

        [HttpPost]
        public ActionResult RetrieveCheckVoucher(int status)
        {
            return Json(this.service.getCheckVoucher(status));
        }

        [HttpPost]
        public ActionResult RetrieveCIRForm(int status)
        {
            return Json(this.service.getCIRForm(status));
        }

        [HttpPost]
        public ActionResult RetrieveDisbursementVoucher(int status)
        {
            return Json(this.service.getDisbursementVoucher(status));
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