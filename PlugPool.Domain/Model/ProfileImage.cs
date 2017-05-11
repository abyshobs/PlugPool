using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlugPool.Domain.Model
{
    public class ProfileImage
    {
        public int profileImageID { get; set; }
        public int accountID { get; set; }
        public byte[] image { get; set; }
        public string caption { get; set; }
        public string type { get; set; }
        public DateTime createDate { get; set; }

        public virtual Account Account { get; set; }
    }
}
