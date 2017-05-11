using PlugPool.Domain.DAL.IDAL;
using PlugPool.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlugPool.Domain.DAL
{
    public class ProfileImageDAL : IProfileImageDAL
    {
        //creates an instance of the database
        private readonly PlugPoolContext _db;

        public ProfileImageDAL(PlugPoolContext db)
        {
            _db = db;
        }

        //fetch all images related to an account
        public IEnumerable<ProfileImage> FetchByAccount(int accountID)
        {
            return _db.ProfileImages.Where(pi => pi.accountID == accountID).ToList();
        }

        //fetch a particular image
        public ProfileImage Fetch(int id)
        {
            return _db.ProfileImages.FirstOrDefault(pi => pi.profileImageID == id);
        }

        //create an image
        public void Create(ProfileImage image)
        {
            image.createDate = DateTime.Now;
            _db.ProfileImages.Add(image);
            _db.SaveChanges();
        }

        //delete image
        public void Delete(int id)
        {
            ProfileImage image = _db.ProfileImages.Find(id);
            _db.ProfileImages.Remove(image);
            _db.SaveChanges();
        }
    }
}
