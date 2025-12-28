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
    public class LeaveTypeConfiguration : IEntityTypeConfiguration<LeaveType>
    {
        public void Configure(EntityTypeBuilder<LeaveType> builder)
        {
            builder.HasData(
                new LeaveType
                {
                    Id = 4,
                    Name = "Vacation",
                    DefaultDays = 10,
                   
                   
                },
                new LeaveType
                {
                    Id = 5,
                    Name = "Sick",
                    DefaultDays = 12,


                },
                new LeaveType
                {
                    Id = 6,
                    Name = "Unpaid",
                    DefaultDays = 20,
                }
                );
        }
    }
}
