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
        private readonly UserModel _currentUser;

        private readonly IMapper _mapper;

        public ProjectService(DmDbContext context, IMapper mapper, CurrentUserService userService)
        {
            _context = context;
            _mapper = mapper;
            _currentUser = userService.CurrentUser;
        }

        public async Task<List<ProjectForReadModel>> GetAll()
        {
            var projects = await _context.Projects
                .Include(x => x.Template)
                .Include(x => x.Items)
                .Include(x => x.UserProjects).ThenInclude(y => y.User)
                .OrderBy(x => x.IsInArchive)
                .ToListAsync();

            return _mapper.Map<List<ProjectForReadModel>>(projects);
        }

        public async Task<ProjectForReadModel> GetById(long projectId)
        {
            var project = await _context.Projects
                .Include(x => x.Template)
                .Include(x => x.Items)
                .Include(x => x.UserProjects).ThenInclude(y => y.User)
                .FirstOrDefaultAsync(x => x.Id == projectId);

            if (project == null) return null;

            return _mapper.Map<ProjectForReadModel>(project);
        }

        public async Task<long> Create(ProjectForReadModel projectModel)
        {
            var project = _mapper.Map<ProjectEntity>(new ProjectForReadModel
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

        public async Task<bool> Update(ProjectForUpdateModel projectModel)
        {
            var fieldForUpdate = await _context.Projects.FirstOrDefaultAsync(x => x.Id == projectModel.Id);

            if (fieldForUpdate == null) return false;

            _context.Projects.Attach(fieldForUpdate);

            fieldForUpdate.Title = projectModel.Title;
            fieldForUpdate.IsInArchive = projectModel.IsInArchive;
            fieldForUpdate.UpdatedAt = projectModel.UpdatedAt;

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

            result.IsInArchive = true;

            _context.Projects.Update(result);

            await _context.SaveChangesAsync();

            return true;
        }
    }
}
