using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using DM.DAL.Entities;
using DM.DAL.Interfaces;

namespace DM.DAL.Repositories
{
    public class FieldRepository : IFieldRepository<Field>
    {
        private readonly DmDbContext _dbContext;

        public FieldRepository(DmDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> Create(Field field)
        {
            await _dbContext.Field.AddAsync(field);
            return true;
        }

        public bool Delete(long? id)
        {
            Field field = _dbContext.Field.Find(id);
            if (field != null)
            {
                _dbContext.Field.Remove(field);
                return true;
            }
            return false;
        }

        public Task<IEnumerable<Field>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Field GetById(long? id)
        {
            throw new NotImplementedException();
        }

        public void Update(Field item)
        {
            throw new NotImplementedException();
        }
    }
}
