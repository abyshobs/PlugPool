using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace PlugPool.ViewModels.Profiles
{
    public class ImageCreateViewModel
    {
        public ImageCreateViewModel() { }
        
        [DisplayName("Caption")]
        public string caption { get; set; }
    }
}