using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Textaland.Controllers
{
    public class SubtitleFileController : Controller
    {
        //Get
        [Authorize]
        public ActionResult UploadSubtitleFile()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public ActionResult UploadSubtitleFile(HttpPostedFileBase file)
        {
            if (file != null && file.ContentLength > 0)
            {
                // extract only the fielname
                var fileName = Path.GetFileName(file.FileName);
                // store the file inside ~/App_Data/uploads folder
                var path = Path.Combine(Server.MapPath("~/App_Data/uploads"), fileName);
                file.SaveAs(path);
            }
            // redirect back to the index action to show the form once again
            return RedirectToAction("Upload");
        }
	}
}