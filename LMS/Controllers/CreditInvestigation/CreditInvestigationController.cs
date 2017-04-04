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
        // GET: CreditInvestigation
        [Route("CreditInvestigation")]
        public ActionResult Index()
        {
            return View();
        }

        [Route("CreditInvestigation/New")]
        public JsonResult NewCreditInvestigation(string LoanApplicationNo)
        {
            BusinessObjects.CreditInvestigation ci = new BusinessObjects.CreditInvestigation();
            ci.CINumber = LoanApplicationNo.Replace("LA-","");
            ci.Date = DateTime.Now.ToShortDateString();
            return Json(ci);
        }

        [Route("CreditInvestigation/Save")]
        public JsonResult SaveCreditInvestigation(string ci)
        {
            JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
            BusinessObjects.CreditInvestigation objCustomer = jsonSerializer.Deserialize<BusinessObjects.CreditInvestigation>(ci);

            string a = "b";
            return Json(null);
        }

    }
}