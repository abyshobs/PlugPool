using PlugPool.Domain.DAL.IDAL;
using PlugPool.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlugPool.Domain.DAL
{
    public class ProfileDAL : IProfileDAL
    {
        //creates an instance of the database
        private readonly PlugPoolContext _db;

        public ProfileDAL(PlugPoolContext db)
        {
            _db = db;
        }

        //returns a profile based on its accountID
        public Profile fetchByAccountID(int accountID)
        {
            return _db.Profiles.Where(p => p.accountID == accountID).FirstOrDefault();
        }

        //returns a profile based on its JobID
        public Profile fetchByJob(int jobID)
        {
            return _db.Profiles.Where(p => p.jobID == jobID).FirstOrDefault();
        }

        public void Create(Profile profile)
        {
            profile.createDate = DateTime.Now;
            _db.Profiles.Add(profile);
            _db.SaveChanges();
        }

        public void Update(Profile profile)
        {
            profile.updateDate = DateTime.Now;

            if (profile.accountID > 0)//if the profile exists, edit the profile
            {
                Profile originalProfile = _db.Profiles.Find(profile.accountID);
                originalProfile.accountID = originalProfile.accountID;
                originalProfile.profileID = profile.profileID;
                originalProfile.jobID = profile.jobID;
                originalProfile.profilePic = profile.profilePic;
                originalProfile.userName = profile.userName;
                originalProfile.businessName = profile.businessName;
                originalProfile.location = profile.location;
                originalProfile.description = profile.description;
                originalProfile.website = profile.website;
                originalProfile.youtube = profile.youtube;
                originalProfile.isSuspended = profile.isSuspended;
                originalProfile.isApproved = profile.isApproved;
                //originalProfile.startYear = profile.startYear;
                originalProfile.additionalInfo = profile.additionalInfo;
                originalProfile.Account.firstName = profile.Account.firstName;
                originalProfile.Account.lastName = profile.Account.lastName;
                originalProfile.Account.updateDate = profile.updateDate;
                _db.SaveChanges();
            }
        }

        //fetches a profile by its username
        public Profile FetchByUsername(string username)
        {
            return _db.Profiles.FirstOrDefault(u => u.userName == username);
        }

    }
}
