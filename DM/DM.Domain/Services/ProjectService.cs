using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

using AutoMapper;

using DM.Domain.DTO;
using DM.Domain.Interfaces;

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
                Items = projectForReadDto.Items.ToList(),
                Users = projectForReadDto.Users.ToList(),
            });

            await Context.Projects.Create(project);
            await Context.SaveAsync();

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

        public async Task<PermissionDto> GetAccess(long roleId)
        {
            var access = await Context.Permissions.GetByRoleAndType(roleId, PermissionEnum.Project);
            return _mapper.Map<PermissionDto>(access);
        }

        public void Dispose()
        {
            Context.Dispose();
        }
    }
}
