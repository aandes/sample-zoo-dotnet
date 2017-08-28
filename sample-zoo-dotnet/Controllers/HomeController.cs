using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json.Linq;
using sample_zoo_dotnet.Utils;
using sample_zoo_dotnet.Models;

namespace sample_zoo_dotnet.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View(GetModel());
        }

        public ActionResult Terrestrial()
        {
            return View(GetModel());
        }

        public ActionResult Aquatic()
        {
            return View(GetModel());
        }

        // Returns the Model for the page with content adeed
        private CmsViewModel GetModel() 
        {
            // fetch content for the page
            ContentHelper contentHelper = Cms.Content(action());

            // set title, description properties
            ViewBag.Title = contentHelper.Text("jcr:title", "Insert Page Title");
            ViewBag.Description = contentHelper.Text("jcr:description", "Insert Page Description");

            // add the content as a property of the view's model
            return new CmsViewModel { cms = contentHelper };
        }

        // Gets the current action name
        private string action()
        {
            return this.ControllerContext.RouteData.Values["action"]
                .ToString().ToLower();
        }
    }
}