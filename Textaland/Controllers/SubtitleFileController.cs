using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Textaland.DataAccessLayer;
using Textaland.Models;

namespace Textaland.Controllers
{
    public class SubtitleFileController : Controller {
        //Get
        //public ActionResult Upload () {

        //    List<SelectListItem> types = new List<SelectListItem>();
        //    types.Add(new SelectListItem { Text = "Kvikmynd", Value = "Kvikmynd" });
        //    types.Add(new SelectListItem { Text = "Þáttur", Value = "Þáttur" });

        //    ViewBag.ListOfTypes = types;


        //    List<SelectListItem> languages = new List<SelectListItem>();
        //    languages.Add(new SelectListItem { Text = "ENG", Value = "ENG" });
        //    languages.Add(new SelectListItem { Text = "ISL", Value = "ISL" });

        //    //ViewBag.ListOfTypes = new SelectList(new[] {
        //    //    new {Id = "1", Name = "Kvikmynd"},
        //    //    new {Id = "2", Name = "Þáttur"},
        //    //}, "Id", "Name");

        //    ViewBag.ListOfLanguages = languages;

        //    //ViewBag.ListOfLanguages = new SelectList(new[] {
        //    //    new {Id = "1", Name = "ENG"},
        //    //    new {Id = "2", Name = "ISL"},
        //    //}, "Id", "Name");

        //    return View();
        //}


        //Get
        public ActionResult UploadFile() {

            List<SelectListItem> types = new List<SelectListItem>();
            types.Add(new SelectListItem { Text = "select" });
            types.Add(new SelectListItem { Text = "Kvikmynd", Value = "Kvikmynd" });
            types.Add(new SelectListItem { Text = "Þáttur", Value = "Þáttur" });

            ViewBag.ListOfTypes = types;


            List<SelectListItem> languages = new List<SelectListItem>();
            languages.Add(new SelectListItem { Text = "select" });
            languages.Add(new SelectListItem { Text = "ENG", Value = "ENG" });
            languages.Add(new SelectListItem { Text = "ISL", Value = "ISL" });

            ViewBag.ListOfLanguages = languages;

            return View();
        }

        [HttpPost]
        public ActionResult UploadFile(UploadCollection uc) {

            if (uc._file != null && uc._file.ContentLength > 0) {
                var fileName = Path.GetFileName(uc._file.FileName);
                var path = Path.Combine(Server.MapPath("~/App_Data/uploads"), fileName); 
                uc._file.SaveAs(path);

                SubtitleFile sf = new SubtitleFile {
                    _name = uc._name,
                    _description = uc._description,
                    _hearingImpaired = uc._hardOfHearing,
                    _type = uc._type,
                    _languageFrom = uc._language
                };

                SubtitleFileRepo sfr = new SubtitleFileRepo();

                int newId = 1;

                //if the list isn't empty the new comment gets the ID according to 
                //the number of comments
                if (sfr.GetAllSubtitles().Count() > 0) {
                    newId = sfr.GetAllSubtitles().Max(x => x.Id) + 1;
                }
                sf.Id = newId;
                sf._dateAdded = DateTime.Now;

                sfr.AddSubtitle(sf);

                ReadFile(path, sf.Id);

            }

            return RedirectToAction("FrontPage", "Home");
        }

        public void ReadFile(string path, int id) {

        }

        
        //[HttpPost]
        //public ActionResult Upload (SubtitleFile sf, HttpPostedFileBase file) {

        //    if (file != null && file.ContentLength > 0) {
        //        var filename = Path.GetFileName(file.FileName);
        //        var path = Path.Combine(Server.MapPath("~/App_Data/uploads"), filename);
        //        file.SaveAs(path);

        //            SubtitleFileRepo sfr = new SubtitleFileRepo();

        //            sfr.AddSubtitle(sf);

        //        }
        //        else {
        //           return RedirectToAction("UploadError", "SubtitleFile");
        //        } 
        //   return RedirectToAction("FrontPage", "Home");
        //}

        public ActionResult UploadError() {
            return View();
        }

        public ActionResult FileError() {
            return View();
        }
		//Get
		public ActionResult AllSubtitleFiles() {

			SubtitleFileRepo myRepo = new SubtitleFileRepo();

			var allSubs = myRepo.GetAllSubtitles();

			return View(allSubs);
		}

		// Get
		[HttpGet]
		public ActionResult SearchResult() {
			
			
			
			
			return View();
		}

		[HttpPost]
		public ActionResult SearchResult(FormCollection formData) {
			
			
			
			
			return View();
		}

		//get
		/*public ActionResult AboutSubtitleFile(){


			return View();
		}*/

		//Operation that shows details about subtitle files
		[HttpPost]
		public ActionResult AboutSubtitleFile(SubtitleFile s){
			
			SubtitleCommentRepo commentRepo = new SubtitleCommentRepo();

			var allComments = from c in commentRepo.GetCommentById(s.Id)
							  orderby c._dateAdded ascending
							  select c;
			ViewBag.AllComments = allComments;
			

			return View(s);
		}

		[HttpPost]
		public ActionResult GiveRating(SubtitleFile s, string rating)
		{
			SubtitleFileRepo fileRepo = new SubtitleFileRepo();



			double newRating;

			if (Double.TryParse(rating, out newRating)) {

				if (newRating < 0 || newRating > 10) {
					ModelState.AddModelError("rating", "Vinsamlegast sláðu inn tölu á milli 0-10");
				}
				else {
					fileRepo.ChangeRating(s.Id, newRating);
				}
			}
			else {
				ModelState.AddModelError("rating", "Einkunnin má ekki innihalda bókstafi");
			}
			return AboutSubtitleFile(s);
		}


		[HttpPost]
		public ActionResult AddComment(SubtitleFile s, string addText)
		{
			SubtitleCommentRepo commentRepo = new SubtitleCommentRepo();

			SubtitleComment newComment = new SubtitleComment();

			SubtitleFileRepo fileRepo = new SubtitleFileRepo();

			newComment._text = addText;
			newComment._textFileId = s.Id;

			commentRepo.AddComment(newComment);

			return AboutSubtitleFile(s);
		}
	}
}