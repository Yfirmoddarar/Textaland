using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Textaland.Models;

namespace Textaland.DataAccessLayer
{
	public class TranslationRequestUpvoteRepo
	{
		private static TranslationRequestUpvoteRepo _instance;

		public static TranslationRequestUpvoteRepo Instance {
			get {
				if (_instance == null) {
					_instance = new TranslationRequestUpvoteRepo();
				}
				return _instance;
			}
		}

		private List<TranslationRequestUpvote> _translationRequestUpvotes = null;

		public IEnumerable<TranslationRequestUpvote> GetAllUpvotes() {

			var allUpvotes = from temp in _translationRequestUpvotes
							select temp;
			return allUpvotes;
		}


	}
}