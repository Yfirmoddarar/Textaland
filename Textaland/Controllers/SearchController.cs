using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Textaland.Models;
using Textaland.DataAccessLayer;

namespace Textaland.Controllers
{
    public class SearchController : Controller
    {
		// Post
		[HttpPost]
		public ActionResult SearchResult(string searchInput)
		{
			SubtitleFileRepo myRepo = new SubtitleFileRepo(); 

			var searchResult = from s in myRepo.GetAllSubtitles()
							   where s._name.Contains(searchInput)
							   select s;

			return View(searchResult);
		}
	}
}