using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Textaland.Models;

namespace Textaland.DataAccessLayer
{
	public class SubtitleCommentRepo {
		private static SubtitleCommentRepo _instance;

		public static SubtitleCommentRepo Instance {
			get {
				if (_instance == null)
					_instance = new SubtitleCommentRepo();
				return _instance;
			}
		}

		//initialize a list of SubtitleComments
		private List<SubtitleComment> _subtitleComments = null;

		//this operation returns all SubtitleComments
		private IEnumerable<SubtitleComment> GetAllComments() {

			//select all comments from the SubtitleComment list in an ascending order
			var _allComments = from temp in _subtitleComments
							   orderby temp._dateAdded ascending
							   select temp;
			return _allComments;						   
		}

	}
}