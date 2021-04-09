using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace AuthService.Interface.Repository
{
    public interface ITokenRepository
    {
        string GenerateToken(List<Claim> userClaims);
    }
}