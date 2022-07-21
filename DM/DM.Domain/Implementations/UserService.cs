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
using Microsoft.EntityFrameworkCore;
using DM.DAL.Entities;

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

        public async Task<bool> Delete(long userId)
        {

            var fieldEntity = await _context.Fields.FirstOrDefaultAsync(x => x.Id == userId);
            _context.Fields.Attach(fieldEntity);

            fieldEntity.AssigneeId = null;
            fieldEntity.IssuerId = null;
            await _context.SaveChangesAsync();
       //     context.Entry(entry).State = EntityState.Detached;
            _context.Entry(fieldEntity).State = EntityState.Detached;

            // check that the fields do not contain users to be deleted
            //        _context.Fields.Where(u => u.AssigneeId == userId).ToList().ForEach(x => x.AssigneeId = null);
            //                  _context.Fields.Where(u => u.IssuerId == userId).ToList().ForEach(x => x.AssigneeId = null);
         //   await _context.SaveChangesAsync();


            var user = _context.Users.FirstOrDefault(q => q.Id == userId);
            

            if (user == null)
            {
                return false;
            }
            
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return true;

        }
    }
}
