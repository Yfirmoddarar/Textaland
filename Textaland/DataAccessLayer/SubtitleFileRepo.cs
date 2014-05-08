using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Textaland.DataAccessLayer
{
	public class SubtitleFileRepo{
        private static SubtitleFileRepo _instance;
        public static SubtitleFileRepo Instance {
            get {
                if (_instance == null)
                    _instance = new SubtitleFileRepo();
                return _instance;
            }
        }
	}
}