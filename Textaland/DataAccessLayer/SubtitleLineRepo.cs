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

	}
}