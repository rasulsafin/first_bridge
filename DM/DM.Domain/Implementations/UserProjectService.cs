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
            if (userProjectModel == null) return false;

            var userProject = _context.UsersProjects.FirstOrDefault(q => q.UserId == userProjectModel.UserId &&
                                                                    q.ProjectId == userProjectModel.ProjectId);

            if (userProject != null) return false;

            userProject = _mapper.Map<UserProjectEntity>(new UserProjectModel
            {
                UserId = userProjectModel.UserId,
                ProjectId = userProjectModel.ProjectId
            });

            _context.UsersProjects.Add(userProject);

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> AddToProjects(List<UserProjectModel> userProjectsModel)
        {
            if (userProjectsModel == null) return false;

            var up = _mapper.Map<List<UserProjectEntity>>(userProjectsModel);

            var userProjects = NormalizedList(up);

            _context.UsersProjects.AddRange(userProjects);

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteFromProject(long userId, long projectId)
        {
            if (userId < 1 && projectId < 1) return false;

            var userProject = _context.UsersProjects.FirstOrDefault(q => q.UserId == userId && q.ProjectId == projectId);

            _context.UsersProjects.Remove(userProject);

            await _context.SaveChangesAsync();

            return true;
        }

        private List<UserProjectEntity> NormalizedList(List<UserProjectEntity> userProjects)
        {
            var normalizedUserProjects = new List<UserProjectEntity>();

            foreach (var item in userProjects)
            {
                var userProject = _context.UsersProjects.FirstOrDefault(o => o.UserId == item.UserId && o.ProjectId == item.ProjectId);
                if (userProject == null)
                {
                    normalizedUserProjects.Add(item);
                }
            }

            return _mapper.Map<List<UserProjectEntity>>(normalizedUserProjects);
        }
    }
}
