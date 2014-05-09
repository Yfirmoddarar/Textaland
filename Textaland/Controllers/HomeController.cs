using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Textaland.Controllers
{
    [RequireHttps]
    public class HomeController : Controller
    {
        public ActionResult FrontPage()
        {
            return View();
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

		public ActionResult FAQ()
		{
			return View();
		}
    }
}