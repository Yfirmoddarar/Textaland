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

                //int newId = 1;

                ////if the list isn't empty the new comment gets the ID according to 
                ////the number of files
                //if (sfr.GetAllSubtitles().Count() > 0) {
                //    newId = sfr.GetAllSubtitles().Max(x => x.Id) + 1;
                //}
                sf.Id = 0;
                sf._dateAdded = DateTime.Now;
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


        //Generate a filelocatio path for writing and downloading
        public string GeneratePath(int id) {

            SubtitleFileRepo sfr = new SubtitleFileRepo();

            string path = Server.MapPath(Url.Content("~/App_Data/uploads/"));

            path += sfr.GetSubtitleFileById(id)._name.Replace(" ", "");
            path += ".srt";

            return path;
        }
    
        public ActionResult Download(int id) {

            WriteToFile(id);

            SubtitleFileRepo sfr = new SubtitleFileRepo();

            string FileName = sfr.GetSubtitleFileById(id)._name.Replace(" ", "");
            FileName += ".srt";

            string path = "~/App_Data/uploads/" + FileName;

            return new DownloadResult 
            { VirtualPath=path, FileDownloadName = FileName };
        }

        public void WriteToFile(int id) {
            SubtitleLineRepo slr = new SubtitleLineRepo();


            FileInfo newfile = new FileInfo(GeneratePath(id));

            if (!newfile.Exists) {
                using (StreamWriter sw = newfile.CreateText()) {
                    foreach (var line in slr.GetLinesById(id)) {
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

			var allComments = from c in commentRepo.GetCommentById(id)
							  orderby c._dateAdded ascending
							  select c;
			ViewBag.AllComments = allComments;
			
			return View(file);
		}

		[HttpPost]
		public ActionResult GiveRating(SubtitleFile s, string rating)
		{
			SubtitleFileRepo fileRepo = new SubtitleFileRepo();

			RatingRepo rateRepo = new RatingRepo();

			Rating newRating = new Rating();

			var userID = User.Identity.GetUserId();

			var getRatings = from r in rateRepo.GetAllRatings()
							 where r._userId == userID &&
							 r._textFileId == s.Id
							 select r;

			if (getRatings.Count() == 0) {

				newRating._userId = userID;
				newRating._textFileId = s.Id;

				rateRepo.AddRating(newRating);

				double giveRating;

				if (Double.TryParse(rating, out giveRating))
				{

					if (giveRating < 0 || giveRating > 10)
					{
						ModelState.AddModelError("rating", "Vinsamlegast sláðu inn tölu á milli 0-10");
					}
					else
					{
						fileRepo.ChangeRating(s.Id, giveRating);
					}
				}
				else
				{
					ModelState.AddModelError("rating", "Einkunnin má ekki innihalda bókstafi");
				}
			}
			else {
				ModelState.AddModelError("existingRating", "Aðeins má gefa skrá einkunn einu sinni");
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