using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WaterRabbit_24.Models
{
    public class Beverage
    {
        public int BeverageId { get; set; }

        [Display(Name = "Beverage")]
        public string BeverageName { get; set; }

        [Display(Name = "Beverage Type")]
        public BeverageType BeverageType { get; set; } //hot, cold

        [Display(Name = "Drinking Vessel Type")]
        public string VeselType { get; set; } //small wine glass, small glass, small cup, large glass, regular mug, large mug, pint glass.

        [Display(Name = "Fluid Volume")]
        public int FluidVolume { get; set; } //120ml, 140ml, 150ml, 180ml, 200ml, 250ml, 500ml

        [Display(Name = "Image")]
        public string ImageUrl { get; set; } //storing path to product image in the database

        //navigational property
        [ForeignKey("DrinkCategory")]
        public int DrinkCategoryId { get; set; }
        public DrinkCategory DrinkCategory { get; set; }
    }

    public enum BeverageType { Hot, Cold}
}