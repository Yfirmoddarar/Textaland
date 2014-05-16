using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Textaland.Interface;
using Textaland.Models;

namespace Textaland.DataAccessLayer
{
    public class TranslationRequestUpvoteRepo : ITranslationRequestUpvoteRepo {

        private static TranslationRequestUpvoteRepo instance;

        public static TranslationRequestUpvoteRepo Instance {
            get {
                if (instance == null) {
                    instance = new TranslationRequestUpvoteRepo();
                }
                return instance;
            }
        }

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
            if (GetAllUpvotes().Count() > 0) {
                newId = db.TranslationRequestUpvotes.Max(x => x.Id) + 1;
            }

            // Give the new line the id.
            newUpvote.Id = newId;
            //And add the new line to the list.
            db.TranslationRequestUpvotes.Add(newUpvote);
            db.SaveChanges();
		}
	}
}