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
                 
        [Route("SundryOR")]
        public ActionResult SundryOR(string Code)
        {
            return View(FetchAccountInfo(Code));
        }

        [Route("ORListing")]
        public ActionResult ORListing()
        {
            return View();
        }

        [HttpGet]
        public ActionResult FetchORListing(string status,string CustomerName)
        {
            return Json(service.getOfficialReceiptListing(status, CustomerName), JsonRequestBehavior.AllowGet);
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
            List<Dictionary<string, object>> session = (List<Dictionary<string, object>>)Session["loginDetails"];
            if (session == null)
                UserCode = "admin";
            else
                UserCode = session[0]["Code"].ToString();
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
            List<Dictionary<string, object>> session = (List<Dictionary<string, object>>)Session["loginDetails"];
            string UserCode = session[0]["Code"].ToString();
            LMS.Models.DevelopmentTools.UserAccount UserAccount = Mapper.Map<BusinessObjects.UserAccount, LMS.Models.DevelopmentTools.UserAccount>(DTSecurityservice.getUserAccountbyCode(UserCode));
            BusinessObjects.OfficialReceipt OfficialReceiptModel = Mapper.Map<Models.OfficialReceipt, BusinessObjects.OfficialReceipt>(ORModel.OfficialReceipt);
            OfficialReceiptModel.UserID = UserAccount.ID;
            OfficialReceiptModel.OrganizationID = UserAccount.OrganizationID;
            return Content(service.SubmitOfficialReceipt(OfficialReceiptModel).ToString());            
        }

        [HttpPost]
        public ActionResult SubmitSundry(Models.OfficialReceiptModel ORModel,IEnumerable<Models.Collection.Sundry> sundry)
        {
            Mapper.CreateMap<Models.OfficialReceipt, BusinessObjects.OfficialReceipt>();
            Mapper.CreateMap<Models.Collection.Sundry, BusinessObjects.Sundry>();
            List<Dictionary<string, object>> session = (List<Dictionary<string, object>>)Session["loginDetails"];
            string UserCode = session[0]["Code"].ToString();                        
            LMS.Models.DevelopmentTools.UserAccount UserAccount = Mapper.Map<BusinessObjects.UserAccount, LMS.Models.DevelopmentTools.UserAccount>(DTSecurityservice.getUserAccountbyCode(UserCode));
            BusinessObjects.OfficialReceipt OfficialReceiptModel = Mapper.Map<Models.OfficialReceipt, BusinessObjects.OfficialReceipt>(ORModel.OfficialReceipt);
            OfficialReceiptModel.UserID = UserAccount.ID;
            OfficialReceiptModel.OrganizationID = UserAccount.OrganizationID;
            IEnumerable<BusinessObjects.Sundry> sundryAccounts = Mapper.Map<IEnumerable<LMS.Models.Collection.Sundry>, IEnumerable<BusinessObjects.Sundry>>(sundry);
            return Content(service.SubmitSundry(OfficialReceiptModel, sundryAccounts).ToString());
        }

        [HttpPost]
        public ActionResult UpdateOfficialReceipt(string ORNumber,string isFinalize)
        {
            Mapper.CreateMap<Models.OfficialReceipt, BusinessObjects.OfficialReceipt>();
            List<Dictionary<string, object>> session = (List<Dictionary<string, object>>)Session["loginDetails"];            
            string UserCode = session[0]["Code"].ToString();
            BusinessObjects.UserAccount UserAccount = DTSecurityservice.getUserAccountbyCode(UserCode);
            BusinessObjects.OfficialReceipt OfficialReceiptModel = new BusinessObjects.OfficialReceipt();
            OfficialReceiptModel.UserID = UserAccount.ID;
            OfficialReceiptModel.OrganizationID = UserAccount.OrganizationID;
            OfficialReceiptModel.ORNumber = ORNumber;            
            return Content(service.UpdateOfficialReceipt(OfficialReceiptModel, isFinalize).ToString());
        }

        [HttpPost]
        public ActionResult UpdateSundry(string UpdateType,LMS.Models.Collection.Sundry sundry)
        {
            var pvr = new PartialViewResult();
            Mapper.CreateMap<BusinessObjects.CMDMAccountType, Models.Collection.CMDMAccountType>();
            LMS.Models.Collection.SundryViewModel SundryModel = new Models.Collection.SundryViewModel();
            SundryModel.CMDMAccountType = Mapper.Map<IEnumerable<BusinessObjects.CMDMAccountType>, IEnumerable<LMS.Models.Collection.CMDMAccountType>>(service.getCMDMAccountType());
            SundryModel.SundryDetails = sundry;
            if (UpdateType == "add")
            {
                pvr = PartialView("_AddSundry", SundryModel);                
            }else if(UpdateType == "edit")
            {
                pvr = PartialView("_UpdateSundry", SundryModel);
            }
            return pvr;
        }

        [HttpGet]
        [Route("ViewORNumber")]
        public ActionResult viewORNumber(string ORNumber)
        {
            Mapper.CreateMap<BusinessObjects.OfficialReceipt, LMS.Models.OfficialReceipt>();
            Mapper.CreateMap<BusinessObjects.Sundry, LMS.Models.Collection.Sundry>();
            LMS.Models.OfficialReceiptModel ORReceiptModel = new Models.OfficialReceiptModel();
            ORReceiptModel.OfficialReceipt = Mapper.Map<BusinessObjects.OfficialReceipt, LMS.Models.OfficialReceipt>(service.getOfficialReceipt(ORNumber));
            ORReceiptModel.Sundry = Mapper.Map<IEnumerable<BusinessObjects.Sundry>, IEnumerable<LMS.Models.Collection.Sundry>>(service.getSundry(ORNumber));
            var pvr = new PartialViewResult();
            if (ORReceiptModel.OfficialReceipt.OfficialReceiptType == "1")
            {
                pvr = PartialView("_ViewOfficialReceipt", ORReceiptModel);
            }
            else if (ORReceiptModel.OfficialReceipt.OfficialReceiptType == "2")
            {
                pvr = PartialView("_ViewSundry", ORReceiptModel);
            }
            return pvr;
        }
    }
}