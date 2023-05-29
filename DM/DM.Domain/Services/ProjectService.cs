using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using AutoMapper;

using DM.Domain.DTO;
using DM.Domain.Interfaces;
using DM.Domain.Infrastructure.Exceptions;

using DM.DAL.Entities;
using DM.DAL.Interfaces;

using DM.Common.Enums;

namespace DM.Domain.Services
{
    public class ProjectService : IProjectService
    {

        private readonly UserDto _currentUser;

        private IUnitOfWork Context { get; set; }
        private readonly IMapper _mapper;

        public ProjectService(IUnitOfWork unitOfWork, IMapper mapper, CurrentUserService userService)
        {
            Context = unitOfWork;
            _mapper = mapper;
            _currentUser = userService.CurrentUser;
        }

        public async Task<IEnumerable<ProjectForReadDto>> GetAll()
        {
            var access = await GetAccess(_currentUser.RoleId, ActionEnum.Read);
            if (!access)
                throw new AccessDeniedException("Access Denied");

            var projects = await Context.Projects.GetAll();
            return _mapper.Map<IEnumerable<ProjectForReadDto>>(projects);
        }

        public ProjectForReadDto GetById(long? projectId)
        {
            if (projectId < 1) return null;

            var project = Context.Projects.GetById(projectId);
            return _mapper.Map<ProjectForReadDto>(project);
        }

        public async Task<long> Create(ProjectForReadDto projectForReadDto)
        {
            var project = _mapper.Map<Project>(new ProjectForReadDto
            {
                Title = projectForReadDto.Title,
                OrganizationId = projectForReadDto.OrganizationId,
                Items = projectForReadDto.Items,
                Users = projectForReadDto.Users,
            });

            var result = await Context.Projects.CreateProjectWithUsers(project);
            await Context.SaveAsync();

            if (projectForReadDto.UserIds != null && projectForReadDto.UserIds.Any())
            {
                foreach (var userId in projectForReadDto.UserIds)
                {
                    var userProject = _mapper.Map<UserProject>(new UserProjectDto
                    {
                        UserId = userId,
                        ProjectId = result.Entity.Id
                    });

                    await Context.UserProjects.Create(userProject);
                    await Context.SaveAsync();
                }
            }

            return project.Id;
        }

        public async Task<bool> Update(ProjectForUpdateDto projectForUpdateDto)
        {
            var project = Context.Projects.GetById(projectForUpdateDto.Id);

            if (project == null) return false;

            project.Title = projectForUpdateDto.Title;
            project.IsInArchive = projectForUpdateDto.IsInArchive;
            project.UpdatedAt = DateTime.UtcNow;

            Context.Projects.Update(project);
            await Context.SaveAsync();

            return true;
        }

        public async Task<bool> Archive(long? projectId)
        {
            var result = Context.Projects.Archive(projectId);
            await Context.SaveAsync();

            return result;
        }

        public async Task<bool> GetAccess(long roleId, ActionEnum action)
        {
            try
            {
                var access = await Context.Permissions.GetByRoleAndType(roleId, PermissionEnum.Project);

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
