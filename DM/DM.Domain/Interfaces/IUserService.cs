using System.Collections.Generic;
using System.Threading.Tasks;

using DM.Domain.Models;

namespace DM.Domain.Interfaces
{
    public interface IUserService
    {
        public Task<AuthenticateResponse> Authenticate(AuthenticateRequest user);
        public Task<IEnumerable<UserModel>> GetAll();
        public UserModel GetById(long id);
        public Task<bool> Create(UserForCreateModel user);
        public Task<bool> Update(UserForUpdateModel user);
        public Task<bool> Delete(long id);
    }
}
