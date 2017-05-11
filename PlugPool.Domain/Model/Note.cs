using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlugPool.Domain.Model
{
    public class Note
    {
        public int noteID { get; set; }
        public int accountID { get; set; }
        public string note { get; set; }
        public int leftBy { get; set; }
        public DateTime? dateCreated { get; set; }

    }
}
