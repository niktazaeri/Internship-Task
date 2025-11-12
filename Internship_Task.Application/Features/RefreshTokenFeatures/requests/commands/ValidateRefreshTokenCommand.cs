using Internship_Task.Application.DTOs;
using Internship_Task.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Internship_Task.Application.Features.RefreshTokenFeatures.requests.commands
{
    public class ValidateRefreshTokenCommand:IRequest<TokensResponse>
    {
        public RefreshTokenDTO RefreshTokenDTO { get; set; }
    }
}
