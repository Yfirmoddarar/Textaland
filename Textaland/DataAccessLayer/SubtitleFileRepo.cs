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
                           orderby f.Id ascending
                           select f;
            return allFiles;
        }

        // This function will go through my list '_subtitleLines'
        // and return the lines that matches with the given 'id'.
        public IEnumerable<SubtitleFile> GetSubtitleFileById(int id)
        {
            var subtitleFilesById = from f in db.SubtitleFiles
                                    where f.Id == id
                                    select f;
            return subtitleFilesById;
        }

        //this operation adds a new Subtitle file to the existing List
        public void AddSubtitle(SubtitleFile _newSubtitleFile)
        {
            int newId = 1;

            //if the list isn't empty the new comment gets the ID according to 
            //the number of comments
            if (_subtitleFiles.Count > 0)
            {
                newId = _subtitleFiles.Max(x => x._id) + 1;
            }
            _newSubtitleFile._id = newId;
            _newSubtitleFile._dateAdded = DateTime.Now;
            _subtitleFiles.Add(_newSubtitleFile);
        }

        //this operation removes the subtitle file that matches the given ID
        public void RemoveSubtitle(int removeId)
        {
            foreach (var item in _subtitleFiles)
            {
                if (item._id == removeId)
                {
                    _subtitleFiles.Remove(item);
                    break;
                }
            }
        }
	}
}