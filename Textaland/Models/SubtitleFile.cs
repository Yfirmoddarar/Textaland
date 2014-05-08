using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Textaland.Models
{
	public class SubtitleFile {
		// Attributes.

		int _id { get; set; }
		int _userId { get; set; }
		int _numOfTranslationParticipants { get; set; }
		int _numOfDownloads { get; set; }
		int _rating { get; set; }
		string _name { get; set; }
		string _type { get; set; }
		string _languageFrom { get; set; }
		string _languageTo { get; set; }
		string _description { get; set; }
		DateTime _dateAdded { get; set; }
		bool _readyForDownload { get; set; }
		bool _inTranslation { get; set; }
		bool _hearingImpaired { get; set; }

		// Operations
	}
}