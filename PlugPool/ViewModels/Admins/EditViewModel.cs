using PlugPool.Domain.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PlugPool.ViewModels.Admins
{
    public class EditViewModel
    {
        public EditViewModel() { }

        public EditViewModel(AccountPermission accountPermission) { }

        public EditViewModel(AccountPermission accountPermission, IEnumerable<Permission> permissions)
        {
            Permission = accountPermission.Permission;
            permissionID = accountPermission.permissionID;
            accountPermissionID = accountPermission.accountPermissionID;
            accountID = accountPermission.accountID;
            email = accountPermission.email;
        }

        public int accountPermissionID { get; set; }

        [DisplayName("Permission")]
        [Required(ErrorMessage = "Please provide a Permission")]
        public int permissionID { get; set; }

        public int accountID { get; set; }

        [DisplayName("Email")]
        public string email { get; set; }

        public IEnumerable<Permission> Permissions { get; set; }
        public virtual Permission Permission { get; set; }

    }
}