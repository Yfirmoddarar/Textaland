using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


//WIP
namespace Textaland.Models {
    public class SubtitleFileEditView {
        public string _name { get; set; }
        public string _languageFrom { get; set; }
        public string _languageTo { get; set; }
        public List<SubtitleLine> _subtitleLines { get; set; }
        public List<SubtitleComment> _subtitleComments { get; set; }
        //need more I think....
    }
}