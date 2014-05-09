using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Textaland.Models;

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
			return Index();
		}
	}


	
}