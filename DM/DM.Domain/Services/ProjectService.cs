using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

using Microsoft.EntityFrameworkCore;

using AutoMapper;

using DM.Domain.Models;
using DM.Domain.Interfaces;

using DM.DAL;
using DM.DAL.Entities;
using System;
using DM.DAL.Interfaces;
using DM.Domain.Helpers;

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

        public async Task<long> Create(ProjectForReadDto projectForReadModel)
        {
            var project = _mapper.Map<Project>(new ProjectForReadDto
            {
                Title = projectForReadModel.Title,
                OrganizationId = projectForReadModel.OrganizationId,
                Items = projectForReadModel.Items.ToList(),
                Users = projectForReadModel.Users.ToList(),
            });

            await Context.Projects.Create(project);
            await Context.SaveAsync();

            return project.Id;
        }

        public async Task<bool> Update(ProjectForUpdateDto projectForUpdateModel)
        {
            var project = Context.Projects.GetById(projectForUpdateModel.Id);

            if (project == null) return false;

            project.Title = projectForUpdateModel.Title;
            project.IsInArchive = projectForUpdateModel.IsInArchive;
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

        public void Dispose()
        {
            Context.Dispose();
        }
    }
}
