using System.Collections.Generic;
using System.Threading.Tasks;

using DM.Domain.Models;

namespace DM.Domain.Interfaces
{
    public interface IUserService
    {
        public Task<AuthenticateResponse> Authenticate(AuthenticateRequest user);
        public Task<IEnumerable<UserForReadDto>> GetAll();
        public UserForReadDto GetById(long? id);
        public Task<bool> Create(UserForCreateDto user);
        public Task<bool> Update(UserForUpdateDto user);
        public Task<bool> Delete(long? id);
        void Dispose();
    }
}
