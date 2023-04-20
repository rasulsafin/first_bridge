using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

using Microsoft.Extensions.Configuration;

using AutoMapper;

using DM.Domain.Interfaces;
using DM.Domain.Models;

using DM.DAL;
using DM.DAL.Entities;

namespace DM.Domain.Implementations
{
    public class UserProjectService : IUserProjectService
    {
        private readonly DmDbContext _context;

        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public UserProjectService(DmDbContext context, IConfiguration configuration, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            _configuration = configuration;
        }

        public async Task<bool> AddToProject(UserProjectModel userProjectModel)
        {
            var userProject = _mapper.Map<UserProjectEntity>(new UserProjectModel
            {
                UserId = userProjectModel.UserId,
                ProjectId = userProjectModel.ProjectId
            });

            if (userProject == null) return false;

            _context.UsersProjects.Add(userProject);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> AddToProjects(List<UserProjectModel> userProjectsModel)
        {
            var userProject = _mapper.Map<List<UserProjectEntity>>(userProjectsModel);

            if (userProject == null) return false;

            _context.UsersProjects.AddRange(userProject);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteFromProject(long userId, long projectId)
        {
            var userProject = _context.UsersProjects.FirstOrDefault(q => q.UserId == userId && q.ProjectId == projectId);

            if (userProject == null) return false;

            _context.UsersProjects.Remove(userProject);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
