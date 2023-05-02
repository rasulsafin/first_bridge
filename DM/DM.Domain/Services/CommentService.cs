using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using AutoMapper;

using DM.Domain.Interfaces;
using DM.Domain.Models;

using DM.DAL;
using DM.DAL.Entities;

namespace DM.Domain.Services
{
    public class CommentService : ICommentService
    {
        private readonly DmDbContext _context;
        private readonly UserDto _currentUser;

        private readonly IMapper _mapper;

        public CommentService(DmDbContext context, IMapper mapper, CurrentUserService userService)
        {
            _context = context;
            _mapper = mapper;
            _currentUser = userService.CurrentUser;
        }

        public async Task<long> Create(CommentDto commentModel)
        {
            var comment = _mapper.Map<Comment>(commentModel);

            var result = await _context.Comments.AddAsync(comment);

            _context.SaveChanges();

            return result.Entity.Id;
        }

        public async Task<bool> Update(CommentForUpdateModel commentModel)
        {
            var comment = await _context.Comments.FirstOrDefaultAsync(x => x.Id == commentModel.Id);

            if (comment == null) return false;

            _context.Comments.Attach(comment);

            comment.Text = commentModel.Text;

            await _context.SaveChangesAsync();

            _context.Entry(comment).State = EntityState.Detached;

            return true;
        }

        public async Task<bool> Delete(long commentId)
        {
            var comment = await _context.Comments.FirstOrDefaultAsync(x => x.Id == commentId);

            if (comment == null) return false;

            _context.Comments.Remove(comment);

            await _context.SaveChangesAsync();

            return true;
        }
    }
}