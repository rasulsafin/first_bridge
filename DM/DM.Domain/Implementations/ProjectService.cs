﻿using AutoMapper;
using DM.DAL.Entities;
using DM.Domain.Helpers;
using DM.Domain.Interfaces;
using DM.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DM.DAL;

namespace DM.Domain.Implementations
{
    public class ProjectService : IProjectService
    {
        private readonly DmDbContext _context;
        private readonly IMapper _mapper;
        private readonly UserEntity _currentUser;

        public ProjectService(DmDbContext context, IMapper mapper, CurrentUserService userService)
        {
            _context = context;
            _mapper = mapper;
            _currentUser = userService.CurrentUser;
        }

        public async Task<List<ProjectModel>> GetAll()
        {
            var projects = await _context.Projects
                .Include(x => x.Template)
                .ToListAsync();

            var projectModel = new List<ProjectModel>();

            foreach (var project in projects)
            {
                if (_currentUser.Roles != "SuperAdmin")
                {
                    var permission = AuthorizationHelper.CheckUsersPermissionsById(_context, _currentUser, PermissionType.Project, project.Id);

                    if (permission == null || !permission.Read)
                    {
                        continue;
                    }
                }

                projectModel.Add(new ProjectModel()
                {
                    Id = project.Id,
                    OrganizationId = project.OrganizationId,
                    Title = project.Title,
                    Description = project.Description,
                });
            }
         
            return projectModel;
        }

        public async Task<ProjectModel> GetById(long projectId)
        {
            var project = await _context.Projects.Include(x => x.Template)
                .FirstOrDefaultAsync(x => x.Id == projectId);

            if (project == null)
            {
                return null;
            }

            var projectModel = new ProjectModel() 
            {   
                OrganizationId = project.OrganizationId,
                Title = project?.Title,
                Description = project.Description,
            };

            return projectModel;
        }

        public async Task<long> Create(ProjectModel projectModel)
        {
         //   var json = JsonDocument.Parse(projectModel.RecordTemplate.ToString());

            var project = new ProjectEntity
            {
                OrganizationId = projectModel.OrganizationId,
                Title = projectModel.Title,
                Description = projectModel.Description
            };

            var organization = _context.Organization.Include(x => x.Projects).First(x => x.Id == projectModel.OrganizationId);

            if (organization == null)
            {
                return 0;
            }

            // добавление зависимой сущности
            organization.Projects.Add(project);

            await _context.SaveChangesAsync();

            return project.Id;
        }
    }
}
