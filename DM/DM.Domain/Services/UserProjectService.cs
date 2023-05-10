using System.Collections.Generic;
using System.Threading.Tasks;

using AutoMapper;

using DM.Domain.Interfaces;
using DM.Domain.DTO;

using DM.DAL.Entities;
using DM.DAL.Interfaces;
using DM.Common.Enums;
using System;

namespace DM.Domain.Services
{
    public class UserProjectService : IUserProjectService
    {
        private IUnitOfWork Context { get; set; }
        private readonly IMapper _mapper;

        public UserProjectService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            Context = unitOfWork;
            _mapper = mapper;
        }

        public async Task<bool> AddToProject(UserProjectDto userProjectDto)
        {
            var isExist = await Context.UserProjects.IsExist(userProjectDto.UserId, userProjectDto.ProjectId);

            if (isExist) return false;

            var userProject = _mapper.Map<UserProject>(new UserProjectDto
            {
                UserId = userProjectDto.UserId,
                ProjectId = userProjectDto.ProjectId
            });

            await Context.UserProjects.Create(userProject);
            await Context.SaveAsync();

            return true;
        }

        public async Task<bool> AddToProjects(List<UserProjectDto> userProjectsDto)
        {
            if (userProjectsDto == null) return false;

            var up = _mapper.Map<List<UserProject>>(userProjectsDto);

            var userProjects = await NormalizedList(up);

            await Context.UserProjects.AddToProjects(userProjects);
            await Context.SaveAsync();

            return true;
        }

        public async Task<bool> DeleteFromProject(long userId, long projectId)
        {
            if (userId < 1 && projectId < 1) return false;

            await Context.UserProjects.DeleteFromProject(userId, projectId);
            await Context.SaveAsync();

            return true;
        }

        private async Task<List<UserProject>> NormalizedList(List<UserProject> userProjects)
        {
            var normalizedUserProjects = new List<UserProject>();

            foreach (var item in userProjects)
            {
                var userProject = await Context.UserProjects.IsExist(item.UserId, item.ProjectId);
                if (!userProject)
                {
                    normalizedUserProjects.Add(item);
                }
            }

            return _mapper.Map<List<UserProject>>(normalizedUserProjects);
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
