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
using LMS.Models.DevelopmentTools;

namespace LMS.Controllers.DevelopmentTools
{
    public class DevelopmentToolsController : Controller
    {
        private ILibrarySvc service;

        public DevelopmentToolsController()
            : this(new LibrarySvc())
        {
        }

        public DevelopmentToolsController(ILibrarySvc service)
        {            
            this.service = service;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Library()
        {
            LibraryComponentModel md = new LibraryComponentModel();
            md.ComponentName = "Company Type";
            return View(md);
        }
        [HttpGet]
        public ActionResult FetchLibraryComponent()
        {
            return Json(this.service.getLibraryComponent("CompanyType"),JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult FetchLibraryUpdateCompent()
        {
            return Json(this.service.getLIbraryUpdateComponent("CompanyType" + "UpdCom"),JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult AddComponent(LibraryComponentModel libcomp)
        {
            return null;
            //int resp = this.service.updLibraryComponent("CompanyType", opCode, data);
            //return Content(resp > 0 ? "ok" : "exist");
        }

    }
}