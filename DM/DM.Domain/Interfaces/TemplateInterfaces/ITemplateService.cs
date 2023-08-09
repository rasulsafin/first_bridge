using System.Collections.Generic;
using System.Threading.Tasks;

using DM.Domain.DTO;

namespace DM.Domain.Interfaces
{
    public interface ITemplateService : IGetAccess
    {
        public Task<IEnumerable<TemplateForReadDto>> GetAllOfProject(long projectId);
        public TemplateForReadDto GetById(long templateId);
        public Task<bool> Create(TemplateForCreateDto templateModel);
        public Task<bool> Update(TemplateForUpdateDto templateModel);
        public Task<bool> Delete(long? templateId);
        void Dispose();
    }
}
