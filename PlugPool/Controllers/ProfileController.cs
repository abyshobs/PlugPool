using PlugPool.Domain.Code.Interfaces;
using PlugPool.Domain.DAL.IDAL;
using PlugPool.Domain.Model;
using PlugPool.ViewModels.Profiles;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PlugPool.Controllers
{
    public class ProfileController : Controller
    {
        //initializes the repositories that will be used in this controller
        private IAccountDAL accountDAL;
        private IProfileDAL profileDAL;
        private IJobDAL jobDAL;
        private IUserSession userSession;
        private IProfileImageDAL profileImageDAL;

        public ProfileController(IAccountDAL accountDAL, IProfileDAL profileDAL, IUserSession userSession, IJobDAL jobDAL, IProfileImageDAL profileImageDAL)
        {
            this.accountDAL = accountDAL;
            this.profileDAL = profileDAL;
            this.userSession = userSession;
            this.jobDAL = jobDAL;
            this.profileImageDAL = profileImageDAL;
        }

        //fetches the account of the logged in user
        public Account GetAccount()
        {
            return userSession.CurrentUser;
        }

        //gets the form that allows users to create a profile
        [HttpGet]
        public ActionResult Create()
        {
            CreateViewModel model = new CreateViewModel(jobDAL.FetchAll());
            model.Jobs = jobDAL.FetchAll(); //fetches all the jobs a user can select from
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(CreateViewModel model)
        {
            if(ModelState.IsValid)
            {
                Account account = GetAccount();
                Profile profile = new Profile
                {
                    accountID = account.accountID,
                    jobID = model.jobID,
                    businessName = model.businessName,
                    userName = model.userName,
                    location = model.location,
                    description = model.description,
                    //startYear = model.startYear,
                    website = model.website,
                    youtube = model.youtube,
                    additionalInfo = model.additionalInfo,
                    isApproved = false,
                    isSuspended = false,
                    createDate = DateTime.Now,
                    
                };

                var existingUsername = profileDAL.FetchByUsername(model.userName);
                if (existingUsername != null )
                {
                    TempData["errorMessage"] = "That username already exists";
                    model.Jobs = jobDAL.FetchAll();
                    return View(model);
                }
                else
                {
                   profileDAL.Create(profile);
                   return RedirectToAction("AvatarUpload");
                }
            }
            model.Jobs = jobDAL.FetchAll();
            return View(model);
        }

        //fetches the details of a profile
        public ActionResult Details(int id = 0)
        {
            Account account = GetAccount();
            Profile profile = profileDAL.fetchByAccountID(account.accountID);

            if (profile == null)
            {
                TempData["errorMessage"] = "Sorry. This user does not exist !";
                return RedirectToAction("Account/Login");
            }

            DetailsViewModel model = new DetailsViewModel(profile);
            return View(model);
        }

        //fetches the page that allows user to upload a profile pic
        [HttpGet]
        public ActionResult AvatarUpload()
        {
          return View();
        }

        public ActionResult AvatarUpload(HttpPostedFileBase file)
        {
            Account account = GetAccount();
            Profile profile = profileDAL.fetchByAccountID(account.accountID);

            if (file != null)
            {
                byte[] uploadedImage = new byte[file.InputStream.Length];
                string pic = System.IO.Path.GetFileName(file.FileName);
                string path = System.IO.Path.Combine(
                                       Server.MapPath("~/content/images/avatars"), pic);
                string extension = Path.GetExtension(file.FileName).ToLower();

                //ensures that only the correct file format is uploaded
                if (extension == ".png")
                {
                    file.SaveAs(path);
                }
                else if (extension == ".jpg")
                {
                    file.SaveAs(path);
                }
                else if (extension == ".gif")
                {
                    file.SaveAs(path);
                }
                // file is uploaded
                else
                {
                    TempData["errorMessage"] = "We only accept .png, .jpg, and .gif!";
                    return RedirectToAction("AvatarUpload");
                }

                // save the image path path to the database or you can send image 
                // directly to database
                // in-case if you want to store byte[] ie. for DB
                if (file.ContentLength / 1000 < 1000)
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        file.InputStream.Read(uploadedImage, 0, uploadedImage.Length);
                        profile.profilePic = uploadedImage;
                    }
                }

                else
                {
                    TempData["errorMessage"] = @"The file you uploaded exceeds the size limit. 
                                              Please reduce the size of your file and try again.";
                }
            }
            profileDAL.Update(profile);
            return RedirectToAction("AvatarImage");
        }

        //this page shows the uploaded profile pic
        [HttpGet]
        public ActionResult AvatarImage()
        {
            return View();
        }

        //this methods displays the image
        public FileContentResult AvatarImg(int id = 0)
        {
            Account account = GetAccount();
            Profile profile = profileDAL.fetchByAccountID(account.accountID);
            byte[] byteArray = profile.profilePic;
            return byteArray != null
            ? new FileContentResult(byteArray, "image/jpeg")
            : null;
        }

        

        //gets the page that allows user to add photos with their profile
        [HttpGet]
        public ActionResult AddProfileImage()
        {
            ImageCreateViewModel model = new ImageCreateViewModel();
            return View(model);
        }

        //upload pictures related to profile
        [HttpPost]
        public ActionResult AddProfileImage(HttpPostedFileBase file, ImageCreateViewModel model)
        {
            Account account = GetAccount();
            ProfileImage profileImage = new ProfileImage();

            if (file != null)
            {
                byte[] uploadedImage = new byte[file.InputStream.Length];
                string pic = System.IO.Path.GetFileName(file.FileName);
                string path = System.IO.Path.Combine(
                                       Server.MapPath("~/content/images/profileImages"), pic);
                string extension = Path.GetExtension(file.FileName).ToLower();

                //ensures that only the correct file format is uploaded
                if (extension == ".png")
                {
                    file.SaveAs(path);
                }
                else if (extension == ".jpg")
                {
                    file.SaveAs(path);
                }
                else if (extension == ".gif")
                {
                    file.SaveAs(path);
                }
                // file is uploaded
                else
                {
                    TempData["errorMessage"] = "We only accept .png, .jpg, and .gif!";
                    return RedirectToAction("AddProfileImage");
                }

                // save the image path path to the database or you can send image 
                // directly to database
                // in-case if you want to store byte[] ie. for DB
                if (file.ContentLength / 1000 < 1000)
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        file.InputStream.Read(uploadedImage, 0, uploadedImage.Length);     

                        profileImage.image = uploadedImage;
                        profileImage.accountID = account.accountID;
                        profileImage.caption = model.caption;
                        profileImage.createDate = DateTime.Now;
                        profileImage.type = "profileImages";             
                    }
                }

                else
                {
                    TempData["errorMessage"] = @"The file you uploaded exceeds the size limit. 
                                              Please reduce the size of your file and try again.";
                }
            }

            /*if (profileImage.image.Count() > 5)
            {
                TempData["errorMessage"] = @"Upload limit is 5";
                return RedirectToAction("AddProfileImage");
            }*/

            profileImageDAL.Create(profileImage);
            return RedirectToAction("AddProfileImage");
        }

        //displays all images related to an account
        public ActionResult ProfileImageIndex()
        {
            Account account = GetAccount();
            IEnumerable <ProfileImage> images = profileImageDAL.FetchByAccount(account.accountID);
            ImageIndexViewModel model = new ImageIndexViewModel(images);
            return View(model);
        }

        //method for displaying the image
        public FileContentResult GetProfileImg(int id = 0)
        {
            Account account = GetAccount();
            IEnumerable<ProfileImage> images = profileImageDAL.FetchByAccount(account.accountID);

            foreach(var x in images)
            {
                byte[] byteArray = x.image;
                return byteArray != null
                ? new FileContentResult(byteArray, "image/jpeg")
                : null;
            }
            return null;
        }


        public ActionResult DeleteProfileImage(int id = 0)
        {
            ProfileImage image = profileImageDAL.Fetch(id);
            if (image == null)
            {
                TempData["errorMessage"] = "This image does not exist!";
                return RedirectToAction("ProfileImageIndex");
            }

            ImageDeleteViewModel model = new ImageDeleteViewModel(image);
            return View(model);
        }

        [HttpPost, ActionName("DeleteProfileImage")]
        public ActionResult DeleteConfirmed(int id)
        {
            ProfileImage image = profileImageDAL.Fetch(id);
            profileImageDAL.Delete(id);

            if (id != null)
            {
                TempData["SuccessMessage"] = "Image deleted";
                return RedirectToAction("ProfileImageIndex");
            }

            else
            {
                TempData["errorMessage"] = "Error deleting image";
            }

            return RedirectToAction("ProfileImageIndex");
        }

        public ActionResult ApplicationConfirmation()
        {
            return View();
        }

        public ActionResult ApplicationPending()
        {
            return View();
        }

        public ActionResult UserProfileHomepage(int id = 0)
        {
            var account = accountDAL.FetchByID(id);
            DetailsViewModel model = new DetailsViewModel(account.Profile);
            return View(model);
        }

        //returns the profile of the logged in user
        public ActionResult MyProfile ()
        {
            Account account = GetAccount();
            DetailsViewModel model = new DetailsViewModel(account.Profile);
            return View(model);
        }

        [HttpGet]
        public ActionResult Edit ()
        {
            Account account = userSession.CurrentUser;
            EditViewModel model = new EditViewModel(account.Profile, jobDAL.FetchAll());
            return View(model);
            
        }

        [HttpPost]
        public  ActionResult Edit (EditViewModel model)
        {
            if (ModelState.IsValid)
            {
                Account _account = GetAccount();
                var account = accountDAL.FetchByID(_account.accountID);

                if (account != null)
                {
                    account.Profile.jobID = model.jobID;
                    account.firstName = model.firstName;
                    account.lastName = model.lastName;
                    account.Profile.businessName = model.businessName;
                    account.Profile.description = model.description;
                    account.Profile.location = model.location;
                    account.Profile.website = model.website;
                    account.Profile.youtube = model.youtube;
                   //account.Profile.startYear = model.startYear;
                    account.Profile.additionalInfo = model.additionalInfo;
                    account.Profile.updateDate = DateTime.Now;
                    account.updateDate = DateTime.Now;

                    var existingUsername = profileDAL.FetchByUsername(model.userName);
                    if(existingUsername != null)
                    {
                        TempData ["errorMessage"] = "That username is already in use";
                    }

                    else if (existingUsername == null)
                    {
                        account.Profile.userName = model.userName;
                    }

                    accountDAL.Update(account);
                    return RedirectToAction("MyProfile");
                }

                else if (account == null)
                {
                    TempData ["errorMessage"] = "That account does not exist.";
                }
            }

             model.Jobs = jobDAL.FetchAll();
            return View(model);
        }
       
    }
}