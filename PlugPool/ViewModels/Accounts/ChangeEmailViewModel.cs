using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PlugPool.ViewModels.Accounts
{
    public class ChangeEmailViewModel
    {
        [Required(ErrorMessage = "Please enter an email address")]
        [Display(Name = "New Email")]
        [EmailAddress]
        public string email { get; set; }

        public int accountID { get; set; }
    }
}