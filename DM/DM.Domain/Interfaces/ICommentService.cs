using System.Collections.Generic;
using System.Threading.Tasks;
using DM.Domain.DTO;

namespace DM.Domain.Interfaces
{
    public interface ICommentService : IGetAccess
    {
        public Task<long> Create(CommentDto comment);
        public Task<bool> Update(CommentForUpdateDto comment);
        public Task<bool> Delete(long commentId);
        void Dispose();
    }
}