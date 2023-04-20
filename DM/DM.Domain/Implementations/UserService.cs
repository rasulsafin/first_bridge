using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

using AutoMapper;

using Xbim.IO.Xml.BsConf;

using DM.Domain.Interfaces;
using DM.Domain.Models;
using DM.Domain.Helpers;

using DM.DAL;
using DM.DAL.Entities;

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
            var user = await _context.Users.Include(x => x.Role)
                                           .FirstOrDefaultAsync(x => x.Login == model.Login);

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
            var token = _configuration.GenerateJwtToken(user, user.Role.Name);

            return new AuthenticateResponse(user, token);
        }

        public async Task<IEnumerable<UserForReadModel>> GetAll()
        {
            var users = await _context.Users
                .Include(x => x.UserProjects).ThenInclude(y => y.Project)
                .ToListAsync();

            return _mapper.Map<List<UserForReadModel>>(users);
        }

        public UserForReadModel GetById(long userId)
        {
            if (userId < 1) return null;

            var user = _context.Users
                .Include(x => x.UserProjects).ThenInclude(y => y.Project)
                .FirstOrDefault(x => x.Id == userId);

            return _mapper.Map<UserForReadModel>(user);
        }

        public async Task<bool> Create(UserForCreateModel userModel)
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
                Password = hashedPass,
            });

            _context.Users.Add(user);

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> Update(UserForUpdateModel user)
        {
            var userForUpdate = await _context.Users.FirstOrDefaultAsync(x => x.Id == user.Id);

            if (userForUpdate == null) return false;

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

            if (user == null) return false;

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
