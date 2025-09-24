using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Configurations.Entities
{
    public class LeaveRequestConfiguration : IEntityTypeConfiguration<LeaveRequest>
    {
        public void Configure(EntityTypeBuilder<LeaveRequest> builder)
        {
            builder.HasData(
                new LeaveRequest
                {
                    Id = 1,
                    StartDate = DateTime.Now,
                    EndDate = DateTime.Now.AddDays(5),
                    LeaveTypeId = 1,
                    DateRequested = DateTime.Now,
                    RequestComments = "Need a break",
                    DateActioned = null,
                    Approved = null,
                    Cancelled = false,
                    RequestingEmployeeId = "1"
                },
                new LeaveRequest
                {
                    Id = 2,
                    StartDate = DateTime.Now.AddDays(10),
                    EndDate = DateTime.Now.AddDays(15),
                    LeaveTypeId = 2,
                    DateRequested = DateTime.Now,
                    RequestComments = "Feeling sick",
                    DateActioned = null,
                    Approved = null,
                    Cancelled = false,
                    RequestingEmployeeId = "2"
                }
                );
        }
    }
}
