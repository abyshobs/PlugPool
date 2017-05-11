using PlugPool.Domain.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace PlugPool.ViewModels.Admins
{
    public class DetailsViewModel
    {
        public DetailsViewModel() { }

        public DetailsViewModel(IEnumerable<Permission> permissions) { }

        public DetailsViewModel(AccountPermission accountPermission)
        {
            Permission = accountPermission.Permission;
            accountPermissionID = accountPermission.accountPermissionID;
            permissionID = accountPermission.permissionID;
            accountID = accountPermission.accountID;
            createDate = accountPermission.createDate;
            updateDate = accountPermission.updateDate;
            email = accountPermission.email;

        }

        public int accountPermissionID { get; set; }
        public int accountID { get; set; }

        [DisplayName("Permission")]
        public int permissionID { get; set; }

        [DisplayName("Email")]
        public string email { get; set; }

        [DisplayName("Date Created")]
        public DateTime? createDate { get; set; }

        [DisplayName("Last Update Date")]
        public DateTime? updateDate { get; set; }

        public Permission Permission { get; set; }
    }
}