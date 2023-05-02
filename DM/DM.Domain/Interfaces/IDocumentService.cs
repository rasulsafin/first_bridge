using System.Collections.Generic;
using System.Threading.Tasks;

using DM.Domain.Models;

namespace DM.Domain.Interfaces
{
    public interface IDocumentService
    {
        public List<DocumentDto> GetAll();
        public DocumentDto GetById(long documentId);
        public Task<long> Create(DocumentDto documentModel);
        public Task<bool> Update(DocumentDto documentModel);
        public Task<bool> Delete(long documentId);
    }
}
