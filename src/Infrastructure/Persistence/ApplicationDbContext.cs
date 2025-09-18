using Domain;
using Domain.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override  void OnModelCreating(ModelBuilder modelBuilder)
        {
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


        public DbSet<LeaveRequest> LeaveRequests { get; set; }
        public DbSet<LeaveType> LeaveTypes { get; set; }
        public DbSet<LeaveAllocation> LeaveAllocations { get; set; }

    }
}
