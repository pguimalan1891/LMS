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
            AutoMapper.Mapper.CreateMap<BusinessObjects.getComponents, Models.Customer.getComponents>();
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
            //custModel.allComponents = setComponents();
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
            Models.Customer.getComponents list = new Models.Customer.getComponents();
            //list = AutoMapper.Mapper.Map<Models.Customer.getComponents>(allComponents);            
            return list;
        }


        public List<Models.Customer.CustomerEmployment> setCustomerEmploymentModel(IEnumerable<BusinessObjects.CustomerEmployment> custEmployment)
        {
            List<Models.Customer.CustomerEmployment> list = new List<Models.Customer.CustomerEmployment>();
            foreach (var rec in custEmployment)
            {
                Models.Customer.CustomerEmployment md = new Models.Customer.CustomerEmployment
                {
                    ID = rec.ID,
                    PISID = rec.PISID,
                    BusinessTypeID = rec.BusinessTypeID,
                    BusinessType = rec.BusinessType,
                    EmployerName = rec.EmployerName,
                    Income = rec.Income,
                    Contact_No = rec.Contact_No,
                    FromDate = rec.FromDate,
                    ToDate = rec.ToDate,
                    IsActive = rec.IsActive,
                    IsSpouse = rec.IsSpouse,
                    NatureOfBusinessID = rec.NatureOfBusinessID,
                    NatureOfBusiness = rec.NatureOfBusiness
                };
                list.Add(md);
            }
            return list;
        }
        public List<Models.Customer.CustomerAddress> setCustomerAddressModel(IEnumerable<BusinessObjects.CustomerAddress> custAddress)
        {
            List<Models.Customer.CustomerAddress> list = new List<Models.Customer.CustomerAddress>();
            foreach (var rec in custAddress)
            {
                Models.Customer.CustomerAddress md = new Models.Customer.CustomerAddress
                {
                    ID = rec.ID,
                    PISID = rec.PISID,
                    AddressTypeID = rec.AddressTypeID,
                    AddressType = rec.AddressType,
                    StreetAddress = rec.StreetAddress,
                    BarangayName = rec.BarangayName,
                    CityID = rec.CityID,
                    City = rec.City,
                    Province = rec.Province,
                    Country = rec.Country,
                    PostalCode = rec.PostalCode,
                    PhoneNumber = rec.PhoneNumber,
                    MobileNumber = rec.MobileNumber,
                    ResidentDate = rec.ResidentDate,
                    HomeOwnershipID = rec.HomeOwnershipID,
                    HomeOwnerShip = rec.HomeOwnerShip
                };
                list.Add(md);
            }
            return list;
        }
        public List<Models.Customer.CustomerDependents> setCustomerDependentModel(IEnumerable<BusinessObjects.CustomerDependents> custDependent)
        {
            List<Models.Customer.CustomerDependents> list = new List<Models.Customer.CustomerDependents>();
            foreach (var rec in custDependent)
            {
                Models.Customer.CustomerDependents md = new Models.Customer.CustomerDependents
                {
                    ID = rec.ID,
                    PISID = rec.PISID,
                    FirstName = rec.FirstName,
                    MiddleName = rec.MiddleName,
                    LastName = rec.LastName,
                    GenderID = rec.GenderID,
                    Gender = rec.Gender,
                    StreetAddress = rec.StreetAddress,
                    CityID = rec.CityID,
                    City = rec.City,
                    Province = rec.Province,
                    RelationshipTypeID = rec.RelationshipTypeID,
                    RelationshipType = rec.RelationshipType,
                    BirthDate = rec.BirthDate,
                    SchoolAddress = rec.SchoolAddress,
                    ContactNo = rec.ContactNo
                };
                list.Add(md);
            }
            return list;
        }
        public List<Models.Customer.CustomerEducation> setCustomerEducationModel(IEnumerable<BusinessObjects.CustomerEducation> custCharacter)
        {
            List<Models.Customer.CustomerEducation> list = new List<Models.Customer.CustomerEducation>();
            foreach (var rec in custCharacter)
            {
                Models.Customer.CustomerEducation md = new Models.Customer.CustomerEducation
                {
                    ID = rec.ID,
                    PISID = rec.PISID,
                    EducationTypeID = rec.EducationTypeID,
                    EducationType = rec.EducationType,
                    SchoolName = rec.SchoolName,
                    GraduationDate = rec.GraduationDate
                };
                list.Add(md);

            }
            return list;
        }    
        public List<Models.Customer.CustomerCharacter> setCustomerCharacterModel(IEnumerable<BusinessObjects.CustomerCharacter> custCharacter)
        {
            List<Models.Customer.CustomerCharacter> list = new List<Models.Customer.CustomerCharacter>();
            foreach (var rec in custCharacter)
            {
                Models.Customer.CustomerCharacter md = new Models.Customer.CustomerCharacter
                {
                    ID = rec.ID,
                    PISID = rec.PISID,
                    FirstName = rec.FirstName,
                    MiddleName = rec.MiddleName,
                    LastName = rec.LastName,
                    RelationShip = rec.RelationShip,
                    StreetAddress = rec.StreetAddress,
                    CityID = rec.CityID,
                    City = rec.City,
                    ContactNo = rec.ContactNo
                };
                list.Add(md);

            }
            return list;
        }
        public Models.Customer.CustomerRecord SetCustomerRecordModel(BusinessObjects.CustomerRecord custRecord)
        {            
            Models.Customer.CustomerRecord retRecord = new Models.Customer.CustomerRecord();
            retRecord.AgentCode = custRecord.AgentCode;
            retRecord.AgentProfileID = custRecord.AgentProfileID;
            retRecord.AgentType = custRecord.AgentType;
            retRecord.AgentLastName = custRecord.AgentLastName;
            retRecord.AgentFirstName = custRecord.AgentFirstName;
            retRecord.AgentMiddleName = custRecord.AgentMiddleName;
            retRecord.ApplicationType = custRecord.ApplicationType;
            retRecord.ApplicationTypeID = custRecord.ApplicationTypeID;
            retRecord.BorrowerGroup = custRecord.BorrowerGroup;
            retRecord.BorrowerType = custRecord.BorrowerType;
            retRecord.BorrowerTypeID = custRecord.BorrowerTypeID;
            retRecord.Citizenship = custRecord.Citizenship;
            retRecord.CitizenshipID = custRecord.CitizenshipID;
            retRecord.CivilStatus = custRecord.CivilStatus;
            retRecord.CivilStatusID = custRecord.CivilStatusID;
            retRecord.Code = custRecord.Code;
            retRecord.DateOfBirth = Convert.ToDateTime(custRecord.DateOfBirth);
            retRecord.DateOfMarriage = custRecord.DateOfMarriage;
            retRecord.DatetimeCreated = custRecord.DatetimeCreated;
            retRecord.District = custRecord.District;
            retRecord.DocumentStatus = custRecord.DocumentStatus;
            retRecord.DocumentStatusCode = custRecord.DocumentStatusCode;
            retRecord.FirstName = custRecord.FirstName;
            retRecord.Gender = custRecord.Gender;
            retRecord.GenderID = custRecord.GenderID;
            retRecord.GSISNumber = custRecord.GSISNumber;
            retRecord.ID = custRecord.ID;
            retRecord.LastName = custRecord.LastName;
            retRecord.LeadSource = custRecord.LeadSource;
            retRecord.LeadSourceID = custRecord.LeadSourceID;
            retRecord.MiddleName = custRecord.MiddleName;
            retRecord.Notes = custRecord.Notes;
            retRecord.Organization = custRecord.Organization;
            retRecord.OrganizationID = custRecord.OrganizationID;
            retRecord.OwnerCode = custRecord.OwnerCode;
            retRecord.OwnerID = custRecord.OwnerID;
            retRecord.Permission = custRecord.Permission;
            retRecord.PreparedByDatetime = custRecord.PreparedByDatetime;
            retRecord.PreparedByID = custRecord.PreparedByID;
            retRecord.RCN = custRecord.RCN;
            retRecord.RCNDateIssued = custRecord.RCNDateIssued;
            retRecord.RCNPlaceIssued = custRecord.RCNPlaceIssued;
            retRecord.SpouseContactNumber = custRecord.SpouseContactNumber;
            retRecord.SpouseDateofBirth = custRecord.SpouseDateofBirth;
            retRecord.SpouseFirstName = custRecord.SpouseFirstName;
            retRecord.SpouseLastName = custRecord.SpouseLastName;
            retRecord.SpouseMiddleName = custRecord.SpouseMiddleName;
            retRecord.SSSNumber = custRecord.SSSNumber;
            retRecord.TinNumber = custRecord.TinNumber;            
            return retRecord;
        }
    }
}