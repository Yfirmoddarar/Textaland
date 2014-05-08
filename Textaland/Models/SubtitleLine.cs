using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Textaland.Models
{
	public class SubtitleLine {
		public int _id { get; set; }
		public int _textFileId { get; set; }
		public string _time { get; set; }
		public string _line1 { get; set; }
		public string _line2 { get; set; }


	}
}