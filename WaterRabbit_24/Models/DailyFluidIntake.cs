using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WaterRabbit_24.Models
{
    public class DailyFluidIntake
    {
        [Key]
        public int DailyFluidIntakeId { get; set; }

        [DataType(DataType.Date)]
        public DateTime IntakeDate { get; set; }

        public int TotalDailyFluid { get; set; }

        //NAVIGATIONAL PROPERTY
        [ForeignKey("User")]
        public string UserId { get; set; }
        public ApplicationUser User { get; set; } //ONE

        //calculates the total daily fluid
        public void CalculateTotalDailyFluid(int fluidVolume)
        {
            TotalDailyFluid += fluidVolume;
        }

        //resets the total daily fluid
        public void ResetTotalDailyFluid()
        {
            TotalDailyFluid = 0;
        }
    }
}