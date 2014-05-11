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
			SubtitleFileRepo allSubtitles = new SubtitleFileRepo();

			allSubtitles.GetAllSubtitles();

			return View(allSubtitles);
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

	}
}