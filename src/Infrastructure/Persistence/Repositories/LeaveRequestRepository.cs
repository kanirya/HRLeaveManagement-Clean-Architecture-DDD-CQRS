using Application.Persistence.Contracts;
using Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class LeaveRequestRepository:GenericRepository<LeaveRequest>, ILeaveRequestRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public LeaveRequestRepository(ApplicationDbContext dbContext):base(dbContext)
        {
            _dbContext=dbContext;
        }

        public async Task<List<LeaveRequest>> GetLeaveRequestsListWithDetails()
        {
           var result=await _dbContext.LeaveRequests.Include(u=>u.LeaveType).ToListAsync();
            return result;
        }

        public async Task<LeaveRequest> GetLeaveRequestWithDetails(int id)
        {
            var leaveRequest= await _dbContext.LeaveRequests.Include(u=>u.LeaveType).FirstOrDefaultAsync(q=>q.Id==id);
            return leaveRequest;
        }
    }
}
