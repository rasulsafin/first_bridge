using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

using Microsoft.EntityFrameworkCore;

using AutoMapper;

using DM.DAL.Entities;
using DM.Domain.Interfaces;
using DM.Domain.Models;
using DM.DAL;

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

        /// <summary>
        /// Get all Templates
        /// </summary>
        public async Task<List<TemplateModel>> GetAll()
        {
            var templates = await _context.Template
                .Include(x => x.Fields)
                .Include(x => x.ListFields).ThenInclude(y => y.Lists)
                .ToListAsync();

            return _mapper.Map<List<TemplateModel>>(templates);
        }

        /// <summary>
        /// Get all Templates of Current Project
        /// </summary>
        public async Task<List<TemplateModel>> GetTemplatesOfProject(long projectId)
        {
            var templates = await _context.Template
                .Include(x => x.Fields)
                .Include(x => x.ListFields).ThenInclude(y => y.Lists)
                .Where(x => x.ProjectId == projectId)
                .ToListAsync();

            return _mapper.Map<List<TemplateModel>>(templates);
        }

        /// <summary>
        /// Create new Template
        /// </summary>
        public bool Create(TemplateModel templateModel)
        {
            var template = _mapper.Map<TemplateEntity>(new TemplateModel
            {
                Name = templateModel.Name,
                ProjectId = templateModel.ProjectId,
                Fields = templateModel.Fields.ToList(),
                ListFields = templateModel.ListFields.ToList()
            });

            _context.Template.Add(template);

            _context.SaveChanges();

            return true;
        }

        /// <summary>
        /// Update only the columns of an existing Template
        /// </summary>
        public bool Update(TemplateModelForEdit templateModelForEdit)
        {
            var templateForUpdate = _context.Template.FirstOrDefault(x => x.Id == templateModelForEdit.TemplateId);

            if (templateForUpdate == null)
            {
                return false;
            }

            _context.Template.Attach(templateForUpdate);

            templateForUpdate.Name = templateModelForEdit.Name;
            templateForUpdate.ProjectId = templateModelForEdit.ProjectId;

            _context.SaveChanges();

            _context.Entry(templateForUpdate).State = EntityState.Detached;

            return true;
        }
    }
}
