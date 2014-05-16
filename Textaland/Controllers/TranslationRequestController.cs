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
	public class TranslationRequestController : Controller {

		TranslationRequest request = new TranslationRequest();

		TranslationRequestRepo requestRepo = new TranslationRequestRepo();

		TranslationRequestUpvote requestUpvote = new TranslationRequestUpvote();

		TranslationRequestUpvoteRepo requestUpvoteRepo  = new TranslationRequestUpvoteRepo();
		//
		// GET: /TranslationRequest/
		public ActionResult Index() {
			return View();
		}

		//Get
        [Authorize]
		public ActionResult NewTranslationRequest() {

            List<SelectListItem> languages = new List<SelectListItem>();
            languages.Add(new SelectListItem { Text = "select" });
            languages.Add(new SelectListItem { Text = "ENG", Value = "ENG" });
            languages.Add(new SelectListItem { Text = "ISL", Value = "ISL" });

            ViewBag.ListOfLanguages = languages;

			return View();
		}

		[HttpPost]
        [Authorize]
		public ActionResult NewTranslationRequest(FormCollection formData) {
			String strName = formData["_name"];
			String strLanguage = formData["_language"];

			request._name = strName;
			request._language = strLanguage;
            request._userId = User.Identity.GetUserId();


			requestRepo.AddTranslationRequest(request);
            return RedirectToAction("TranslationRequests", new { num = 0 });		
		}

		//Get
		public ActionResult TranslationRequests(int num) {


			//We give errors for the following actions..

			if (TempData["addVoteAgain"] != null) {
				ViewBag.votedAgain = TempData["addVoteAgain"].ToString();
			}

			//The following success message is given if the user upvotes
			if (TempData["upVoteSuccess"] != null) {
				ViewBag.voteSuccess = TempData["upVoteSuccess"].ToString();
			}

            ViewBag.Upvotes = requestUpvoteRepo.GetAllUpvotes();

            var requests = (from r in requestRepo.GetAllTranslationRequests()
							orderby r._numUpvotes descending
							select r).Skip(num * 10).Take(10);
			var countRequests = from r in requestRepo.GetAllTranslationRequests()
							orderby r._numUpvotes
							select r;

			TempData["requestPageNumber"] = num;
			ViewBag.allRequests = requests;
			ViewBag.countRequest = countRequests;

			return View();
		}

		//This operation Adds a Vote to a the "tr" requests.
		[HttpPost]
		[Authorize]
		public ActionResult AddVote(TranslationRequest request) {
            var userId = User.Identity.GetUserId();
			//taka við TranslationRequest Id búa til upvote út frá því. gefa því Id þ.e.a.s kalla á add fallið
			//í TranslationRequestUpvote og búa þannig nýtt vote.

			//Takes all upvotes from the TranslationRequest whith "id".
			//var userIdUpvotes = upvoteRepo.GetUpvoteById(id);
			//Checks if there exists an upvote with the same userId as the new vote.
			/*foreach(var item in userIdUpvotes) {
				if (item._userId == tr._userId)	{
					return RedirectToAction("TranslationRequests");
				}
			}*/

			if (User.Identity.IsAuthenticated) {

				var upvotes = from u in requestUpvoteRepo.GetAllUpvotes()
							  where u._userId == userId &&
							  u._requestId == request.Id
							  select u;
				if (upvotes.Count() == 0) {
					requestUpvote._requestId = request.Id;
					requestUpvote._userId = userId;
					requestRepo.upVote(requestUpvote._requestId);
					requestUpvoteRepo.AddUpvote(requestUpvote);
					TempData["upVoteSuccess"] = "Þú bættir við atkvæði á beiðni fyrir " + request._name;
				}
				else {
					TempData["addVoteAgain"] = "Aðeins er hægt að kjósa hverja beiðni einu sinni";
				}
			}
	
			//Changes the number of upvotes in the TranslationRequest "tr".
			//Returns the TranslationRequests view were "tr" has one more upvotes.

            return RedirectToAction("TranslationRequests", new { num = 0 });
			
		}

		[HttpPost]
		public ActionResult AnswerRequest(TranslationRequest tr) {
			if (User.Identity.IsAuthenticated) {
				requestRepo.RemoveTranslationRequestById(tr);
			}

			return RedirectToAction("UploadFile", "SubtitleFile", new { area = "" });		
		}
	}


	
}