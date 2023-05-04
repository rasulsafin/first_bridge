using System.Threading.Tasks;

using DM.Domain.DTO;

namespace DM.Domain.Interfaces
{
    public interface IFieldService : IGetAccess
    {
        public Task<bool> Delete(long id);
        public Task<bool> Create(FieldDto fieldModel);
        void Dispose();
    }
}
