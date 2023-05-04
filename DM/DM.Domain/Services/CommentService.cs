using System.Threading.Tasks;

using AutoMapper;

using DM.Domain.Interfaces;
using DM.Domain.Models;

using DM.DAL.Entities;
using DM.DAL.Interfaces;

namespace DM.Domain.Services
{
    public class CommentService : ICommentService
    {
        private readonly UserDto _currentUser;
        private IUnitOfWork Context { get; set; }
        private readonly IMapper _mapper;

        public CommentService(IUnitOfWork unitOfWork, IMapper mapper, CurrentUserService userService)
        {
            Context = unitOfWork;
            _mapper = mapper;
            _currentUser = userService.CurrentUser;
        }

        public async Task<long> Create(CommentDto commentModel)
        {
            var comment = _mapper.Map<Comment>(commentModel);

            await Context.Comments.Create(comment);
            await Context.SaveAsync();

            return comment.Id;
        }

        public async Task<bool> Update(CommentForUpdateModel commentModel)
        {
            var comment = Context.Comments.GetById(commentModel.Id);

            if (comment == null) return false;

            comment.Text = commentModel.Text;

            Context.Comments.Update(comment);
            await Context.SaveAsync();

            return true;
        }

        public async Task<bool> Delete(long commentId)
        {
            var result = Context.Comments.Delete(commentId);
            await Context.SaveAsync();

            return result;
        }

        public void Dispose()
        {
            Context.Dispose();
        }
    }
}