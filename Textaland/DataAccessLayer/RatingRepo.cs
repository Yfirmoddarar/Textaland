using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Textaland.Models;

namespace Textaland.DataAccessLayer
{
	public class RatingRepo
	{
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