using PlugPool.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlugPool.Domain.DAL.IDAL
{
    public interface IPermissionDAL
    {
        void CreatePermission(Permission permission);
        void Update(Permission permission);
        void Delete(int id);
        IEnumerable<Permission> FetchAll();
        Permission FetchByID(int id);
        Permission FetchByName(string name);
    }
}
