using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Textaland.Models;

namespace Textaland.DataAccessLayer
{
	public class TranslationRequestUpvoteRepo
	{

        AppDataContext db = new AppDataContext();

		public IEnumerable<TranslationRequestUpvote> GetAllUpvotes() {

			//select all upvotes from the list and return them
			var allUpvotes = from u in db.TranslationRequestUpvotes
							select u;
			return allUpvotes;
		}

		public IEnumerable<TranslationRequestUpvote> GetUpvoteById(int id) {

			//select the TranslationRequestUpvote that matches the given ID
			var correctId = from u in db.TranslationRequestUpvotes
							where u._requestId == id
							select u;
			return correctId;
		}

		public void AddUpvote(TranslationRequestUpvote newUpvote) {

			int newId = 1;

            // But if the list is not empty than it will get id according the the list.
            if (db.TranslationRequestUpvotes.Count() > 0)
            {
                newId = db.TranslationRequestUpvotes.Max(x => x._id) + 1;
			}

            // Give the new line the id.
			newUpvote._id = newId;
            // And add the new line to the list.
            db.TranslationRequestUpvotes.Add(newUpvote);
            db.SaveChanges();
		}

        /*
		public void RemoveUpvote(int id) {
			foreach (var item in _translationRequestUpvotes) {
				if (item._id == id) {
					_translationRequestUpvotes.Remove(item);
					break;
				}
			}
		}
        */

	}
}