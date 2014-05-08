using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Textaland.Models
{
	public class SubtitleFile {
		// Attributes.

		public int _id { get; set; }
		public int _userId { get; set; }
		public int _numOfTranslationParticipants { get; set; }
		public int _numOfDownloads { get; set; }
		public int _numOfTimesRated { get; set; }
		public double _rating { get; set; }
		public string _name { get; set; }
		public string _type { get; set; }
		public string _languageFrom { get; set; }
		public string _languageTo { get; set; }
		public string _description { get; set; }
		public DateTime _dateAdded { get; set; }
		public bool _readyForDownload { get; set; }
		public bool _inTranslation { get; set; }
		public bool _hearingImpaired { get; set; }

		// Operations

		// Function that tell us whether textfile is in
		// translation or not.
		bool IsInTranslation() {
			return _inTranslation;
		}

		// Function that tell us whether textfile is ready for
		// download or not.
		bool IsReadyForDownload() {
			return _readyForDownload;
		}

		// Function that will increase '_numOfDownload' by one.
		void IncreaseNumDownloads() {
			_numOfDownloads++;
		}

		// Function that will increase '_numOfTranslationParticipants'
		// by one.
		void IncreaseNumOfTranslationParticipants() {
			_numOfTranslationParticipants++;
		}

		// This function will update the rating of some user when somebody 
		// gives him new rating.
		// The function has not been tested - 8th of May.
		void ChangeRating(int newRating) {
			double currTotalRating = _rating * _numOfTimesRated;
			currTotalRating += newRating;
			_numOfTimesRated++;
			_rating = currTotalRating / _numOfTimesRated;
		}


	}
}