﻿using System;
using System.Collections.Generic;
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

        public async Task<AuthenticateResponse> Authenticate(AuthenticateRequest request)
        {
            var user = await Context.Users.Authenticate(request.Login);

            if (user == null)
            {
                // todo: need to add logger
                return null;
            }

            var passwordChecker = PasswordHelper.VerifyHashedPassword(user.HashedPassword, request.Password);

            if (!passwordChecker) return null;

            var token = _configuration.GenerateJwtToken(user);

            return new AuthenticateResponse(token);
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
            if (userId < 1) return null;

            var user = Context.Users.GetById(userId);
            return _mapper.Map<UserForReadDto>(user);
        }

        public async Task<bool> Create(UserForCreateDto userForCreateDto)
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

        public async Task<bool> Update(UserForUpdateDto userForUpdateDto)
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

        public async Task<bool> Delete(long? userId)
        {
            var result = Context.Users.Delete(userId);
            await Context.SaveAsync();

            return result;
        }

        public async Task<PermissionDto> GetAccess(long roleId)
        {
            var access = await Context.Permissions.GetByRoleAndType(roleId, PermissionEnum.User);
            return _mapper.Map<PermissionDto>(access);
        }

        public void Dispose()
        {
            Context.Dispose();
        }
    }
}
