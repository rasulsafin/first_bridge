using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using DM.DAL.Entities;
using DM.DAL.Interfaces;

namespace DM.DAL.Repositories
{
    public class TemplateRepository : ITemplateRepository<Template>
    {
        private readonly DmDbContext _dbContext;

        public TemplateRepository(DmDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> Create(Template template)
        {
            await _dbContext.Template.AddAsync(template);
            return true;
        }

        public bool Delete(long? id)
        {
            Template template = _dbContext.Template.Find(id);
            if (template != null)
            {
                _dbContext.Template.Remove(template);
                return true;
            }
            return false;
        }

        public async Task<IEnumerable<Template>> GetAllOfProject(long? id)
        {
            IEnumerable<Template> templates = await _dbContext.Template
                .Include(x => x.Fields)
                .Include(x => x.ListFields).ThenInclude(y => y.Lists)
                .Where(x => x.ProjectId == id)
                .ToListAsync();

            return templates;
        }

        public Template GetById(long? id)
        {
            Template template = _dbContext.Template
                .Include(x => x.Fields)
                .Include(x => x.ListFields).ThenInclude(y => y.Lists)
                .FirstOrDefault(y => y.Id == id);

            return template;
        }

        public void Update(Template template)
        {
            _dbContext.Entry(template).State = EntityState.Modified;
        }

        public async Task<IEnumerable<Template>> GetAll()
        {
            IEnumerable<Template> templates = await _dbContext.Template
                .Include(x => x.Fields)
                .Include(x => x.ListFields).ThenInclude(y => y.Lists)
                .ToListAsync();

            return templates;
        }
    }
}
