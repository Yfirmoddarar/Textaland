using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Textaland.DataAccessLayer;

namespace Textaland.Controllers
{
    public class SubtitleFileController : Controller
    {
        //Get
        public ActionResult Upload () {

            ViewBag.ListOfTypes = new SelectList(new[] {
                new {Id = "1", Name = "Kvikmynd"},
                new {Id = "2", Name = "Þáttur"},
            }, "Id", "Name");

            ViewBag.ListOfLanguages = new SelectList(new[] {
                new {Id = "1", Name = "ISL"},
                new {Id = "2", Name = "ENG"},
            }, "Id", "Name");

            return View();
        }

        /*
        //Post
        [HttpPost]
        public  ActionResult upload (string name, string description, string language, ) {

        }
        */
	}
}