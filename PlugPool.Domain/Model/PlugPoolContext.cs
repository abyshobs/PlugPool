using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlugPool.Domain.Model
{
    public class PlugPoolContext : DbContext
    {
        public PlugPoolContext() : base("PlugPoolContext") { }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<Profile> Profiles { get; set; }
        public DbSet<Job> Jobs { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<AccountPermission> AccountPermissions { get; set; }
        public DbSet<ProfileImage> ProfileImages { get; set; }
        public DbSet<Note> Notes { get; set; }
    }
}
