﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlugPool.Domain.Model
{
    public class Job
    {
        public int jobID { get; set; }
        public string name { get; set; }
        public DateTime? createDate { get; set; }
        public DateTime? updateDate { get; set; }

        public ICollection<Profile> Profiles { get; set; }
    }
}
