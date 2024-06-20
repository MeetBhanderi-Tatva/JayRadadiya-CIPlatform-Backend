using CI_Platform.Entity.RequestModel;
using CI_Platform.Entity;
using CI_Platform.Repository.Interface;
using CI_Platform.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CI_Platform.Entity.ResponseModel;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.Extensions.Configuration;
using AutoMapper;
using Microsoft.Win32;
using CI_Platform.Entity.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Web;
using CI_Platform.Helper.Helper;
using Microsoft.AspNetCore.Http;
using Org.BouncyCastle.Utilities.Net;
using Newtonsoft.Json;

namespace CI_Platform.Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepo;
        private readonly ICityRepository _cityRepo;
        private readonly IEmailService _emailService;
        public IConfiguration _configuration;
        public IMapper _mapper;
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private static readonly HashSet<string> ImageExtensions = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
    {
        ".jpg", ".jpeg", ".png", ".gif", ".bmp", ".tiff", ".ico", ".svg", ".webp"
    };

        private static readonly HashSet<string> VideoExtensions = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
    {
        ".mp4", ".mov", ".wmv", ".flv", ".avi", ".mkv", ".webm", ".mpeg", ".mpg", ".m4v"
    };
        public UserService(IUserRepository userRepository,ICityRepository cityRepository, IConfiguration config, IMapper mapper, IEmailService emailService, HttpClient httpClient, IHttpContextAccessor httpContextAccessor)
        {
            _userRepo = userRepository;
            _cityRepo = cityRepository;
            _emailService = emailService;
            _configuration = config;
            _mapper = mapper;
            _httpClient = httpClient;
            _httpContextAccessor = httpContextAccessor;
        }

        
        public async Task<JsonResult> UserLoginAsync(LoginRequest model)
        {
            try
            {
                var user = await _userRepo.FindUserAsync(model);
                if (user == null)
                {
                    return new JsonResult(new ApiResponse<string>
                    {
                        Result = false,
                        Message = "Invalid credential or user doesn't exist",
                        StatusCode = HttpStatusCode.NotFound.ToString(),
                    });
                }
                var token = GenerateToken(user, 60); //generate jwt token
                if (token == null)
                {
                    return new JsonResult(new ApiResponse<string>
                    {
                        Result = false,
                        Message = "Internal server error",
                        StatusCode = HttpStatusCode.InternalServerError.ToString(),
                    });
                }
                return new JsonResult(new ApiResponse<string>
                {
                    Data = token,
                    Result = true,
                    Message = "Login Successfully!",
                    StatusCode = HttpStatusCode.OK.ToString(),
                });
            }
            catch (Exception ex)
            {
                return new JsonResult(new ApiResponse<string>
                {
                    Result = false,
                    Message = ex.Message.ToString(),
                    StatusCode = HttpStatusCode.InternalServerError.ToString(),
                });
            }
        }
        public string GenerateToken(User user, int minutes)
        {
            try
            {
                var claims = new[] {
                        new Claim("UserId", user.UserId.ToString()),
                        new Claim("Email", user.Email)
                    };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
                var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var token = new JwtSecurityToken(
                    _configuration["Jwt:Issuer"],
                    _configuration["Jwt:Audience"],
                    claims: claims,
                    expires: DateTime.UtcNow.AddMinutes(minutes),
                    signingCredentials: signIn);

                return new JwtSecurityTokenHandler().WriteToken(token);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<JsonResult> RegisterUserAsync(RegisterRequest model)
        {
            try
            {
                var user = await _userRepo.FindUserByEmailAsync(model.Email);
                if (user != null)
                {
                    return new JsonResult(new ApiResponse<string>
                    {
                        Result = false,
                        Message = "User already exist",
                        StatusCode = HttpStatusCode.Forbidden.ToString(),
                    });
                }
                User userModel = _mapper.Map<User>(model);
                var ip = "202.131.123.10";
                var ipAddress = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress?.ToString();
                var ip1 = Dns.GetHostAddresses(Dns.GetHostName())[1].ToString();
                var response = await _httpClient.GetAsync($"https://ipinfo.io/{ip}/json?token=bd13c6a7e9b91f");

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    IpApiResponse locationInfo = JsonConvert.DeserializeObject<IpApiResponse>(result);

                    // Access the city property
                    var city = await _cityRepo.GetCityByName(locationInfo.City);
                    if (city == null)
                    {
                        return new JsonResult(new ApiResponse<string>
                        {
                            Result = false,
                            Message = "Internal server error",
                            StatusCode = HttpStatusCode.InternalServerError.ToString(),
                        });
                    }
                    else
                    {
                        userModel.CityId = city.CityId;
                        userModel.CountryId = city.CountryId;
                    }
                }
                else
                {
                    // Log or handle error
                    return new JsonResult(new ApiResponse<string>
                    {
                        Result = false,
                        Message = "Internal server error",
                        StatusCode = HttpStatusCode.InternalServerError.ToString(),
                    });
                }
                await _userRepo.AddUserAsync(userModel);
                return new JsonResult(new ApiResponse<string>
                {
                    Data = null,
                    Result = true,
                    Message = "User registered Successfully!",
                    StatusCode = HttpStatusCode.OK.ToString(),
                });
            }
            catch (Exception ex)
            {
                return new JsonResult(new ApiResponse<string>
                {
                    Result = false,
                    Message = ex.Message.ToString(),
                    StatusCode = HttpStatusCode.InternalServerError.ToString(),
                });
            }
        }
        public async Task<JsonResult> SendForgotPassLinkAsync(string email)
        {
            try
            {
                var user = await _userRepo.FindUserByEmailAsync(email);
                if (user == null)
                {
                    return new JsonResult(new ApiResponse<string>
                    {
                        Result = false,
                        Message = "Invalid credential or user doesn't exist",
                        StatusCode = HttpStatusCode.NotFound.ToString(),
                    });
                }
                var token = GenerateToken(user, 240); // genearte jwt token
                //Generate mail content
                MailRequest mailrequest = new(); //MailRequeest Model
                string resetPasswordUrl = $"http://localhost:4200/changepassword?email={email}&token={token}";
                string resetPasswordLink = $"<a href='{resetPasswordUrl}'>Reset Password</a>";
                string subject = "Reset Your Password";
                string body = "<b>Please find the Reset Password link. </b><br/>" + resetPasswordLink;
                mailrequest.ToEmail = email;
                mailrequest.Subject = subject;
                mailrequest.Body = body;
                await _emailService.SendEmailAsync(mailrequest); //call mailservice
                return new JsonResult(new ApiResponse<string>
                {
                    Result = true,
                    Message = "Mail sent succussfully!",
                    StatusCode = HttpStatusCode.OK.ToString(),
                });
            }
            catch (Exception ex)
            {
                return new JsonResult(new ApiResponse<string>
                {
                    Result = false,
                    Message = ex.Message.ToString(),
                    StatusCode = HttpStatusCode.InternalServerError.ToString(),
                });
            }
        }
        public async Task<JsonResult> ChangePasswordAsync(ChangePasswordRequest model)
        {
            try
            {
                var user = await _userRepo.FindUserByEmailAsync(model.Email);
                if (user == null)
                {
                    return new JsonResult(new ApiResponse<string>
                    {
                        Result = false,
                        Message = "Invalid credential or user doesn't exist",
                        StatusCode = HttpStatusCode.NotFound.ToString(),
                    });
                }
                if (!IsTokenValid(model.Email, model.Token))
                {
                    return new JsonResult(new ApiResponse<string>
                    {
                        Result = false,
                        Message = "Invalid token or token has expired",
                        StatusCode = HttpStatusCode.BadRequest.ToString(),
                    });
                }
                user.Password = model.Password;
                await _userRepo.Save();
                return new JsonResult(new ApiResponse<string>
                {
                    Result = true,
                    Message = "Password changed succussfully!",
                    StatusCode = HttpStatusCode.OK.ToString(),
                });
            }
            catch (Exception ex)
            {
                return new JsonResult(new ApiResponse<string>
                {
                    Result = false,
                    Message = ex.Message.ToString(),
                    StatusCode = HttpStatusCode.InternalServerError.ToString(),
                });
            }
        }
        public bool IsTokenValid(string email, string token) // To check jwt token provided during password change request is authorized or not and check expiration time of 4 hours 
        {
            try
            {
                ValidateToken(token, out JwtSecurityToken jwtToken);
                if (jwtToken == null)
                {
                    return false;
                }
                var tokenEmail = jwtToken.Claims.FirstOrDefault(claim => claim.Type == "Email")!.Value;
                //var expTime = jwtToken.Claims.FirstOrDefault(claim => claim.Type.Equals("exp"))!.Value;
                if (tokenEmail != null && tokenEmail == email)
                {
                    return true;
                }
                return false;
            }
            catch (Exception) { throw; }
        }
        public bool ValidateToken(string token, out JwtSecurityToken jwtSecurityToken)
        {
            try
            {
                jwtSecurityToken = null!;
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!);
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                jwtSecurityToken = (JwtSecurityToken)validatedToken;

                if (jwtSecurityToken == null)
                {
                    return false;
                }
                return true;
            }
            catch (SecurityTokenExpiredException)
            {
                jwtSecurityToken = null!;
                return false;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
