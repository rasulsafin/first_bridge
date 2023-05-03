using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using DM.DAL.Entities;
using DM.DAL.Interfaces;

namespace DM.DAL.Repositories
{
    public class ItemRepository : IItemRepository<Item>
    {
        private readonly DmDbContext _dbContext;

        public ItemRepository(DmDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Item>> GetAllByProject(long id)
        {
            IEnumerable<Item> items = await _dbContext.Items
                .Where(x => x.ProjectId == id)
                .ToListAsync();

            return items;
        }

        public Item GetById(long? id)
        {
            Item item = _dbContext.Items.FirstOrDefault(y => y.Id == id);

            return item;
        }

        public async Task<bool> Create(Item item)
        {
            await _dbContext.Items.AddAsync(item);
            return true;
        }

        public bool Delete(long? id)
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<Item>> GetAll()
        {
            throw new System.NotImplementedException();
        }

        public void Update(Item item)
        {
            throw new System.NotImplementedException();
        }
    }
}
