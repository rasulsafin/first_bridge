using System.Collections.Generic;
using System.Threading.Tasks;

using DM.Domain.Models;

namespace DM.Domain.Interfaces
{
    public interface ITemplateService
    {
        public Task<List<TemplateModel>> GetAllOfProject(long projectId);
        public Task<TemplateModel> GetById(long templateId);
        public Task<bool> Create(TemplateModel templateModel);
        public Task<bool> Update(TemplateForUpdateModel templateModel);
    }
}
