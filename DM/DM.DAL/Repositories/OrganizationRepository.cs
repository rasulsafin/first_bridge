using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using DM.DAL.Entities;
using DM.DAL.Interfaces;

namespace DM.DAL.Repositories
{
    public class OrganizationRepository : IOrganizationRepository<Organization>
    {
        private readonly DmDbContext _dbContext;

        public OrganizationRepository(DmDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> Create(Organization organization)
        {
            await _dbContext.Organization.AddAsync(organization);
            return true;
        }

        public bool Delete(long? id)
        {
            Organization organization = _dbContext.Organization.Find(id);
            if (organization != null)
            {
                _dbContext.Organization.Remove(organization);
                return true;
            }
            return false;
        }

        public async Task<IEnumerable<Organization>> GetAll()
        {
            IEnumerable<Organization> organizations = await _dbContext.Organization
                .Include(x => x.Users)
                .Include(y => y.Projects)
                .ToListAsync();

            return organizations;
        }

        public Organization GetById(long? id)
        {
            Organization organization = _dbContext.Organization
                .Include(x => x.Users)
                .Include(y => y.Projects)
                .FirstOrDefault(y => y.Id == id);

            return organization;
        }

        public void Update(Organization organization)
        {
            _dbContext.Entry(organization).State = EntityState.Modified;
        }
    }
}
