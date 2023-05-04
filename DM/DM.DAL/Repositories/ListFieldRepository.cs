using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using DM.DAL.Entities;
using DM.DAL.Interfaces;

namespace DM.DAL.Repositories
{
    public class ListFieldRepository : IListFieldRepository<ListField>
    {
        private readonly DmDbContext _dbContext;

        public ListFieldRepository(DmDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> Create(ListField listField)
        {
            await _dbContext.ListField.AddAsync(listField);
            return true;
        }

        public bool Delete(long? id)
        {
            ListField listField = _dbContext.ListField.Find(id);
            if (listField != null)
            {
                _dbContext.ListField.Remove(listField);
                return true;
            }
            return false;
        }

        public Task<IEnumerable<ListField>> GetAll()
        {
            throw new System.NotImplementedException();
        }

        public ListField GetById(long? id)
        {
            throw new System.NotImplementedException();
        }

        public void Update(ListField item)
        {
            throw new System.NotImplementedException();
        }
    }
}
