using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlugPool.ViewModels.Admins
{
    public class IndexViewModel
    {
        public IndexViewModel()
        {
        }

        public IndexViewModel(IEnumerable<PlugPool.Domain.Model.AccountPermission> accountPermissions)
        {
            AccountPermissions = accountPermissions;
        }

        public IEnumerable<PlugPool.Domain.Model.AccountPermission> AccountPermissions { get; set; }
    }
}