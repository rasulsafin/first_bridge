using DM.DAL.Entities;
using DM.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DM.Domain.Interfaces
{
    public interface ITemplateService
    {
        public Task<List<TemplateModel>> GetAll();
        public Task<List<TemplateModel>> GetTemplatesOfProject(long projectId);
        public bool Create(TemplateModel templateModel);
        public bool Update(TemplateModelForEdit templateModelForEdit);
    }
}
