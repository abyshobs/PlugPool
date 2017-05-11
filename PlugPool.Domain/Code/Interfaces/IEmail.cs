using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlugPool.Domain.Code.Interfaces
{
    public interface IEmail
    {
        void SendEmail(string From, string Subject, string Message);
        void SendEmail(string To, string CC, string BCC, string Subject, string Message);
        void SendEmail(string[] To, string[] CC, string[] BCC, string Subject, string Message);
        void SendEmailAddressVerificationEmail(string email, string To);
        void PasswordRecoveryEmail(string email, string To);
        void SendProfileApprovedEmail(string email, string To);
        void SendProfileDeclinedEmail(string email, string To, string msg);
        void SendChangedEmailVerificationEmail(string email, string To);
    }
}
