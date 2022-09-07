using System.Linq;
using DM.Domain.Interfaces;
using DM.Entities;
using DM.repository;
using Microsoft.EntityFrameworkCore;

namespace DM.Domain.Implementations
{
    public class CurrentUserService
    {
        private readonly DmDbContext _context;

        public CurrentUserService(DmDbContext context)
        {
            _context = context;
        }

        public UserEntity CurrentUser { get; set; }

        public void SetCurrentUser(long userId)
        {
            CurrentUser = _context.Users.FirstOrDefault(x => x.Id == userId);
        }
    }
}