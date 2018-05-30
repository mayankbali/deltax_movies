using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DeltaMovies.Framework.Extension;

namespace DeltaMovies.Controllers
{
    public class MovieController : Controller
    {
        private string[] imageExtensions = new string[] { ".jpeg", ".jpg", ".bmp", ".tiff", ".png", ".gif" };
        // GET: Movie
        [Route("Movies")]
        public ActionResult AllMovies()
        {
            return PartialView("AllMovies");
        }

        [Route("Movie/Edit/{name}/{id:int?}")]
        public ActionResult CreateEdit(string name, int? id)
        {
            return PartialView("CreateEdit");
        }


        [Route("Movie/Create")]
        public ActionResult CreateEdit()
        {
            return PartialView("CreateEdit");
        }




        [Route("Movie/SaveMoviePoster")]
        public ActionResult SaveMoviePoster(IEnumerable<HttpPostedFileBase> moviePoster)
        {
            string fileGuid = Guid.NewGuid().ToString(), fileName = string.Empty, fileExtension = string.Empty;
            int fileLength = 0;
            bool uploaded = false;
            if (moviePoster.IsNotNullOrEmpty())
            {
                try
                {
                    string subPath = Server.MapPath(AppSettings.PosterFilePath);
                    if (!System.IO.Directory.Exists(subPath))
                        System.IO.Directory.CreateDirectory(subPath);

                    var file = moviePoster.FirstOrDefault();
                    if (imageExtensions.Contains(Path.GetExtension(file.FileName).ToLower()))
                    {
                        fileExtension = Path.GetExtension(file.FileName).ToLower();
                        fileName = fileGuid + fileExtension;
                        fileLength = file.ContentLength;
                        file.SaveAs(Path.Combine(subPath, fileName));
                        uploaded = true;
                    }
                    else
                    {
                        throw new Exception("Only Image file is allowed!");
                    }

                }
                catch (Exception Error)
                {
                    throw new Exception(Error.Message);
                }
            }

            return Json(new
            {
                FileName = fileName,
                FileExtension = fileExtension,
                FileSize = fileLength,
                uploaded = uploaded
            }, JsonRequestBehavior.AllowGet);
        }

        [Route("Movie/RemoveMoviePoster")]
        public ActionResult RemoveMoviePoster(string[] fileNames)
        {
            if (fileNames != null)
            {
                foreach (var fullName in fileNames)
                {
                    var fileName = Path.GetFileName(fullName);
                    var physicalPath = Path.Combine(Server.MapPath(AppSettings.PosterFilePath), fileName);
                    if (System.IO.File.Exists(physicalPath))
                    {
                        System.IO.File.Delete(physicalPath);
                    }
                }
            }
            return Content("");
        }
    }
}