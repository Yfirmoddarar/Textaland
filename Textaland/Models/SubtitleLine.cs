using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Textaland.Models
{
	// This class is for every line in textfile. 
	public class SubtitleLine {
		// Attributes.

		public int _id { get; set; }
		public int _textFileId { get; set; }
		public string _time { get; set; }
		public string _line1 { get; set; }
		public string _line2 { get; set; }
		
		// Operations.

		// This function is unfinished - but is should update '_time'.
        void ShiftTime(int shiftBy)
        {

        }

		// This function will edit the first line and replace it with
		// 'newLine'.
		void EditLine1(string newLine) {
			_line1 = newLine;
		}

		// This function will edit the second line and replace it with
		// 'newLine'.
		void EditLine2(string newLine) {
			_line2 = newLine;
		}
	}
}