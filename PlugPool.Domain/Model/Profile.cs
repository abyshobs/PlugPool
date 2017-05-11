using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlugPool.Domain.Model
{
    public class Profile
    {
        public int profileID { get; set; }
        [Key]
        [ForeignKey("Account")]
        public int accountID { get; set; }
        public int jobID { get; set; }
        public string businessName { get; set; }
        public string description { get; set; }
        public string location { get; set; }
        public string website { get; set; }
        public byte[] images { get; set; }
        public string youtube { get; set; }
        public string userName { get; set; }
        public byte[] profilePic { get; set; }
        public bool isSuspended { get; set; }
        public bool isApproved { get; set; }
        //public string startYear { get; set; }
        public string additionalInfo { get; set; }
        public DateTime? createDate { get; set; }
        public DateTime? updateDate { get; set; }

        public virtual Job Job { get; set; }
        public virtual Account Account { get; set; }

    }
}
