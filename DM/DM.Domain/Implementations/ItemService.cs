﻿using System.Collections.Generic;
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
        private readonly IMapper _mapper;
        private readonly UserEntity _currentUser;

        public ItemService(DmDbContext context, IMapper mapper, CurrentUserService userService)
        {
            _context = context;
            _mapper = mapper;
            _currentUser = userService.CurrentUser;
        }
        public async Task<List<ItemModel>> GetAll(long projectId)
        {
            var listItemModel = new List<ItemModel>();
            var items = await _context.Items.Where(x => x.ProjectId == projectId).ToListAsync();

            foreach (var i in items)
            {
                // if (_currentUser.Roles != "SuperAdmin")
                // {
                //     var permission = AuthorizationHelper.CheckUsersPermissionsById(_context, _currentUser, PermissionType.Item, i.Id);
                //
                //     if (permission == null || !permission.Read)
                //     {
                //         continue;
                //     }
                // }

                listItemModel.Add(_mapper.Map<ItemModel>(i));
            }

            return listItemModel;
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
