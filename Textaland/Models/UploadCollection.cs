using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace Textaland.Models {
    public class UploadCollection {
        public int? Id { get; set; }
        [Required(ErrorMessage = "Please enter name"), StringLength(50, ErrorMessage = "Name too long.")]
        public string fName { get; set; }
        [Required(ErrorMessage = "Please enter description"), StringLength(140, ErrorMessage="Description too long.")]
        public string fDescription { get; set; }
        [Required(ErrorMessage = "Please select type")]
        public string fType { get; set; }
        [Required(ErrorMessage = "Please select language")]
        public string fLanguage { get; set; }
        public bool _hardOfHearing { get; set; }
        public HttpPostedFileBase _file { get; set; }
    }
}