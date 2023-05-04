using DM.Domain.Interfaces;
using DM.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public Task<long> Create(DocumentDto documentModel)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(DocumentDto documentModel)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(long documentId)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
