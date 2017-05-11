using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PlugPool.ViewModels.Accounts
{
    public class RecoverPasswordViewModel
    {
        [Required(ErrorMessage = "Please enter an email address")]
        [Display(Name = "Email")]
        [EmailAddress]
        public string email { get; set; }
    }
}