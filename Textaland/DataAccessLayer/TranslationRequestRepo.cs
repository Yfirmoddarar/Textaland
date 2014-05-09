using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Textaland.Models;

namespace Textaland.DataAccessLayer
{
	public class TranslationRequestRepo {
		// Initialize the db.
		AppDataContext db = new AppDataContext();

		//This operation returns all TranslationRequests.
		public IEnumerable<TranslationRequest> GetAllTranslationRequests() {
			//allTranslationRequests will be a list of all Requests ordered by
			//number of upvotes.
			var allTranslationRequests = from r in db.TranslationRequests
										 orderby r._numUpvotes descending
										 select r;
			return allTranslationRequests;
		}

		//This operation returns a Request by a specific id.
		public IEnumerable<TranslationRequest> GetTranslationRequestById(int id) {
			//Temp will be a translation request with the id that the operation took in.
			var translationRequestById = from r in db.TranslationRequests
										 where r.Id == id
										 select r;
			return translationRequestById;
		}

		//This operation adds a new TranslationRaquest to the list.
		public void AddTranslationRequest(TranslationRequest newTranslationRequest) {
			int newId = 1;

			//If the list is not empty the new translation request will get id according
			//to the highest excisting Id.
            if (GetAllTranslationRequests().Count() > 0) {
                newId = db.TranslationRequests.Max(x => x.Id) + 1;
            }
            newTranslationRequest.Id = newId;
			db.TranslationRequests.Add(newTranslationRequest);
            db.SaveChanges();
		}

		//This operation removes a specific TranslationRequest from the list by id.
        //public void RemoveTranslationRequestById(int removeId) {
        //    foreach(var item in db.TranslationRequests){
        //        if (item.Id == removeId) {
        //            db.TranslationRequests.Remove(item);
        //        }
        //    }
        //}

	}
}