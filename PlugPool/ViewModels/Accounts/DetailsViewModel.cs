using PlugPool.Domain.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace PlugPool.ViewModels.Accounts
{
    public class DetailsViewModel
    {
        public DetailsViewModel() { }

        public DetailsViewModel(Account account)
        {
            accountID = account.accountID;           
            firstName = account.firstName;
            lastName = account.lastName;
            email = account.email;
            createDate = account.createDate;
            updateDate = account.updateDate;
            Profile = account.Profile;
            job = account.Profile.Job.name;
            businessName = account.Profile.businessName;
            location = account.Profile.location;
            description = account.Profile.description;
            //startYear = account.Profile.startYear;
            website = account.Profile.website;
            youtube = account.Profile.youtube;
            additionalInfo = account.Profile.additionalInfo;
            username = account.Profile.userName;
            profileCreateDate = account.Profile.createDate;
        }

        public int accountID { get; set; }

        [DisplayName("First Name")]
        public string firstName { get; set; }

        [DisplayName("Last Name")]
        public string lastName { get; set; }

        [DisplayName("Email")]
        public string email { get; set; }

        [DisplayName("Account Create Date")]
        public DateTime? createDate { get; set; }

        [DisplayName("Last Update Date")]
        public DateTime? updateDate { get; set; }

        public virtual Profile Profile { get; set; }

        [DisplayName("Profile pic")]
        public byte[] image { get; set; }

        [DisplayName("Job")]
        public string job { get; set; }

        [DisplayName("Business Name")]
        public string businessName { get; set; }

        [DisplayName("Location")]
        public string location { get; set; }

        [DisplayName("Description")]
        public string description { get; set; }

        //[DisplayName("Start Year")]
        //public string startYear { get; set; }

        [DisplayName("website")]
        public string website { get; set; }

        [DisplayName("Youtube")]
        public string youtube { get; set; }

        [DisplayName("Additional Info")]
        public string additionalInfo { get; set; }

        [DisplayName("Username")]
        public string username { get; set; }

        [DisplayName("Date Created")]
        public DateTime? profileCreateDate { get; set; }
    }
}