using System;
using System.Threading.Tasks;
using DM.DAL.Entities;

namespace DM.DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository<User> Users { get; }
        IProjectRepository<Project> Projects { get; }
        IUserProjectRepository<UserProject> UserProjects { get; }
        ITemplateRepository<Template> Templates { get; }
        IRecordRepository<Record> Records { get; }
        IRoleRepository<Role> Roles { get; }

        void Save();
        Task SaveAsync();
    }
}
