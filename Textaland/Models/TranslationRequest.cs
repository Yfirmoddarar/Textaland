using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Textaland.Models
{
	public class TranslationRequest {
		public int Id { get; set; }
		public string _userId { get; set; }
		public int _numUpvotes { get; set; }
        [Required(ErrorMessage = "Name required")]
        public string _name { get; set; }
        [Required(ErrorMessage = "Please select language")]
		public string _language { get; set; }

		public void IncreaseNumUpvotes(){
			_numUpvotes++;
		}
	}
}