using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace LMS.Controllers.Collections
{
    public class OfficialReceiptController : Controller
    {
        // GET: OfficialReceipt
        public ActionResult NewSundry()
        {
            Models.OfficialReceiptViewModel mdl = new Models.OfficialReceiptViewModel();
            mdl.isSundry = true;
            return View("OfficialReceipt", mdl);
            
        }

        

    }
}