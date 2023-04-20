using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using AutoMapper;

using DM.Domain.Interfaces;
using DM.Domain.Models;

using DM.DAL.Entities;
using DM.DAL;

namespace DM.Domain.Implementations
{
    public class TemplateService : ITemplateService
    {
        private readonly DmDbContext _context;
        private readonly UserEntity _currentUser;

        private readonly IMapper _mapper;
        private readonly ILogger<TemplateService> logger;

        public TemplateService(DmDbContext context, IMapper mapper, CurrentUserService userService)
        {
            _context = context;
            _mapper = mapper;
            _currentUser = userService.CurrentUser;
        }

        public async Task<List<TemplateModel>> GetAllOfProject(long projectId)
        {
            var templates = await _context.Template
                .Include(x => x.Fields)
                .Include(x => x.ListFields).ThenInclude(y => y.Lists)
                .Where(x => x.ProjectId == projectId)
                .ToListAsync();

            return _mapper.Map<List<TemplateModel>>(templates);
        }

        public async Task<TemplateModel> GetById(long templateId)
        {
            var template = await _context.Template
                .Include(x => x.Fields)
                .Include(x => x.ListFields).ThenInclude(y => y.Lists)
                .Where(x => x.Id == templateId)
                .ToListAsync();

            return _mapper.Map<TemplateModel>(template);
        }

        public async Task<bool> Create(TemplateModel templateModel)
        {
            var template = _mapper.Map<TemplateEntity>(new TemplateModel
            {
                Name = templateModel.Name,
                ProjectId = templateModel.ProjectId,
                Fields = templateModel.Fields.ToList(),
                ListFields = templateModel.ListFields.ToList()
            });

            _context.Template.Add(template);

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> Update(TemplateForUpdateModel templateModelForEdit)
        {
            var templateForUpdate = _context.Template.FirstOrDefault(x => x.Id == templateModelForEdit.Id);

            if (templateForUpdate == null) return false;

            _context.Template.Attach(templateForUpdate);

            templateForUpdate.Name = templateModelForEdit.Name;
            templateForUpdate.ProjectId = templateModelForEdit.ProjectId;

            await _context.SaveChangesAsync();

            _context.Entry(templateForUpdate).State = EntityState.Detached;

            return true;
        }
    }
}
