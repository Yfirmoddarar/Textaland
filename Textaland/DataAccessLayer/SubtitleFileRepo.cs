using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Textaland.Models;

namespace Textaland.DataAccessLayer
{
	public class SubtitleFileRepo{
        private static SubtitleFileRepo _instance;

        //this creates a new SubtitleFileRepo
        public static SubtitleFileRepo Instance {
            get {
                if (_instance == null)
                    _instance = new SubtitleFileRepo();
                return _instance;
            }
        }

        //initialize a list of SubtitleFiles
        private List<SubtitleFile> _subtitleFiles = null;

        //this operation returns all SubtitleFiles
        public IEnumerable<SubtitleFile> GetAllSubtitles() {
            //select all comments from the SubtitleComment list in an ascending order
            var _allSubtitles = from temp in _subtitleFiles
                               orderby temp._dateAdded ascending
                               select temp;
            return _allSubtitles;
        }

        //this operation returns the Subtitle File that matches the given ID
        public IEnumerable<SubtitleFile> GetSubtitleFileById(int id) {
            var _subtitleFileById = from temp in _subtitleFiles
                                       where temp._id == id
                                       select temp;
            return _subtitleFileById;
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