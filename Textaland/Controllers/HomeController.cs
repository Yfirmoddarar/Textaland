using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Textaland.DataAccessLayer;
using Textaland.Models;

namespace Textaland.Controllers
{
    [RequireHttps]
    public class HomeController : Controller
    {
        public ActionResult FrontPage()
        {
			SubtitleFileRepo sfp = new SubtitleFileRepo();

			var mostPopularList = from mp in sfp.GetAllSubtitles()
								  orderby mp._numOfDownloads descending
								  select mp;

			ViewBag.PopularList = mostPopularList;

			var newestList = from mp in sfp.GetAllSubtitles()
							 orderby mp._dateAdded descending
							 select mp;

			ViewBag.NewestList = newestList;

			var ratingList = from mp in sfp.GetAllSubtitles()
								  orderby mp._rating descending
								  select mp;

			ViewBag.RatingList = ratingList;

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