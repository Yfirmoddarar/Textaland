using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Textaland.Models;
using Textaland.DataAccessLayer;
using Microsoft.AspNet.Identity;

namespace Textaland.Controllers
{
	public class TranslationRequestController : Controller
	{
		//
		// GET: /TranslationRequest/
		public ActionResult Index()
		{
			return View();
		}

		//Get
        [Authorize]
		public ActionResult NewTranslationRequest()
		{

            List<SelectListItem> languages = new List<SelectListItem>();
            languages.Add(new SelectListItem { Text = "select" });
            languages.Add(new SelectListItem { Text = "ENG", Value = "ENG" });
            languages.Add(new SelectListItem { Text = "ISL", Value = "ISL" });

            ViewBag.ListOfLanguages = languages;

			return View();
		}

		[HttpPost]
        [Authorize]
		public ActionResult NewTranslationRequest(FormCollection formData) 
		{
			String strName = formData["_name"];
			String strLanguage = formData["_language"];

			TranslationRequest newRequest = new TranslationRequest();

			newRequest._name = strName;
			newRequest._language = strLanguage;
            newRequest._userId = User.Identity.GetUserId();

			TranslationRequestRepo requestRepo = new TranslationRequestRepo();

			requestRepo.AddTranslationRequest(newRequest);
            return RedirectToAction("TranslationRequests", new { num = 0 });		
		}

		//Get
		public ActionResult TranslationRequests(int num)
		{

			TranslationRequestRepo trr = new TranslationRequestRepo();
            TranslationRequestUpvoteRepo trur = new TranslationRequestUpvoteRepo();

			if (TempData["loggedIn"] != null) {
				ModelState.AddModelError("loggedIn", TempData["loggedIn"].ToString());
			}
			else if (TempData["loggdInUpVote"] != null) {
				ModelState.AddModelError("loggedInUpVote", TempData["loggedInUpVote"].ToString());
			}
			else if (TempData["addVoteAgain"] != null) {
				ModelState.AddModelError("addVoteAgain", TempData["addVoteAgain"].ToString());
			}

            ViewBag.Upvotes = trur.GetAllUpvotes();

            var requests = (from r in trr.GetAllTranslationRequests()
							orderby r._numUpvotes descending
							select r).Skip(num * 10).Take(10);
			var countRequests = from r in trr.GetAllTranslationRequests()
							orderby r._numUpvotes
							select r;



			ViewBag.allRequests = requests;
			ViewBag.countRequest = countRequests;

			return View();
		}

		//This operation Adds a Vote to a the "tr" requests.
		[HttpPost]
		[Authorize]
		public ActionResult AddVote(TranslationRequest request)
		{
            var userId = User.Identity.GetUserId();
			//taka við TranslationRequest Id búa til upvote út frá því. gefa því Id þ.e.a.s kalla á add fallið
			//í TranslationRequestUpvote og búa þannig nýtt vote.
            TranslationRequestRepo trr = new TranslationRequestRepo();
			TranslationRequestUpvote upvote = new TranslationRequestUpvote();
			TranslationRequestUpvoteRepo ur = new TranslationRequestUpvoteRepo();
			//Takes all upvotes from the TranslationRequest whith "id".
			//var userIdUpvotes = upvoteRepo.GetUpvoteById(id);
			//Checks if there exists an upvote with the same userId as the new vote.
			/*foreach(var item in userIdUpvotes) {
				if (item._userId == tr._userId)	{
					return RedirectToAction("TranslationRequests");
				}
			}*/

			if (User.Identity.IsAuthenticated)
			{

				var upvotes = from u in ur.GetAllUpvotes()
							  where u._userId == userId &&
							  u._requestId == request.Id
							  select u;
				if (upvotes.Count() == 0) {
					upvote._requestId = request.Id;
					upvote._userId = userId;
					trr.upVote(upvote._requestId);
					ur.AddUpvote(upvote);
				}
				else {
					TempData["addVoteAgain"] = "Aðeins er hægt að kjósa hverja beiðni einu sinni";
				}
			}
			else {
				TempData["loggedInUpVote"] = "Verður að vera innskráður til þess að geta kosið beiðni";
			}
			//Changes the number of upvotes in the TranslationRequest "tr".
			//Returns the TranslationRequests view were "tr" has one more upvotes.

            return RedirectToAction("TranslationRequests", new { num = 0 });
			
		}

		[HttpPost]
		public ActionResult AnswerRequest(TranslationRequest tr)
		{
			if (User.Identity.IsAuthenticated)
			{
				TranslationRequestRepo trr = new TranslationRequestRepo();

				trr.RemoveTranslationRequestById(tr);
			}
			else {
				TempData["loggedIn"] = "Aðeins innskráðir notendur geta svarað beiðni";	
			}

			return RedirectToAction("UploadFile", "SubtitleFile", new { area = "" });
			
		}
	}


	
}