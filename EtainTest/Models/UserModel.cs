using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EtainTest.Models
{
    public class UserModel
    {
        [Required(ErrorMessage ="First Name is a required field.")]
        [Display(Name ="First Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is a required field.")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email Address is a required field.")]
        [Display(Name = "Email Address")]
        [DataType(DataType.EmailAddress)]
        public string EmailAddress { get; set; }

        [Compare("EmailAddress", ErrorMessage ="Email and Confirm Email do not match.")]
        [Display(Name = "Confirm Email")]
        public string ConfirmEmail { get; set; }

        [DataType(DataType.Password)]
        [Required]
        [StringLength(50,MinimumLength = 6, ErrorMessage ="Your password is not long enough.")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password and Confirm Password do not match.")]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }
    }
}