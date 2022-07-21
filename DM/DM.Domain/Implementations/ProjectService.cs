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
            var projects = await _context.Projects.Include(u => u.Users).ToListAsync();
            return _mapper.Map<List<ProjectModel>>(projects);
        }

        public ProjectModel GetById(long projectId)
        {
            var project = _context.Projects.Where(x => x.Id == projectId).Include(u => u.Users).FirstOrDefault();
            return _mapper.Map<ProjectModel>(project);
        }

        public async Task<long> Create(ProjectModel projectModel)
        {
            /*
            var project = _mapper.Map<ProjectEntity>(new ProjectModel { Title = projectModel.Title});
            var result = await _context.Projects.AddAsync(project);
            await _context.SaveChangesAsync();

            foreach (var user in projectModel.Users)
            {
                var projectUser = await _context.Users.Where(u => u.Id == user).FirstOrDefaultAsync();
                foreach (var )
                projectUser.Projects = project.Id;
            }
            
            return result.Entity.Id;
            */

            
            var project = _mapper.Map<ProjectEntity>(projectModel);
            var result = await _context.Projects.AddAsync(project);
            await _context.SaveChangesAsync();
            return result.Entity.Id;
            
        }

    }
}
