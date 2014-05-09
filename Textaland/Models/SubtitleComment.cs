using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Textaland.Models
{
	public class SubtitleComment
	{
		// Attributes
		public int Id { get; set; }
		public int _textFileId { get; set; }
		public int _userId { get; set; }
		public string _text { get; set; }
		public DateTime _dateAdded { get; set; }

		// Constructor.

		// Initialise the attribute '_dateAdded' to the time
		// when the subtitlecomment is created.
		public SubtitleComment() {
			_dateAdded = DateTime.Now;
		}
	}
}