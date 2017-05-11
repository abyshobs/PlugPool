using PlugPool.Domain.DAL.IDAL;
using PlugPool.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlugPool.Domain.DAL
{
    public class AccountPermissionDAL : IAccountPermissionDAL
    {
        //creates an instance of the database
        private readonly PlugPoolContext _db;

        public AccountPermissionDAL(PlugPoolContext db)
        {
            _db = db;
        }

        public IEnumerable<AccountPermission> FetchAll()
        {
            return _db.AccountPermissions.ToList();
        }

        public AccountPermission FetchByID(int id)
        {
            return _db.AccountPermissions.FirstOrDefault(a => a.accountPermissionID == id);
        }

        public AccountPermission FetchByEmail(string email)
        {
            return _db.AccountPermissions.FirstOrDefault(a => a.email == email);
        }

        public void Create(AccountPermission adminUser)
        {
            _db.AccountPermissions.Add(adminUser);
            _db.SaveChanges();
        }

        public void Update(AccountPermission adminUser)
        {
            AccountPermission originalAdminUser = _db.AccountPermissions.Find(adminUser.accountPermissionID);
            originalAdminUser.accountPermissionID = adminUser.accountPermissionID;
            originalAdminUser.accountID = adminUser.accountID;
            originalAdminUser.permissionID = adminUser.permissionID;
            originalAdminUser.email = adminUser.email;
            originalAdminUser.updateDate = DateTime.Now;
            _db.SaveChanges();
        }

        public void Delete(int id)
        {
            AccountPermission adminUser = _db.AccountPermissions.Find(id);
            _db.AccountPermissions.Remove(adminUser);
            _db.SaveChanges();

        }
    }
}
