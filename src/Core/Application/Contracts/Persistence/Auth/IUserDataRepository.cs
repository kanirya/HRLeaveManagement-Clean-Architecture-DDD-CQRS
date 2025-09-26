using Application.DTOs.AuthDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Contracts.Persistence.Auth
{
    public interface IUserDataRepository
    {
        Task<UserDataDtos?> GetByIdAsync(Guid id);
    }
}
