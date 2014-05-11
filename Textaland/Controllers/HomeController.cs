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
			// Get the subtitles from repo.
			SubtitleFileRepo sfp = new SubtitleFileRepo();

			// Get the translation request from the repo.
			TranslationRequest tq = new TranslationRequest();

			// Here I sort the subtitles by number of downloads and add them to the
			// ViewBag.
			var mostPopularList = from mp in sfp.GetAllSubtitles()
								  orderby mp._numOfDownloads descending
								  select mp;

			ViewBag.PopularList = mostPopularList;

			// Here I sort the subtitles by date and add them to the ViewBag.
			var newestList = from mp in sfp.GetAllSubtitles()
							 orderby mp._dateAdded descending
							 select mp;

			ViewBag.NewestList = newestList;

			// Here I sort the subtitles by rating and add them to the ViewBag.
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