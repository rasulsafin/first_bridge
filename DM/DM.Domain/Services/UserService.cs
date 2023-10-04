using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

using AutoMapper;

using DM.Domain.Interfaces;
using DM.Domain.DTO;
using DM.Domain.Infrastructure;

using DM.DAL.Entities;
using DM.DAL.Interfaces;

using DM.Common.Helpers;
using DM.Common.Enums;
using offline_module.Domain.Interfaces;

namespace DM.Domain.Services
{
    public class UserService : IUserService
    {
        private IUnitOfWork Context { get; set; }
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly ILogger<UserService> _logger;
        private readonly ISyncDotMimService _syncService;
        private readonly IBearerConfigService _bearerConfigService;

        public UserService(IUnitOfWork unitOfWork, IConfiguration configuration, ISyncDotMimService syncService, IMapper mapper, ILogger<UserService> logger, IBearerConfigService bearerConfigService)
        {
            Context = unitOfWork;
            _mapper = mapper;
            _configuration = configuration;
            _logger = logger;
            _syncService = syncService;
            _bearerConfigService = bearerConfigService;
        }

        public async Task<UserDto> Authenticate(AuthenticateRequest request)
        {
            try
            {
                var user = await Context.Users.Authenticate(request.Login);

                if (user == null)
                {
                    // todo: need to add logger
                    throw new UnauthorizedAccessException();
                }
                var passwordChecker = PasswordHelper.VerifyHashedPassword(user.HashedPassword, request.Password);

                if (!passwordChecker) return null;

                var token = _configuration.GenerateJwtToken(user);
                _bearerConfigService.WriteFile(token);

                return _mapper.Map<UserDto>(user);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IEnumerable<UserForReadDto>> GetAll()
        {
            try
            {
                var users = await Context.Users.GetAll();
                return _mapper.Map<IEnumerable<UserForReadDto>>(users);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public UserForReadDto GetById(long? userId)
        {
            try
            {
                if (userId < 1) return null;

                var user = Context.Users.GetById(userId);
                return _mapper.Map<UserForReadDto>(user);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> Create(UserForCreateDto userForCreateDto)
        {
            try
            {
                var hashedPass = PasswordHelper.HashPassword(userForCreateDto.HashedPassword);
                var user = _mapper.Map<User>(new UserForCreateDto
                {
                    Login = userForCreateDto.Login,
                    Name = userForCreateDto.Name,
                    LastName = userForCreateDto.LastName,
                    FathersName = userForCreateDto.FathersName,
                    Email = userForCreateDto.Email,
                    RoleId = userForCreateDto.RoleId,
                    Position = userForCreateDto.Position,
                    OrganizationId = userForCreateDto.OrganizationId,
                    HashedPassword = hashedPass,
                });

                await Context.Users.Create(user);
                await Context.SaveAsync();

                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<long> CreateUserWithProjects(UserForCreateDto userForCreateDto)
        {
            try
            {
                var hashedPass = PasswordHelper.HashPassword(userForCreateDto.HashedPassword);
                var user = _mapper.Map<User>(new UserForCreateDto
                {
                    Login = userForCreateDto.Login,
                    Name = userForCreateDto.Name,
                    LastName = userForCreateDto.LastName,
                    FathersName = userForCreateDto.FathersName,
                    Email = userForCreateDto.Email,
                    RoleId = userForCreateDto.RoleId,
                    Position = userForCreateDto.Position,
                    OrganizationId = userForCreateDto.OrganizationId,
                    HashedPassword = hashedPass,
                });

                var result = await Context.Users.CreateUserWithProjects(user);

                await Context.SaveAsync();

                if (userForCreateDto.ProjectsIds != null && userForCreateDto.ProjectsIds.Any())
                {
                    foreach (var projectId in userForCreateDto.ProjectsIds)
                    {
                        var userProject = _mapper.Map<UserProject>(new UserProjectDto
                        {
                            UserId = result.Entity.Id,
                            ProjectId = projectId
                        });

                        await Context.UserProjects.Create(userProject);
                        await Context.SaveAsync();
                    }
                }

                return result.Entity.Id;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> Update(UserForUpdateDto userForUpdateDto)
        {
            try
            {
                var user = Context.Users.GetById(userForUpdateDto.Id);

                if (user == null) return false;

                user.Name = userForUpdateDto.Name;
                user.LastName = userForUpdateDto.LastName;
                user.FathersName = userForUpdateDto.FathersName;
                user.Email = userForUpdateDto.Email;
                user.Login = userForUpdateDto.Login;
                user.HashedPassword = userForUpdateDto.HashedPassword;
                user.Position = userForUpdateDto.Position;
                user.RoleId = userForUpdateDto.RoleId;
                user.OrganizationId = userForUpdateDto.OrganizationId;
                user.UpdatedAt = DateTime.UtcNow;

                Context.Users.Update(user);
                await Context.SaveAsync();

                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> Delete(long? userId)
        {
            try
            {
                var result = Context.Users.Delete(userId);
                await Context.SaveAsync();

                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> GetAccess(long roleId, ActionEnum action)
        {
            try
            {
                var access = await Context.Permissions.GetByRoleAndType(roleId, PermissionEnum.User);

                return action switch
                {
                    ActionEnum.Read => access.Read,
                    ActionEnum.Create => access.Create,
                    ActionEnum.Delete => access.Delete,
                    ActionEnum.Update => access.Update,
                    _ => false,
                };
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void Dispose()
        {
            Context.Dispose();
        }
    }
}
