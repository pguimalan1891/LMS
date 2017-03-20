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
        [Route("Reference")]
        public ActionResult Reference()
        {
            return View();
        }

        [Route("LibraryList")]
        public ActionResult LibraryList()
        {
            return View();
        }

        [Route("Library")]
        public ActionResult Library(string ComponentName)
        {
            LibraryComponentModel md = new LibraryComponentModel();
            string[] cmp = ComponentName.Split('|');
            md.ComponentName = cmp[0];
            md.DisplayName = cmp[1].ToUpper();
            ViewBag.Title = "Loans Management System";
            return View(md);
        }
     
        [HttpGet]
        public ActionResult FetchLibraryComponent(string ComponentName)
        {
            return Json(this.service.getLibraryComponent(ComponentName), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult FetchLibraryUpdateComponent(string ComponentName)
        {
            return Json(this.service.getLIbraryUpdateComponent(ComponentName + "UpdCom"), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult AddComponent(string ComponentName, string compData, int opCode)
        {
            compData = Guid.NewGuid().ToString() + "|" + compData;
            int resp = this.service.updLibraryComponent(ComponentName, opCode, compData);
            return Content(resp == 1 ? "1" : resp.ToString());
        }

        [HttpPost]
        public ActionResult UpdateComponent(string ComponentName, string compData, int opCode)
        {
            int resp = this.service.updLibraryComponent(ComponentName, opCode, compData);
            return Content(resp == 1 ? "1" : resp.ToString());
        }

        [HttpPost]
        public ActionResult DeleteComponent(string ComponentName, string compData, int opCode)
        {
            int resp = this.service.updLibraryComponent(ComponentName, opCode, compData);
            return Content(resp == 1 ? "1" : resp.ToString());
        }
    }
}