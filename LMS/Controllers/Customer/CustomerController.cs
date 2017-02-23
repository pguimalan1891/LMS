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
using LMS.Models.Customer;
using AutoMapper;
using AutoMapper.Configuration;

namespace LMS.Controllers
{
    public class CustomerController : Controller
    {
        private ICustomerSvc service;
        private BusinessObjects.getComponents allComponents;
        // GET: Customer
        public CustomerController()
            : this(new CustomerSvc())
        {
        }
        public CustomerController(ICustomerSvc service)
        {
            this.service = service;            
        }

        [Route("Customer")]
        public ActionResult Customer()
        {
            //Models.Customer.CustomerModel custModel = new CustomerModel();
            //custModel.custRecord = SetCustomerRecordModel(service.getCustomerRecordByCode("096-20150604000022"));
            //var ID = custModel.custRecord.ID;
            //custModel.custCharacter = setCustomerCharacterModel(service.getCustomerCharacterByID(ID));
            //custModel.custEducation = setCustomerEducationModel(service.getCustomerEducationByID(ID));
            //custModel.custDependents = setCustomerDependentModel(service.getCustomerDependentsByID(ID));
            //custModel.custAddress = setCustomerAddressModel(service.getCustomerAddressByID(ID));
            //custModel.custEmployment = setCustomerEmploymentModel(service.getCustomerEmploymentRecordByID(ID));
            //custModel.allComponents = setComponents(custModel);
            return View();
        }

