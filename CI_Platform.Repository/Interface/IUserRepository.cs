using CI_Platform.Entity;
using CI_Platform.Entity.Models;
using CI_Platform.Entity.RequestModel;
using CI_Platform.Entity.ResponseModel;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI_Platform.Repository.Interface
{
    public interface IUserRepository
    {

        IDbContextTransaction BeginTransaction();
        Task<User> FindUserAsync(LoginRequest model);

        Task<User> FindUserByEmailAsync(string email);

        Task AddUserAsync(User user);

        Task Save();
    }
}
