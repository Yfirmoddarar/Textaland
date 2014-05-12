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
        public ActionResult Upload () {

            ViewBag.ListOfTypes = new SelectList(new[] {
                new {Id = "1", Name = "Kvikmynd"},
                new {Id = "2", Name = "Þáttur"},
            }, "Id", "Name");

            ViewBag.ListOfLanguages = new SelectList(new[] {
                new {Id = "1", Name = "ENG"},
                new {Id = "2", Name = "ISL"},
            }, "Id", "Name");

            return View();
        }

        
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Upload (SubtitleFile sf, HttpPostedFileBase file) {

            if (file != null && file.ContentLength > 0) {
                var filename = Path.GetFileName(file.FileName);
                var path = Path.Combine(Server.MapPath("~/App_Data/uploads"), filename);
                file.SaveAs(path);

                    SubtitleFileRepo sfr = new SubtitleFileRepo();

                    sfr.AddSubtitle(sf);


                }
                else {
                    RedirectToAction("UploadError", "SubtitleFile");
            }


            return RedirectToAction("FrontPage", "Home");
        }

        public ActionResult UploadError() {
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

		//Operation that shows details about subtitle files
		[HttpPost]
		public ActionResult AboutSubtitleFile(int? id){

			SubtitleFileRepo sfr = new SubtitleFileRepo();

			var file = sfr.GetSubtitleFileById(id.Value);
			//AppDataContext db  = new AppDataContext();
			//SubtitleFile file = db.SubtitleFiles.Find(id);
			//Getting all the comments that hafa a specific subtitleFile id.
			SubtitleCommentRepo commentRepo = new SubtitleCommentRepo();

			var allComments = from c in commentRepo.GetCommentById(id.Value)
							  orderby c._dateAdded ascending
							  select c;
			ViewBag.AllComments = allComments;
			
			return View(file);
		}

		//This function adds a new comment to a specific text file.
		[HttpPost]
		public ActionResult AddComment(SubtitleFile s, string addText)
		{
			SubtitleCommentRepo commentRepo = new SubtitleCommentRepo();

			SubtitleComment newComment = new SubtitleComment();

			SubtitleFileRepo fileRepo = new SubtitleFileRepo();

			//The new comment gets an ID and text.
			newComment._text = addText;
			newComment._textFileId = s.Id;

			commentRepo.AddComment(newComment);

			return AboutSubtitleFile(s.Id);
		}
	}
}