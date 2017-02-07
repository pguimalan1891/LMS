using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LMS.Controllers
{
    public class CollectionsController : Controller
    {
        // GET: Collections
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult vw_OfficialReceipt_forSubmission()
        {
            return View("OfficialReceipt_ForSubmission",null);
        }

        public ActionResult vw_OfficialReceipt_postStatus()
        {
            return View("OfficialReceipt_ForSubmission", null);
        }

    }
}