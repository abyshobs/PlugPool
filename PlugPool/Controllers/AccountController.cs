using PlugPool.Domain.DAL.IDAL;
using PlugPool.Domain.Model;
using PlugPool.ViewModels.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PlugPool.Domain.Code;
using PlugPool.Domain.Code.Interfaces;

namespace PlugPool.Controllers
{
    public class AccountController : Controller
    {

        private IAccountDAL accountDAL;
        private IProfileDAL profileDAL;
        private IEmail _email;
        private IUserSession userSession;
        private ISessionWrapper sessionWrapper;
        private IAccountPermissionDAL accountPermissionDAL;

        public AccountController(IAccountDAL accountDAL, IEmail _email, IUserSession userSession, ISessionWrapper sessionWrapper, IAccountPermissionDAL accountPermissionDAL, IProfileDAL profileDAL)
        {
            this.accountDAL = accountDAL;
            this._email = _email;
            this.userSession = userSession;
            this.sessionWrapper = sessionWrapper;
            this.accountPermissionDAL = accountPermissionDAL;
            this.profileDAL = profileDAL;
        }

        public AccountController() { }

        [HttpGet]
        public ActionResult Register()
        {
            RegisterViewModel model = new RegisterViewModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                Account account = new Account
                {
                    firstName = model.firstName,
                    lastName = model.lastName,
                    email = model.email,
                    password = model.password.Encrypt(model.email),
                    isVerified = false,
                    createDate = DateTime.Now,
                };

                var _account = accountDAL.FetchByEmail(account.email);

                if (_account != null)
                {
                    TempData["errorMessage"] = "Oops ! It appears that email is already in use ! ";
                    return View(model);
                }

                accountDAL.createAccount(account);

                _email.SendEmailAddressVerificationEmail(account.email, account.email);

                return View("RegConfirmation");
            }
            return View(model);
        }

        public ActionResult RegConfirmation()
        {
            return View();
        }


        //uses link from email to verify the email
        public ActionResult VerifyEmail()
        {
            //fetches the email being verified and fetches the account associated with the email
            string email = Cryptography.Decrypt(sessionWrapper.EmailToVerify, "verify");
            Account _account = accountDAL.FetchByEmail(email);

            //updates the account 
            if (_account != null)
            {
                _account.isVerified = true;
                accountDAL.Update(_account);
                TempData["successMessage"] = "Account has been verified ! ";
                return RedirectToAction("Login");
            }

            //shows error message if account can't be updated
            else
            {
                TempData["errorMessage"] = "We couldn't verify your account ! ";
                return RedirectToAction("Login");
            }
        }

        [HttpGet]
        public ActionResult Login(string email, string password)
        {
            LoginViewModel model = new LoginViewModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel model, string email, string password)
        {
            if (ModelState.IsValid)
            {
                email = model.email;
                password = model.password.Encrypt(email);
                Account account = accountDAL.FetchByEmail(email);

                if (account != null)
                {
                    if (account.password == password)
                    {
                        if (account.isVerified == true)
                        {
                            var adminUser = accountPermissionDAL.FetchByEmail(account.email);
                            if (adminUser != null)
                            {
                                userSession.LoggedIn = true;
                                userSession.Email = adminUser.email;
                                userSession.CurrentUser = accountDAL.FetchByID(adminUser.accountID);
                                return RedirectToAction("AdminDashboard", "Admin");
                            }

                            else if (adminUser == null)
                            {
                                userSession.LoggedIn = true;
                                userSession.Email = email;
                                userSession.CurrentUser = accountDAL.FetchByID(account.accountID);

                                if (account.Profile != null) 
                                {
                                    if (account.Profile.isApproved == true) 
                                    {
                                        return RedirectToAction("Index", "UserDashboard");
                                    }

                                    else if (account.Profile.isApproved == false)
                                    {
                                        return RedirectToAction("ApplicationPending", "Profile");
                                    }
                                }

                                else if (account.Profile == null)
                                {
                                    return RedirectToAction("Create", "Profile");
                                }
                                //return RedirectToAction("Register");
                            }
                        }

                        else
                        {
                            _email.SendEmailAddressVerificationEmail(account.email, account.email);
                            TempData["errorMessage"] = @"The login information you provided was correct 
                                but your email address has not yet been verified.  
                                We have just sent another email verification email to you.  
                                Please follow the instructions in that email.";
                        }
                    }
                    else
                    {
                        TempData["errorMessage"] = "Incorrect password ! ";
                    }
                }
                else
                {
                    TempData["errorMessage"] = "This account doesn't exist ! ";
                }
                }
                return View(model);
            }

