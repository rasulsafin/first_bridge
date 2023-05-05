using System.Threading.Tasks;

using AutoMapper;

using DM.Domain.Interfaces;
using DM.Domain.DTO;

using DM.DAL.Entities;
using DM.DAL.Interfaces;
using DM.Common.Enums;

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

        public async Task<long> Create(CommentDto commentDto)
        {
            var comment = _mapper.Map<Comment>(commentDto);

            await Context.Comments.Create(comment);
            await Context.SaveAsync();

            return comment.Id;
        }

        public async Task<bool> Update(CommentForUpdateDto commentDto)
        {
            var comment = Context.Comments.GetById(commentDto.Id);

            if (comment == null) return false;

            comment.Text = commentDto.Text;

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

        public async Task<PermissionDto> GetAccess(long roleId)
        {
            var access = await Context.Permissions.GetByRoleAndType(roleId, PermissionEnum.Record);
            return _mapper.Map<PermissionDto>(access);
        }

        public void Dispose()
        {
            Context.Dispose();
        }
    }
}