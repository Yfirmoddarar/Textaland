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
        public IEnumerable<SubtitleFile> GetAllSubtitles()
        {

            //select all comments from the SubtitleComment list in an ascending order
            var _allSubtitles = from temp in _subtitleFiles
                               orderby temp._dateAdded ascending
                               select temp;
            return _allSubtitles;
        }

	}
}