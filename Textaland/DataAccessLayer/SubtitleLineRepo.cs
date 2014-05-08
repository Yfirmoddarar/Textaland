using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Textaland.Models;

namespace Textaland.DataAccessLayer
{
	public class SubtitleLineRepo {

		// This is my repo for SubtitleLine which is private. 
		private static SubtitleLineRepo _instance;

		// My repo is private so I have to create a public repo
		// which contains get function for my private repo.
		public static SubtitleLineRepo Instance {
			get {
				// If my repo is null than I have to create a new one.
				if (_instance == null) {
					_instance = new SubtitleLineRepo();
				}
				// And than I return my repo.
				return _instance;
			}
		}

		// Initialize a list of SubtitleLines.
		private List<SubtitleLine> _subtitleLines = null;

		// Function that will get all lines in the list _subtitleLines
		// and order them in ascending order by their id.
		public IEnumerable<SubtitleLine> GetAllLines() {
			var _allLines = from temp in _subtitleLines
							orderby temp._id ascending
							select temp;
			return _allLines;
		}

		// This function will go through my list '_subtitleLines'
		// and return the line that matches with the given 'id'.
		public IEnumerable<SubtitleLine> GetLineById(int id) {
			var _subtitleLinesById = from temp in _subtitleLines
									 where temp._id == id
									 select temp;
			return _subtitleLinesById;
		}

	}
}