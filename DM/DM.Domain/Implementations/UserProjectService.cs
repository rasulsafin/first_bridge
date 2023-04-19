using AutoMapper;
using DM.DAL;
using DM.DAL.Entities;
using DM.Domain.Interfaces;
using DM.Domain.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public async Task<bool> DeleteFromProject(long id)
        {
            var userProject = _context.UsersProjects.FirstOrDefault(q => q.Id == id);

            if (userProject == null) return false;

            _context.UsersProjects.Remove(userProject);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
