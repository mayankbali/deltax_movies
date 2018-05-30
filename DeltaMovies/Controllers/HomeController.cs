using DeltaMovies.DeltaAPI.DbEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DeltaMovies.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Default()
        {
            return View("Default");
        }

        public ActionResult TreeResult()
        {
            return View();
        }

        [HttpPost]
        public virtual ActionResult GetTree(string dir, string relType)
        {
            var model = new List<FileTreeViewModel>();
            if (dir == "Server")
            {
                model.Add(new FileTreeViewModel()
                {
                    relType = "Server",
                    IsDirectory = true,
                    Name = "Server001",
                    Ext = null,
                    Path = "Server001"
                });

                model.Add(new FileTreeViewModel()
                {
                    relType = "Server",
                    IsDirectory = true,
                    Name = "Server002",
                    Ext = null,
                    Path = "Server002"
                });
            }

            if (dir == "Server001")
            {
                model.Add(new FileTreeViewModel()
                {
                    relType = "DataBase",
                    IsDirectory = true,
                    Name = "nVision",
                    Ext = "bat",
                    Path = "nVision"
                });
                model.Add(new FileTreeViewModel()
                {
                    relType = "DataBase",
                    IsDirectory = true,
                    Name = "nVisionController",
                    Ext = "bat",
                    Path = "nVision"
                });
            }

            if (dir == "Server002")
            {
                model.Add(new FileTreeViewModel()
                {
                    relType = "DataBase",
                    IsDirectory = true,
                    Name = "RentKicker",
                    Ext = "bat",
                    Path = "RentKicker"
                });
                model.Add(new FileTreeViewModel()
                {
                    relType = "DataBase",
                    IsDirectory = true,
                    Name = "SSPLEntities",
                    Ext = "bat",
                    Path = "SSPLEntities"
                });
            }
            return PartialView("_TreeResultHtml", model);
        }

    }
}
public class FileTreeViewModel
{
    public string Name { get; set; }
    public string relType { get; set; }
    public string Ext { get; set; }
    public string Path { get; set; }
    public bool IsDirectory { get; set; }

    public string PathAltSeparator()
    {
        return Path.Replace("\\", "/");
    }
}