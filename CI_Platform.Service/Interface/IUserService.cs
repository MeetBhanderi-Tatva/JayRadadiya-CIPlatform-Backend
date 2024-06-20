using CI_Platform.Entity;
using CI_Platform.Entity.RequestModel;
using CI_Platform.Entity.ResponseModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI_Platform.Service.Interface
{
    public interface IUserService
    {
        Task<JsonResult> UserLoginAsync(LoginRequest model);
        string GenerateToken(User user, int minutes);

        Task<JsonResult> RegisterUserAsync(RegisterRequest model);

        Task<JsonResult> SendForgotPassLinkAsync(string email);

        Task<JsonResult> ChangePasswordAsync(ChangePasswordRequest model);


        bool IsTokenValid(string email, string token);

        bool ValidateToken(string token, out JwtSecurityToken jwtSecurityToken);

        
    }
}
