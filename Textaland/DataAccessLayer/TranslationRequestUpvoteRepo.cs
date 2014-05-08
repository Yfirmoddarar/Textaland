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

		//initalize a new list of TranslationRequestUpvotes
		private List<TranslationRequestUpvote> _translationRequestUpvotes = null;

		public IEnumerable<TranslationRequestUpvote> GetAllUpvotes() {

			//select all upvotes from the list and return them
			var allUpvotes = from temp in _translationRequestUpvotes
							select temp;
			return allUpvotes;
		}

		public IEnumerable<TranslationRequestUpvote> GetUpvoteById(int id) {

			//select the TranslationRequestUpvote that matches the given ID
			var correctId = from temp in _translationRequestUpvotes
							where temp._id == id
							select temp;
			return correctId;
		}

		public void AddUpvote(TranslationRequestUpvote newUpvote) {

			int newId = 1;

			if (_translationRequestUpvotes.Count > 0) {

				newId = _translationRequestUpvotes.Max(x => x._id) + 1;
			}

			newUpvote._id = newId;
			_translationRequestUpvotes.Add(newUpvote);
		}

		public void RemoveUpvote(int id) {
			foreach (var item in _translationRequestUpvotes) {
				if (item._id == id) {
					_translationRequestUpvotes.Remove(item);
					break;
				}
			}
		}


	}
}