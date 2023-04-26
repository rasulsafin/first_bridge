using System;
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

            if (!passwordChecker) return null;

            var token = _configuration.GenerateJwtToken(user);

            return new AuthenticateResponse(token);
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

        public async Task<bool> Create(UserForCreateModel userForCreateModel)
        {
            var hashedPass = PasswordHelper.HashPassword(userForCreateModel.Password);
            var user = _mapper.Map<UserEntity>(new UserForCreateModel
            {
                Login = userForCreateModel.Login,
                Name = userForCreateModel.Name,
                LastName = userForCreateModel.LastName,
                FathersName = userForCreateModel.FathersName,
                Email = userForCreateModel.Email,
                RoleId = userForCreateModel.RoleId,
                Position = userForCreateModel.Position,
                OrganizationId = userForCreateModel.OrganizationId,
                Password = hashedPass,
            });

            _context.Users.Add(user);

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> Update(UserForUpdateModel userForUpdateModel)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == userForUpdateModel.Id);

            if (user == null) return false;

            _context.Users.Attach(user);

            user.Name = user.Name;
            user.LastName = user.LastName;
            user.FathersName = user.FathersName;
            user.Email = user.Email;
            user.Login = user.Login;
            user.Password = user.Password;
            user.Position = user.Position;
            user.RoleId = user.RoleId;
            user.OrganizationId = user.OrganizationId;
            user.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            _context.Entry(user).State = EntityState.Detached;

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
