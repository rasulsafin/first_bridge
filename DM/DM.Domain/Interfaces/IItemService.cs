using System.Collections.Generic;
using System.Threading.Tasks;

using DM.Domain.DTO;

namespace DM.Domain.Interfaces
{
    public interface IItemService : IGetAccess
    {
        public Task<IEnumerable<ItemDto>> GetAll(long projectId);
        public ItemDto GetById(long itemId);
        public Task<long> Create(ItemDto item);
        void Dispose();
    }
}
