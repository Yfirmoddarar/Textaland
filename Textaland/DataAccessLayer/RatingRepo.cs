using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Textaland.Interface;
using Textaland.Models;

namespace Textaland.DataAccessLayer
{
    public class RatingRepo : IRatingRepo {

        private static RatingRepo instance;

        public static RatingRepo Instance {
            get {
                if (instance == null) {
                    instance = new RatingRepo();
                }
                return instance;
            }
        }

		AppDataContext db = new AppDataContext();

		public void AddRating(Rating newRating) {
			db.UserRatings.Add(newRating);
			db.SaveChanges();
		}

		public IEnumerable<Rating> GetAllRatings() {
			var allRatings = from r in db.UserRatings
							 select r;
			return allRatings;
		}
	}
}