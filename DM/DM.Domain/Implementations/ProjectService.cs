using AutoMapper;
using DM.DAL.Entities;
using DM.Domain.Interfaces;
using DM.Domain.Models;
using DM.repository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
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
            var projects = await _context.UserProjects.Include(x => x.User).Include(x => x.Project).ToListAsync();

            var projectModel = new List<ProjectModel>();

            foreach (var project in projects)
            {
                projectModel.Add(new ProjectModel() { Title = project.Project.Title, User = new List<string> { project.User.Name } });
            }
         
            return projectModel;
        }

        public ProjectModel GetById(long projectId)
        {
            var project = _context.UserProjects.Include(x => x.User).Include(x => x.Project).Where(x => x.ProjectId == projectId).FirstOrDefault();
            var projectModel = new ProjectModel() { Title = project.Project.Title, User = new List<string> { project.User.Name } };

            return projectModel;
        }

        public async Task<long> Create(ProjectModel projectModel)
        {
            var project = _mapper.Map<ProjectEntity>(projectModel);
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

    }
}
