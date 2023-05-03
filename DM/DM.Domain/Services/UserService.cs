using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

using AutoMapper;

using DM.Domain.Interfaces;
using DM.Domain.Models;
using DM.Domain.Helpers;

using DM.DAL.Entities;
using DM.DAL.Interfaces;

using DM.Common.Helpers;

namespace DM.Domain.Services
{
    public class UserService : IUserService
    {
        private IUnitOfWork Context { get; set; }
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly ILogger<UserService> _logger;

        public UserService(IUnitOfWork unitOfWork, IConfiguration configuration, IMapper mapper, ILogger<UserService> logger)
        {
            Context = unitOfWork;
            _mapper = mapper;
            _configuration = configuration;
            _logger = logger;
        }

        public async Task<AuthenticateResponse> Authenticate(AuthenticateRequest model)
        {
            var user = await Context.Users.Authenticate(model.Login);

            if (user == null)
            {
                // todo: need to add logger
                return null;
            }

            var passwordChecker = PasswordHelper.VerifyHashedPassword(user.HashedPassword, model.Password);

            if (!passwordChecker) return null;

            var token = _configuration.GenerateJwtToken(user);

            return new AuthenticateResponse(token);
        }

        public async Task<IEnumerable<UserForReadDto>> GetAll()
        {
            var users = await Context.Users.GetAll();
            return _mapper.Map<IEnumerable<UserForReadDto>>(users);
        }

        public UserForReadDto GetById(long? userId)
        {
            if (userId < 1) return null;

            var user = Context.Users.GetById(userId);
            return _mapper.Map<UserForReadDto>(user);
        }

        public async Task<bool> Create(UserForCreateDto userForCreateModel)
        {
            var hashedPass = PasswordHelper.HashPassword(userForCreateModel.HashedPassword);
            var user = _mapper.Map<User>(new UserForCreateDto
            {
                Login = userForCreateModel.Login,
                Name = userForCreateModel.Name,
                LastName = userForCreateModel.LastName,
                FathersName = userForCreateModel.FathersName,
                Email = userForCreateModel.Email,
                RoleId = userForCreateModel.RoleId,
                Position = userForCreateModel.Position,
                OrganizationId = userForCreateModel.OrganizationId,
                HashedPassword = hashedPass,
            });

            await Context.Users.Create(user);
            await Context.SaveAsync();

            return true;
        }

        public async Task<bool> Update(UserForUpdateDto userForUpdateModel)
        {
            var user = Context.Users.GetById(userForUpdateModel.Id);

            if (user == null) return false;

            user.Name = userForUpdateModel.Name;
            user.LastName = userForUpdateModel.LastName;
            user.FathersName = userForUpdateModel.FathersName;
            user.Email = userForUpdateModel.Email;
            user.Login = userForUpdateModel.Login;
            user.HashedPassword = userForUpdateModel.HashedPassword;
            user.Position = userForUpdateModel.Position;
            user.RoleId = userForUpdateModel.RoleId;
            user.OrganizationId = userForUpdateModel.OrganizationId;
            user.UpdatedAt = DateTime.UtcNow;

            Context.Users.Update(user);
            await Context.SaveAsync();

            return true;
        }

        public async Task<bool> Delete(long? userId)
        {
            var result = Context.Users.Delete(userId);
            await Context.SaveAsync();

            return result;
        }

        public void Dispose()
        {
            Context.Dispose();
        }
    }
}
