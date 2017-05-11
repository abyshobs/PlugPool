using PlugPool.Domain.Code.Interfaces;
using PlugPool.Domain.DAL.IDAL;
using PlugPool.Domain.Model;
using PlugPool.ViewModels.Notes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PlugPool.Controllers
{
    public class NoteController : Controller
    {
        private INoteDAL noteDAL;
        private IAccountDAL accountDAL;
        private IProfileDAL profileDAL;
        private IEmail _email;
        private ISessionWrapper sessionWrapper;
        private IUserSession userSession;

        public NoteController(INoteDAL noteDAL, IAccountDAL accountDAL, IProfileDAL profileDAL, IEmail _email, ISessionWrapper sessionWrapper, IUserSession userSession)
        {
            this.accountDAL = accountDAL;          
            this.profileDAL = profileDAL;
            this._email = _email;
            this.sessionWrapper = sessionWrapper;
            this.userSession = userSession;
            this.noteDAL = noteDAL;
        }

        public Account GetAccount()
        {
            return userSession.CurrentUser;
        }

        [HttpGet]
        public ActionResult Create() 
        {
            CreateViewModel model = new CreateViewModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(CreateViewModel model, int id = 0)
        { 
            Account account = GetAccount();
            Account userAccount = accountDAL.FetchByID(id);

            if(ModelState.IsValid)
            {
                Note _note = new Note
                {
                    accountID = userAccount.accountID,
                    leftBy = account.accountID,
                    note = model.note,
                    dateCreated = DateTime.Now
                };
                noteDAL.Create(_note);
                _email.SendProfileDeclinedEmail(userAccount.email, userAccount.email, model.note);
                return RedirectToAction("AdminDashboard", "Admin");
            }
            return View(model);
        }

    }
}