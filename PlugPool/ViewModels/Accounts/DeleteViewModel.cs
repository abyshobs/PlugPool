using PlugPool.Domain.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace PlugPool.ViewModels.Accounts
{
    public class DeleteViewModel
    {
        public DeleteViewModel() { }

        public DeleteViewModel(Account account)
        {
            accountID = account.accountID;
            firstName = account.firstName;
            lastName = account.lastName;
            email = account.email;
            createDate = account.createDate;
            updateDate = account.updateDate;
        }

        public int accountID { get; set; }

        [DisplayName("First Name")]
        public string firstName { get; set; }

        [DisplayName("Last Name")]
        public string lastName { get; set; }

        [DisplayName("Email")]
        public string email { get; set; }

        [DisplayName("Date Created")]
        public DateTime? createDate { get; set; }

        [DisplayName("Last Update date")]
        public DateTime? updateDate { get; set; }

    }
}