using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WaterRabbit_24.Models;

namespace WaterRabbit_24.Controllers
{
    public class HomeController : Controller
    {

        private ApplicationDbContext context = new ApplicationDbContext();

        public ActionResult SplashScreen()
        {
            return View();
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Contact(Message message)
        {
            if (ModelState.IsValid)
            {
                message.DateOfMessage = DateTime.Now.Date;
                context.Messages.Add(message);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(message);
        }

        public ActionResult Sounds()
        {
            //i will sotre all the audio files here
            List<Sound> sounds = new List<Sound>();

            sounds.Add(new Sound { Name = "Ocean", FilePath = "~/Sounds/oceano2.mp3" });
            sounds.Add(new Sound { Name = "Water Flowing", FilePath = "~/Sounds/water-flowing.mp3" });
            sounds.Add(new Sound { Name = "Water Running", FilePath = "~/Sounds/water_running_loop.mp3" });
            sounds.Add(new Sound { Name = "Waterfall", FilePath = "~/Sounds/waterfall.mp3" });

            //send the list of sounds to the view
            return View(sounds);
        }
    }
}