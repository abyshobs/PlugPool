using PlugPool.Domain.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PlugPool.ViewModels.Jobs
{
    public class EditViewModel
    {
        public EditViewModel() { }

        public EditViewModel(Job job)
        {
            jobID = job.jobID;
            name = job.name;
            updateDate = job.updateDate;
        }

        public int jobID { get; set; }

        [DisplayName("Name")]
        [Required(ErrorMessage = "Please provide a Name")]
        public string name { get; set; }

        public DateTime? updateDate { get; set; }
    }
}