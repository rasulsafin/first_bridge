using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using AutoMapper;
using DM.DAL;
using DM.DAL.Entities;
using DM.Domain.Interfaces;
using DM.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace DM.Domain.Implementations
{
    public class CommentService : ICommentService
    {
        private readonly DmDbContext _context;
        private readonly UserEntity _currentUser;

        private readonly IMapper _mapper;

        public CommentService(DmDbContext context, IMapper mapper, CurrentUserService userService)
        {
            _context = context;
            _mapper = mapper;
            _currentUser = userService.CurrentUser;
        }

        public async Task<long> Create(CommentModel commentModel)
        {
            var m = new CommentEntity() { Text = commentModel.Text, User = _currentUser, RecordId = commentModel.RecordId };

            var result = await _context.Comments.AddAsync(m);
            await _context.SaveChangesAsync();
            return result.Entity.Id;
        }

        public async Task<bool> Update(CommentModelForUpdate commentModel)
        {
            var fieldForUpdate = await _context.Comments.FirstOrDefaultAsync(x => x.Id == commentModel.Id);

            if (fieldForUpdate == null) return false;

            _context.Comments.Attach(fieldForUpdate);

            fieldForUpdate.Text = commentModel.Text;

            await _context.SaveChangesAsync();

            _context.Entry(fieldForUpdate).State = EntityState.Detached;

            return true;
        }

        public async Task<bool> Delete(long commentId)
        {
            var result = await _context.Comments.FirstOrDefaultAsync(x => x.Id == commentId);
            if (result == null) return false;

            _context.Comments.Remove(result);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}