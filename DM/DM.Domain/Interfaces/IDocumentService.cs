using System.Collections.Generic;
using System.Threading.Tasks;

using DM.Domain.Models;

namespace DM.Domain.Interfaces
{
    public interface IDocumentService
    {
        public List<DocumentModel> GetAll();
        public DocumentModel GetById(long documentId);
        public Task<long> Create(DocumentModel documentModel);
        public Task<bool> Update(DocumentModel documentModel);
        public Task<bool> Delete(long documentId);
    }
}
