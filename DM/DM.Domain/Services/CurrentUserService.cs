using System.Linq;

using AutoMapper;

using DM.Domain.DTO;

using DM.DAL;
using DM.DAL.Interfaces;

namespace DM.Domain.Services
{
    public class CurrentUserService
    {
        private IUnitOfWork Context { get; set; }
        public UserDto CurrentUser { get; set; }

        private readonly IMapper _mapper;

        public CurrentUserService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            Context = unitOfWork;
            _mapper = mapper;
        }

        public void SetCurrentUser(long userId)
        {
            var user = Context.Users.GetById(userId);

            CurrentUser = _mapper.Map<UserDto>(user);
        }
    }
}