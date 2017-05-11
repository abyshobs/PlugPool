using PlugPool.Domain.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace PlugPool.ViewModels.Profiles
{
    public class ImageDeleteViewModel
    {
        public ImageDeleteViewModel() { }

       // public ImageDeleteViewModel(ProfileImage profileImage) { }

        public ImageDeleteViewModel(ProfileImage profileImage)
        {
            Account = profileImage.Account;
            accountID = profileImage.accountID;
            profileImageID = profileImage.profileImageID;
            image = profileImage.image;
            caption = profileImage.caption;
            type = profileImage.type;
            createDate = profileImage.createDate;
        }

        public int profileImageID { get; set; }

        public int accountID { get; set; }

        public byte[] image { get; set; }

        public string caption { get; set; }
        
        public string type { get; set; }

        [DisplayName("Date Created")]
        public DateTime? createDate { get; set; }

        public virtual Account Account { get; set; }
    }
}