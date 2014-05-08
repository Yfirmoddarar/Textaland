using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Textaland.Models
{
	public class SubtitleComment
	{
		//Attributes
		public int _id { get; set; }
		public int _textFileId { get; set; }
		public int _userId { get; set; }
		public string _text { get; set; }
		public DateTime _dateAdded { get; set; }
	}
}