using AutoMapper;
using DM.Domain.Interfaces;
using DM.Domain.Models;
using DM.Entities;
using DM.repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DM.Domain.Implementations
{
    public class UserService : IUserService
    {
        private readonly DmDbContext _context;
        private readonly IMapper _mapper;

        public UserService(DmDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public List<UserModel> GetAll()
        {
            var users = _context.Users.ToList();
            return _mapper.Map<List<UserModel>>(users);
        }
        public UserModel GetById(long userId)
        {
            var user = _context.Users.FirstOrDefault(x => x.Id == userId);
            return _mapper.Map<UserModel>(user);
        }
        public async Task<long> Create(UserModel userModel)
        {
            var user = _mapper.Map<UserEntity>(userModel);
            var result = await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return result.Entity.Id;
        }
    }
}
