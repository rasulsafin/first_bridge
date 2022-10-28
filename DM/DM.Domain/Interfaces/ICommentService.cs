using System.Collections.Generic;
using System.Threading.Tasks;
using DM.Domain.Models;

namespace DM.Domain.Interfaces
{
    public interface ICommentService
    {
     //   public List<CommentModel> GetAllCommentsOfRecord(long recordId);
        public Task<long> Create(CommentModel commentModel);
        public Task<bool> Update(CommentModelForUpdate commentModel);
        public Task<bool> Delete(long commentId);
    }
}