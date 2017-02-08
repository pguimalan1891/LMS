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
            return View("OfficialReceipt", mdl);
            
        }

        


    }
}