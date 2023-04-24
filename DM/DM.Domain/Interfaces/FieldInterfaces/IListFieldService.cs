using DM.Domain.Models;

namespace DM.Domain.Interfaces
{
    public interface IListFieldService
    {
        public bool Delete(long id);
        public bool Create(ListFieldModel listFieldModel);
    }
}
