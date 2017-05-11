using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlugPool.Domain.Model
{
    public class Account
    {
        public int accountID { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public bool isVerified { get; set; }
        public DateTime? createDate { get; set; }
        public DateTime? updateDate { get; set; }

        public ICollection<AccountPermission> AccountPermissions { get; set; }
        public virtual Profile Profile { get; set; }
        public virtual ICollection<ProfileImage> ProfileImages { get; set; }
        public ICollection<Note> Notes { get; set; }
    }
}
