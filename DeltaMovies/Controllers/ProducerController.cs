using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DeltaMovies.Controllers
{
    public class ProducerController : Controller
    {
        [Route("Producers")]
        public ActionResult AllProducers()
        {
            return PartialView("AllProducers");
        }
 
        [Route("Producer/Edit/{name}/{id:int?}")]
        public ActionResult CreateEdit(string name, int? id)
        {
            return PartialView("CreateEdit");
        }


        [Route("Producer/Create")]
        public ActionResult CreateEdit()
        {
            return PartialView("CreateEdit");
        }
    }
}