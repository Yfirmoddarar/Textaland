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
		public ActionResult SearchResult(FormCollection formdata)
		{
			SubtitleFileRepo sfr = new SubtitleFileRepo(); 

			var searchResult = from s in sfr.GetAllSubtitles()
							   where s._name.Contains(formdata["leit"])
							   select s;

			return View(searchResult);
		}
	}
}