using PlugPool.Domain.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace PlugPool.ViewModels.Profiles
{
    public class EditViewModel
    {
        public EditViewModel() { }

        public EditViewModel(Profile profile, IEnumerable<Job> jobs)
        {
            Account = profile.Account;
            Job = profile.Job;
            Jobs = jobs;
            profileID = profile.profileID;
            accountID = profile.accountID;
            jobID = profile.jobID;
            firstName = profile.Account.firstName;
            lastName = profile.Account.lastName;
            businessName = profile.businessName;
            description = profile.description;
            location = profile.location;
            website = profile.website;
            youtube = profile.youtube;
            userName = profile.userName;
            isSuspended = profile.isSuspended;
            isApproved = profile.isApproved;
            //startYear = profile.startYear;
            additionalInfo = profile.additionalInfo;
            createDate = profile.createDate;
            updateDate = profile.updateDate;
        }

        public int profileID { get; set; }
        public int accountID { get; set; }

        [DisplayName("Job")]
        public int jobID { get; set; }

        [DisplayName("First Name")]
        public string firstName { get; set; }

        [DisplayName("Last Name")]
        public string lastName { get; set; }

        [DisplayName("Business Name")]
        public string businessName { get; set; }

        [DisplayName("Description")]
        public string description { get; set; }

        [DisplayName("Location")]
        public string location { get; set; }

        [DisplayName("Website")]
        public string website { get; set; }

        [DisplayName("Youtube")]
        public string youtube { get; set; }

        [DisplayName("Username")]
        public string userName { get; set; }

        [DisplayName("Profile Image")]
        public byte[] profilePic { get; set; }

        //[DisplayName("Start Year")]
        //public string startYear { get; set; }

        [DisplayName("isSuspended")]
        public bool isSuspended { get; set; }

        [DisplayName("isApproved")]
        public bool isApproved { get; set; }

        [DisplayName("Create Date")]
        public DateTime? createDate { get; set; }

        [DisplayName("Update Date")]
        public DateTime? updateDate { get; set; }

        [DisplayName("Additional Information")]
        public string additionalInfo { get; set; }

        public string email { get; set; }

    
        public IEnumerable<Job> Jobs { get; set; }

        public virtual Account Account { get; set; }
        public virtual Job Job { get; set; }
      
    }
}