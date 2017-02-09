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

        [HttpPost]
        public ActionResult RetrieveBookingRecords(int status)
        {
            return Json(this.service.getBookingRecords(status));
        }
    }
}