using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Internship_Task.Application.DTOs
{
    public record UserDTO(string Id, string Email, string UserName, string FirstName, string LastName);
}
