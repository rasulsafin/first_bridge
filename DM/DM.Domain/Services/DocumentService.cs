using DM.Domain.Interfaces;
using DM.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DM.Common.Enums;

namespace DM.Domain.Services
{
    public class DocumentService : IDocumentService
    {
        public List<DocumentDto> GetAll()
        {
            throw new NotImplementedException();
        }

        public DocumentDto GetById(long documentId)
        {
            throw new NotImplementedException();
        }

        public Task<long> Create(DocumentDto documentDto)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(DocumentDto documentDto)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(long documentId)
        {
            throw new NotImplementedException();
        }

        public async Task<PermissionDto> GetAccess(long roleId, PermissionEnum permission)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
