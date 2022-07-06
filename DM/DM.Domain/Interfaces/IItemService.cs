using DM.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DM.Domain.Interfaces
{
    public interface IItemService
    {
        public List<ItemModel> GetAll();
        public ItemModel GetById(long userId);
        public Task<long> Create(ItemModel userModel);
    }
}
