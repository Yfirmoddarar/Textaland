using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Textaland.DataAccessLayer;
using Textaland.Models;
using Microsoft.AspNet.Identity;
using Textaland.Interface;


namespace Textaland.Controllers
{
    public class SubtitleFileController : Controller {

        private readonly ISubtitleFileRepo _sfr;
        private readonly ISubtitleLineRepo _slr; 
        private readonly ISubtitleCommentRepo _scr;
        private readonly IRatingRepo _srr;

        public SubtitleFileController() {
            _sfr = SubtitleFileRepo.Instance;
            _slr = SubtitleLineRepo.Instance;
            _scr = SubtitleCommentRepo.Instance;
            _srr = RatingRepo.Instance;
        }

        public SubtitleFileController(ISubtitleFileRepo sfr,
            ISubtitleLineRepo slr, ISubtitleCommentRepo scr,
            IRatingRepo srr) {
                _sfr = sfr;
                _slr = slr;
                _scr = scr;
                _srr = srr;
        }


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
                    _name = uc.fName,
                    _description = uc.fDescription,
                    _hearingImpaired = uc._hardOfHearing,
                    _type = uc.fType,
                    _languageFrom = uc.fLanguage		
                };

                sf.Id = 0;
                sf._dateAdded = DateTime.Now;
				sf._userName = User.Identity.GetUserName();
                sf._userId = User.Identity.GetUserId();

                    _sfr.AddSubtitle(sf);

                int newId = sf.Id;

