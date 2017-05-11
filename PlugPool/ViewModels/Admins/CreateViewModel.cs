using PlugPool.Domain.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PlugPool.ViewModels.Admins
{
    public class CreateViewModel
    {

        public CreateViewModel() { }

        public CreateViewModel(IEnumerable<Permission> permissions)
        {
            Permissions = permissions;
        }

        public IEnumerable<Permission> Permissions { get; set; }

        public int accountPermissionID { get; set; }

        public int accountID { get; set; }

        [Required(ErrorMessage = "Please provide a Permission")]
        [DisplayName("Permission")]
        public int permissionID { get; set; }

        [Required(ErrorMessage = "Please provide an Email")]
        [DisplayName("Email")]
        [EmailAddress]
        public string email { get; set; }
    }
}