using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using DM.DAL.Entities;
using DM.DAL.Interfaces;

namespace DM.DAL.Repositories
{
    public class CommentRepository : ICommentRepository<Comment>
    {
        private readonly DmDbContext _dbContext;

        public CommentRepository(DmDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> Create(Comment comment)
        {
            await _dbContext.Comments.AddAsync(comment);
            return true;
        }

        public void Update(Comment comment)
        {
            _dbContext.Entry(comment).State = EntityState.Modified;
        }

        public bool Delete(long? id)
        {
            Comment comment = _dbContext.Comments.Find(id);
            if (comment != null)
            {
                _dbContext.Comments.Remove(comment);
                return true;
            }
            return false;
        }

        public Comment GetById(long? id)
        {
            Comment comment = _dbContext.Comments.FirstOrDefault(y => y.Id == id);

            return comment;
        }

        public Task<IEnumerable<Comment>> GetAll()
        {
            throw new System.NotImplementedException();
        }
    }
}
