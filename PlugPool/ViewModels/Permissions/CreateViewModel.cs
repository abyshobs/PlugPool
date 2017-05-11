using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PlugPool.ViewModels.Permissions
{
    public class CreateViewModel
    {
        public int permissionID { get; set; }

        [DisplayName("Name")]
        [Required(ErrorMessage = "Please provide a Name")]
        public string name { get; set; }
    }
}