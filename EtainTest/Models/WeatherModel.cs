using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EtainTest.Models
{
    public class WeatherModel
    {
        [Display(Name ="Date")]
        public string Applicable_Date { get; set; }
        [Display(Name ="Weather")]
        public string Weather_State_Name { get; set; }
        [Display(Name = "")]
        public string WeatherImage { get; set; }
        public string LastSyncTime { get; set; }
    }
}