using AutoMapper;
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
    public class LoginCommandHandler : IRequestHandler<LoginCommand, TokensResponses>
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;
        private readonly IRefreshTokenRepository _refreshTokenRepository;

        public LoginCommandHandler(IUserRepository userRepository , ITokenService tokenService , IMapper mapper
            ,IRefreshTokenRepository refreshTokenRepository)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
            _mapper = mapper;
            _refreshTokenRepository = refreshTokenRepository;
        }
        public async Task<TokensResponses> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var response = new TokensResponses();
            var user = await _userRepository.LoginAsync(request.loginDTO.username, request.loginDTO.password);
            if(user == null)
            {
                response.Success = false;
                response.Message = "Invalid username or password.";
            }
            else
            {
                response.Success = true;
                response.Message = "You've logged in successfully.";
                var accessToken = await _tokenService.CreateAsync(_mapper.Map<UserDTO>(user));
                var refreshToken = await _refreshTokenRepository.CreateAsync(user.Id);
                response.AccessToken = accessToken.Token;
                response.RefreshToken = refreshToken.Token;
            }
            return response;
        }
    }
}
