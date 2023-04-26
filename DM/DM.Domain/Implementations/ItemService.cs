using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using AutoMapper;

using DM.Domain.Interfaces;
using DM.Domain.Models;

using DM.DAL.Entities;
using DM.DAL;

namespace DM.Domain.Implementations
{
    public class ItemService : IItemService
    {
        private readonly DmDbContext _context;
        private readonly UserModel _currentUser;

        private readonly IMapper _mapper;

        public ItemService(DmDbContext context, IMapper mapper, CurrentUserService userService)
        {
            _context = context;
            _currentUser = userService.CurrentUser;
            _mapper = mapper;
        }

        public async Task<List<ItemModel>> GetAll(long projectId)
        {
            var items = await _context.Items.Where(x => x.ProjectId == projectId).ToListAsync();

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
