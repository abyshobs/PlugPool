using PlugPool.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlugPool.Domain.Code.Interfaces
{
    public interface ISessionWrapper
    {
        void ClearSession();
        bool ContainsInSession(string key);
        void RemoveFromSession(string key);
        //string GetQueryStringValue(string key);
        bool LoggedIn { get; set; }
        Account CurrentUser { get; set; }
        string Email { get; set; }
        string EmailToVerify { get; }
        int AccountID { get; }
              
    }
}
