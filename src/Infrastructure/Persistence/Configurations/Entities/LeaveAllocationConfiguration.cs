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
    public class LeaveAllocationConfiguration : IEntityTypeConfiguration<LeaveAllocation>
    {
        public void Configure(EntityTypeBuilder<LeaveAllocation> builder)
        {
           builder.HasData(
                new LeaveAllocation
                {
                    Id = 1,
                    NumberOfDays = 10,
                    LeaveTypeId = 1,
                    Period = DateTime.Now.Year,
                    EmployeeId = "1"
                },
                new LeaveAllocation
                {
                    Id = 2,
                    NumberOfDays = 12,
                    LeaveTypeId = 2,
                    Period = DateTime.Now.Year,
                    EmployeeId = "2"
                }
                );
        }
    }
}
