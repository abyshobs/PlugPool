using PlugPool.Domain.Code.Interfaces;
using PlugPool.Domain.DAL.IDAL;
using PlugPool.Domain.Model;
using PlugPool.ViewModels.Admins;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PlugPool.Controllers
{
    public class AdminController : Controller
    {
        private IAccountPermissionDAL accountPermissionDAL;
        private IAccountDAL accountDAL;
        private IPermissionDAL permissionDAL;
        private IProfileDAL profileDAL;
        private IEmail _email;

        public AdminController(IAccountPermissionDAL accountPermissionDAL, IAccountDAL accountDAL, IPermissionDAL permissionDAL, IProfileDAL profileDAL, IEmail _email)
        {
            this.accountPermissionDAL = accountPermissionDAL;
            this.accountDAL = accountDAL;
            this.permissionDAL = permissionDAL;
            this.profileDAL = profileDAL;
            this._email = _email;
        }

        public ActionResult Index()
        {
            var accountPermissions = accountPermissionDAL.FetchAll();
            IndexViewModel model = new IndexViewModel(accountPermissions);
            return View(model);
        }

        [HttpGet]
        public ActionResult Create()
        {
            CreateViewModel model = new CreateViewModel(permissionDAL.FetchAll());
            model.Permissions = permissionDAL.FetchAll();
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(CreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                AccountPermission accountPermission = new AccountPermission()
                {
                    permissionID = model.permissionID,
                    email = model.email,
                    createDate = DateTime.Now,
                };

                //admin permissions cannot be added to an account that does not exist on the system
                var existingMember = accountDAL.FetchByEmail(model.email);
                if (existingMember == null)
                {
                    TempData["errorMessage"] = "This user does not exist in the system";
                    return RedirectToAction("Index");
                }

                else if (existingMember != null)
                {
                    if (existingMember.isVerified == false)
                    {
                        TempData["errorMessage"] = "This user hasn't verified their email";
                        return RedirectToAction("Create");
                    }

                    if (existingMember.isVerified == true)
                    {
                        //admin permissions cannot be given to a user who is already admin
                        var existingAdmin = accountPermissionDAL.FetchByEmail(model.email);
                        if (existingAdmin != null)
                        {
                            TempData["errorMessage"] = "This user is already admin. You can change their permission in Admin Users/Change Permission !";
                            return RedirectToAction("Index");
                        }

                        else if (existingAdmin == null)
                        {
                            //adds the admin user to the database
                            accountPermission.accountID = existingMember.accountID;
                            accountPermissionDAL.Create(accountPermission);

                            TempData["successMessage"] = "Success. You have created a new admin user !";
                            return RedirectToAction("Index");
                        }
                    }
                }
            }
            model.Permissions = permissionDAL.FetchAll();
            return View(model);
        }

        public ActionResult Details(int id)
        {
            AccountPermission accountPermission = accountPermissionDAL.FetchByID(id);
            if (accountPermission == null)
            {
                TempData["errorMessage"] = "Sorry. That admin user does not exist !";
                return RedirectToAction("Index");
            }

            DetailsViewModel model = new DetailsViewModel(accountPermission);
            return View(model);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            AccountPermission accountPermission = accountPermissionDAL.FetchByID(id);

            EditViewModel model = new EditViewModel(accountPermission, permissionDAL.FetchAll());
            model.Permissions = permissionDAL.FetchAll();
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(EditViewModel model)
        {
            if (ModelState.IsValid)
            {
                AccountPermission _accountPermission = accountPermissionDAL.FetchByID(model.accountPermissionID);
                var user = accountPermissionDAL.FetchByID(model.accountPermissionID);

                AccountPermission accountPermission = new AccountPermission
                {
                    accountPermissionID = model.accountPermissionID,
                    accountID = model.accountID,
                    permissionID = model.permissionID,
                    email = _accountPermission.email,
                    updateDate = DateTime.Now
                };

                //prevents user from changing the permission of a super admin user
                if (user.Permission.name == "SuperAdmin")
                {
                    TempData["errorMessage"] = "SuperAdmin users cannot be modified. Please see System Administrator !";
                    return RedirectToAction("Index");
                }
                else if (user.Permission.name != "SuperAdmin")
                {
                    accountPermissionDAL.Update(accountPermission);
                    return RedirectToAction("Details", new { id = accountPermission.accountPermissionID });
                }

            }
            model.Permissions = permissionDAL.FetchAll();
            return View(model);

        }


        public ActionResult Delete(int id = 0)
        {
            //returns error message if user fetches an account permission that does not exist
            AccountPermission accountPermission = accountPermissionDAL.FetchByID(id);
            if (accountPermission == null)
            {
                TempData["errorMessage"] = "This admin user does not exist!";
                return RedirectToAction("Index");
            }

            //prevents user from deleting the permission of a super admin user
            if (accountPermission.Permission.name == "SuperAdmin")
            {
                TempData["errorMessage"] = "SuperAdmin users cannot be deleted. See System Administrator !";
                return RedirectToAction("Index");
            }

            DeleteViewModel model = new DeleteViewModel(accountPermission);
            model.email = accountPermission.email;
            model.Permission = accountPermission.Permission;
            model.permissionID = accountPermission.permissionID;
            model.createDate = accountPermission.createDate;
            model.updateDate = accountPermission.updateDate;
            return View(model);
        }


        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            AccountPermission accountPermission = accountPermissionDAL.FetchByID(id);
            accountPermissionDAL.Delete(id);

            if (id != 0)
            {
                TempData["SuccessMessage"] = "Admin user was successfully deleted";
                return RedirectToAction("Index");
            }

            else
            {
                TempData["errorMessage"] = "Error deleting Admin user";
            }

            return RedirectToAction("Index");
        }

        public ActionResult AdminDashboard()
        {
            return View();
        }

        //displays all profiles that are yet to be approved
        public ActionResult PendingApplications()
        {
            var applications = accountDAL.fetchPending();
            PlugPool.ViewModels.Accounts.IndexViewModel model = new PlugPool.ViewModels.Accounts.IndexViewModel(applications);
            return View(model);
        }

        //displays all user accounts on the website
        public ActionResult UserAccounts()
        {
            var applications = accountDAL.fetchAccounts();
            PlugPool.ViewModels.Accounts.IndexViewModel model = new PlugPool.ViewModels.Accounts.IndexViewModel(applications);
            return View(model);
        }

        //fetches the details of a user
        public ActionResult AccountDetails(int id = 0)
        {
            Account account = accountDAL.FetchByID(id);

            if (account == null)
            {
                TempData["errorMessage"] = "Sorry. This user does not exist !";
                return RedirectToAction("AdminDashboard");
            }

            PlugPool.ViewModels.Accounts.DetailsViewModel model = new PlugPool.ViewModels.Accounts.DetailsViewModel(account);
            return View(model);
        }

        public ActionResult AcceptProfile(int id = 0)
        {
            Account account = accountDAL.FetchByID(id);
            if (account == null)
            {
                TempData["errorMessage"] = "Sorry. This user does not exist !";
                return RedirectToAction("AdminDashboard");
            }

            else
            {
                account.Profile.isApproved = true;
                _email.SendProfileApprovedEmail(account.email, account.email);
                profileDAL.Update(account.Profile);
                TempData["successMessage"] = "Profile has been approved";
            }

            return RedirectToAction("AdminDashboard");
        }

        public ActionResult DeclineProfile()
        {
            return View();
        }
    }
}