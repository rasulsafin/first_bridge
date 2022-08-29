using DM.Domain.Models;
using DM.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DM.Domain.Interfaces
{
    public interface IUserService
    {
        AuthenticateResponse Authenticate(AuthenticateRequest model);
        public List<UserModel> GetAll();
        public UserModel GetById(long userId);
        public Task<bool> Create(UserModel userModel);
        public Task<bool> Update(UserModelForUpdate user);
        public Task<bool> Delete(long userId);
    }
}
