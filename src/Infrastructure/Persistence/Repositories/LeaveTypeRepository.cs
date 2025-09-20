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
    public class LeaveTypeRepository : GenericRepository<LeaveType>, ILeaveTypeRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public LeaveTypeRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext=dbContext;
        }

        public async Task<List<LeaveType>> GetLeaveTypesListWithDetails(CancellationToken ct)
        {

            return await _dbContext.LeaveTypes
        .FromSqlRaw("WAITFOR DELAY '00:00:05'; SELECT * FROM LeaveTypes")
        .ToListAsync(ct);

        }

        public async Task<LeaveType> GetLeaveTypeWithDetails(int id)
        {
            return await Get(id);
        }
    }
}
