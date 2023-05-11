using System;
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
        private ItemRepository itemRepository;
        private OrganizationRepository organizationRepository;
        private CommentRepository commentRepository;
        private FieldRepository fieldRepository;
        private ListFieldRepository listFieldRepository;
        private DocumentRepository documentRepository;

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

        public IItemRepository<Item> Items
        {
            get
            {
                itemRepository ??= new ItemRepository(_dbContext);
                return itemRepository;
            }
        }

        public IOrganizationRepository<Organization> Organizations
        {
            get
            {
                organizationRepository ??= new OrganizationRepository(_dbContext);
                return organizationRepository;
            }
        }

        public ICommentRepository<Comment> Comments
        {
            get
            {
                commentRepository ??= new CommentRepository(_dbContext);
                return commentRepository;
            }
        }

        public IFieldRepository<Field> Fields
        {
            get
            {
                fieldRepository ??= new FieldRepository(_dbContext);
                return fieldRepository;
            }
        }

        public IListFieldRepository<ListField> ListFields
        {
            get
            {
                listFieldRepository ??= new ListFieldRepository(_dbContext);
                return listFieldRepository;
            }
        }

        public IDocumentRepository<Document> Documents
        {
            get
            {
                documentRepository ??= new DocumentRepository(_dbContext);
                return documentRepository;
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
