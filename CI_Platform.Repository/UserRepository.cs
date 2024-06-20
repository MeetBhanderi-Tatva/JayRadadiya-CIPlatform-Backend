using CI_Platform.Entity.RequestModel;
using CI_Platform.Entity.ResponseModel;
using CI_Platform.Entity;
using CI_Platform.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CI_Platform.Entity.DBContext;
using Microsoft.EntityFrameworkCore;
using CI_Platform.Entity.Models;
using System.Reflection;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;
using Npgsql;
using Dapper;

namespace CI_Platform.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;
        private readonly string _connectionString;
        public UserRepository(AppDbContext context, IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
            _context = context;
        }

        public IDbContextTransaction BeginTransaction()
        {
            return _context.Database.BeginTransaction();
        }
        public async Task<User> FindUserAsync(LoginRequest model)
        {
            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == model.Email && x.Password == model.Password);
                if (user == null)
                {
                    throw new NullReferenceException();
                }
                return user;
            }
            catch (Exception)
            {
                throw;
            }

        }

        public async Task<User> FindUserByEmailAsync(string email)
        {
            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == email);
                if (user == null)
                {
                    throw new NullReferenceException();
                }
                return user;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task AddUserAsync(User user)
        {
            try
            {
                await _context.AddAsync(user);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task Save()
        {
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        

    }
}
