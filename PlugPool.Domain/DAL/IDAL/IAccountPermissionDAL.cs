using PlugPool.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlugPool.Domain.DAL.IDAL
{
    public interface IAccountPermissionDAL
    {
        IEnumerable<AccountPermission> FetchAll();
        AccountPermission FetchByID(int id);
        AccountPermission FetchByEmail(string email);
        void Create(AccountPermission adminUser);
        void Update(AccountPermission adminUser);
        void Delete(int id);
    }
}
