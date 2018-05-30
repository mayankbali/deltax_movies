using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DeltaMovies.Controllers
{
    public class ActorController : Controller
    {
        // GET: Actor
        [Route("Actors")]
        public ActionResult AllActors()
        {
            return PartialView("AllActors");
        }
 
        [Route("Actor/Edit/{name}/{id:int?}")]
        public ActionResult CreateEdit(string name, int? id)
        {
            return PartialView("CreateEdit");
        }


        [Route("Actor/Create")]
        public ActionResult CreateEdit()
        {
            return PartialView("CreateEdit");
        }
    }
}