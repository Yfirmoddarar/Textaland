using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Textaland.Models;

namespace Textaland.DataAccessLayer
{
	public class SubtitleLineRepo {
		// Initialize the db.
		AppDataContext db = new AppDataContext();

		// Function that will get all lines in the list _subtitleLines
		// and order them in ascending order by their id.
		public IEnumerable<SubtitleLine> GetAllLines() {
			var allLines =  from l in db.SubtitleLines
							orderby l.Id ascending
							select l;
			return allLines;
		}

		// This function will go through my list '_subtitleLines'
		// and return the lines that matches with the given 'id'.
		public IEnumerable<SubtitleLine> GetLinesById(int id) {
			var subtitleLinesById = from l in db.SubtitleLines
									where l._textFileId == id
                                    orderby l._lineNumber ascending 
									select l;
			return subtitleLinesById;
		}

		// This function will add the new line to the list.
		// NOTE - perhabs we will add a whole list instead of single line. 
		public void AddLine(SubtitleLine newSubtitleLine) {
			// If the list it empty than the 'newSubtitleLine' will get the id 1.
			int newId = 1;

			// But if the list is not empty than it will get id according the the list.
			if (db.SubtitleLines.Count() > 0) {
				newId = db.SubtitleLines.Max(x => x.Id) + 1;
			}

			// Give the new line the id.
			newSubtitleLine.Id = newId;
			// And add the new line to the list.
			db.SubtitleLines.Add(newSubtitleLine);
			db.SaveChanges();
		}

		// This function will remove the line with a given id. If no line has the given
		// id than the function will do nothing.
		// NOTE - we think that we will not use this function but we will see.
		
		public void RemoveLines(int id) {
			foreach (var item in db.SubtitleLines) {
				if (item._textFileId == id) {
                    db.SubtitleLines.Remove(item);
				}
			}
            db.SaveChanges();
		}
		

		// This function will update the lines of the subtitleline with the
 		// same id as 'newLine'. First I find the line in the list and than I
		// update the lines.
		public void UpdateLine(SubtitleLine newLine) {
			db.SubtitleLines.Find(newLine.Id)._line1 = newLine._line1;
			db.SubtitleLines.Find(newLine.Id)._line2 = newLine._line2;
			db.SaveChanges();
		}
	}
}