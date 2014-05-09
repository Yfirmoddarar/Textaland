﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Textaland.Models;
using Textaland.DataAccessLayer;

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
		public ActionResult TranslationRequests()
		{

			TranslationRequestRepo trr = new TranslationRequestRepo();

			var requests = trr.GetAllTranslationRequests();

			return View(requests);
		}

		//This operation Adds a Vote to a the "tr" requests.
		/*[HttpPost]
		[Authorize]
		public ActionResult AddVote(int id)
		{
			//taka við TranslationRequest Id búa til upvote út frá því. gefa því Id þ.e.a.s kalla á add fallið
			//í TranslationRequestUpvote og búa þannig nýtt vote.
			TranslationRequestRepo upvoteRepo = new TranslationRequestRepo();
			//Takes all upvotes from the TranslationRequest tr.
			var userIdUpvotes = upvoteRepo.GetUpvoteById(upvoteRepo.Id);
			//Checks if there exists an upvote with the same userId as the new vote.
			foreach(var item in userIdUpvotes) {
				if (item._userId == tr._userId)	{
					return RedirectToAction("TranslationRequests");
				}
			}
			//Changes the number of upvotes in the TranslationRequest "tr".
			tr._numUpvotes++;

			//Returns the TranslationRequests view were "tr" has one more upvotes.
			return RedirectToAction("TranslationRequests");
			
		}*/
	}


	
}