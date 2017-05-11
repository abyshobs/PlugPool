using PlugPool.Domain.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace PlugPool.ViewModels.Admins
{
    public class DeleteViewModel
    {
        public DeleteViewModel() { }

        public DeleteViewModel(AccountPermission accountPermission) { }

        public DeleteViewModel(AccountPermission accountPermission, IEnumerable<Permission> permissions)
        {
            Permission = accountPermission.Permission;
            permissionID = accountPermission.permissionID;
            accountPermissionID = accountPermission.accountPermissionID;
            accountID = accountPermission.accountID;
            email = accountPermission.email;
            createDate = accountPermission.createDate;
            updateDate = accountPermission.updateDate;
        }

        public int accountPermissionID { get; set; }
        [DisplayName("Permission")]
        public int permissionID { get; set; }

        public int accountID { get; set; }

        [DisplayName("Email")]
        public string email { get; set; }

        [DisplayName("Date created")]
        public DateTime? updateDate { get; set; }

        [DisplayName("Last update date")]
        public DateTime? createDate { get; set; }

        public IEnumerable<Permission> Permissions { get; set; }
        public virtual Permission Permission { get; set; }
    
    }
}