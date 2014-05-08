using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Textaland.Models
{
	// This class is for every line in textfile. 
	public class SubtitleLine {
		public int _id { get; set; }
		public int _textFileId { get; set; }
		public string _time { get; set; }
		public string _line1 { get; set; }
		public string _line2 { get; set; }

		void ShiftTime(int shiftBy);
		void EditLine1(string newLine);
		void EditLine2(string newLine);
	}
}