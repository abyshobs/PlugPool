using PlugPool.Domain.Code.Interfaces;
using PlugPool.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace PlugPool.Domain.Code
{
    public class Email : IEmail
    {

        const string TO_EMAIL_ADDRESS = "aswebsolutions16@gmail.com";
        const string FROM_EMAIL_ADDRESS = "aswebsolutions16@gmail.com";

        private IConfiguration configuration;
        private readonly PlugPoolContext _db;

        public Email(IConfiguration configuration, PlugPoolContext db)
        {
            this.configuration = configuration;
            _db = db;
        }

        public void SendEmail(string From, string Subject, string Message)
        {
            MailMessage mm = new MailMessage(From, TO_EMAIL_ADDRESS);
            mm.Subject = Subject;
            mm.Body = Message;
            Send(mm);
        }

        public void SendEmail(string To, string CC, string BCC, string Subject, string Message)
        {
            MailMessage mm = new MailMessage(FROM_EMAIL_ADDRESS, To);
            //mm.To.Add(To);
            //mm.CC.Add(CC);             
            //mm.Bcc.Add(BCC);             
            if (!string.IsNullOrEmpty(CC))
                mm.CC.Add(CC);

            if (!string.IsNullOrEmpty(BCC))
                mm.Bcc.Add(BCC);

            mm.Subject = Subject;
            mm.Body = Message;
            mm.IsBodyHtml = true;
            Send(mm);
        }

        public void SendEmail(string[] To, string[] CC, string[] BCC, string Subject, string Message)
        {
            MailMessage mm = new MailMessage();

            foreach (string to in To)
            {
                mm.To.Add(to);
            }

            foreach (string cc in CC)
            {
                mm.CC.Add(cc);
            }

            foreach (string bcc in BCC)
            {
                mm.Bcc.Add(bcc);
            }

            mm.From = new MailAddress(FROM_EMAIL_ADDRESS);
            mm.Subject = Subject;
            mm.Body = Message;
            mm.IsBodyHtml = true;
            Send(mm);
        }

        private void Send(MailMessage Message)
        {
            var client = new SmtpClient("smtp.gmail.com", 587)
            {
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential("aswebsolutions16@gmail.com", "faosat63"),
                EnableSsl = true
            };
            client.Send(Message);
        }



        public void SendEmailAddressVerificationEmail(string email, string To)
        {
            string msg = "Please click on the link below or paste it into a browser to verify your email account.<BR><BR>" +
                            "<a href=\"" + configuration.RootURL + "Account/VerifyEmail?a=" +
                            HttpUtility.UrlEncode(email.Encrypt("verify")) + "\">" +
                            configuration.RootURL + "Account/VerifyEmail?a=" +
                            HttpUtility.UrlEncode(email.Encrypt("verify")) + "</a>";

            SendEmail(To, "", "", "Account created! Email verification required.", msg);
            //Cryptography.Encrypt(Username, "verify")
        }

        public void PasswordRecoveryEmail(string email, string To)
        {
            string msg = "Please click on the link below or paste it into a browser to change your password.<BR><BR>" +
                            "<a href=\"" + configuration.RootURL + "Account/ChangePassword?a=" +
                            HttpUtility.UrlEncode(email.Encrypt("verify")) + "\">" +
                            configuration.RootURL + "Account/ChangePassword?a=" +
                            HttpUtility.UrlEncode(email.Encrypt("verify")) + "</a>";

            SendEmail(To, "", "", "You requested to change your password!.", msg);
        }

        public void SendProfileApprovedEmail(string email, string To)
        {
            string msg = "Your PlugPool profile has been approved. Login to now to start networking <BR><BR>" +
                            "<a href=\"" + configuration.RootURL + "Account/login?a=" + "\">" +"</a>";

            SendEmail(To, "", "", "PlugPool Profile Approved", msg);
            //Cryptography.Encrypt(Username, "verify")
        }

        public void SendProfileDeclinedEmail(string email, string To, string msg)
        {
            SendEmail(To, "", "", "PlugPool profile pending approval", msg);
            //Cryptography.Encrypt(Username, "verify")
        }

        public void SendChangedEmailVerificationEmail(string email, string To)
        {
            string msg = "Please click on the link below or paste it into a browser to verify your email account.<BR><BR>" +
                            "<a href=\"" + configuration.RootURL + "Account/VerifyChangedEmail?a=" +
                            HttpUtility.UrlEncode(email.Encrypt("verify")) + "\">" +
                            configuration.RootURL + "Account/VerifyChangedEmail?a=" +
                            HttpUtility.UrlEncode(email.Encrypt("verify")) + "</a>";

            SendEmail(To, "", "", "You asked to change your email! Email verification required.", msg);
            //Cryptography.Encrypt(Username, "verify")
        }


    }
}
