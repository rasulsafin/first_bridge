using System.Threading.Tasks;

using DM.Domain.DTO;

namespace DM.Domain.Interfaces
{
    public interface IListFieldService : IGetAccess
    {
        public Task<bool> Delete(long id);
        public Task<bool> Create(ListFieldDto listFieldModel);
        void Dispose();
    }
}
