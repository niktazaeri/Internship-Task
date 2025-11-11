using Internship_Task.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Internship_Task.Application.Interfaces
{
    public interface ITokenService
    {
        Task<AccessTokenDTO> CreateAsync(UserDTO userDTO);
    }
}
