﻿using System.Threading.Tasks;

using DM.Domain.Models;

namespace DM.Domain.Interfaces
{
    public interface IListFieldService
    {
        public Task<bool> Delete(long id);
        public Task<bool> Create(ListFieldDto listFieldModel);
        void Dispose();
    }
}
