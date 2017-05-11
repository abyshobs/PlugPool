using PlugPool.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlugPool.Domain.DAL.IDAL
{
    public interface IProfileImageDAL
    {
        IEnumerable<ProfileImage> FetchByAccount(int accountID);
        ProfileImage Fetch(int id);
        void Create(ProfileImage image);
        void Delete(int id);
    }
}