                if (ReadFile(uc._file, newId)) {
                    return RedirectToAction("AboutSubtitleFile", "SubtitleFile", new { id = newId});
                }
                else {

                    //Remove what was already uploaded, since file is of wrong format.

                    _sfr.RemoveSubtitle(newId);
                    _slr.RemoveLines(newId);

                    return RedirectToAction("FileError", "SubtitleFile");
                }
            }
            else {
                return RedirectToAction("UploadError", "SubtitleFile");
            }
        }

        [Authorize]
        public bool ReadFile(HttpPostedFileBase file, int id) {
            try {
                using (StreamReader sr = new StreamReader(file.InputStream, System.Text.Encoding.UTF8, true)) {
                    
                    while (!(sr.EndOfStream)) {
                        SubtitleLine sl = new SubtitleLine();

                        try {
                            string line = sr.ReadLine();
                            int lId = Convert.ToInt32(line);
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
                                    string emptyLine = sr.ReadLine();
                                }
                            }
                        }
                        
                        sl._textFileId = id;
                        _slr.AddLine(sl);

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

			//The list of files that are shown in the view.  Skip 'x' many files
			//according to the number the actionresult receives, and then take 10 after
			//the ones that are skipped
			var allSubs = (from c in _sfr.GetAllSubtitles()
							where c._readyForDownload == true
							select c).Skip(num * 10).Take(10);

			//This is a list that is used for the pagination in the view. Basically just
			//to count how many files there are
			var subsCount = from c in _sfr.GetAllSubtitles()
                            where c._readyForDownload == true
							select c;

			ViewBag.numOfSubs = subsCount;

			TempData["pageNumber"] = num;

			ViewBag.allSubs = allSubs;

			return View();
		}

		public ActionResult InTranslationFiles(int num) {

			//The list of files that are shown in the view.  Skip 'x' many files
			//according to the number the actionresult receives, and then take 10 after
			//the ones that are skipped
			var allInTranslation = (from f in _sfr.GetAllSubtitles()
									where f._readyForDownload == false
									select f).Skip(num * 10).Take(10);

			//This is a list that is used for the pagination in the view. Basically just
			//to count how many files there are
			var countInTranslation = from f in _sfr.GetAllSubtitles()
                                     where f._readyForDownload == false
									 select f;

			TempData["pageNum"] = num;
			ViewBag.InTranslation = allInTranslation;
			ViewBag.numInTranslation = countInTranslation;

			return View();
		}

		//Operation that shows details about subtitle files
		
		public ActionResult AboutSubtitleFile(int id){
			
			//We print out error messages for the following errors..

			//if User tries to vote twice
			if (TempData["existingRating"] != null) {
				ModelState.AddModelError("existingRating", TempData["existingRating"].ToString());
			}
			//if the user entered letters in the rating
			else if (TempData["notNumerical"] != null) {
				ModelState.AddModelError("notNumerical", TempData["notNumerical"].ToString());
			}
			//if the user types in a number outside of 0 - 10
			else if (TempData["wrongRating"] != null) {
				ModelState.AddModelError("wrongRating", TempData["wrongRating"].ToString());
			}
			//if the user types in a empty comment
			else if (TempData["addText"] != null) {
				ModelState.AddModelError("addText", TempData["addText"].ToString());
			}
			//"file" vill be the SubtitleFile that has the ID the same as "id".
			var file = _sfr.GetSubtitleFileById(id);

			if(file == null) {
				return HttpNotFound();
			}
			//Getting all the comments that hafa a specific subtitleFile id.

            ViewBag.AllComments = _scr.GetCommentsById(id);
			
			return View(file);
		}

		[HttpPost]
        [Authorize]
		public ActionResult GiveRating(SubtitleFile s, string rating) {

			Rating giveRating = new Rating();

			var userID = User.Identity.GetUserId();

			//Get all the ratings to see if the current user has already liked this file
			var allRatings = from r in _srr.GetAllRatings()
							 where r._userId == userID &&
							 r._textFileId == s.Id
							 select r;

			//If the user hasn't liked the file the new rating is added to the database and
			//the current rating of the file is changed according to the new rating
			if (allRatings.Count() == 0) {

				double newRating;

				//check if the rating conatins any non-numerical letter
				if (Double.TryParse(rating, out newRating)) {

					//if the rating is purely numerical we check if it is between 0 and 10
					//If it isn't we print out an error message, otherwise we change the rating
					if (newRating < 0 || newRating > 10) {
						TempData["wrongRating"] = "Vinsamlegast sláðu inn tölu á milli 0-10";
					}
					else {
						giveRating._textFileId = s.Id;
						giveRating._userId = userID;
						_srr.AddRating(giveRating);
						_sfr.ChangeRating(s.Id, newRating);
					}
				}
				//if the rating isn't numerical we print out an error message
				else {
					TempData["notNumerical"] = "Einkunnin má ekki innihalda bókstafi";
				}
			}
			else {
				TempData["existingRating"] = "Aðeins er hægt að gefa skrá einu sinni einkunn";
			}

			//finally we redirect back to the file
			return RedirectToAction("AboutSubtitleFile", new { id = s.Id });
		}

		//This function adds a new comment to a specific text file.
		[HttpPost]
        [Authorize]
		public ActionResult AddComment(SubtitleFile s, string addText) {
			SubtitleComment newComment = new SubtitleComment();

			if (!String.IsNullOrEmpty(addText)) {

			//the new comment gets all the right attributes and is then added to the database
			newComment._text = addText;

			newComment._textFileId = s.Id;

			newComment._userName = User.Identity.GetUserName();

			newComment._userId = User.Identity.GetUserId();

			_scr.AddComment(newComment);
			}
			else {
				TempData["addText"] = "Athugasemdin var tóm. Vinsamlegast sláðu inn aftur.";
			}
			return RedirectToAction("AboutSubtitleFile", new { id = s.Id });
		}

		[HttpPost]
        [Authorize]
		public ActionResult DeleteComment(int commentID) {

			SubtitleComment comment = _scr.GetSingleCommentById(commentID);

			_scr.RemoveComment(comment);

			return RedirectToAction("AboutSubtitleFile", new { id = comment._textFileId});
		}

        //Generate a filelocatio path for writing and downloading
        public string GeneratePath(int id) {

            string path = Server.MapPath(Url.Content("~/App_Data/downloads/"));

            path += _sfr.GetSubtitleFileById(id)._name.Replace(" ", "");
            path += (id + ".srt");

            return path;
        }

        [Authorize]
        public ActionResult FinishFile(int id) {
            SubtitleFile sf = _sfr.GetSubtitleFileById(id);
            if (sf._userId == User.Identity.GetUserId()) {
                _sfr.setInTranslation(false, id, User.Identity.GetUserId());
                _sfr.setDownload(true, id);
                _sfr.setLanguage(sf._languageTo, id);
            }
            else {
                ModelState.AddModelError("FinishError", "Only original uploader can delete the file.");
                return RedirectToAction("EditSubtitleFile", new { id = id, num = 0 });
            }
            return RedirectToAction("AboutSubtitleFile", new { id = id });
        }

        [Authorize]
        public ActionResult DeleteFile(int id) {
            SubtitleFile sf = _sfr.GetSubtitleFileById(id);
            if (sf._userId == User.Identity.GetUserId()) {
                _slr.RemoveLines(id);
                _sfr.RemoveSubtitle(id);
            }
            else {
                ModelState.AddModelError("DeleteError", "Only original uploader can delete the file.");
            }
            return RedirectToAction("AllSubtitleFiles", new { num = 0 });
        }

        public ActionResult Download(int id) {

            WriteToFile(id);

            _sfr.wasDownloaded(id);

            string FileName = _sfr.GetSubtitleFileById(id)._name.Replace(" ", "");
            FileName += (id + ".srt");

            string path = "~/App_Data/downloads/" + FileName;

            return new DownloadResult { VirtualPath = path, FileDownloadName = FileName };
        }

        public void WriteToFile(int id) {
            FileInfo newfile = new FileInfo(GeneratePath(id));

            if (!newfile.Exists) {
                using (StreamWriter sw = newfile.CreateText()) {
                    foreach (var line in _slr.GetLinesById(id)) {
                        sw.WriteLine(line._lineNumber);
                        sw.WriteLine(line._time);
                        sw.WriteLine(line._line1);
                        if (line._line2 != null) {
                            sw.WriteLine(line._line2);
                            if (line._line3 != null) {
                                sw.WriteLine(line._line3);
                            }
                        }
                        sw.WriteLine("");
                    }
                    sw.Close();
                }
            }
        }

        [Authorize]
        public ActionResult TranslateFile(int id) {
            SubtitleFile sf = _sfr.GetSubtitleFileById(id);

            SubtitleFile sfTranslation = sf;
            if (sf._languageFrom == "ENG") {
                sfTranslation._languageTo = "ISL";
            } else {
                sfTranslation._languageTo = "ENG";
            }

            sfTranslation._userId = User.Identity.GetUserId();
            sfTranslation._readyForDownload = false;
            sfTranslation._dateAdded = DateTime.Now;
            sfTranslation._originalId = id;
        
            _sfr.AddSubtitle(sfTranslation);

            _sfr.setCounters(sfTranslation.Id);

            _slr.copyLines(sfTranslation.Id, id);

            return RedirectToAction("EditSubtitleFile", new { id = sfTranslation.Id, num = 0 });
        }


        //get
        [Authorize]
        public ActionResult EditSubtitleFile(int id, int num) {
            SubtitleFile sf = _sfr.GetSubtitleFileById(id);

            if (sf != null) {
                if (!(sf._inTranslation) || DateTime.Now > sf._dateAdded.AddMinutes(5) || User.Identity.GetUserId() == sf._lastTranslatorId) {
                    SubtitleFileEditView sfev = new SubtitleFileEditView();

                    _sfr.setTime(sf.Id);
                    _sfr.setInTranslation(true, sf.Id, User.Identity.GetUserId());

                    sfev.fileId = id;
                    sfev.fileName = sf._name;
                    sfev.languageFrom = sf._languageFrom;
                    sfev.languageTo = sf._languageTo;
                    sfev.subtitleLines = _slr.GetLinesById(id).ToList();
                    sfev.originalLines = _slr.GetLinesById(sf._originalId).ToList();

                    ViewBag.pageNum = num;
                    int numberOfPages = (_slr.GetLinesById(id).Count() / 10);
                    ViewBag.numPages = numberOfPages;

                    return View(sfev);
                }
            }
            TempData["Error"] = true;
            return RedirectToAction("FrontPage", "Home");
        }

        public ActionResult ChangePage(int id, int num) {
            _sfr.setInTranslation(false, id, User.Identity.GetUserId());
            return RedirectToAction("EditSubtitleFile", new { id = id, num = num });
        }

        public ActionResult CloseFile(int id) {
            _sfr.setInTranslation(false, id, User.Identity.GetUserId());
            return RedirectToAction("FrontPage", "Home");
        }

        [HttpPost]
        [Authorize]
        public ActionResult EditLine(FormCollection fc) {
            SubtitleFile sf = _sfr.GetSubtitleFileById(Convert.ToInt32(fc["fileId"]));

            if (sf._lastTranslatorId == User.Identity.GetUserId()) {
                SubtitleLine sl = new SubtitleLine();
                _sfr.setInTranslation(false, sf.Id, User.Identity.GetUserId());

                sl.Id = Convert.ToInt32(fc["lineId"]);
                sl._line1 = fc["line1"];
                sl._line2 = fc["line2"];
                sl._line3 = fc["line3"];

                _slr.UpdateLine(sl);
            }
            return RedirectToAction("EditSubtitleFile", "SubtitleFile", new { id = fc["fileId"], num = 0 });    
        }
	}
}