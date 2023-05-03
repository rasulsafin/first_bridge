using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

using Microsoft.Extensions.Configuration;

using AutoMapper;

using DM.Domain.Interfaces;
using DM.Domain.Models;

using DM.DAL;
using DM.DAL.Entities;
using DM.DAL.Interfaces;

namespace DM.Domain.Services
{
    public class UserProjectService : IUserProjectService
    {
        private IUnitOfWork Context { get; set; }
        private readonly IMapper _mapper;

        public UserProjectService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            Context = unitOfWork;
            _mapper = mapper;
        }

        public async Task<bool> AddToProject(UserProjectDto userProjectModel)
        {
            var isExist = await Context.UserProjects.IsExist(userProjectModel.UserId, userProjectModel.ProjectId);

            if (isExist) return false;

            var userProject = _mapper.Map<UserProject>(new UserProjectDto
            {
                UserId = userProjectModel.UserId,
                ProjectId = userProjectModel.ProjectId
            });

            await Context.UserProjects.Create(userProject);
            await Context.SaveAsync();

            return true;
        }

        public async Task<bool> AddToProjects(List<UserProjectDto> userProjectsModel)
        {
            if (userProjectsModel == null) return false;

            var up = _mapper.Map<List<UserProject>>(userProjectsModel);

            var userProjects = await NormalizedList(up);

            await Context.UserProjects.AddToProjects(userProjects);
            await Context.SaveAsync();

            return true;
        }

        public async Task<bool> DeleteFromProject(long userId, long projectId)
        {
            if (userId < 1 && projectId < 1) return false;

            await Context.UserProjects.DeleteFromProject(userId, projectId);
            await Context.SaveAsync();

            return true;
        }

        private async Task<List<UserProject>> NormalizedList(List<UserProject> userProjects)
        {
            var normalizedUserProjects = new List<UserProject>();

            foreach (var item in userProjects)
            {
                var userProject = await Context.UserProjects.IsExist(item.UserId, item.ProjectId);
                if (!userProject)
                {
                    normalizedUserProjects.Add(item);
                }
            }

            return _mapper.Map<List<UserProject>>(normalizedUserProjects);
        }

        public void Dispose()
        {
            Context.Dispose();
        }
    }
}
