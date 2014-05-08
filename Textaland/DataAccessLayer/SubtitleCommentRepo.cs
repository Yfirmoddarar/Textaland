using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Textaland.Models;

namespace Textaland.DataAccessLayer
{
	public class SubtitleCommentRepo : ApplicationDbContext {

		private static SubtitleCommentRepo _instance;

		//this function creates a new SubtitleCommentRepo
		public static SubtitleCommentRepo Instance {
			get {
				if (_instance == null) {
					_instance = new SubtitleCommentRepo();
				}
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

		public IEnumerable<SubtitleComment> GetCommentById(int id) {
			var _subtitleCommentById = from temp in _subtitleComments
									   where temp._id == id
									   select temp;
			return _subtitleCommentById;
		}

		//this operation adds a new comment to the existing List
		public void AddComment(SubtitleComment _newSubtitleComment) {
			int newId = 1;
			
			//if the list isn't empty the new comment gets the ID according to 
			//the numnber of comments
			if (_subtitleComments.Count > 0) {
				newId = _subtitleComments.Max(x => x._id) + 1;
			}
			_newSubtitleComment._id = newId;
			_newSubtitleComment._dateAdded = DateTime.Now;
			_subtitleComments.Add(_newSubtitleComment);
		}

		//this operation removes the comment that matches the given ID
		public void RemoveComment(int removeId) {

			foreach (var item in _subtitleComments) {
				if (item._id == removeId) {
					_subtitleComments.Remove(item);
					break;
				}
			}
		}
	

	}
}