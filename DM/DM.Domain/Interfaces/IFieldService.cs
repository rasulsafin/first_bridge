using DM.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DM.Domain.Interfaces
{
    public interface IFieldService
    {
        public bool Delete(long id);
        public bool Create(FieldModel fieldModel);
    }

    public interface IListFieldService
    {
        public bool Delete(long id);
        public bool Create(ListFieldModel listFieldModel);
    }
}
