﻿using System;

using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using AutoMapper;
using DM.Domain.Models;
using DM.Domain.Interfaces;

using DM.DAL;
using DM.DAL.Entities;

namespace DM.Domain.Services
{
    public class TemplateService : ITemplateService
    {
        private readonly DmDbContext _context;
        private readonly UserDto _currentUser;

        private readonly IMapper _mapper;
        private readonly ILogger<TemplateService> logger;

        public TemplateService(DmDbContext context, IMapper mapper, CurrentUserService userService)
        {
            _context = context;
            _mapper = mapper;
            _currentUser = userService.CurrentUser;
        }

        public async Task<List<TemplateDto>> GetAllOfProject(long projectId)
        {
            var templates = await _context.Template
                .Include(x => x.Fields)
                .Include(x => x.ListFields).ThenInclude(y => y.Lists)
                .Where(x => x.ProjectId == projectId)
                .ToListAsync();

            return _mapper.Map<List<TemplateDto>>(templates);
        }

        public async Task<TemplateDto> GetById(long templateId)
        {
            var template = await _context.Template
                .Include(x => x.Fields)
                .Include(x => x.ListFields).ThenInclude(y => y.Lists)
                .Where(x => x.Id == templateId)
                .ToListAsync();

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

            _context.Template.Add(template);

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> Update(TemplateForUpdateDto templateForUpdateModel)
        {
            var template = _context.Template.FirstOrDefault(x => x.Id == templateForUpdateModel.Id);

            if (template == null) return false;

            _context.Template.Attach(template);

            template.Name = templateForUpdateModel.Name;
            template.ProjectId = templateForUpdateModel.ProjectId;
            template.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            _context.Entry(template).State = EntityState.Detached;

            return true;
        }
    }
}