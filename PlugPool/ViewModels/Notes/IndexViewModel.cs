using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlugPool.ViewModels.Notes
{
    public class IndexViewModel
    {
        public IndexViewModel()
        {
        }

        public IndexViewModel(IEnumerable<PlugPool.Domain.Model.Note> notes)
        {
            Notes = notes;
        }

        public IEnumerable<PlugPool.Domain.Model.Note> Notes { get; set; }
    }
}