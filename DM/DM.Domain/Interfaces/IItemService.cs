using System.Collections.Generic;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;

using DM.Domain.DTO;

namespace DM.Domain.Interfaces
{
    public interface IItemService : IGetAccess
    {
        public Task<IEnumerable<ItemDto>> GetAll(long projectId);
        public ItemDto GetById(long itemId);
        public Task<long> Create(ItemDto item, IFormFile file);
        public Task<bool> Delete(string fileName);

        public Task<int> UploadItems(int userId, IEnumerable<int> itemIds);

        public Task<int> DownloadItems(int userId, IEnumerable<int> itemIds);

        public Task<long> LinkItem(long projectId, ItemDto itemDto);

        public ItemDto Find(string fileName);
        void Dispose();
    }
}
