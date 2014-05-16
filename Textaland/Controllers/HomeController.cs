﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Textaland.DataAccessLayer;
using Textaland.Interface;
using Textaland.Models;

namespace Textaland.Controllers
{
    [RequireHttps]
    public class HomeController : Controller {

        private readonly ISubtitleFileRepo _sfr;
        private readonly ITranslationRequestRepo _trr;

        public HomeController() {
            _sfr = SubtitleFileRepo.Instance;
            _trr = TranslationRequestRepo.Instance;
        }

        public HomeController(ISubtitleFileRepo sfr, ITranslationRequestRepo trr) {
            _sfr = sfr;
            _trr = trr;
        }

        public ActionResult FrontPage() {

            if (TempData["Error"] != null) {
                ModelState.AddModelError("FileInUse", "Því miður er skráin í notkun.");
            }

			// Here I sort the subtitles by number of downloads and add them to the
			// ViewBag.
			var mostPopularList = (from mp in _sfr.GetAllSubtitles()
								  where mp._readyForDownload == true
								  orderby mp._numOfDownloads descending
								  select mp).Take(10);

			ViewBag.PopularList = mostPopularList;

			// Here I sort the subtitles by date and add them to the ViewBag.
			var newestList = (from mp in _sfr.GetAllSubtitles()
							  where mp._readyForDownload == true
							 orderby mp._dateAdded descending
							 select mp).Take(10);

			ViewBag.NewestList = newestList;

			// Here I sort the subtitles by rating and add them to the ViewBag.
			var ratingList = (from mp in _sfr.GetAllSubtitles()
							  where mp._readyForDownload == true
							 orderby mp._rating descending
							 select mp).Take(10);

			ViewBag.RatingList = ratingList;

			// Here I get the subtitles that are in translation and order them by
			// number of participants and add them to the ViewBag.
			var translationList = (from tl in _sfr.GetAllSubtitles()
								   where tl._readyForDownload == false
								  orderby tl._dateAdded descending
								  select tl).Take(10);

			ViewBag.TranslationList = translationList;

			// Here I get all the request and order them by number of upvotes and 
			// add them to the ViewBag.
			var requestList = (from rl in _trr.GetAllTranslationRequests()
							  orderby rl._numUpvotes descending
							  select rl).Take(10);

			ViewBag.RequestList = requestList;

            return View();
        }

        public ActionResult About() {
            return View();
        }

        public ActionResult Contact() {
            return View();
        }

		public ActionResult FAQ() {
			return View();
		}

    }
}