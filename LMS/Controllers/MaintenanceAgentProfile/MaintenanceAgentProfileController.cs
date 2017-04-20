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
using LMS.Models.MaintenanceAgentProfile;
using AutoMapper;
using AutoMapper.Configuration;

namespace LMS.Controllers.MaintenanceAgentProfile
{
    public class MaintenanceAgentProfileController : Controller
    {
        private IMaintenanceAgentProfileSvc service;
        private ICustomerSvc customerSvc;
        private BusinessObjects.getComponents allComponents;

        public MaintenanceAgentProfileController()
            : this(new MaintenanceAgentProfileSvc(), new CustomerSvc())
        {
        }
        public MaintenanceAgentProfileController(IMaintenanceAgentProfileSvc service,ICustomerSvc customerSvc)
        {
            this.service = service;
            this.customerSvc = customerSvc;
        }

        // GET: MaintenanceAgentProfile
        [Route("AgentProfile")]
        public ActionResult Index(string ID)
        {
            if (ID == null)
                ID = "";
            string Code;
            Code = service.getAgentCodebyID(ID);
            LMS.Models.MaintenanceAgentProfile.AgentProfile AgentProfile = new Models.MaintenanceAgentProfile.AgentProfile();
            if (Code == null)
                Code = "";
            AgentProfile.AGENTCode = Code;
            return View(AgentProfile);
        }

