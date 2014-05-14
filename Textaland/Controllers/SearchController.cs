using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Textaland.Models;
using Textaland.DataAccessLayer;

namespace Textaland.Controllers
{
    public class SearchController : Controller {
		
		TranslationRequestRepo trr = new TranslationRequestRepo();
		SubtitleFileRepo sfr = new SubtitleFileRepo(); 

		// Post
		[HttpPost]
		public ActionResult SearchResult(FormCollection formdata) {

			//splits the string from the user into many strings, one string is one word.
			string sentence = formdata["leit"].ToLower();
			string[] words = sentence.Split(new char[] { ' ' });
			
			//Just get the subtitles that contains every string in "words"
			var searchResult = (from s in sfr.GetAllSubtitles() select s);
			foreach (var w in words) {
				searchResult = (from s in searchResult where s._name.ToLower().Contains(w) select s );
			}

			return View(searchResult);
		}

		[HttpPost]
		public ActionResult SearchResultForRequests(FormCollection formdata) {

			//splits the string from the user into many strings, one string is one word.
			string sentence = formdata["leit2"].ToLower();
			string[] words = sentence.Split(new char[] { ' ' });

			//Just get the subtitles that contains every string in "words"
			var searchResult = (from t in trr.GetAllTranslationRequests() select t);
			foreach (var w in words) {
				searchResult = (from t in searchResult where t._name.ToLower().Contains(w) select t);
			}
			return View(searchResult);
		}
	}
}