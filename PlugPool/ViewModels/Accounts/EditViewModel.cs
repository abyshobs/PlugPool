using PlugPool.Domain.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PlugPool.ViewModels.Accounts
{
    public class EditViewModel
    {
        public EditViewModel(Account account)
        {
            accountID = account.accountID;
            firstName = account.firstName;
            lastName = account.lastName;
            updateDate = account.updateDate;
        }

        public EditViewModel() { }

        public int accountID { get; set; }

        [DisplayName("First Name")]
        [Required(ErrorMessage = "This field cannot be left blank")]
        public string firstName { get; set; }

        [DisplayName("Last Name")]
        [Required(ErrorMessage = "This field cannot be left blank")]
        public string lastName { get; set; }

        public DateTime? updateDate { get; set; }
    }
}