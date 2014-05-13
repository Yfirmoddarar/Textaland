using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;

namespace Textaland.Models
{
	public class SubtitleFile {
		// Attributes.

		public int Id { get; set; }
		public string _userId { get; set; }
		public int _numOfTranslationParticipants { get; set; }
		public int _numOfDownloads { get; set; }
		public int _numOfTimesRated { get; set; }
		public double _rating { get; set; }
        [Required(ErrorMessage="Please enter a name")]
		public string _name { get; set; }
       // [Required(ErrorMessage="Please select type")]
		public string _type { get; set; }
       // [Required(ErrorMessage="Missing language")]
    	public string _languageFrom { get; set; }
		public string _languageTo { get; set; }
        [Required(ErrorMessage = "Please add a description")]
		public string _description { get; set; }
		public DateTime _dateAdded { get; set; }
		public bool _readyForDownload { get; set; }
		public bool _inTranslation { get; set; }
		public bool _hearingImpaired { get; set; }
		public string _userName { get; set; }

		// Constructor.

		// Initialise the attribute '_dateAdded' to the time
		// when the textfile is created.
		public SubtitleFile() {
			_dateAdded = DateTime.Now;
			_inTranslation = false;
			_readyForDownload = true;
		}

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
		public void ChangeRating(double newRating) {
			double currTotalRating = _rating * _numOfTimesRated;
			currTotalRating += newRating;
			_numOfTimesRated++;
			_rating = currTotalRating / _numOfTimesRated;
		}


	}
}