        [HttpGet]
        public ActionResult getAgentProfileList()
        {
            return Json(service.getAgentProfileList(), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult getGUID()
        {
            return Content(System.Guid.NewGuid().ToString());
        }

        public ActionResult FetchAgentProfileByCode(string Code)
        {
            Models.MaintenanceAgentProfile.AgentModel AgentModel = new Models.MaintenanceAgentProfile.AgentModel();
            AgentModel.AgentProfile = setAgentProfile(service.getAgentProfilebyCode(Code));
            var ID = AgentModel.AgentProfile.ID;
            AgentModel.AgentAddress = setAgentAddress(service.getAgentAddressByID(ID));
            var pvr = new PartialViewResult();
            pvr = PartialView("_Display_AgentProfile", AgentModel);
            return pvr;
        }

        [HttpGet]
        [Route("AgentProfileUpdate")]
        public ActionResult Update_AgentProfile(string Code)
        {
            Models.MaintenanceAgentProfile.AgentModel AgentModel = new Models.MaintenanceAgentProfile.AgentModel();
            AgentModel.AgentProfile = setAgentProfile(service.getAgentProfilebyCode(Code));
            var ID = AgentModel.AgentProfile.ID;
            AgentModel.AgentAddress = setAgentAddress(service.getAgentAddressByID(ID));
            AgentModel.Gender = setGender(customerSvc.getGender());
            AgentModel.CivilStatus = setCivilStatus(customerSvc.getCivilStatus());
            AgentModel.District = setDistrict(customerSvc.getDistrict());
            AgentModel.Organization = setOrganization(customerSvc.getOrganization());
            AgentModel.Province = setProvince(customerSvc.getProvince());
            AgentModel.City = setCity(service.getCity(ID));
            AgentModel.AgentType = setAgentType(service.getAgentType());
            AgentModel.HomeOwnership = setHomeOwnership(customerSvc.getHomeOwnership());
            AgentModel.WithCashCard = setWithCashCard();
            return View(AgentModel);
        }       

        [HttpGet]
        [Route("AgentProfileAdd")]
        public ActionResult Add_AgentProfile(string Code)
        {
            Models.MaintenanceAgentProfile.AgentModel AgentModel = new Models.MaintenanceAgentProfile.AgentModel();
            Models.MaintenanceAgentProfile.AgentProfile AgentProfile = new Models.MaintenanceAgentProfile.AgentProfile();
            List<Models.MaintenanceAgentProfile.AgentAddress> LAgentAddress = new List<Models.MaintenanceAgentProfile.AgentAddress>();
            Models.MaintenanceAgentProfile.AgentAddress AgentAddress = new Models.MaintenanceAgentProfile.AgentAddress();
            LAgentAddress.Add(AgentAddress);
            AgentModel.AgentProfile = AgentProfile;
            AgentModel.AgentAddress = LAgentAddress;
            var ID = System.Guid.NewGuid().ToString();
            AgentModel.AgentProfile.ID = ID;
            AgentModel.AgentAddress[0].ID = System.Guid.NewGuid().ToString();
            AgentModel.AgentAddress[0].AgentProfileID = ID;
            AgentModel.AgentAddress[0].AddressTypeID = "0";            
            AgentModel.Gender = setGender(customerSvc.getGender());
            AgentModel.CivilStatus = setCivilStatus(customerSvc.getCivilStatus());
            AgentModel.District = setDistrict(customerSvc.getDistrict());
            AgentModel.Organization = setOrganization(customerSvc.getOrganization());
            AgentModel.Province = setProvince(customerSvc.getProvince());
            AgentModel.City = setCity(service.getCity(ID));
            AgentModel.AgentType = setAgentType(service.getAgentType());
            AgentModel.HomeOwnership = setHomeOwnership(customerSvc.getHomeOwnership());
            AgentModel.WithCashCard = setWithCashCard();
            return View(AgentModel);
        }
        public List<Models.MaintenanceAgentProfile.WithCashCardYesNo> setWithCashCard()
        {
            List<Models.MaintenanceAgentProfile.WithCashCardYesNo> WCC = new List<Models.MaintenanceAgentProfile.WithCashCardYesNo> {
                new Models.MaintenanceAgentProfile.WithCashCardYesNo { WithCashCard = "N", text = "NO" },
                new Models.MaintenanceAgentProfile.WithCashCardYesNo { WithCashCard = "Y", text = "YES" }
            };
            return WCC;
        }

        [HttpPost]
        public ActionResult GetAgentFullData(string ID, string Type)
        {
            Models.MaintenanceAgentProfile.AgentModel AgentModel = new Models.MaintenanceAgentProfile.AgentModel();                        
            if (Type == "Address")
                AgentModel.AgentAddress = setAgentAddress(service.getAgentAddressByID(ID));            
            return Json(AgentModel, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult UpdateAgentData(Models.MaintenanceAgentProfile.AgentModel AgentProfileModel, List<Models.MaintenanceAgentProfile.AgentAddress> AgentAddress, string AgentProfileID)
        {
            if (AgentAddress == null)
                AgentAddress = new List<Models.MaintenanceAgentProfile.AgentAddress>();
            foreach (Models.MaintenanceAgentProfile.AgentAddress address in AgentAddress)
            {
                AgentProfileModel.AgentAddress.Add(address);
            }
            Mapper.CreateMap<Models.MaintenanceAgentProfile.AgentAddress, BusinessObjects.AgentAddress>();
            Mapper.CreateMap<Models.MaintenanceAgentProfile.AgentProfile, BusinessObjects.AgentProfile>();
            Mapper.CreateMap<Models.MaintenanceAgentProfile.AgentModel, BusinessObjects.AgentModel>();
            BusinessObjects.AgentModel aModel = Mapper.Map<Models.MaintenanceAgentProfile.AgentModel, BusinessObjects.AgentModel>(AgentProfileModel);
            return Content(service.UpdateAgentData("Update", aModel, AgentProfileID).ToString());
        }

        [HttpPost]
        public ActionResult AddAgentData(Models.MaintenanceAgentProfile.AgentModel AgentProfileModel, List<Models.MaintenanceAgentProfile.AgentAddress> AgentAddress, string AgentProfileID)
        {
            if (AgentAddress == null)
                AgentAddress = new List<Models.MaintenanceAgentProfile.AgentAddress>();
            foreach (Models.MaintenanceAgentProfile.AgentAddress address in AgentAddress)
            {
                AgentProfileModel.AgentAddress.Add(address);
            }
            Mapper.CreateMap<Models.MaintenanceAgentProfile.AgentAddress, BusinessObjects.AgentAddress>();
            Mapper.CreateMap<Models.MaintenanceAgentProfile.AgentProfile, BusinessObjects.AgentProfile>();
            Mapper.CreateMap<Models.MaintenanceAgentProfile.AgentModel, BusinessObjects.AgentModel>();
            BusinessObjects.AgentModel aModel = Mapper.Map<Models.MaintenanceAgentProfile.AgentModel, BusinessObjects.AgentModel>(AgentProfileModel);
            List<Dictionary<string, object>> session = (List<Dictionary<string, object>>)Session["loginDetails"];
            string UserID = session[0]["ID"].ToString();
            aModel.AgentProfile.PreparedByID = UserID;
            aModel.AgentProfile.DocumentStatusCode = "7";
            aModel.AgentProfile.Permission = "65541";
            return Content(service.UpdateAgentData("Add", aModel, AgentProfileID).ToString());
        }

        [HttpPost]
        public ActionResult UpdateAddress(string UpdateType, Models.MaintenanceAgentProfile.AgentAddress AgentAddress)
        {
            Models.MaintenanceAgentProfile.AgentAddressModel md = new Models.MaintenanceAgentProfile.AgentAddressModel();
            Mapper.CreateMap<BusinessObjects.Province, Models.Customer.Province>();
            Mapper.CreateMap<BusinessObjects.City, Models.Customer.City>();
            Mapper.CreateMap<BusinessObjects.HomeOwnership, Models.Customer.HomeOwnership>();
            Mapper.CreateMap<BusinessObjects.AddressType, Models.Customer.AddressType>();
            IEnumerable<Models.Customer.Province> Province = Mapper.Map<IEnumerable<BusinessObjects.Province>, IEnumerable<Models.Customer.Province>>(customerSvc.getProvince());
            IEnumerable<Models.Customer.City> City = Mapper.Map<IEnumerable<BusinessObjects.City>, IEnumerable<Models.Customer.City>>(customerSvc.updateCity(AgentAddress.ProvinceID));
            IEnumerable<Models.Customer.HomeOwnership> HomeOwnership = Mapper.Map<IEnumerable<BusinessObjects.HomeOwnership>, IEnumerable<Models.Customer.HomeOwnership>>(customerSvc.getHomeOwnership());
            IEnumerable<Models.Customer.AddressType> AddressType = Mapper.Map<IEnumerable<BusinessObjects.AddressType>, IEnumerable<Models.Customer.AddressType>>(customerSvc.getAddressType(false));
            md.AgentAddress = AgentAddress;
            md.Province = Province;
            md.City = City;
            md.HomeOwnership = HomeOwnership;
            md.AddressType = AddressType;
            var pvr = new PartialViewResult();
            if (UpdateType == "update")
            {
                pvr = PartialView("_UpdateAddress", md);
            }
            else if (UpdateType == "add")
            {
                pvr = PartialView("_AddAddress", md);
            }
            return pvr;
        }
        public IEnumerable<Models.Customer.Gender> setGender(IEnumerable<BusinessObjects.Gender> Gender)
        {
            Mapper.CreateMap<BusinessObjects.Gender, Models.Customer.Gender>();
            IEnumerable<Models.Customer.Gender> retGender = Mapper.Map<IEnumerable<BusinessObjects.Gender>, IEnumerable<Models.Customer.Gender>>(Gender);
            return retGender;
        }
        public IEnumerable<Models.Customer.CivilStatus> setCivilStatus(IEnumerable<BusinessObjects.CivilStatus> CivilStatus)
        {
            Mapper.CreateMap<BusinessObjects.CivilStatus, Models.Customer.CivilStatus>();
            IEnumerable<Models.Customer.CivilStatus> retCivilStatus = Mapper.Map<IEnumerable<BusinessObjects.CivilStatus>, IEnumerable<Models.Customer.CivilStatus>>(CivilStatus);
            return retCivilStatus;
        }
        public IEnumerable<Models.Customer.District> setDistrict(IEnumerable<BusinessObjects.District> District)
        {
            Mapper.CreateMap<BusinessObjects.District, Models.Customer.District>();
            IEnumerable<Models.Customer.District> retDistrict = Mapper.Map<IEnumerable<BusinessObjects.District>, IEnumerable<Models.Customer.District>>(District);
            return retDistrict;
        }
        public IEnumerable<Models.Customer.Organization> setOrganization(IEnumerable<BusinessObjects.Organization> Organization)
        {
            Mapper.CreateMap<BusinessObjects.Organization, Models.Customer.Organization>();
            IEnumerable<Models.Customer.Organization> retOrganization = Mapper.Map<IEnumerable<BusinessObjects.Organization>, IEnumerable<Models.Customer.Organization>>(Organization);
            return retOrganization;
        }
        public IEnumerable<Models.MaintenanceAgentProfile.AgentType> setAgentType(IEnumerable<BusinessObjects.AgentType> AgentType)
        {
            Mapper.CreateMap<BusinessObjects.AgentType, Models.MaintenanceAgentProfile.AgentType>();
            IEnumerable<Models.MaintenanceAgentProfile.AgentType> retAgentType = Mapper.Map<IEnumerable<BusinessObjects.AgentType>, IEnumerable<Models.MaintenanceAgentProfile.AgentType>>(AgentType);
            return retAgentType;
        }
        public IEnumerable<Models.Customer.Province> setProvince(IEnumerable<BusinessObjects.Province> Province)
        {
            Mapper.CreateMap<BusinessObjects.Province, Models.Customer.Province>();
            IEnumerable<Models.Customer.Province> retProvince = Mapper.Map<IEnumerable<BusinessObjects.Province>, IEnumerable<Models.Customer.Province>>(Province);
            return retProvince;
        }
        public IEnumerable<Models.Customer.City> setCity(IEnumerable<BusinessObjects.City> City)
        {
            Mapper.CreateMap<BusinessObjects.City, Models.Customer.City>();
            IEnumerable<Models.Customer.City> retCity = Mapper.Map<IEnumerable<BusinessObjects.City>, IEnumerable<Models.Customer.City>>(City);
            return retCity;
        }
        public ActionResult UpdateCity(string ProvinceID)
        {
            Mapper.CreateMap<BusinessObjects.City, Models.Customer.City>();
            IEnumerable<Models.Customer.City> City = Mapper.Map<IEnumerable<BusinessObjects.City>, IEnumerable<Models.Customer.City>>(customerSvc.updateCity(ProvinceID));
            return Json(City, JsonRequestBehavior.AllowGet);
        }
        public IEnumerable<Models.Customer.HomeOwnership> setHomeOwnership(IEnumerable<BusinessObjects.HomeOwnership> HomeOwnership)
        {
            Mapper.CreateMap<BusinessObjects.HomeOwnership, Models.Customer.HomeOwnership>();
            IEnumerable<Models.Customer.HomeOwnership> retHomeOwnership = Mapper.Map<IEnumerable<BusinessObjects.HomeOwnership>, IEnumerable<Models.Customer.HomeOwnership>>(HomeOwnership);
            return retHomeOwnership;
        }

        public List<Models.MaintenanceAgentProfile.AgentAddress> setAgentAddress(IEnumerable<BusinessObjects.AgentAddress> AgentAddress)
        {            
            Mapper.CreateMap<BusinessObjects.AgentAddress, Models.MaintenanceAgentProfile.AgentAddress>();
            List<Models.MaintenanceAgentProfile.AgentAddress> retAgentAddress = Mapper.Map<IEnumerable<BusinessObjects.AgentAddress>, List<Models.MaintenanceAgentProfile.AgentAddress>>(AgentAddress);
            return retAgentAddress;
        }

        public Models.MaintenanceAgentProfile.AgentProfile setAgentProfile(BusinessObjects.AgentProfile AgentProfile)
        {
            Models.MaintenanceAgentProfile.AgentProfile retAgentProfile = new Models.MaintenanceAgentProfile.AgentProfile();
            Mapper.CreateMap<BusinessObjects.AgentProfile, Models.MaintenanceAgentProfile.AgentProfile>();
            retAgentProfile = Mapper.Map<BusinessObjects.AgentProfile, Models.MaintenanceAgentProfile.AgentProfile>(AgentProfile);
            return retAgentProfile;
        }

    }
}