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
using AutoMapper;
using AutoMapper.Configuration;

namespace LMS.Controllers.Collections
{
    public class OfficialReceiptController : Controller
    {
        private IOfficialReceiptSvc service;
        private IDTSecurityManagerSvc DTSecurityservice;        

        public OfficialReceiptController()
            : this(new OfficialReceiptSvc(),new DTSecurityManagerSvc())
        {
        }
        public OfficialReceiptController(IOfficialReceiptSvc service,IDTSecurityManagerSvc DTSecurityservice)
        {
            this.service = service;
            this.DTSecurityservice = DTSecurityservice;
        }

        // GET: OfficialReceipt        
        [Route("OfficialReceipt")]
        public ActionResult OfficialReceipt(string Code)
        {        

            return View(FetchAccountInfo(Code));            
        }
                      
        [HttpGet]
        public ActionResult FetchDLRActiveAccounts()
        {
            List<Dictionary<string, object>> dlrAccounts = new List<Dictionary<string, object>>();
            dlrAccounts = service.getDLRActiveAccounts();
            return Json(dlrAccounts, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult getCollectionDues(string DLRNumber)
        {
            var obj = service.getCollectionDues(DLRNumber);
            return Json(obj, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public LMS.Models.OfficialReceiptModel FetchAccountInfo(string UserCode)
        {
            LMS.Models.OfficialReceiptModel OfficialReceiptModel = new LMS.Models.OfficialReceiptModel();
            UserCode = "coykie.segarino";
            Mapper.CreateMap<BusinessObjects.PaymentMode, Models.PaymentMode>();
            Mapper.CreateMap<BusinessObjects.Bank, Models.Bank>();
            Mapper.CreateMap<BusinessObjects.UserAccount, Models.DevelopmentTools.UserAccount>();
            IEnumerable<LMS.Models.PaymentMode> PaymentMode = Mapper.Map<IEnumerable<BusinessObjects.PaymentMode>,IEnumerable<Models.PaymentMode>>(service.getPaymentMode());
            IEnumerable<LMS.Models.Bank> Bank = Mapper.Map<IEnumerable<BusinessObjects.Bank>, IEnumerable<Models.Bank>>(service.getBank());
            LMS.Models.DevelopmentTools.UserAccount UserAccount = Mapper.Map<BusinessObjects.UserAccount, LMS.Models.DevelopmentTools.UserAccount>(DTSecurityservice.getUserAccountbyCode(UserCode));
            OfficialReceiptModel.PaymentMode = PaymentMode;            
            OfficialReceiptModel.Bank = Bank;
            OfficialReceiptModel.UserAccount = UserAccount;
            LMS.Models.OfficialReceipt OfficialReceipt = new Models.OfficialReceipt();
            OfficialReceipt.ORDate = service.getServerDate();
            OfficialReceipt.AmountDue = "0.00"; OfficialReceipt.AmountReceived = "0.00"; OfficialReceipt.PIPDue = "0.00"; OfficialReceipt.GIBCODue = "0.00"; OfficialReceipt.RFCDue = "0.00"; OfficialReceipt.PPD = "0.00";
            OfficialReceipt.AccelerationDiscount = "0.00"; OfficialReceipt.PenaltyWaived = "0.00"; OfficialReceipt.PromptPaymentDiscount = "0.00"; OfficialReceipt.TotalDiscount = "0.00"; OfficialReceipt.PIP = "0.00";
            OfficialReceipt.GIBCO = "0.00"; OfficialReceipt.RFC = "0.00"; OfficialReceipt.TotalRFC = "0.00";
            OfficialReceiptModel.OfficialReceipt = OfficialReceipt;
            return OfficialReceiptModel;
        }

        [HttpPost]
        public ActionResult SubmitOfficialReceipt(Models.OfficialReceiptModel ORModel)
        {
            Mapper.CreateMap<Models.OfficialReceipt, BusinessObjects.OfficialReceipt>();
            string UserCode = "coykie.segarino";            
            LMS.Models.DevelopmentTools.UserAccount UserAccount = Mapper.Map<BusinessObjects.UserAccount, LMS.Models.DevelopmentTools.UserAccount>(DTSecurityservice.getUserAccountbyCode(UserCode));
            BusinessObjects.OfficialReceipt OfficialReceiptModel = Mapper.Map<Models.OfficialReceipt, BusinessObjects.OfficialReceipt>(ORModel.OfficialReceipt);
            OfficialReceiptModel.UserID = UserAccount.ID;
            OfficialReceiptModel.OrganizationID = UserAccount.OrganizationID;
            return Content(service.SubmitOfficialReceipt(OfficialReceiptModel).ToString());            
        }
    }
}