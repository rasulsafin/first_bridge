using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using DM.DAL.Entities;
using DM.DAL.Interfaces;

namespace DM.DAL.Repositories
{
    public class DocumentRepository : IDocumentRepository<Document>
    {
        private readonly DmDbContext _dbContext;

        public DocumentRepository(DmDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> Create(Document document)
        {
            await _dbContext.Documents.AddAsync(document);
            return true;
        }

        public bool Delete(long? id)
        {
            Document document = _dbContext.Documents.Find(id);
            if (document != null)
            {
                _dbContext.Documents.Remove(document);
                return true;
            }
            return false;
        }

        public async Task<IEnumerable<Document>> GetAll()
        {
            IEnumerable<Document> documents = await _dbContext.Documents.ToListAsync();
            return documents;
        }

        public Document GetById(long? id)
        {
            Document document = _dbContext.Documents.FirstOrDefault(y => y.Id == id);
            return document;
        }

        public void Update(Document document)
        {
            _dbContext.Entry(document).State = EntityState.Modified;
        }
    }
}
