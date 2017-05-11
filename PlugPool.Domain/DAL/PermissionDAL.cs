using PlugPool.Domain.DAL.IDAL;
using PlugPool.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlugPool.Domain.DAL
{
    public class PermissionDAL : IPermissionDAL
    {
        //creates an instance of the database
        private readonly PlugPoolContext _db;

        public PermissionDAL(PlugPoolContext db)
        {
            _db = db;
        }

        public IEnumerable<Permission> FetchAll()
        {
            return _db.Permissions.ToList();
        }

        public Permission FetchByID(int id)
        {
            return _db.Permissions.FirstOrDefault(p => p.permissionID == id);
        }

        public Permission FetchByName(string name)
        {
            return _db.Permissions.FirstOrDefault(p => p.name == name);
        }

        //creates a permission and adds it to the database
        public void CreatePermission(Permission permission)
        {
            _db.Permissions.Add(permission);
            _db.SaveChanges();
        }

        public void Update(Permission permission)
        {
            Permission originalPermission = _db.Permissions.Find(permission.permissionID);
            originalPermission.permissionID = permission.permissionID;
            originalPermission.name = permission.name;
            _db.SaveChanges();
        }


        public void Delete(int id)
        {
            Permission permission = _db.Permissions.Find(id);
            _db.Permissions.Remove(permission);
            _db.SaveChanges();
        }

    }
}
