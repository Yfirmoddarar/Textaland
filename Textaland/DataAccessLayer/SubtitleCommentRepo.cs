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
		public IEnumerable<SubtitleComment> GetAllComments() {

			//select all comments from the SubtitleComment list in an ascending order
			var _allComments = from temp in _subtitleComments
							   orderby temp._dateAdded ascending
							   select temp;
			return _allComments;						   
		}

		//this operation returns the Comment that matches the given ID

		public IEnumerable<SubtitleComment> GetCommentById(int _newId) {
			var _subtitleCommentById = from temp in _subtitleComments
									   where temp._id == _newId
									   select temp;
			return _subtitleCommentById;
		}

		//this operation adds a new comment to the existing List
		public void AddComment(SubtitleComment _newSubtitleComment) {
			int _newId = 1;
			
			//if the list isn't empty the new comment gets the ID according to 
			//the numnber of comments
			if (_subtitleComments.Count > 0) {
				_newId = _subtitleComments.Count + 1;
			}
			_newSubtitleComment._id = _newId;
			_newSubtitleComment._dateAdded = DateTime.Now;
			_subtitleComments.Add(_newSubtitleComment);
		}

	}
}