using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlugPool.ViewModels.Permissions
{
    public class IndexViewModel
    {
        public IndexViewModel()
        {
        }

        public IndexViewModel(IEnumerable<PlugPool.Domain.Model.Permission> permissions)
        {
            Permissions = permissions;
        }

        public IEnumerable<PlugPool.Domain.Model.Permission> Permissions { get; set; }
    }
}