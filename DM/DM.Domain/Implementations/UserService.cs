using AutoMapper;
using DM.Domain.Interfaces;
using DM.Domain.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DM.DAL;
using DM.DAL.Entities;
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

        public async Task<AuthenticateResponse> Authenticate(AuthenticateRequest model)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Login == model.Login);
            var userRole = await _context.Role.FirstOrDefaultAsync(x => x.Id == user.RoleId);

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
            var token = _configuration.GenerateJwtToken(user, userRole.Name);

            return new AuthenticateResponse(user, token);
        }

        public async Task<IEnumerable<UserModel>> GetAll()
        {
            var users = await _context.Users.ToListAsync();
            return _mapper.Map<List<UserModel>>(users);
        }

        public UserModel GetById(long userId)
        {
            if (userId < 1) return null;

            var user = _context.Users.FirstOrDefault(x => x.Id == userId);
            return _mapper.Map<UserModel>(user);
        }

        public async Task<bool> Create(UserModel userModel)
        {
            var hashedPass = PasswordHelper.HashPassword(userModel.Password);
            var user = _mapper.Map<UserEntity>(new UserModel
            {
                Login = userModel.Login,
                Name = userModel.Name,
                LastName = userModel.LastName,
                FathersName = userModel.FathersName,
                Email = userModel.Email,
                RoleId = userModel.RoleId,
                Position = userModel.Position,
                OrganizationId = userModel.OrganizationId,
                Password = hashedPass
            });

            var organization = _context.Organization.Include(x => x.Users).First(x => x.Id == userModel.OrganizationId);

            if (organization == null)
            {
                return false;
            }

            // добавление зависимой сущности
            organization.Users.Add(user);

            await _context.SaveChangesAsync();

            return true;
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
            userForUpdate.LastName = user.LastName;
            userForUpdate.FathersName = user.FathersName;
            userForUpdate.Email = user.Email;
            userForUpdate.Login = user.Login;
            userForUpdate.Password = user.Password;
            userForUpdate.Position = user.Position;
            userForUpdate.RoleId = user.RoleId;
            userForUpdate.OrganizationId = user.OrganizationId;

            await _context.SaveChangesAsync();

            _context.Entry(userForUpdate).State = EntityState.Detached;

            return true;
        }

        public async Task<bool> Delete(long userId)
        {
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