        [HttpGet]
        public ActionResult RecoverPassword() 
        {
            RecoverPasswordViewModel model = new RecoverPasswordViewModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult RecoverPassword(RecoverPasswordViewModel model, string email)
        {
            if(ModelState.IsValid)
            {
                email = model.email;
                Account account = accountDAL.FetchByEmail(email);

                if (account != null)
                {
                    if(account.isVerified == true)
                    {
                        _email.PasswordRecoveryEmail(account.email, account.email);
                        TempData["successMessage"] = @"An email has been sent to you.";
                        return RedirectToAction("RecoverPassword");
                    }

                    else
                    {
                        _email.SendEmailAddressVerificationEmail(account.email, account.email);
                        TempData["errorMessage"] = @"The email you provided was correct 
                                but your email address has not yet been verified.  
                                We just sent another email verification email to you.  
                                Please follow the instructions in that email.";
                    }
                }

                else if (account == null)
                {
                    TempData["errorMessage"] = "That email does not exist.";
                }
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult ChangePassword ()
        {
            ChangePasswordViewModel model = new ChangePasswordViewModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordViewModel model, string password)
        {
            if (ModelState.IsValid)
            {

                //fetches the account associated with the email obtaained in the GET method
                Account account = accountDAL.FetchByEmail(model.email);

                if (model.email == null)
                {
                    return Content("We couldn't find that email. Please follow the link in your email address");
                }
                //updates the account if it exists
                if (account != null)
                {
                    account.password = model.password.Encrypt(model.email);
                    accountDAL.Update(account);
                    TempData["successMessage"] = @"Your Password has been changed !";
                    return RedirectToAction("Login", "Account");
                }

                 //raises error message if the password cannot be changed
                else
                {
                    TempData["errorMessage"] = @"We could not change your password email customer service
                                                         if you need more help ";
                    return RedirectToAction("ChangePassword", "Account");
                }
            }
            return View();
        }
       
        public ActionResult Logout ()
        {
            userSession.LoggedIn = false;
            return RedirectToAction("Login");
        }

        //fetches the account of the logged in user
        public Account GetAccount()
        {
            return userSession.CurrentUser;
        }

        public ActionResult AccountDetails()
        {
            Account _account = GetAccount();
            var account = accountDAL.FetchByEmail(_account.email);           
            DetailsViewModel model = new DetailsViewModel(account);
            return View(model);
        }

        [HttpGet]
        public ActionResult Edit(int id = 0)
        {
            var account = accountDAL.FetchByID(id);
            EditViewModel model = new EditViewModel(account);
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(EditViewModel model) 
        { 
            if(ModelState.IsValid)
            {
                Account account = userSession.CurrentUser;
                account.firstName = model.firstName;
                account.lastName = model.lastName;
                account.updateDate = DateTime.Now;

                accountDAL.Update(account);

                if (account != null)
                {
                    TempData["SuccessMessage"] = "Account was successfully updated";
                }

                //raises error if account cannot be updated
                else
                {
                    TempData["errorMessage"] = "Error updating Account";
                }

                return RedirectToAction("AccountDetails", new { id = account.accountID });
            }

            return View(model);
        }


        [HttpGet]
        public ActionResult EditPassword ()
        {
            EditPasswordViewModel model = new EditPasswordViewModel ();
            return View(model);
        }

        [HttpPost]
        public ActionResult EditPassword(EditPasswordViewModel model, string currentPassword, string NewPassword)
        {
            if (ModelState.IsValid)
            {
                Account account = userSession.CurrentUser;
                currentPassword = model.currentPassword.Encrypt(account.email);

                if (account.password == currentPassword)
                {
                    account.password = model.newPassword.Encrypt(account.email);
                    accountDAL.Update(account);
                    TempData["successMessage"] = "Your password has been changed !";
                }

                else if (account.password != currentPassword)
                {
                    TempData["errorMessage"] = "The current password is incorrect";
                }

                else
                {
                    TempData["errorMessage"] = "An error occured. We could not update your password. Contact customer service for further assistance";
                }

            }

            return View(model);
        }

        [HttpGet]
        public ActionResult ChangeEmail ()
        {
            ChangeEmailViewModel model = new ChangeEmailViewModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult ChangeEmail (ChangeEmailViewModel model)
        {
            if(ModelState.IsValid)
            {
                var email = accountDAL.FetchByEmail(model.email);
                if (email == null)
                {
                    _email.SendChangedEmailVerificationEmail(model.email, model.email);
                    TempData["successMessage"] = "A confirmation has been sent to your email. Your email will be changed once you follow the link in your email";
                    return RedirectToAction("ChangeEmail", "Account");
                }

                else if (email != null)
                {
                    TempData["errorMessage"] = "That email already exists in the system";
                    return RedirectToAction("ChangeEmail", "Account");
                }
            }

            return View(model);
        }

        public ActionResult VerifyChangedEmail()
        {

            //fetches the email from link
            string email = Cryptography.Decrypt(sessionWrapper.EmailToVerify, "verify");
            Account account = userSession.CurrentUser;

            //updates the user account with the new email
            if (account != null)
            {
                account.password = account.password.Decrypt(account.email); //decrypts the existing password
                account.email = email;
                account.password = account.password.Encrypt(account.email);//encrypts the exsisting password using the new email
                accountDAL.Update(account); //updates the account in the database
                return Content("Your email has been changed. Please login again to view changes");
            }

            //returns error message if email can't be verified
            else
            {
                return Content("we couldn't change that email");
            }

        }

        //Get and post methods for deleting a user's account
        [HttpGet]
        public ActionResult Delete(int id = 0)
        {
            Account account = userSession.CurrentUser;
            if (account == null)
            {
                TempData["errorMessage"] = "That account does not exist";
                //return Content("This account does not exist");
            }

            var _account = accountDAL.FetchByID(id);
            if (_account == null)
            {
                TempData["errorMessage"] = "That account does not exist";
                //return Content("This account does not exist");
            }

            DeleteViewModel model = new DeleteViewModel(account);
            return View(model);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            accountDAL.Delete(id);

            if (id != null)
            {
                return Content("Sorry you had to leave us. We hope to see you again soon !");
                //return RedirectToAction("Register", "Account");
            }

            else
            {
                TempData["errorMessage"] = "Error deleting Account";
            }

            return RedirectToAction("Register", "Account");
        }



        }
    }
