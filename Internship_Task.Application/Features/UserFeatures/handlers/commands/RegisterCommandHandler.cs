using AutoMapper;
using Azure.Core;
using Internship_Task.Application.DTOs;
using Internship_Task.Application.Features.UserFeatures.requests.commands;
using Internship_Task.Application.Interfaces;
using Internship_Task.Application.Responses;
using Internship_Task.Domain.Entities;
using Internship_Task.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Internship_Task.Application.Features.UserFeatures.handlers.commands
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, RegisterResponse>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly ITokenService _tokenService;
        private readonly IRefreshTokenRepository _refreshTokenRepository;

        public RegisterCommandHandler(IUserRepository userRepository , IMapper mapper , ITokenService tokenService
            , IRefreshTokenRepository refreshTokenRepository)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _tokenService = tokenService;
            _refreshTokenRepository = refreshTokenRepository;
        }
        public async Task<RegisterResponse> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            var user = _mapper.Map<User>(request.registerDTO);
            var result = await _userRepository.RegisterAsync(user, request.registerDTO.Password);
            var response = new RegisterResponse();

            if (result.Succeeded)
            {
                var accessToken = await _tokenService.CreateAsync(_mapper.Map<UserDTO>(user));
                var refreshToken = await _refreshTokenRepository.CreateAsync(user.Id);

                response.Success = true;
                response.Message = "You've registered successfully!";
                response.FirstName = user.FirstName;
                response.LastName = user.LastName;
                response.Email = user.Email;
                response.PhoneNumber = user.PhoneNumber;
                response.UserName = user.UserName;
                response.AccessToken = accessToken.Token;
                response.RefreshToken = refreshToken.Token;
            }
            else
            {
                response.Success = false;
                response.Message = string.Join("; ", result.Errors.Select(e => e.Description));
            }
            return response;
        }
    }
}
