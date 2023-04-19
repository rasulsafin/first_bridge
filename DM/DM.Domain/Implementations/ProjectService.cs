using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

using Microsoft.EntityFrameworkCore;

using AutoMapper;

using DM.Domain.Models;
using DM.Domain.Interfaces;

using DM.DAL;
using DM.DAL.Entities;

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
                .Include(x => x.Items)
                .Include(x => x.UserProjects).ThenInclude(y => y.User)
                .ToListAsync();

            return _mapper.Map<List<ProjectModel>>(projects);
        }

        public async Task<ProjectModel> GetById(long projectId)
        {
            var project = await _context.Projects
                .Include(x => x.Template)
                .Include(x => x.Items)
                .Include(x => x.UserProjects).ThenInclude(y => y.User)
                .FirstOrDefaultAsync(x => x.Id == projectId);

            if (project == null) return null;

            return _mapper.Map<ProjectModel>(project);
        }

        public async Task<long> Create(ProjectModel projectModel)
        {
            var project = _mapper.Map<ProjectEntity>(new ProjectModel
            {
                Title = projectModel.Title,
                OrganizationId = projectModel.OrganizationId,
                Items = projectModel.Items.ToList(),
                Users = projectModel.Users.ToList(),
                IsInArchive = projectModel.IsInArchive
            });

            _context.Projects.Add(project);

            await _context.SaveChangesAsync();

            return project.Id;
        }

        public async Task<bool> Update(ProjectModel projectModel)
        {
            var fieldForUpdate = await _context.Projects.FirstOrDefaultAsync(x => x.Id == projectModel.Id);

            if (fieldForUpdate == null) return false;

            _context.Projects.Attach(fieldForUpdate);

            fieldForUpdate.Title = projectModel.Title;

            await _context.SaveChangesAsync();

            _context.Entry(fieldForUpdate).State = EntityState.Detached;

            return true;
        }

        public async Task<bool> Delete(long projectId)
        {
            var result = await _context.Projects
                .Include(x => x.Items)
                .Include(x => x.Records)
                .Include(x => x.Template)
                .FirstOrDefaultAsync(x => x.Id == projectId);

            if (result == null) return false;

            _context.Projects.Remove(result);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
