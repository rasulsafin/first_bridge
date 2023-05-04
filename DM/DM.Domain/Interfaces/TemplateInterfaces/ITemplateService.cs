using System.Collections.Generic;
using System.Threading.Tasks;

using DM.Domain.DTO;

namespace DM.Domain.Interfaces
{
    public interface ITemplateService : IGetAccess
    {
        public Task<IEnumerable<TemplateDto>> GetAllOfProject(long projectId);
        public TemplateDto GetById(long templateId);
        public Task<bool> Create(TemplateForCreateDto templateModel);
        public Task<bool> Update(TemplateForUpdateDto templateModel);
        void Dispose();
    }
}
