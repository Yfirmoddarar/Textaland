using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Textaland.Models
{
	public class TranslationRequestUpvote
	{
		public int _id { get; set; }
		public int _requestId { get; set; }
		public int _userId { get; set; }
	}
}