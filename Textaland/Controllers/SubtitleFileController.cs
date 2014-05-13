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
        [Authorize]
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
        [Authorize]
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

                sf.Id = 0;
                sf._dateAdded = DateTime.Now;
				sf._userName = User.Identity.GetUserName();
                sf._userId = User.Identity.GetUserId();

                    sfr.AddSubtitle(sf);

                int newId = sf.Id;

                if (ReadFile(uc._file, newId)) {
                    return RedirectToAction("AboutSubtitleFile", "SubtitleFile", sf);
                }
                else {
                    SubtitleLineRepo slr = new SubtitleLineRepo();

                    //Remove what was already uploaded, since file is of wrong format.

                    sfr.RemoveSubtitle(newId);
                    slr.RemoveLines(newId);

                    return RedirectToAction("FileError", "SubtitleFile");
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

			var allSubs = (from c in myRepo.GetAllSubtitles()
							where c._inTranslation == false
							select c).Skip(num * 10).Take(10);

			ViewBag.allSubs = allSubs;

			return View();
		}

		//Operation that shows details about subtitle files
		
		public ActionResult AboutSubtitleFile(int id){
			
			SubtitleFileRepo sfr = new SubtitleFileRepo();

			//"file" vill be the SubtitleFile that has the ID the same as "id".
			var file = sfr.GetSubtitleFileById(id);

			if(file == null)
			{
				return HttpNotFound();
			}
			//Getting all the comments that hafa a specific subtitleFile id.
			SubtitleCommentRepo commentRepo = new SubtitleCommentRepo();

            ViewBag.AllComments = commentRepo.GetCommentsById(id);
			
			return View(file);
		}

		[HttpPost]
		public ActionResult GiveRating(SubtitleFile s, string rating)
		{
			SubtitleFileRepo fileRepo = new SubtitleFileRepo();

			RatingRepo rateRepo = new RatingRepo();

			Rating giveRating = new Rating();

			var userID = User.Identity.GetUserId();

			var allRatings = from r in rateRepo.GetAllRatings()
							 where r._userId == userID &&
							 r._textFileId == s.Id
							 select r;

			if (allRatings.Count() == 0)
			{

				giveRating._textFileId = s.Id;
				giveRating._userId = userID;
				rateRepo.AddRating(giveRating);

				double newRating;

				if (Double.TryParse(rating, out newRating))
				{

					if (newRating < 0 || newRating > 10)
					{
						ModelState.AddModelError("rating", "Vinsamlegast sláðu inn tölu á milli 0-10");
					}
					else
					{
						fileRepo.ChangeRating(s.Id, newRating);
					}
				}
				else
				{
					ModelState.AddModelError("rating", "Einkunnin má ekki innihalda bókstafi");
				}
			}
			else {
				ModelState.AddModelError("existingRating", "Aðeins er hægt að gefa skrá einu sinni einkunn");
			}
			return RedirectToAction("AboutSubtitleFile", new { id = s.Id });
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
			newComment._userName = User.Identity.GetUserName();
			newComment._userId = User.Identity.GetUserId();

			commentRepo.AddComment(newComment);
			}
			else {
				ModelState.AddModelError("addText", "Vinsamlegast sláðu inn athugasemd");
			}
			return RedirectToAction("AboutSubtitleFile", new { id = s.Id });
		}

		[HttpPost]
		public ActionResult DeleteComment(int commentID) {

			SubtitleCommentRepo commentRepo = new SubtitleCommentRepo();

			SubtitleComment comment = commentRepo.GetSingleCommentById(commentID);

			commentRepo.RemoveComment(comment);

			return RedirectToAction("AboutSubtitleFile", new { id = comment._textFileId});
		}


        //get
        public ActionResult EditSubtitleFile(int id) {
            return View();
        }
	}
}