        [HttpGet]
        public ActionResult FetchCustomerRecord()
        {
            return Json(this.service.getCustomerRecord(), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult FetchCustomerRecordByID(string Code)
        {
            Models.Customer.CustomerModel custModel = new CustomerModel();
            custModel.custRecord = SetCustomerRecordModel(service.getCustomerRecordByCode(Code));
            var ID = custModel.custRecord.ID;
            custModel.custCharacter = setCustomerCharacterModel(service.getCustomerCharacterByID(ID));
            custModel.custEducation = setCustomerEducationModel(service.getCustomerEducationByID(ID));
            custModel.custDependents = setCustomerDependentModel(service.getCustomerDependentsByID(ID));
            custModel.custAddress = setCustomerAddressModel(service.getCustomerAddressByID(ID));
            custModel.custEmployment = setCustomerEmploymentModel(service.getCustomerEmploymentRecordByID(ID));
            var pvr = new PartialViewResult();
            pvr = PartialView("_Display_CustomerRecord", custModel);
            return pvr;
        }

        [HttpGet]
        [Route("CustomerUpdate")]
        public ActionResult Update_CustomerRecord(string Code)
        {
            Models.Customer.CustomerModel custModel = new CustomerModel();
            var custRecord = service.getCustomerRecordByCode(Code);
            custModel.custRecord = SetCustomerRecordModel(custRecord);
            var ID = custModel.custRecord.ID;
            custModel.custCharacter = setCustomerCharacterModel(service.getCustomerCharacterByID(ID));
            custModel.custEducation = setCustomerEducationModel(service.getCustomerEducationByID(ID));
            custModel.custDependents = setCustomerDependentModel(service.getCustomerDependentsByID(ID));
            custModel.custAddress = setCustomerAddressModel(service.getCustomerAddressByID(ID)).Where(p => p.AddressTypeID.Contains("0")).ToList();            
            custModel.custEmployment = setCustomerEmploymentModel(service.getCustomerEmploymentRecordByID(ID));
            custModel.allComponents = setComponents(custRecord);
            return View(custModel);
        }

        [HttpPost]
        public ActionResult GetCustomerFullData(string Code, string Type)
        {
            Models.Customer.CustomerModel custModel = new CustomerModel();
            custModel.custRecord = SetCustomerRecordModel(service.getCustomerRecordByCode(Code));
            var ID = custModel.custRecord.ID;
            if (Type == "Character")
                custModel.custCharacter = setCustomerCharacterModel(service.getCustomerCharacterByID(ID));
            if (Type == "Education")
                custModel.custEducation = setCustomerEducationModel(service.getCustomerEducationByID(ID));
            if (Type == "Dependent")
                custModel.custDependents = setCustomerDependentModel(service.getCustomerDependentsByID(ID));
            if (Type == "Address")
                custModel.custAddress = setCustomerAddressModel(service.getCustomerAddressByID(ID));
            if (Type == "Employment")
                custModel.custEmployment = setCustomerEmploymentModel(service.getCustomerEmploymentRecordByID(ID));            
            return Json(custModel, JsonRequestBehavior.AllowGet);
        }

        public Models.Customer.getComponents setComponents(BusinessObjects.CustomerRecord custRecord)
        {
            allComponents = service.getAllComponents(custRecord);
            Mapper.CreateMap<BusinessObjects.Gender, Models.Customer.Gender>();
            Mapper.CreateMap<BusinessObjects.Citizenship, Models.Customer.Citizenship>();
            Mapper.CreateMap<BusinessObjects.District, Models.Customer.District>();
            Mapper.CreateMap<BusinessObjects.Organization, Models.Customer.Organization>();
            Mapper.CreateMap<BusinessObjects.ApplicationType, Models.Customer.ApplicationType>();
            Mapper.CreateMap<BusinessObjects.BorrowerType, Models.Customer.BorrowerType>();
            Mapper.CreateMap<BusinessObjects.LeadSource, Models.Customer.LeadSource>();
            Mapper.CreateMap<BusinessObjects.CivilStatus, Models.Customer.CivilStatus>();
            Mapper.CreateMap<BusinessObjects.City, Models.Customer.City>();
            Mapper.CreateMap<BusinessObjects.Province, Models.Customer.Province>();
            Mapper.CreateMap<BusinessObjects.HomeOwnership, Models.Customer.HomeOwnership>();
            Mapper.CreateMap<BusinessObjects.BusinessType, Models.Customer.BusinessType>();
            Mapper.CreateMap<BusinessObjects.NatureofBusiness, Models.Customer.NatureofBusiness>();
            Mapper.CreateMap<BusinessObjects.AddressType, Models.Customer.AddressType>();
            Mapper.CreateMap<BusinessObjects.RelationshipType, Models.Customer.RelationshipType>();
            Mapper.CreateMap<BusinessObjects.EducationType, Models.Customer.EducationType>();
            Mapper.CreateMap<BusinessObjects.Agent, Models.Customer.Agent>();

            Mapper.CreateMap<BusinessObjects.getComponents, Models.Customer.getComponents>();
            Models.Customer.getComponents list = new Models.Customer.getComponents();
            list = Mapper.Map<BusinessObjects.getComponents, Models.Customer.getComponents>(allComponents);
            return list;
        }

        [HttpPost]
        public ActionResult UpdateCity(string ProvinceID)
        {
            Mapper.CreateMap<BusinessObjects.City, Models.Customer.City>();
            IEnumerable<Models.Customer.City> City = Mapper.Map<IEnumerable<BusinessObjects.City>, IEnumerable<Models.Customer.City>>(service.updateCity(ProvinceID));
            return Json(City, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult UpdateEmployment(string UpdateType, Models.Customer.CustomerEmployment custEmployment)
        {
            Models.Customer.CustomerEmploymentModel md = new CustomerEmploymentModel();
            Mapper.CreateMap<BusinessObjects.BusinessType, Models.Customer.BusinessType>();
            Mapper.CreateMap<BusinessObjects.NatureofBusiness, Models.Customer.NatureofBusiness>();
            IEnumerable<Models.Customer.BusinessType> BusinessType = Mapper.Map<IEnumerable<BusinessObjects.BusinessType>, IEnumerable<Models.Customer.BusinessType>>(service.getBusinessType());
            IEnumerable<Models.Customer.NatureofBusiness> NatureofBusiness = Mapper.Map<IEnumerable<BusinessObjects.NatureofBusiness>, IEnumerable<Models.Customer.NatureofBusiness>>(service.getNatureofBusiness());
            md.custEmployment = custEmployment;
            md.BusinessType = BusinessType;
            md.NatureofBusiness = NatureofBusiness;
            var pvr = new PartialViewResult();
            if (UpdateType == "update")
            {
                pvr = PartialView("_UpdateEmployment", md);
            }
            else if (UpdateType == "add")
            {
                pvr = PartialView("_AddEmployment", md);
            }
            return pvr;
        }
        [HttpPost]
        public ActionResult UpdateAddress(string UpdateType, Models.Customer.CustomerAddress custAddress)
        {
            Models.Customer.CustomerAddressModel md = new CustomerAddressModel();
            Mapper.CreateMap<BusinessObjects.Province, Models.Customer.Province>();
            Mapper.CreateMap<BusinessObjects.City, Models.Customer.City>();
            Mapper.CreateMap<BusinessObjects.HomeOwnership, Models.Customer.HomeOwnership>();
            Mapper.CreateMap<BusinessObjects.AddressType, Models.Customer.AddressType>();
            IEnumerable<Models.Customer.Province> Province = Mapper.Map<IEnumerable<BusinessObjects.Province>, IEnumerable<Models.Customer.Province>>(service.getProvince());
            IEnumerable<Models.Customer.City> City = Mapper.Map<IEnumerable<BusinessObjects.City>, IEnumerable<Models.Customer.City>>(service.updateCity(custAddress.ProvinceID));
            IEnumerable<Models.Customer.HomeOwnership> HomeOwnership = Mapper.Map<IEnumerable<BusinessObjects.HomeOwnership>, IEnumerable<Models.Customer.HomeOwnership>>(service.getHomeOwnership());
            IEnumerable<Models.Customer.AddressType> AddressType = Mapper.Map<IEnumerable<BusinessObjects.AddressType>, IEnumerable<Models.Customer.AddressType>>(service.getAddressType(false));
            md.custAddress = custAddress;
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
        [HttpPost]
        public ActionResult UpdateDependent(string UpdateType, Models.Customer.CustomerDependents custDependents)
        {
            Models.Customer.CustomerDependentsModel md = new CustomerDependentsModel();
            Mapper.CreateMap<BusinessObjects.Province, Models.Customer.Province>();
            Mapper.CreateMap<BusinessObjects.City, Models.Customer.City>();
            Mapper.CreateMap<BusinessObjects.Gender, Models.Customer.Gender>();
            Mapper.CreateMap<BusinessObjects.RelationshipType, Models.Customer.RelationshipType>();
            IEnumerable<Models.Customer.Province> Province = Mapper.Map<IEnumerable<BusinessObjects.Province>, IEnumerable<Models.Customer.Province>>(service.getProvince());
            IEnumerable<Models.Customer.City> City = Mapper.Map<IEnumerable<BusinessObjects.City>, IEnumerable<Models.Customer.City>>(service.updateCity(custDependents.ProvinceID));
            IEnumerable<Models.Customer.Gender> Gender = Mapper.Map<IEnumerable<BusinessObjects.Gender>, IEnumerable<Models.Customer.Gender>>(service.getGender());
            IEnumerable<Models.Customer.RelationshipType> RelationshipType = Mapper.Map<IEnumerable<BusinessObjects.RelationshipType>, IEnumerable<Models.Customer.RelationshipType>>(service.getRelationshipType());
            md.custDependents = custDependents;
            md.Province = Province;
            md.City = City;
            md.Gender = Gender;
            md.RelationshipType = RelationshipType;
            var pvr = new PartialViewResult();
            if (UpdateType == "update")
            {
                pvr = PartialView("_UpdateDependent", md);
            }
            else if (UpdateType == "add")
            {
                pvr = PartialView("_AddDependent", md);
            }
            return pvr;
        }
        [HttpPost]
        public ActionResult UpdateEducation(string UpdateType, Models.Customer.CustomerEducation custEducation)
        {
            Models.Customer.CustomerEducationModel md = new CustomerEducationModel();
            Mapper.CreateMap<BusinessObjects.EducationType, Models.Customer.EducationType>();            
            IEnumerable<Models.Customer.EducationType> EducationType = Mapper.Map<IEnumerable<BusinessObjects.EducationType>, IEnumerable<Models.Customer.EducationType>>(service.getEducationType());
            md.custEducation = custEducation;
            md.EducationType = EducationType;            
            var pvr = new PartialViewResult();
            if (UpdateType == "update")
            {
                pvr = PartialView("_UpdateEducation", md);
            }
            else if (UpdateType == "add")
            {
                pvr = PartialView("_AddEducation", md);
            }
            return pvr;
        }
        [HttpPost]
        public ActionResult UpdateCharacter(string UpdateType, Models.Customer.CustomerCharacter custCharacter)
        {
            Models.Customer.CustomerCharacterModel md = new Models.Customer.CustomerCharacterModel();
            Mapper.CreateMap<BusinessObjects.Province, Models.Customer.Province>();
            Mapper.CreateMap<BusinessObjects.City, Models.Customer.City>();
            IEnumerable<Models.Customer.Province> Province = Mapper.Map<IEnumerable<BusinessObjects.Province>, IEnumerable<Models.Customer.Province>>(service.getProvince());
            IEnumerable<Models.Customer.City> City = Mapper.Map<IEnumerable<BusinessObjects.City>, IEnumerable<Models.Customer.City>>(service.getCity(custCharacter.ProvinceID));
            md.custCharacter = custCharacter;
            md.Province = Province;
            md.City = City;
            var pvr = new PartialViewResult();
            if (UpdateType == "update")
            {
                pvr = PartialView("_UpdateCharacter", md);
            }
            else if (UpdateType == "add")
            {
                pvr = PartialView("_AddCharacter", md);
            }
            return pvr;
        }

        public List<Models.Customer.CustomerEmployment> setCustomerEmploymentModel(IEnumerable<BusinessObjects.CustomerEmployment> custEmployment)
        {
            List<Models.Customer.CustomerEmployment> list = new List<Models.Customer.CustomerEmployment>();
            Mapper.CreateMap<BusinessObjects.CustomerEmployment, Models.Customer.CustomerEmployment>();
            list = Mapper.Map<IEnumerable<BusinessObjects.CustomerEmployment>, List<Models.Customer.CustomerEmployment>>(custEmployment);
            return list;
        }
        public List<Models.Customer.CustomerAddress> setCustomerAddressModel(IEnumerable<BusinessObjects.CustomerAddress> custAddress)
        {
            List<Models.Customer.CustomerAddress> list = new List<Models.Customer.CustomerAddress>();
            Mapper.CreateMap<BusinessObjects.CustomerAddress, Models.Customer.CustomerAddress>();
            list = Mapper.Map<IEnumerable<BusinessObjects.CustomerAddress>, List<Models.Customer.CustomerAddress>>(custAddress);
            return list;
        }
        public List<Models.Customer.CustomerDependents> setCustomerDependentModel(IEnumerable<BusinessObjects.CustomerDependents> custDependent)
        {

            List<Models.Customer.CustomerDependents> list = new List<Models.Customer.CustomerDependents>();
            Mapper.CreateMap<BusinessObjects.CustomerDependents, Models.Customer.CustomerDependents>();
            list = Mapper.Map<IEnumerable<BusinessObjects.CustomerDependents>, List<Models.Customer.CustomerDependents>>(custDependent);
            return list;
        }
        public List<Models.Customer.CustomerEducation> setCustomerEducationModel(IEnumerable<BusinessObjects.CustomerEducation> custEducation)
        {
            List<Models.Customer.CustomerEducation> list = new List<Models.Customer.CustomerEducation>();
            Mapper.CreateMap<BusinessObjects.CustomerEducation, Models.Customer.CustomerEducation>();
            list = Mapper.Map<IEnumerable<BusinessObjects.CustomerEducation>, List<Models.Customer.CustomerEducation>>(custEducation);
            return list;
        }
        public List<Models.Customer.CustomerCharacter> setCustomerCharacterModel(IEnumerable<BusinessObjects.CustomerCharacter> custCharacter)
        {
            List<Models.Customer.CustomerCharacter> list = new List<Models.Customer.CustomerCharacter>();
            Mapper.CreateMap<BusinessObjects.CustomerCharacter, Models.Customer.CustomerCharacter>();
            list = Mapper.Map<IEnumerable<BusinessObjects.CustomerCharacter>, List<Models.Customer.CustomerCharacter>>(custCharacter);
            return list;
        }
        public Models.Customer.CustomerRecord SetCustomerRecordModel(BusinessObjects.CustomerRecord custRecord)
        {
            Models.Customer.CustomerRecord retRecord = new Models.Customer.CustomerRecord();
            Mapper.CreateMap<BusinessObjects.CustomerRecord, Models.Customer.CustomerRecord>();
            retRecord = Mapper.Map<BusinessObjects.CustomerRecord, Models.Customer.CustomerRecord>(custRecord);
            return retRecord;
        }
    }
}