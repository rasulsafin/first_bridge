using AutoMapper;
using DM.DAL.Entities;
using DM.Domain.Interfaces;
using DM.Domain.Models;
using DM.repository;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace DM.Domain.Implementations
{
    public class ProjectService : IProjectService
    {
        private readonly DmDbContext _context;
        private readonly IMapper _mapper;

        public ProjectService(DmDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<ProjectModel>> GetAll()
        {
            var projects = await _context.UserProjects
                .Include(x => x.User)
                .Include(x => x.Project)
                .ToListAsync();

            var projectModel = new List<ProjectModel>();

            foreach (var project in projects)
            {
                projectModel.Add(new ProjectModel() { 
                    Id = project.Project.Id, 
                    Title = project.Project.Title, 
                    User = new List<string> { project.User.Name }, 
                    Description = project.Project.Description, 
                    RecordTemplate = JObject.Parse(project.Project.RecordTemplate.RootElement.ToString())
                });
            }
         
            return projectModel;
        }

        public ProjectModel GetById(long projectId)
        {
            var project = _context.UserProjects
                .Include(x => x.User)
                .Include(x => x.Project)
                .FirstOrDefault(x => x.ProjectId == projectId);
            var projectModel = new ProjectModel() { Title = project?.Project.Title, User = new List<string> { project?.User.Name } };

            return projectModel;
        }

        public async Task<long> Create(ProjectModel projectModel)
        {
            var json = JsonDocument.Parse(projectModel.RecordTemplate.ToString());

            var project = new ProjectEntity
            {
                Title = projectModel.Title,
                Description = projectModel.Description,
                RecordTemplate = json
            };

            var result = await _context.Projects.AddAsync(project);
            await _context.SaveChangesAsync();

            foreach (var user in projectModel.User)
            {
                var userProj = new UserProjectEntity
                {
                    UserId = await _context.Users.Where(x => x.Login == user).Select(x => x.Id).FirstOrDefaultAsync(),
                    ProjectId = result.Entity.Id
                };

                await _context.UserProjects.AddAsync(userProj);
            }
            await _context.SaveChangesAsync();

            return result.Entity.Id;
        }

        public async Task<JsonDocument> GetProjectTemplateOfRecord(long projectId)
        {
            var project = await _context.Projects.Where(x => x.Id == projectId).Select(q => q.RecordTemplate).FirstOrDefaultAsync();

            return project;
        }

    }
}
