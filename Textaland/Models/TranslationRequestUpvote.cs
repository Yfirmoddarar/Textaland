using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Textaland.Models
{
	public class TranslationRequestUpvote {
		public int Id { get; set; }
		public int _requestId { get; set; }
		public string _userId { get; set; }
	}
}