using System;
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
		public ActionResult NewTranslationRequest()
		{
			return View();
		}

		[HttpPost]
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

		public ActionResult TranslationRequests()
		{
			return View();
		}
	}


	
}