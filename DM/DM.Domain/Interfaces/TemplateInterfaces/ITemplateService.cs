using System.Collections.Generic;
using System.Threading.Tasks;

using DM.Domain.Models;

namespace DM.Domain.Interfaces
{
    public interface ITemplateService
    {
        public Task<List<TemplateDto>> GetAllOfProject(long projectId);
        public Task<TemplateDto> GetById(long templateId);
        public Task<bool> Create(TemplateForCreateDto templateModel);
        public Task<bool> Update(TemplateForUpdateDto templateModel);
    }
}
