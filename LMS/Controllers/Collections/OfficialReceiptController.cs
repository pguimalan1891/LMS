using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LMS.Controllers.Collections
{
    public class OfficialReceiptController : Controller
    {
        // GET: OfficialReceipt
        public ActionResult Index()
        {
            Models.OfficialReceiptViewModel mdl = new Models.OfficialReceiptViewModel();
            if (ViewBag.id != null)
            {
                mdl.OR_Number = ViewBag.id;
            }
            return View("OfficialReceipt", mdl);
            //return View("OfficialReceipt",new Models.OfficialReceiptViewModel());
        }

        public ActionResult get(string id, string test)
        {
            Models.OfficialReceiptViewModel mdl = new Models.OfficialReceiptViewModel();
            if(id != null)
            {
                mdl.OR_Number = id+"-"+test;
            }
            return View("OfficialReceipt", mdl);
            //return View("OfficialReceipt",new Models.OfficialReceiptViewModel());
        }


    }
}