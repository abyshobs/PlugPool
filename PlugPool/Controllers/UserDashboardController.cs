using PlugPool.Domain.Code.Interfaces;
using PlugPool.Domain.DAL.IDAL;
using PlugPool.Domain.Model;
using PlugPool.ViewModels.Profiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PlugPool.Controllers
{
    public class UserDashboardController : Controller
    {
        private IProfileDAL profileDAL;
        private IUserSession userSession;

         public UserDashboardController(IProfileDAL profileDAL, IUserSession userSession)
        {
            this.profileDAL = profileDAL;
            this.userSession = userSession;
        }



        //fetches the account of the logged in user
        public Account GetAccount()
        {
            return userSession.CurrentUser;
        }
        
        //user dashboard homepage
        public ActionResult Index()
        {
            return View();
        }

       

    }
}