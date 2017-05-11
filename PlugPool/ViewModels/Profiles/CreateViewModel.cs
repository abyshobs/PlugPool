using PlugPool.Domain.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PlugPool.ViewModels.Profiles
{
    public class CreateViewModel
    {
        public CreateViewModel() { }
        public CreateViewModel(IEnumerable<Job> jobs)
        {
            Jobs = jobs;
        }

        public IEnumerable<Job> Jobs { get; set; }
        public int profileID { get; set; }
        public int accountID { get; set; }

        [Required(ErrorMessage = "Please provide a Job")]
        [DisplayName("Job")]
        public int jobID { get; set; }

        [Required(ErrorMessage = "Please provide the name of your business")]
        [DisplayName("Business Name")]
        public string businessName { get; set; }

        [Required(ErrorMessage = "Please provide your location")]
        [DisplayName("Location")]
        public string location { get; set; }

        [Required(ErrorMessage = "Please provide a description for your business")]
        [DisplayName("Description")]
        public string description { get; set; }

        //[Required(ErrorMessage = "Please provide the year you started your business ")]
        //[DisplayName("Start Year")]
        //public string startYear { get; set; }

        [DisplayName("Website")]
        [Required(ErrorMessage = "Please enter a valid url")]
        public string website { get; set; }

        [DisplayName("Youtube")]
        //[Url(ErrorMessage = "Please enter a valid url")]
        public string youtube { get; set; }

        [DisplayName("Additional Information")]
        public string additionalInfo { get; set; }

        [Required(ErrorMessage = "Please provide a Username")]
        [DisplayName("Username")]
        public string userName { get; set; }

    }
}

