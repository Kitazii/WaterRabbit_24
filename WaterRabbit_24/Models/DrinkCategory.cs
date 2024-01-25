using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WaterRabbit_24.Models
{
    public class DrinkCategory
    {
        public int DrinkCategoryId { get; set; }

        [Display(Name = "Category")]
        public string CategoryName { get; set; }

        //naviagational property
        public List<Beverage> Beverages { get; set; } //many
    }
}