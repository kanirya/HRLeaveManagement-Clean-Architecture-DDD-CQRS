using Application.Persistence.Contracts;
using Dapper;
using Domain;
using Microsoft.EntityFrameworkCore;
using Persistence.data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class LeaveRequestRepository:GenericRepository<LeaveRequest>, ILeaveRequestRepository
    {
        private readonly DapperContext _dapper;
        private readonly ApplicationDbContext _dbContext;
        public LeaveRequestRepository(ApplicationDbContext dbContext, DapperContext dapperContext) : base(dbContext)
        {
            _dbContext=dbContext;
            _dapper=dapperContext;
        }

        //public async Task<List<LeaveRequest>> GetLeaveRequestsListWithDetails()
        //{

        //   var result=await _dbContext.LeaveRequests.Include(u=>u.LeaveType).ToListAsync();
        //    return result;
        //}

        //public async Task<LeaveRequest> GetLeaveRequestWithDetails(int id)
        //{
        //    var leaveRequest= await _dbContext.LeaveRequests.Include(u=>u.LeaveType).FirstOrDefaultAsync(q=>q.Id==id);
        //    return leaveRequest;
        //}


        public async Task<List<LeaveRequest>> GetLeaveRequestsListWithDetails()
        {
            var query = @"
            SELECT lr.*, lt.Id, lt.Name 
            FROM LeaveRequests lr
            INNER JOIN LeaveTypes lt ON lr.LeaveTypeId = lt.Id";

            using var connection = _dapper.CreateConnection();

            var result = await connection.QueryAsync<LeaveRequest, LeaveType, LeaveRequest>(
                query,
                (leaveRequest, leaveType) =>
                {
                    leaveRequest.LeaveType = leaveType;
                    return leaveRequest;
                },
                splitOn: "Id" // tells Dapper where LeaveType starts
            );

            return result.ToList();
        }

        // ✅ 2. Get single leave request with details
        public async Task<LeaveRequest> GetLeaveRequestWithDetails(int id)
        {
            var query = @"
            SELECT lr.*, lt.Id, lt.Name 
            FROM LeaveRequests lr
            INNER JOIN LeaveTypes lt ON lr.LeaveTypeId = lt.Id
            WHERE lr.Id = @Id";

            using var connection = _dapper.CreateConnection();

            var result = await connection.QueryAsync<LeaveRequest, LeaveType, LeaveRequest>(
                query,
                (leaveRequest, leaveType) =>
                {
                    leaveRequest.LeaveType = leaveType;
                    return leaveRequest;
                },
                new { Id = id },
                splitOn: "Id"
            );

            return result.FirstOrDefault();
        }
    }
}
