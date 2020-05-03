using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EtainTest.Models
{
    public class SigninModel
    {
        [Display(Name ="Email Address")]
        [Required(ErrorMessage = "Email Address is a required field")]
        public string EmailAddress { get; set; }

        [Required(ErrorMessage = "Password is a required field")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

    }
}