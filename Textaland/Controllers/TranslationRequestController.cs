using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Textaland.Models;
using Textaland.DataAccessLayer;
using Textaland.Interface;
using Microsoft.AspNet.Identity;

namespace Textaland.Controllers
{
	public class TranslationRequestController : Controller {

        private readonly ITranslationRequestRepo _requestRepo;
        private readonly ITranslationRequestUpvoteRepo _requestUpvoteRepo;

        TranslationRequest request = new TranslationRequest();

        TranslationRequestUpvote requestUpvote = new TranslationRequestUpvote();

        public TranslationRequestController(ITranslationRequestRepo requestRepo, 
            ITranslationRequestUpvoteRepo requestUpvoteRepo) {
                _requestRepo = requestRepo;
                _requestUpvoteRepo = requestUpvoteRepo;
                request._userId = "TestId";
        }

        public TranslationRequestController() {
            _requestRepo = TranslationRequestRepo.Instance;
            _requestUpvoteRepo = TranslationRequestUpvoteRepo.Instance;
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
            if (request._userId != "TestId") {
                request._userId = User.Identity.GetUserId();
            }

			_requestRepo.AddTranslationRequest(request);
            return RedirectToAction("TranslationRequests", new { num = 0 });		
		}

		//Get
		public ActionResult TranslationRequests(int num) {


			//We give errors for the following actions..

			if (TempData["addVoteAgain"] != null) {
				ViewBag.votedAgain = TempData["addVoteAgain"].ToString();
			}
			else if (TempData["loggedIn"] != null) {
				ViewBag.requiredLogIn = TempData["loggedIn"].ToString();		
			}

			//The following success message is given if the user upvotes
			if (TempData["upVoteSuccess"] != null) {
				ViewBag.voteSuccess = TempData["upVoteSuccess"].ToString();
			}

            ViewBag.Upvotes = _requestUpvoteRepo.GetAllUpvotes();

            var requests = (from r in _requestRepo.GetAllTranslationRequests()
							orderby r._numUpvotes descending
							select r).Skip(num * 10).Take(10);
			var countRequests = from r in _requestRepo.GetAllTranslationRequests()
							orderby r._numUpvotes
							select r;

			TempData["requestPageNumber"] = num;
			ViewBag.allRequests = requests;
			ViewBag.countRequest = countRequests;

			return View();
		}

		//This operation Adds a Vote to a the "tr" requests.
		[HttpPost]
		public ActionResult AddVote(TranslationRequest request) {

			//Gets the user ID.
            var userId = User.Identity.GetUserId();

			//Checks if the user is registered and logged in.
			if (User.Identity.IsAuthenticated) {

				// Takes all the votes that have the same user and request ID.
				var upvotes = from u in _requestUpvoteRepo.GetAllUpvotes()
							  where u._userId == userId &&
							  u._requestId == request.Id
							  select u;
				if (upvotes.Count() == 0) {
					requestUpvote._requestId = request.Id;
					requestUpvote._userId = userId;
					_requestRepo.upVote(requestUpvote._requestId);
					_requestUpvoteRepo.AddUpvote(requestUpvote);
					TempData["upVoteSuccess"] = "Þú bættir við atkvæði á beiðni fyrir " + request._name;
				}
				else {
					TempData["addVoteAgain"] = "Aðeins er hægt að kjósa hverja beiðni einu sinni";
				}
			}
			else {
				TempData["loggedIn"] = "Aðeins innskráðir notendur geta framkvæmt þessa aðgerð";
				return RedirectToAction("TranslationRequests", new { num = 0 });
			}
	
            return RedirectToAction("TranslationRequests", new { num = 0 });
			
		}

		public ActionResult AnswerRequest(int id) {
			if (User.Identity.IsAuthenticated) {
				_requestRepo.RemoveTranslationRequestById(id);
			}
			else {
				TempData["loggedIn"] = "Aðeins innskráðir notendur geta framkvæmt þessa aðgerð";
				return RedirectToAction("TranslationRequests", new { num = 0 });
			}

			return RedirectToAction("UploadFile", "SubtitleFile", new { area = "" });		

		}
	}


	
}