using System;

using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using AutoMapper;
using DM.Domain.Models;
using DM.Domain.Interfaces;

using DM.DAL.Entities;
using DM.DAL.Interfaces;
using DM.Domain.Helpers;

namespace DM.Domain.Services
{
    public class TemplateService : ITemplateService
    {
        private readonly UserDto _currentUser;

        private IUnitOfWork Context { get; set; }
        private readonly IMapper _mapper;
        private readonly ILogger<TemplateService> logger;

        public TemplateService(IUnitOfWork unitOfWork, IMapper mapper, CurrentUserService userService)
        {
            Context = unitOfWork;
            _mapper = mapper;
            _currentUser = userService.CurrentUser;
        }

        public async Task<IEnumerable<TemplateDto>> GetAllOfProject(long projectId)
        {
            var templates = await Context.Templates.GetAllOfProject(projectId);
            return _mapper.Map<IEnumerable<TemplateDto>>(templates);
        }

        public TemplateDto GetById(long templateId)
        {
            if (templateId < 1) return null;

            var template = Context.Templates.GetById(templateId);
            return _mapper.Map<TemplateDto>(template);
        }

        public async Task<bool> Create(TemplateForCreateDto templateForCreateModel)
        {
            var template = _mapper.Map<Template>(new TemplateForCreateDto
            {
                Name = templateForCreateModel.Name,
                ProjectId = templateForCreateModel.ProjectId,
                Fields = templateForCreateModel.Fields.ToList(),
                ListFields = templateForCreateModel.ListFields.ToList()
            });

            await Context.Templates.Create(template);
            await Context.SaveAsync();

            return true;
        }

        public async Task<bool> Update(TemplateForUpdateDto templateForUpdateModel)
        {
            var template = Context.Templates.GetById(templateForUpdateModel.Id);

            if (template == null) return false;

            template.Name = templateForUpdateModel.Name;
            template.ProjectId = templateForUpdateModel.ProjectId;
            template.UpdatedAt = DateTime.UtcNow;

            Context.Templates.Update(template);
            await Context.SaveAsync();

            return true;
        }

        public void Dispose()
        {
            Context.Dispose();
        }
    }
}
