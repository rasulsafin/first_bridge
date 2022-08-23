using AutoMapper;
using DM.Domain.Interfaces;
using DM.Domain.Models;
using DM.Entities;
using DM.repository;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using DM.Domain.Helpers;

namespace DM.Domain.Implementations
{
    public class UserService : IUserService
    {
        private readonly DmDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public UserService(DmDbContext context, IConfiguration configuration, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            _configuration = configuration;
        }

        public AuthenticateResponse Authenticate(AuthenticateRequest model)
        {
            var user = _context.Users.Include(x => x.Roles).FirstOrDefault(x => x.Login == model.Login);

            if (user == null)
            {
                // todo: need to add logger
                return null;
            }

            var passwordChecker = PasswordHelper.VerifyHashedPassword(user.Password, model.Password);

            if (passwordChecker == false)
            {
                return null;
            }
            var token = _configuration.GenerateJwtToken(user);

            return new AuthenticateResponse(user, token);
        }
        public List<UserModel> GetAll()
        {
            var users = _context.Users.ToList();
            return _mapper.Map<List<UserModel>>(users);
        }
        public UserModel GetById(long userId)
        {
            var user = _context.Users.FirstOrDefault(x => x.Id == userId);
            return _mapper.Map<UserModel>(user);
        }
        public async Task<long> Create(UserModel userModel)
        {
            var hashedPass = PasswordHelper.HashPassword(userModel.Password);
            var user = _mapper.Map<UserEntity>(new UserModel
            { Login = userModel.Login, Name = userModel.Name, Email = userModel.Email, Password = hashedPass });


            var result = await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return result.Entity.Id;
        }

        public async Task<bool> Update(UserModelForUpdate user)
        {
            var userForUpdate = await _context.Users.FirstOrDefaultAsync(x => x.Id == user.UserId);

            if (userForUpdate == null)
            {
                return false;
            }

            _context.Users.Attach(userForUpdate);

            userForUpdate.Name = user.Name;
            userForUpdate.Email = user.Email;
            userForUpdate.Login = user.Login;

            await _context.SaveChangesAsync();

            _context.Entry(userForUpdate).State = EntityState.Detached;

            return true;
        }

        public async Task<bool> Delete(long userId)
        {
            //TODO: check that the Records/fields do'nt contain users to be deleted

            var user = _context.Users.FirstOrDefault(q => q.Id == userId);

            if (user == null)
            {
                return false;
            }
            
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            
            return true;
        }
    }
}
