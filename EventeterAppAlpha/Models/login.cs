using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace EventeterAppAlpha.Models
{
    public class login
    {
        [Required(ErrorMessage = "Please provide a password")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Please provide an email")]
        public string Email { get; set; }
    }
}