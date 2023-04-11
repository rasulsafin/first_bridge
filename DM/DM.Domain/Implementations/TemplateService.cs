using DM.DAL.Entities;
using DM.Domain.Interfaces;
using DM.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DM.DAL;
using AutoMapper;
using DM.Domain.Helpers;

namespace DM.Domain.Implementations
{
    public class TemplateService : ITemplateService
    {
        private readonly DmDbContext _context;
        private readonly UserEntity _currentUser;

        private readonly IMapper _mapper;

        public TemplateService(DmDbContext context, IMapper mapper, CurrentUserService userService)
        {
            _context = context;
            _mapper = mapper;
            _currentUser = userService.CurrentUser;
        }

        public bool AddTemplateToProject(TemplateModel templateModel)
        {
            var template = _mapper.Map<TemplateEntity>(new TemplateModel
            {
                Name = templateModel.Name,
                ProjectId = templateModel.ProjectId,
            });

            var project = _context.Projects.Include(x => x.Template).First(x => x.Id == templateModel.ProjectId);

            if (project == null)
            {
                return false;
            }

            // добавление зависимой сущности
            project.Template.Add(template);

            _context.SaveChanges();
            return true;
        }

        public bool EditExistingTemplateOfProject(TemplateModelForEdit templateModelForEdit)
        {
            var project = _context.Projects.Where(x => x.Id == templateModelForEdit.ProjectId).Include(x => x.Template).First();

            if (project == null)
            {
                return false;
            }

            var templateForUpdate = project.Template
                .FirstOrDefault(x => x.Id == templateModelForEdit.TemplateId);

            if (templateForUpdate == null)
            {
                return false;
            }

            templateForUpdate.Name = templateModelForEdit.Name;
            templateForUpdate.ProjectId = templateModelForEdit.ProjectId;

            _context.SaveChanges();
            return true;
        }

        public async Task<List<TemplateModel>> GetTemplatesOfProject(long projectId)
        {
            var templates = await _context.Template
                .Where(x => x.ProjectId == projectId)
                .ToListAsync();

            return _mapper.Map<List<TemplateModel>>(templates);
        }
    }
}
