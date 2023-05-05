using System.Collections.Generic;
using System.Threading.Tasks;

using DM.Domain.DTO;

namespace DM.Domain.Interfaces
{
    public interface IDocumentService : IGetAccess
    {
        public List<DocumentDto> GetAll();
        public DocumentDto GetById(long documentId);
        public Task<long> Create(DocumentDto document);
        public Task<bool> Update(DocumentDto document);
        public Task<bool> Delete(long documentId);
        void Dispose();
    }
}
