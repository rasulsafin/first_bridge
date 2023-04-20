using System.Collections.Generic;
using System.Threading.Tasks;

using DM.Domain.Models;

namespace DM.Domain.Interfaces
{
    public interface IItemService
    {
        public Task<List<ItemModel>> GetAll(long projectId);
        public ItemModel GetById(long itemId);
        public Task<long> Create(ItemModel itemModel);
    }
}
