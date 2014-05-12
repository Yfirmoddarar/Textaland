using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Textaland.DataAccessLayer;
using Textaland.Models;
using Microsoft.AspNet.Identity;


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

            //Selector info for type of video
            List<SelectListItem> types = new List<SelectListItem>();
            types.Add(new SelectListItem { Text = "select" });
            types.Add(new SelectListItem { Text = "Kvikmynd", Value = "Kvikmynd" });
            types.Add(new SelectListItem { Text = "Þáttur", Value = "Þáttur" });

            ViewBag.ListOfTypes = types;

            //selector info for language of file
            List<SelectListItem> languages = new List<SelectListItem>();
            languages.Add(new SelectListItem { Text = "select" });
            languages.Add(new SelectListItem { Text = "ENG", Value = "ENG" });
            languages.Add(new SelectListItem { Text = "ISL", Value = "ISL" });

            ViewBag.ListOfLanguages = languages;

            return View();
        }

        [HttpPost]
        public ActionResult UploadFile(UploadCollection uc) {

            //Check if file is empty, if not save and create new subtitle file
            //Call read function to collect lines from saved file
            if (uc._file != null && uc._file.ContentLength > 0) {

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
                //the number of files
                if (sfr.GetAllSubtitles().Count() > 0) {
                    newId = sfr.GetAllSubtitles().Max(x => x.Id) + 1;
                }
                sf.Id = newId;
                sf._dateAdded = DateTime.Now;
                sf._userId = User.Identity.GetUserId();

                if (ReadFile(uc._file, sf.Id)) {
                    sfr.AddSubtitle(sf);
                    return RedirectToAction("FrontPage", "Home");
                    // TODO redirect something!!!!
                }
                else {
                    SubtitleLineRepo slr = new SubtitleLineRepo();

                    slr.RemoveLines(sf.Id);

                    return RedirectToAction("FileError", "SubtitleFile");
                    //TODO redirect to error reading file
                }
            }
            else {
                return RedirectToAction("UploadError", "SubtitleFile");
            }
        }

        public bool ReadFile(HttpPostedFileBase file, int id) {
            try {
                using (StreamReader sr = new StreamReader(file.InputStream, System.Text.Encoding.UTF8, true)) {

                    SubtitleLineRepo slr = new SubtitleLineRepo();
                    
                    while (!(sr.EndOfStream)) {
                        SubtitleLine sl = new SubtitleLine();

                        try {
                            int lId = Convert.ToInt32(sr.ReadLine());
                            sl._lineNumber = lId;
                        }
                        catch (Exception e) {
                            Console.WriteLine("File type wrong");
                            Console.WriteLine(e.Message);
                            return false;
                        }
                
                        string lTime = sr.ReadLine();

                        if (lTime != "") {
                            sl._time = lTime;
            }
            else {
                            return false;
            }

                        string lText1 = sr.ReadLine();

                        if (lText1 != "") {
                            sl._line1 = lText1;

                            string lText2 = sr.ReadLine();

                            if (lText2 != "") {
                                sl._line2 = lText2;

                                string lText3 = sr.ReadLine();
                    
                                if (lText3 != "") {
                                    sl._line3 = lText3;
                                }
                            }
                        }
                        
                        sl._textFileId = id;
                        slr.AddLine(sl);

                    }
                }
                return true;
            }
            catch (Exception e) {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
                return false;
            }
        }

        public ActionResult UploadError() {
            return View();
        }

        public ActionResult FileError() {
            return View();
        }
		//Get
		public ActionResult AllSubtitleFiles(int num) {

			SubtitleFileRepo myRepo = new SubtitleFileRepo();

			var allSubs = myRepo.GetAllSubtitles().Skip(num * 10).Take(10);

			return View(allSubs);
		}

		//Operation that shows details about subtitle files
		
		public ActionResult AboutSubtitleFile(int? id){
			
			SubtitleFileRepo sfr = new SubtitleFileRepo();

			//"file" vill be the SubtitleFile that has the ID the same as "id".
			var file = sfr.GetSubtitleFileById(id.Value);

			if(file == null)
			{
				return HttpNotFound();
			}
			//Getting all the comments that hafa a specific subtitleFile id.
			SubtitleCommentRepo commentRepo = new SubtitleCommentRepo();

			var allComments = from c in commentRepo.GetCommentById(id.Value)
							  orderby c._dateAdded ascending
							  select c;
			ViewBag.AllComments = allComments;
			
			return View(file);
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
			return AboutSubtitleFile(s.Id);
		}

		//This function adds a new comment to a specific text file.
		[HttpPost]
		public ActionResult AddComment(SubtitleFile s, string addText)
		{
			SubtitleCommentRepo commentRepo = new SubtitleCommentRepo();

			SubtitleComment newComment = new SubtitleComment();

			SubtitleFileRepo fileRepo = new SubtitleFileRepo();
			if (!String.IsNullOrEmpty(addText)) {
			newComment._text = addText;
			newComment._textFileId = s.Id;
			newComment._userId = User.Identity.GetUserId();

			commentRepo.AddComment(newComment);
			}
			else {
				ModelState.AddModelError("addText", "Vinsamlegast sláðu inn athugasemd");
			}
			return AboutSubtitleFile(s.Id);
		}

		[HttpPost]
		public ActionResult DeleteComment(int commentID) {

			SubtitleCommentRepo commentRepo = new SubtitleCommentRepo();

			SubtitleFileRepo fileRepo = new SubtitleFileRepo();

			SubtitleComment comment = commentRepo.GetSingleCommentById(commentID);

			var userID = User.Identity.GetUserId();

			if (userID == comment._userId) {
				commentRepo.RemoveComment(comment);
			}
			else {
				ModelState.AddModelError("deleteComment", "Getur aðeins fjarlægt þína eigin athugasemd");
			}

			return AboutSubtitleFile(comment._textFileId);
		}
	}
}