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
        public Task<List<TemplateModel>> GetTemplatesOfProject(long projectId);
        public bool AddTemplateToProject(TemplateModel templateModel);
        public bool EditExistingTemplateOfProject(TemplateModelForEdit templateModelForEdit);
    }
}
