using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Textaland.Models;

namespace Textaland.DataAccessLayer
{
	public class TranslationRequestRepo {
		private static TranslationRequestRepo _instance;

		public static TranslationRequestRepo Instance {
			get {
				if (_instance == null) {
					_instance = new TranslationRequestRepo();
				}
				return _instance;
			}
		}

	    //Initialize a list of Translation Requests.
		private List<TranslationRequest> _translationRequests = null;

		//This operation returns all TranslationRequests.
		public IEnumerable<TranslationRequest> GetAllTranslationRequests() {

		}
	}
}