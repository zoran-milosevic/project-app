using System;
using System.Threading.Tasks;
using AuthService.Interface.Repository;
using AuthService.Interface.Service;
using AuthService.Model.Entities;

namespace AuthService.Service
{
    public class TokenService : ITokenService
    {
        private readonly IUserService _userService;
        private readonly ITokenRepository _tokenRepository;

        public TokenService(IUserService userService, ITokenRepository tokenRepository)
        {
            _userService = userService;
            _tokenRepository = tokenRepository;
        }

        public async Task<string> GetToken(ApplicationUser user)
        {
            var userClaims = await _userService.GetUserClaimsAsync(user);

            var token = _tokenRepository.GenerateToken(userClaims);

            return token;
        }
    }
}