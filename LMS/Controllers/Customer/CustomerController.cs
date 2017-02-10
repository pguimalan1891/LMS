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
            allComponents = service.getAllComponents();            
        }

        [Route("Customer")]
        public ActionResult Customer()
        {
            Models.Customer.CustomerModel custModel = new CustomerModel();
            custModel.custRecord = SetCustomerRecordModel(service.getCustomerRecordByCode("096-20150604000022"));
            var ID = custModel.custRecord.ID;
            custModel.custCharacter = setCustomerCharacterModel(service.getCustomerCharacterByID(ID));
            custModel.custEducation = setCustomerEducationModel(service.getCustomerEducationByID(ID));
            custModel.custDependents = setCustomerDependentModel(service.getCustomerDependentsByID(ID));
            custModel.custAddress = setCustomerAddressModel(service.getCustomerAddressByID(ID));
            custModel.custEmployment = setCustomerEmploymentModel(service.getCustomerEmploymentRecordByID(ID));
            custModel.allComponents = setComponents();
            return View(custModel);
        }        

        [HttpGet]
        public ActionResult FetchCustomerRecord()
        {
            return Json(this.service.getCustomerRecord(), JsonRequestBehavior.AllowGet);
        }

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

        public Models.Customer.getComponents setComponents()
        {
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
            list = Mapper.Map<BusinessObjects.getComponents,Models.Customer.getComponents>(allComponents);            
            return list;
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