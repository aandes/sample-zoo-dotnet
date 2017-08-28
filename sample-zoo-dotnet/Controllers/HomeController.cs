using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json.Linq;
using sample_zoo_dotnet.Utils;

namespace sample_zoo_dotnet.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            GetContent();

            return View();
        }

        public ActionResult Terrestrial()
        {
            GetContent();

            return View();
        }

        public ActionResult Aquatic()
        {
            GetContent();

            return View();
        }

        private void GetContent () 
        {
            ContentHelper contentHelper = Cms.Content(action());

            ViewBag.cms = contentHelper;
            ViewBag.Title = contentHelper.Text("jcr:title", "Insert Page Title");
            ViewBag.Description = contentHelper.Text("jcr:description", "Insert Page Description");
        }

        private string action()
        {
            return this.ControllerContext.RouteData.Values["action"].ToString().ToLower();
        }
    }
}