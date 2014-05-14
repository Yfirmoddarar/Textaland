using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


//WIP
namespace Textaland.Models {
    public class SubtitleFileEditView {
        public string fileName { get; set; }
        public int fileId { get; set; }
        public string languageFrom { get; set; }
        public string languageTo { get; set; }
        public List<SubtitleLine> subtitleLines { get; set; }
        //need more I think....
    }
}