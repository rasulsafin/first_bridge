using AutoMapper;
using DM.DAL.Entities;
using DM.Domain.Interfaces;
using DM.Domain.Models;
using DM.repository;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DM.Domain.Implementations
{
    public class ItemService : IItemService
    {
        private readonly DmDbContext _context;
        private readonly IMapper _mapper;

        public ItemService(DmDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public List<ItemModel> GetAll()
        {
            var items = _context.Items.ToList();
            return _mapper.Map<List<ItemModel>>(items);
        }
        public ItemModel GetById(long itemId)
        {
            var item = _context.Items.FirstOrDefault(x => x.Id == itemId);
            return _mapper.Map<ItemModel>(item);
        }
        public async Task<long> Create(ItemModel itemModel)
        {
            var item = _mapper.Map<ItemEntity>(itemModel);
            var result = await _context.Items.AddAsync(item);
            await _context.SaveChangesAsync();
            return result.Entity.Id;
        }
    }
}
