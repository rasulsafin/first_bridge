using System.Collections.Generic;
using System.Threading.Tasks;

using DM.Domain.Models;

namespace DM.Domain.Interfaces
{
    public interface IItemService
    {
        public Task<List<ItemDto>> GetAll(long projectId);
        public ItemDto GetById(long itemId);
        public Task<long> Create(ItemDto itemModel);
    }
}
