using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Textaland.Models;

namespace Textaland.DataAccessLayer
{
	public class SubtitleCommentRepo : ApplicationDbContext {

		//initialize a list of SubtitleComments

		AppDataContext db = new AppDataContext();

		//this operation returns all SubtitleComments
		public IEnumerable<SubtitleComment> GetAllComments() {

			//select all comments from the SubtitleComment list in an ascending order
			var allComments = from comments in db.SubtitleComments
							   orderby comments._dateAdded ascending
							   select comments;
			return allComments;						   
		}

		//this operation returns the Comment that matches the given ID

		public IEnumerable<SubtitleComment> GetCommentById(int id) {
			var subtitleCommentById = from comment in db.SubtitleComments
									   where comment._textFileId == id
									   select comment;
			return subtitleCommentById;
		}

		public SubtitleComment GetSingleCommentById(int id)
		{
			var subtitleCommentById = (from comment in db.SubtitleComments
									  where comment.Id == id
									  select comment).SingleOrDefault();
			return subtitleCommentById;
		}

		//this operation adds a new comment to the existing List
		public void AddComment(SubtitleComment newSubtitleComment) {
			int newId = 1;
			
			//if the list isn't empty the new comment gets the ID according to 
			//the numnber of comments
			if (GetAllComments().Count() > 0) {
				newId = db.SubtitleComments.Max(x => x.Id) + 1;
			}
			newSubtitleComment.Id = newId;
			newSubtitleComment._dateAdded = DateTime.Now;
			db.SubtitleComments.Add(newSubtitleComment);
			db.SaveChanges();
		}

        ////this operation removes the comment that matches the given ID
        public void RemoveComment(SubtitleComment comment) {
			db.SubtitleComments.Attach(comment);
			db.SubtitleComments.Remove(comment);
			db.SaveChanges();		
        }
	

	}
}