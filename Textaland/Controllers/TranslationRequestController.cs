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
			return View();
		}

		[HttpPost]
        [Authorize]
		public ActionResult NewTranslationRequest(FormCollection formData) 
		{
			String strName = formData["_name"];
			String strLanguage = formData["_language"];

			if (!String.IsNullOrEmpty(strName) && !String.IsNullOrEmpty(strLanguage)) {

				TranslationRequest newRequest = new TranslationRequest();

				newRequest._name = strName;
				newRequest._language = strLanguage;

				TranslationRequestRepo requestRepo = new TranslationRequestRepo();

				requestRepo.AddTranslationRequest(newRequest);
				return RedirectToAction("TranslationRequests");
			}
			else {
				ModelState.AddModelError("_language", "Fylla verður út í báða reiti");
				return NewTranslationRequest();
			}

			
		}

		//Get
		public ActionResult TranslationRequests(int num)
		{

			TranslationRequestRepo trr = new TranslationRequestRepo();
            TranslationRequestUpvoteRepo trur = new TranslationRequestUpvoteRepo();

            ViewBag.Upvotes = trur.GetAllUpvotes();

            var requests = (from r in trr.GetAllTranslationRequests()
							orderby r._numUpvotes
							select r).Skip(num * 10).Take(10);

			ViewBag.allRequests = requests;

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

            var upvotes = from u in ur.GetAllUpvotes()
                          where u._userId == userId &&
                          u._requestId == request.Id
                          select u;
            if (upvotes.Count() == 0)
            {
                upvote._requestId = request.Id;
                upvote._userId = userId;
                trr.upVote(upvote._requestId);
                ur.AddUpvote(upvote);
            }
			
			//Changes the number of upvotes in the TranslationRequest "tr".
			//Returns the TranslationRequests view were "tr" has one more upvotes.
		        
            return RedirectToAction("TranslationRequests");
			
		}

		[HttpPost]
		public ActionResult AnswerRequest(TranslationRequest tr)
		{

			TranslationRequestRepo trr = new TranslationRequestRepo();

			trr.RemoveTranslationRequestById(tr);

			return RedirectToAction("Upload", "SubtitleFile", new { area = "" });
		}
	}


	
}