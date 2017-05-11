using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlugPool.ViewModels.Accounts
{
    public class IndexViewModel
    {
        public IndexViewModel()
        {
        }

        public IndexViewModel(IEnumerable<PlugPool.Domain.Model.Account> accounts)
        {
            Accounts = accounts;
        }

        public IEnumerable<PlugPool.Domain.Model.Account> Accounts { get; set; }
    }
}