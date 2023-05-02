﻿using System;
using System.Threading.Tasks;

using DM.DAL.Entities;
using DM.DAL.Interfaces;

namespace DM.DAL.Repositories
{
    public class EFUnitOfWork : IUnitOfWork
    {
        private readonly DmDbContext _dbContext;

        private UserRepository userRepository;
        private ProjectRepository projectRepository;
        private UserProjectRepository userProjectRepository;
        private TemplateRepository templateRepository;
        private RecordRepository recordRepository;
        private RoleRepository roleRepository;
        private PermissionRepository permissionRepository;

        private bool disposed = false;

        public IUserRepository<User> Users
        {
            get
            {
                userRepository ??= new UserRepository(_dbContext);
                return userRepository;
            }
        }

        public IProjectRepository<Project> Projects
        {
            get
            {
                projectRepository ??= new ProjectRepository(_dbContext);
                return projectRepository;
            }
        }

        public IUserProjectRepository<UserProject> UserProjects
        {
            get
            {
                userProjectRepository ??= new UserProjectRepository(_dbContext);
                return userProjectRepository;
            }
        }

        public ITemplateRepository<Template> Templates
        {
            get
            {
                templateRepository ??= new TemplateRepository(_dbContext);
                return templateRepository;
            }
        }

        public IRecordRepository<Record> Records
        {
            get
            {
                recordRepository ??= new RecordRepository(_dbContext);
                return recordRepository;
            }
        }

        public IRoleRepository<Role> Roles
        {
            get
            {
                roleRepository ??= new RoleRepository(_dbContext);
                return roleRepository;
            }
        }

        public IPermissionRepository<Permission> Permissions
        {
            get
            {
                permissionRepository ??= new PermissionRepository(_dbContext);
                return permissionRepository;
            }
        }

        public EFUnitOfWork(DmDbContext context)
        {
            _dbContext = context;
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }

        public async Task SaveAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        public virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
                }
                disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
