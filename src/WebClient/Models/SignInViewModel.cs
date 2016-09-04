using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Shared.DtoObjects;

namespace WebClient.Models
{
    public class SignInViewModel
    {
        public string ErrorText { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public SignInViewModel()
        {
        }
    }
}