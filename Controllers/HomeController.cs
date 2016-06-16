using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RPMTest.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            using (var db = new RPMTest.Models.RPMTestDB())
            {
                Models.Thing myThing = new Models.Thing {Id = 1, Name = "Test thing", Total = 3 };

                db.Thing.Add(myThing);
                db.SaveChanges();

                ViewData.Add("Thing1", myThing);
            }

            return View();
        }
    }
}