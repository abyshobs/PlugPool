using PlugPool.Domain.DAL.IDAL;
using PlugPool.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlugPool.Domain.DAL
{
    public class accountDAL : IAccountDAL
    {
        //creates an instance of the database
        private readonly PlugPoolContext _db;

        public accountDAL(PlugPoolContext db)
        {
            _db = db;
        }

        //creates an account and adds it to the database
        public void createAccount(Account account)
        {
            _db.Accounts.Add(account);
            _db.SaveChanges();
        }

        //fetches an account by its email
        public Account FetchByEmail(string email)
        {
            return _db.Accounts.FirstOrDefault(e => e.email == email);
        }

        //fetches an account by its email
        public Account FetchByID(int id)
        {
            return _db.Accounts.FirstOrDefault(a => a.accountID == id);
        }

        public void Update(Account account)
        {
            Account originalAccount = _db.Accounts.Find(account.accountID);
            originalAccount.accountID = account.accountID;
            originalAccount.firstName = account.firstName;
            originalAccount.lastName = account.lastName;
            originalAccount.updateDate = DateTime.Now;
            originalAccount.password = account.password;
            originalAccount.email = account.email;
            _db.SaveChanges();
        }

        public IEnumerable<Account> fetchApproved()
        {
            return _db.Accounts.Where(a => a.Profile.isApproved == true).ToList();
        }

        public IEnumerable<Account> fetchAccounts()
        {
            return _db.Accounts.ToList();
        }

        public IEnumerable<Account> fetchSuspended()
        {
            return _db.Accounts.Where(a => a.Profile.isSuspended == true).ToList();
        }

        public IEnumerable<Account> fetchPending()
        {
            return _db.Accounts.Where(a => a.Profile.isApproved == false).ToList();
        }

        //Remove an account
        public void Delete(int id)
        {
            Account account = _db.Accounts.Find(id);
            if (account.Profile != null)
            {
                _db.Profiles.Remove(account.Profile);
                _db.Accounts.Remove(account);
                _db.SaveChanges();

            }
            else if (account.Profile == null)
            {
                _db.Accounts.Remove(account);
                _db.SaveChanges();
            }
        }


    }
}
