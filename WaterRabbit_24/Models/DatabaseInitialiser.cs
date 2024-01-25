using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WaterRabbit_24.Models
{
    public class DatabaseInitialiser :DropCreateDatabaseAlways<ApplicationDbContext>
    {

        protected override void Seed(ApplicationDbContext context)
        {
            base.Seed(context);

            //if there are no record stored in the Users table
            if(!context.Users.Any())
            {
                UserManager<ApplicationUser> userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

                //creating a few user accounts
                var bob = new ApplicationUser
                {
                    UserName = "bob@gmail.com",
                    Email = "bob@gmail.com",
                    EmailConfirmed = true
                };
                userManager.Create(bob, "member");

                var bill = new ApplicationUser
                {
                    UserName = "bill@gmail.com",
                    Email = "bill@gmail.com",
                    EmailConfirmed = true
                };
                userManager.Create(bill, "member");

                context.SaveChanges();

                //create a few daily fluid intakes
                var day1 = new DailyFluidIntake
                {
                    IntakeDate = DateTime.Now,
                    User = bob,
                    TotalDailyFluid = 250
                };

                context.DailyFluidIntakes.Add(day1);
                context.SaveChanges();

                //Create a few drink categories
                var water = new DrinkCategory { CategoryName = "Water" };
                var hotBeverages = new DrinkCategory { CategoryName = "Hot Beverages" };
                var icedBeverages = new DrinkCategory { CategoryName = "Iced Beverages" };
                var juices = new DrinkCategory { CategoryName = "Juices" };
                var smoothies = new DrinkCategory { CategoryName = "Smoothies" };

                //add them to the DrinkCategories table
                context.DrinkCategories.Add(water);
                context.DrinkCategories.Add(hotBeverages);
                context.DrinkCategories.Add(icedBeverages);
                context.DrinkCategories.Add(juices);
                context.DrinkCategories.Add(smoothies);

                //save the changes to the database
                context.SaveChanges();

                //seeding the database with a few beverages
                context.Beverages.Add(new Beverage()
                {
                    BeverageName = "Still Water",
                    ImageUrl = "/Images/images_sml/glass_water_sml.jpg",
                    BeverageType = BeverageType.Cold,
                    VeselType = "Large Glass",
                    FluidVolume = 180,
                    DrinkCategory = water
                });

                context.Beverages.Add(new Beverage()
                {
                    BeverageName = "Still Water",
                    ImageUrl = "/Images/images_sml/glass_water_sml.jpg",
                    BeverageType = BeverageType.Cold,
                    VeselType = "Small Glass",
                    FluidVolume = 140,
                    DrinkCategory = water
                });

                context.Beverages.Add(new Beverage()
                {
                    BeverageName = "Sparkling Water",
                    ImageUrl = "/Images/images_sml/glass_water_sml.jpg",
                    BeverageType = BeverageType.Cold,
                    VeselType = "Large Glass",
                    FluidVolume = 180,
                    DrinkCategory = water
                });

                context.Beverages.Add(new Beverage()
                {
                    BeverageName = "Sparkling Water",
                    ImageUrl = "/Images/images_sml/glass_water_sml.jpg",
                    BeverageType = BeverageType.Cold,
                    VeselType = "Small Glass",
                    FluidVolume = 140,
                    DrinkCategory = water
                });

                context.Beverages.Add(new Beverage()
                {
                    BeverageName = "Americano Coffee",
                    ImageUrl = "/Images/images_sml/coffee_cup_sml.jpg",
                    BeverageType = BeverageType.Hot,
                    VeselType = "Small cup",
                    FluidVolume = 150,
                    DrinkCategory = hotBeverages
                });

                context.Beverages.Add(new Beverage()
                {
                    BeverageName = "Americano Coffee",
                    ImageUrl = "/Images/images_sml/coffee_cup_sml.jpg",
                    BeverageType = BeverageType.Hot,
                    VeselType = "Regular Mug",
                    FluidVolume = 200,
                    DrinkCategory = hotBeverages
                });

                context.Beverages.Add(new Beverage()
                {
                    BeverageName = "Americano Coffee",
                    ImageUrl = "/Images/images_sml/coffee_cup_sml.jpg",
                    BeverageType = BeverageType.Hot,
                    VeselType = "Large Mug",
                    FluidVolume = 250,
                    DrinkCategory = hotBeverages
                });

                context.SaveChanges();
            }
        }
    }
}