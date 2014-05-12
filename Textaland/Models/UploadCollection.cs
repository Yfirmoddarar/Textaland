using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace Textaland.Models {
    public class UploadCollection {
        public int? Id { get; set; }
        [Required(ErrorMessage="Please enter name")]
        public string _name { get; set; }
        [Required(ErrorMessage = "Please enter description")]
        public string _description { get; set; }
        [Required(ErrorMessage = "Please select type")]
        public string _type { get; set; }
        [Required(ErrorMessage = "Please select language")]
        public string _language { get; set; }
        public bool _hardOfHearing { get; set; }
        public HttpPostedFileBase _file { get; set; }
    }
}