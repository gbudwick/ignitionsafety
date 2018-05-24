using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IS.Data.Model;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace IS.Data.DbContexts
{
    public class IsDbContext : IdentityDbContext<IgnitionUser>
    {

        public IsDbContext ( DbContextOptions options ) : base ( options )
        {
             Database.Migrate ( );
        }

        public DbSet<EventLog> EventLogs { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<RosterMember> RosterMembers { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<SafetyZone> SafetyZones { get; set; }  
        public DbSet<MembersStatus> MembersStatuses { get; set; }
       // public DbSet<DepartmentSafetyZone> DepartmentSafetyZones { get; set; }

        public override int SaveChanges ( )
        {
            AddTimestamps ( );
            return base.SaveChanges ( );
        }



        private void AddTimestamps ( )
        {
            var entities =
                ChangeTracker.Entries ( )
                    .Where (
                        x => x.Entity is BaseEntity && ( x.State == EntityState.Added || x.State == EntityState.Modified ) );

            //var currentUsername = !string.IsNullOrEmpty(System.Web.HttpContext.Current?.User?.Identity?.Name)
            //    ? HttpContext.Current.User.Identity.Name
            //    : "Anonymous";

            foreach ( var entity in entities )
            {
                if ( entity.State == EntityState.Added )
                {
                    ( ( BaseEntity ) entity.Entity ).DateCreated = DateTime.UtcNow;
                    //((BaseEntity)entity.Entity).UserCreated = currentUsername;
                }

                ( ( BaseEntity ) entity.Entity ).DateModified = DateTime.UtcNow;
                // ((BaseEntity)entity.Entity).UserModified = currentUsername;
            }

        }
        
    }
}
