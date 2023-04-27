using DM.Domain.Interfaces;
using DM.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DM.Domain.Implementations
{
    public class DocumentService : IDocumentService
    {
        public List<DocumentModel> GetAll()
        {
            throw new NotImplementedException();
        }

        public DocumentModel GetById(long documentId)
        {
            throw new NotImplementedException();
        }

        public Task<long> Create(DocumentModel documentModel)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(DocumentModel documentModel)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(long documentId)
        {
            throw new NotImplementedException();
        }
    }
}
