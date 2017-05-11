using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlugPool.ViewModels.Jobs
{
    public class IndexViewModel
    {
        public IndexViewModel()
        {
        }

        public IndexViewModel(IEnumerable<PlugPool.Domain.Model.Job> jobs)
        {
            Jobs = jobs;
        }

        public IEnumerable<PlugPool.Domain.Model.Job> Jobs { get; set; }
    }
}