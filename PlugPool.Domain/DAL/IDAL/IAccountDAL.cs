using PlugPool.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlugPool.Domain.DAL.IDAL
{
    public interface IAccountDAL
    {
        void createAccount(Account account);
        Account FetchByEmail(string email);
        Account FetchByID(int id);
        void Update(Account account);
        IEnumerable<Account> fetchApproved();
        IEnumerable<Account> fetchAccounts();
        IEnumerable<Account> fetchPending();
        void Delete(int id);
    }
}
