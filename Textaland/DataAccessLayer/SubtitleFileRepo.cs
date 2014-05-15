using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Textaland.Models;

namespace Textaland.DataAccessLayer
{
	public class SubtitleFileRepo{
        // Initialize the db.
        AppDataContext db = new AppDataContext();

        // Function that will get all lines in the list _subtitleLines
        // and order them in ascending order by their id.
        public IEnumerable<SubtitleFile> GetAllSubtitles()
        {
            var allFiles = from f in db.SubtitleFiles
                           orderby f._rating descending
                           select f;
            return allFiles;
        }

        // This function will go through my list '_subtitleLines'
        // and return the lines that matches with the given 'id'.
        public SubtitleFile GetSubtitleFileById(int id)
        {
            var subtitleFilesById = (from f in db.SubtitleFiles
                                    where f.Id == id
                                    select f).SingleOrDefault();
            return subtitleFilesById;
        }

        //this operation adds a new Subtitle file to the existing List
        public void AddSubtitle(SubtitleFile _newSubtitleFile)
        {          
            db.SubtitleFiles.Add(_newSubtitleFile);
            db.SaveChanges();
        }

		public void ChangeRating(int id, double rating) {
			var req = db.SubtitleFiles.First(r => r.Id == id);

			req.ChangeRating(rating);
			db.SaveChanges();
		}

        
        //this operation removes the subtitle file that matches the given ID
        public void RemoveSubtitle(int removeId)
        {
            foreach (var item in db.SubtitleFiles)
            {
                if (item.Id == removeId)
                {
                    db.SubtitleFiles.Remove(item);
                    db.SaveChanges();
                }
            }
        }

        public void setTime(int id) {
            db.SubtitleFiles.Find(id)._dateAdded = DateTime.Now;
            db.SaveChanges();
        }

        public void setInTranslation(bool t, int id) {
            db.SubtitleFiles.Find(id)._inTranslation = t;
            db.SaveChanges();
        }

        public void setDownload(bool t, int id) {
            db.SubtitleFiles.Find(id)._readyForDownload = t;
            db.SaveChanges();
        }

	}
}