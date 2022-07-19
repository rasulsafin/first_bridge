using DM.Domain.Models;
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
   //     Task<AuthenticateResponse> Register(UserModel userModel);
        public List<UserModel> GetAll();
        public UserModel GetById(long userId);
        public Task<long> Create(UserModel userModel);
        public Task<bool> Delete(long userId);
    }
}
