using Application.Persistence.Contracts;
using Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class LeaveAllocationRepository:GenericRepository<LeaveAllocation>, ILeaveAllocationRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public LeaveAllocationRepository(ApplicationDbContext dbContext):base(dbContext)
        {
            _dbContext=dbContext;
        }
        public async Task<List<LeaveAllocation>> GetLeaveAllocationsListWithDetails()
        {
            var result=await _dbContext.LeaveAllocations
                .Include(u=>u.LeaveType).ToListAsync();
            return result;
        }

        public async Task<LeaveAllocation> GetLeaveAllocationWithDetails(int id)
        {
           var leaveAllocation=await _dbContext.LeaveAllocations
                .Include(u=>u.LeaveType)
                .FirstOrDefaultAsync(q=>q.Id==id);
            return leaveAllocation;
        }
    }
}
