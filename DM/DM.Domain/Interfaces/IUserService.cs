using System.Collections.Generic;
using System.Threading.Tasks;

using DM.Domain.DTO;

namespace DM.Domain.Interfaces
{
    public interface IUserService : IGetAccess
    {
        public Task<UserDto> Authenticate(AuthenticateRequest user);
        public Task<IEnumerable<UserForReadDto>> GetAll();
        public UserForReadDto GetById(long? id);
        public Task<bool> Create(UserForCreateDto user);
        public Task<long> CreateUserWithProjects(UserForCreateDto user);
        public Task<bool> Update(UserForUpdateDto user);
        public Task<bool> Delete(long? id);
        void Dispose();
    }
}
