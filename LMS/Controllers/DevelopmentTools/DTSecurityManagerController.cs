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

namespace LMS.Controllers.DevelopmentTools
{    
    public class DTSecurityManagerController : Controller
    {
        private IDTSecurityManagerSvc service;
        private ICustomerSvc customerSvc;
        // GET: DTSecurityManager
        public DTSecurityManagerController()
            : this(new DTSecurityManagerSvc(),new CustomerSvc())
        {
        }
        public DTSecurityManagerController(IDTSecurityManagerSvc service,ICustomerSvc customerSvc)
        {
            this.service = service;
            this.customerSvc = customerSvc;
        }

        [Route("SecurityManager")]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult FetchUserAccounts()
        {
            return Json(service.getUserAccounts(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult Update_UserAccount(string ID)
        {
            LMS.Models.DevelopmentTools.UserAccountModel userAccountModel = new Models.DevelopmentTools.UserAccountModel();
            Mapper.CreateMap<BusinessObjects.UserAccountStatus, Models.DevelopmentTools.UserAccountStatus>();
            Mapper.CreateMap<BusinessObjects.Position, Models.DevelopmentTools.Position>();
            Mapper.CreateMap<BusinessObjects.Organization, Models.Customer.Organization>();
            Mapper.CreateMap<BusinessObjects.Company, Models.DevelopmentTools.Company>();
            Mapper.CreateMap<BusinessObjects.UserAccount, Models.DevelopmentTools.UserAccount>();

            IEnumerable<Models.DevelopmentTools.UserAccountStatus> userAccountStatus = Mapper.Map<IEnumerable<BusinessObjects.UserAccountStatus>, IEnumerable<Models.DevelopmentTools.UserAccountStatus>>(service.getUserAccountStatus());
            IEnumerable<Models.DevelopmentTools.Position> Position = Mapper.Map<IEnumerable<BusinessObjects.Position>, IEnumerable<Models.DevelopmentTools.Position>>(service.getPosition());
            IEnumerable<Models.DevelopmentTools.Company> Company = Mapper.Map<IEnumerable<BusinessObjects.Company>, IEnumerable<Models.DevelopmentTools.Company>>(service.getListCompany());
            IEnumerable<Models.Customer.Organization> Organization = Mapper.Map<IEnumerable<BusinessObjects.Organization>, IEnumerable<Models.Customer.Organization>>(customerSvc.getOrganization());
            Models.DevelopmentTools.UserAccount userAccount = Mapper.Map<BusinessObjects.UserAccount, Models.DevelopmentTools.UserAccount>(service.getUserAccountbyID(ID));

            userAccountModel.userAccount = userAccount;
            userAccountModel.userAccountStatus = userAccountStatus;
            userAccountModel.Position = Position;
            userAccountModel.Organization = Organization;
            userAccountModel.PasswordNeverExpires = setPasswordNeverExpire();
            userAccountModel.Company = Company;
            var pvr = new PartialViewResult();
            pvr = PartialView("_Update_UserAccount", userAccountModel);
            return pvr;
        }

        public ActionResult ResetPassword_UserAccount(string ID)
        {
            LMS.Models.DevelopmentTools.UserAccountModel userAccountModel = new Models.DevelopmentTools.UserAccountModel();
            Mapper.CreateMap<BusinessObjects.UserAccount, Models.DevelopmentTools.UserAccount>();
            Models.DevelopmentTools.UserAccount userAccount = Mapper.Map<BusinessObjects.UserAccount, Models.DevelopmentTools.UserAccount>(service.getUserAccountbyID(ID));
            userAccountModel.userAccount = userAccount;            
            var pvr = new PartialViewResult();
            pvr = PartialView("_ResetPassword_UserAccount", userAccountModel);
            return pvr;
        }

        public ActionResult UpdateStatus_UserAccount(string ID)
        {
            LMS.Models.DevelopmentTools.UserAccountModel userAccountModel = new Models.DevelopmentTools.UserAccountModel();
            Mapper.CreateMap<BusinessObjects.UserAccount, Models.DevelopmentTools.UserAccount>();
            Models.DevelopmentTools.UserAccount userAccount = Mapper.Map<BusinessObjects.UserAccount, Models.DevelopmentTools.UserAccount>(service.getUserAccountbyID(ID));
            userAccountModel.userAccount = userAccount;
            var pvr = new PartialViewResult();
            pvr = PartialView("_UpdateStatus_UserAccount", userAccountModel);
            return pvr;
        }


        public ActionResult Add_UserAccount()
        {
            LMS.Models.DevelopmentTools.UserAccountModel userAccountModel = new Models.DevelopmentTools.UserAccountModel();
            Mapper.CreateMap<BusinessObjects.UserAccountStatus, Models.DevelopmentTools.UserAccountStatus>();
            Mapper.CreateMap<BusinessObjects.Position, Models.DevelopmentTools.Position>();
            Mapper.CreateMap<BusinessObjects.Organization, Models.Customer.Organization>();
            Mapper.CreateMap<BusinessObjects.Company, Models.DevelopmentTools.Company>();

            IEnumerable<Models.DevelopmentTools.UserAccountStatus> userAccountStatus = Mapper.Map<IEnumerable<BusinessObjects.UserAccountStatus>, IEnumerable<Models.DevelopmentTools.UserAccountStatus>>(service.getUserAccountStatus());
            IEnumerable<Models.DevelopmentTools.Position> Position = Mapper.Map<IEnumerable<BusinessObjects.Position>, IEnumerable<Models.DevelopmentTools.Position>>(service.getPosition());
            IEnumerable<Models.DevelopmentTools.Company> Company = Mapper.Map<IEnumerable<BusinessObjects.Company>, IEnumerable<Models.DevelopmentTools.Company>>(service.getListCompany());
            IEnumerable<Models.Customer.Organization> Organization = Mapper.Map<IEnumerable<BusinessObjects.Organization>, IEnumerable<Models.Customer.Organization>>(customerSvc.getOrganization());
            LMS.Models.DevelopmentTools.UserAccount userAccount = new Models.DevelopmentTools.UserAccount();
            userAccount.ID = Guid.NewGuid().ToString();
            userAccountModel.userAccount = userAccount;
            userAccountModel.userAccountStatus = userAccountStatus;
            userAccountModel.Position = Position;
            userAccountModel.Organization = Organization;
            userAccountModel.PasswordNeverExpires = setPasswordNeverExpire();
            userAccountModel.Company = Company;            
            var pvr = new PartialViewResult();
            pvr = PartialView("_Add_UserAccount", userAccountModel);
            return pvr;
        }

        [HttpPost]
        public ActionResult AddUserAccount(Models.DevelopmentTools.UserAccountModel userAccountModel)
        {
            Mapper.CreateMap<Models.DevelopmentTools.UserAccount, BusinessObjects.UserAccount>();
            BusinessObjects.UserAccount userAccount = Mapper.Map<Models.DevelopmentTools.UserAccount, BusinessObjects.UserAccount>(userAccountModel.userAccount);
            userAccount.Code = userAccount.Code.ToUpper();
            return Content(service.UpdateUserAccounts("Add", userAccount).ToString());
        }

        [HttpPost]
        public ActionResult ResetPasswordUserAccount(Models.DevelopmentTools.UserAccountModel userAccountModel)
        {
            Mapper.CreateMap<Models.DevelopmentTools.UserAccount, BusinessObjects.UserAccount>();
            BusinessObjects.UserAccount userAccount = Mapper.Map<Models.DevelopmentTools.UserAccount, BusinessObjects.UserAccount>(userAccountModel.userAccount);
            userAccount.Code = userAccount.Code.ToUpper();
            return Content(service.ResetPasswordUserAccount(userAccount).ToString());
        }

        [HttpPost]
        public ActionResult UpdateStatusUserAccount(string ID,string Status)
        {
            return Content(service.UpdateStatusUserAccount(ID, Status).ToString());
        }

        [HttpPost]
        public ActionResult UpdateUserAccount(Models.DevelopmentTools.UserAccountModel userAccountModel)
        {
            Mapper.CreateMap<Models.DevelopmentTools.UserAccount, BusinessObjects.UserAccount>();
            BusinessObjects.UserAccount userAccount = Mapper.Map<Models.DevelopmentTools.UserAccount, BusinessObjects.UserAccount>(userAccountModel.userAccount);
            return Content(service.UpdateUserAccounts("Update", userAccount).ToString());
        }

        public List<Models.DevelopmentTools.PasswordNeverExpiresYesNo> setPasswordNeverExpire()
        {
            List<Models.DevelopmentTools.PasswordNeverExpiresYesNo> PNE = new List<Models.DevelopmentTools.PasswordNeverExpiresYesNo> {
                new Models.DevelopmentTools.PasswordNeverExpiresYesNo { PasswordNeverExpires = "N", text = "NO" },
                new Models.DevelopmentTools.PasswordNeverExpiresYesNo { PasswordNeverExpires = "Y", text = "YES" }
            };
            return PNE;
        }

        public ActionResult UserRoles(string ID)
        {            
            Mapper.CreateMap<BusinessObjects.UserAccount, Models.DevelopmentTools.UserAccount>();
            Models.DevelopmentTools.UserAccount userAccount = Mapper.Map<BusinessObjects.UserAccount, Models.DevelopmentTools.UserAccount>(service.getUserAccountbyID(ID));            

            Models.DevelopmentTools.UserRoleModel RoleModel = new Models.DevelopmentTools.UserRoleModel();
            RoleModel.userAccount = userAccount;
            var pvr = new PartialViewResult();
            pvr = PartialView("_UpdateRoles_UserAccount", RoleModel);
            return pvr;            
        }

        public ActionResult getUserRoles(string ID)
        {
            Mapper.CreateMap<BusinessObjects.Roles, Models.DevelopmentTools.Roles>();
            Models.DevelopmentTools.UserRoleModel RoleModel = new Models.DevelopmentTools.UserRoleModel();
            RoleModel.GrantedRoles = Mapper.Map<IEnumerable<BusinessObjects.Roles>, IEnumerable<Models.DevelopmentTools.Roles>>(service.getGrantedRoles(ID));
            RoleModel.NotGrantedRoles = Mapper.Map<IEnumerable<BusinessObjects.Roles>, IEnumerable<Models.DevelopmentTools.Roles>>(service.getNotGrantedRoles(ID));
            return Json(RoleModel, JsonRequestBehavior.AllowGet);
        }

        public ActionResult UpdateUserRoles(List<LMS.Models.DevelopmentTools.Roles> userRoles, string UserAccountID)
        {
            Mapper.CreateMap<Models.DevelopmentTools.Roles, BusinessObjects.Roles>();
            List<BusinessObjects.Roles> uRoles = Mapper.Map<List<Models.DevelopmentTools.Roles>, List<BusinessObjects.Roles>>(userRoles);
            return Content(service.updateUserRoles(uRoles, UserAccountID).ToString());
        }
    }
}