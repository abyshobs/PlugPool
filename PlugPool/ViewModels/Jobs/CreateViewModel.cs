using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PlugPool.ViewModels.Jobs
{
    public class CreateViewModel
    {
        public int jobID { get; set; }

        [DisplayName("Name")]
        [Required(ErrorMessage = "Please provide a Name")]
        public string name { get; set; }
    }
}