using AutoMapper;
using Internship_Task.Application.DTOs;
using Internship_Task.Application.Features.RefreshTokenFeatures.requests.commands;
using Internship_Task.Application.Interfaces;
using Internship_Task.Application.Responses;
using Internship_Task.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Internship_Task.Application.Features.RefreshTokenFeatures.handlers.commands
{
    public class ValidateRefreshTokenHandler : IRequestHandler<ValidateRefreshTokenCommand, TokensResponse>
    {
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly IUserRepository _userRepository;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;

        public ValidateRefreshTokenHandler(IRefreshTokenRepository refreshTokenRepository , IUserRepository userRepository
            ,ITokenService tokenService , IMapper mapper)
        {
            _refreshTokenRepository = refreshTokenRepository;
            _userRepository = userRepository;
            _tokenService = tokenService;
            _mapper = mapper;
        }
        public async Task<TokensResponse> Handle(ValidateRefreshTokenCommand request, CancellationToken cancellationToken)
        {
            var response = new TokensResponse();
            var token = request.RefreshTokenDTO.Token;
            var refreshToken = await _refreshTokenRepository.GetAsync(token);
            if(refreshToken == null || refreshToken.Expiration < DateTime.Now)
            {
                response.Success = false;
                response.Message = "Invalid refresh token!";
            }
            else
            {
                await _refreshTokenRepository.DeleteAsync(refreshToken.Token);
                var user = await _userRepository.GetUserAsync(refreshToken.UserId);
                if (user == null)
                {
                    response.Success = false;
                    response.Message = "User Not found";
                }
                else
                {
                    var accessToken = await _tokenService.CreateAsync(_mapper.Map<UserDTO>(user));
                    var new_refreshToken = await _refreshTokenRepository.CreateAsync(user.Id);
                    response.RefreshToken = new_refreshToken.Token;
                    response.AccessToken = accessToken.Token;
                    response.Message = "Token refreshed successfully.";
                    response.Success = true;
                }   
            }
            
            return response;
        }
    }
}
