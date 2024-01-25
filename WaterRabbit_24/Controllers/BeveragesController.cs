using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WaterRabbit_24.Models;
using System.Data.Entity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace WaterRabbit_24.Controllers
{
    public class BeveragesController : Controller
    {
        private ApplicationDbContext context = new ApplicationDbContext();

        // GET: Beverages
        public ActionResult Index()
        {
            //getting all the beverages
            var beverages = context.Beverages.ToList();

            foreach (var item in beverages)
            {
                context.Entry(item).Reload(); //refresh entities
            }

            //send all the drink categories in a viewbag
            ViewBag.Categories = context.DrinkCategories.ToList();

            //send the users updated daily fluid intake to display on the page
            //get the user id
            string id = User.Identity.GetUserId();
            //if user is logged in (id is not null)
            if (id != null)
            {
                //then get the current user's daily fluid intake for the current date
                DailyFluidIntake dailyIntake = context.DailyFluidIntakes
                    .Where(d => DbFunctions.TruncateTime(d.IntakeDate) == DateTime.Today)
                    .SingleOrDefault(d=>d.UserId.Equals(id));

                //if there is a current daily fluid intake record then send the total fluid at the view using a viewbag.
                if (dailyIntake != null)
                {
                    ViewBag.TotalFluid = dailyIntake.TotalDailyFluid;
                }
            }

            //send the products to the Products view
            return View("BeveragesView", beverages);
        }

        public ActionResult Beverages(int? id)
        {
            //getting all the beverages that are in a specific category
            var beverages = context.Beverages.Where(b => b.DrinkCategoryId == id).ToList();

            //also don't forget to send all the drink categories in a viewbag
            ViewBag.Categories = context.DrinkCategories.ToList();

            //send the users updated daily fluid intake to display on the page
            //get the user id
            string userId = User.Identity.GetUserId();

            //if user is logged in (id is not null)
            if (userId != null)
            {
                //then get the current user's daily fluid intake for the current date
                DailyFluidIntake dailyIntake = context.DailyFluidIntakes
                    .Where(d => DbFunctions.TruncateTime(d.IntakeDate) == DateTime.Today)
                    .SingleOrDefault(d => d.UserId.Equals(userId));

                //if there is a current daily fluid intake record then send the total fluid at the view using a viewbag.
                if (dailyIntake != null)
                {
                    ViewBag.TotalFluid = dailyIntake.TotalDailyFluid;
                }
            }
            return View("BeveragesView", beverages);
        }

        public ActionResult AddToDailyFluid(int fluid)
        {
            UserManager<ApplicationUser> userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            //get the current user's ID
            string userId = userManager.FindByEmail(User.Identity.GetUserName()).Id;

            //get the user
            ApplicationUser user = context.Users.Find(userId);

            //get the Daily fluid intake of current date
            DailyFluidIntake dailyIntake = context.DailyFluidIntakes
                    .Where(d => DbFunctions.TruncateTime(d.IntakeDate) == DateTime.Today)
                    .SingleOrDefault(d => d.UserId.Equals(userId));

            //if this is the first entry of the day then create a dailyfluidintake and add the fluid of the totalfluid
            if (dailyIntake == null)
            {
                DailyFluidIntake dfi = new DailyFluidIntake
                {
                    IntakeDate = DateTime.Now.Date,
                    User = user
                };

                dfi.CalculateTotalDailyFluid(fluid);
                context.DailyFluidIntakes.Add(dfi);
                context.SaveChanges();
                return RedirectToAction("Index");
            }

            //if there is already an entry for the current date then just update the total daily fluid
            dailyIntake.CalculateTotalDailyFluid(fluid);
            context.Entry(dailyIntake).State = EntityState.Modified;
            context.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult RestDailyFluid()
        {
            UserManager<ApplicationUser> userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            //get the current user's ID
            string userId = userManager.FindByEmail(User.Identity.GetUserName()).Id;

            //get the user
            ApplicationUser user = context.Users.Find(userId);

            //get the Daily fluid intake of current date
            DailyFluidIntake dailyIntake = context.DailyFluidIntakes
                    .Where(d => DbFunctions.TruncateTime(d.IntakeDate) == DateTime.Today)
                    .SingleOrDefault(d => d.UserId.Equals(userId));

            //if this is the first entry of the day then create a dailyfluidintake and add the fluid of the totalfluid
            if (dailyIntake == null)
            {
                DailyFluidIntake dfi = new DailyFluidIntake
                {
                    IntakeDate = DateTime.Now.Date,
                    User = user
                };

                dfi.ResetTotalDailyFluid();
                context.DailyFluidIntakes.Add(dfi);
                context.SaveChanges();
                return RedirectToAction("Index");
            }

            //if there is already an entry for the current date then just update the total daily fluid
            dailyIntake.ResetTotalDailyFluid();
            context.Entry(dailyIntake).State = EntityState.Modified;
            context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
    
}