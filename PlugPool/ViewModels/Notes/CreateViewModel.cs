using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PlugPool.ViewModels.Notes
{
    public class CreateViewModel
    {
        //public int noteID { get; set; }
        //public int accountID { get; set; }

        //public string leftBy { get; set; }

        [DisplayName("Note")]
        [Required(ErrorMessage = "Please leave a message")]
        public string note { get; set; }

        public DateTime? dateCreated { get; set; }

    }
}