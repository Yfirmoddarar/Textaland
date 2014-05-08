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
		public int _rating { get; set; }
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

		// Function that will increase 'numOfDownload' by one.
		void IncreaseNumDownloads() {
			_numOfDownloads++;
		}

		// 
		void ChangeRating() {

		}


	}
}