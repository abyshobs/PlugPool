using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlugPool.Domain.Model
{
    public class Permission
    {
        public int permissionID { get; set; }
        public string name { get; set; }
        public DateTime? createDate { get; set; }
        public DateTime? updateDate { get; set; }
    }
}
