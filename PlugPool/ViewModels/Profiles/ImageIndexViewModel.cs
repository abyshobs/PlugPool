using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlugPool.ViewModels.Profiles
{
    public class ImageIndexViewModel
    {
        public ImageIndexViewModel()
        {
        }

        public ImageIndexViewModel(IEnumerable<PlugPool.Domain.Model.ProfileImage> profileImages)
        {
            ProfileImages = profileImages;
        }

        public IEnumerable<PlugPool.Domain.Model.ProfileImage> ProfileImages { get; set; }
    }
}