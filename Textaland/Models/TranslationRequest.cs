using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Textaland.Models
{
	public class TranslationRequest {
		public int Id { get; set; }
		public int _userId { get; set; }
		public int _numUpvotes { get; set; }
		public string _name { get; set; }
		public string _language { get; set; }

		public void IncreaseNumUpvotes(){
			_numUpvotes++;
		}
	}
}