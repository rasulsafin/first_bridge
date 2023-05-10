﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using AutoMapper;

using DM.Domain.Interfaces;
using DM.Domain.DTO;

using DM.DAL.Entities;
using DM.DAL.Interfaces;
using DM.Common.Enums;
using System;

namespace DM.Domain.Services
{
    public class ItemService : IItemService
    {
        private readonly UserDto _currentUser;
        private IUnitOfWork Context { get; set; }
        private readonly IMapper _mapper;

        public ItemService(IUnitOfWork unitOfWork, IMapper mapper, CurrentUserService userService)
        {
            Context = unitOfWork;
            _currentUser = userService.CurrentUser;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ItemDto>> GetAll(long projectId)
        {
            var items = await Context.Items.GetAllByProject(projectId);
            return _mapper.Map<IEnumerable<ItemDto>>(items);
        }

        public ItemDto GetById(long itemId)
        {
            if (itemId < 1) return null;

            var item = Context.Items.GetById(itemId);
            return _mapper.Map<ItemDto>(item);
        }

        public async Task<long> Create(ItemDto itemDto)
        {
            var item = _mapper.Map<Item>(itemDto);

            await Context.Items.Create(item);
            await Context.SaveAsync();

            return item.Id;
        }

        public async Task<bool> GetAccess(long roleId, ActionEnum action)
        {
            try
            {
                var access = await Context.Permissions.GetByRoleAndType(roleId, PermissionEnum.User);

                return action switch
                {
                    ActionEnum.Read => access.Read,
                    ActionEnum.Create => access.Create,
                    ActionEnum.Delete => access.Delete,
                    ActionEnum.Update => access.Update,
                    _ => false,
                };
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void Dispose()
        {
            Context.Dispose();
        }
    }
}
