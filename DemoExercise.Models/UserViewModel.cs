using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DemoExercise.Models
{
    public class UserViewModel
    {
        public int UserId { get; set; }

        [Required]
        [StringLength(15, MinimumLength = 6, ErrorMessage = "{0} must be between {2} and {1} characters long.")]
        public string Username { get; set; }

        public string Password { get; set; }
    }
}
