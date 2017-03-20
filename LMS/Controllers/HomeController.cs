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
using LMS.Models;
using AutoMapper;
using AutoMapper.Configuration;

namespace LMS.Controllers
{
    public class HomeController : Controller
    {

        private IHomeSvc service;

        public HomeController()
            :this(new HomeSvc())
        {
        }

        public HomeController(IHomeSvc service)
        {
            this.service = service;
        }

        [Route("Home")]
        public ActionResult Index(LMS.Models.ApplicationUserAccount user)
        {
            if(Session["loginDetails"] != null)
            {
                
                return View();
            }else
            {
                return RedirectToAction("Login", "Account");
            }
            
        }

        [HttpGet]
        public ActionResult GetCustomerList()
        {
            return Json(service.GetCustomerList(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult RenderMenu()
        {
            return PartialView("_MenuView");
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            return View();
        }
    }
}