﻿using System.Collections.Generic;
using System.Threading.Tasks;

using DM.DAL.Entities;
using DM.Domain.Models;

namespace DM.Domain.Interfaces
{
    public interface IOrganizationService
    {
        public Task<List<OrganizationModel>> GetAll();
        public Task<OrganizationModel> GetById(long organizationId);
        public Task<bool> Create(OrganizationForCreateModel organizationModel);
        public Task<bool> Update(OrganizationForUpdateModel organizationModel);
        public Task<bool> Delete(long organizationId);
    }
}
