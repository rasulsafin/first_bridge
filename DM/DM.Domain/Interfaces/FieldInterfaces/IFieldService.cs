using System.Threading.Tasks;

using DM.Domain.Models;

namespace DM.Domain.Interfaces
{
    public interface IFieldService
    {
        public Task<bool> Delete(long id);
        public Task<bool> Create(FieldModel fieldModel);
    }
}
