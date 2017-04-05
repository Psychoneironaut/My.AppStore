using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace My.AppStore.Models
{
    public class ResetPasswordViewModel
    {
        public ResetPasswordViewModel()
        {

        }

        public ResetPasswordViewModel(string code, string email)
        {
            Code = code;
            EmailAddress = email;
        }

        [Required]
        [EmailAddress]
        public string EmailAddress { get; set; }
        public string Code { get; set; }
        public UserManager<User> ManagerSent { get; set; }

        [Required]
        [MinLength(7)]
        public string NewPassword { get; set; }

        [Required]
        [MinLength(7)]
        public string ConfirmPassword { get; set; }
    }
}