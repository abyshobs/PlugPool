using PlugPool.Domain.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace PlugPool.ViewModels.Profiles
{
    public class DetailsViewModel
    {
        public DetailsViewModel() { }

        public DetailsViewModel(IEnumerable<Job> jobs) { }

        public DetailsViewModel(Profile profile)
        {

            Job = profile.Job;
            profileID = profile.profileID;
            accountID = profile.accountID;
            jobID = profile.jobID;
            businessName = profile.businessName;
            location = profile.location;
            description = profile.description;
            //startYear = profile.startYear;
            website = profile.website;
            youtube = profile.youtube;
            additionalInfo = profile.additionalInfo;
            userName = profile.userName;
            createDate = profile.createDate;
            Account = profile.Account;
        }


        public Job Job { get; set; }
        public int profileID { get; set; }
        public int accountID { get; set; }

        [DisplayName("Job")]
        public int jobID { get; set; }

        [DisplayName("Business Name")]
        public string businessName { get; set; }

        [DisplayName("Location")]
        public string location { get; set; }

        [DisplayName("Description")]
        public string description { get; set; }

        //[DisplayName("Start Year")]
        //public string startYear { get; set; }

        [DisplayName("Website")]
        public string website { get; set; }

        [DisplayName("Youtube")]
        public string youtube { get; set; }

        [DisplayName("Additional Information")]
        public string additionalInfo { get; set; }

        [DisplayName("Username")]
        public string userName { get; set; }

        [DisplayName("Start Year")]
        public DateTime? createDate { get; set; }

        public Account Account { get; set; }
    }
}