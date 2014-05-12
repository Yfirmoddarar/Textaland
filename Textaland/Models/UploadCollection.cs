using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Textaland.Models {
    public class UploadCollection {
        public int? Id { get; set; }
        public string _name { get; set; }
        public string _description { get; set; }
        public string _type { get; set; }
        public string _language { get; set; }
        public bool _hardOfHearing { get; set; }
        public HttpPostedFileBase _file { get; set; }
    }
}