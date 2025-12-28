using Domain;
using Domain.Auth;
using Domain.Common;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Persistence.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.data
{
    public class ApplicationDbContext:IdentityDbContext<ApplicationUser, ApplicationRole, Guid>    
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }





        public DbSet<LeaveRequest> LeaveRequests { get; set; }
        public DbSet<LeaveType> LeaveTypes { get; set; }
        public DbSet<LeaveAllocation> LeaveAllocations { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        protected override  void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        }





        override public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
         foreach(var entry in ChangeTracker.Entries<BaseDomainEntity>())
            {
               entry.Entity.LastModifiedDate=DateTime.Now;
                if(entry.State==EntityState.Added)
                {
                    entry.Entity.DateCreated=DateTime.Now;
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }


      

    }
}
