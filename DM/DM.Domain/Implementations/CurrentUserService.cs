using System.Linq;

using AutoMapper;

using DM.Domain.Models;

using DM.DAL;

namespace DM.Domain.Implementations
{
    public class CurrentUserService
    {
        private readonly DmDbContext _context;
        public UserModel CurrentUser { get; set; }

        private readonly IMapper _mapper;

        public CurrentUserService(DmDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public void SetCurrentUser(long userId)
        {
            var user = _context.Users.FirstOrDefault(x => x.Id == userId);

            CurrentUser = _mapper.Map<UserModel>(user);
        }
    }
}