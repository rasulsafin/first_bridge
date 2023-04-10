﻿using DM.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DM.Domain.Interfaces
{
    public interface IItemService
    {
        public Task<List<ItemModel>> GetAll(long projectId);
        public ItemModel GetById(long itemId);
        public Task<long> Create(ItemModel itemModel);
    }
}
