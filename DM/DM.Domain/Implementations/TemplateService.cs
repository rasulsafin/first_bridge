using AutoMapper;
using DM.DAL.Entities;
using DM.Domain.Interfaces;
using DM.Domain.Models;
using DM.repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DM.Domain.Implementations
{
    public class TemplateService : ITemplateService
    {
        private readonly DmDbContext _context;

        public TemplateService(DmDbContext context)
        {
            _context = context;
        }

        public bool AddTemplateToProject(TemplateModel templateModel)
        {
            var template = new TemplateEntity()
            {
                Name = templateModel.Name,
                ProjectId = templateModel.ProjectId,
                RecordTemplate = JsonDocument.Parse(templateModel.RecordTemplate.ToString())
            };

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
            var template = new TemplateEntity()
            {
                Name = templateModelForEdit.Name,
                ProjectId = templateModelForEdit.ProjectId,
                RecordTemplate = JsonDocument.Parse(templateModelForEdit.RecordTemplate.ToString())
            };

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
            templateForUpdate.RecordTemplate = JsonDocument.Parse(templateModelForEdit.RecordTemplate.ToString());

            _context.SaveChanges();
            return true;
        }

        public async Task<string> GetTemplatesOfProject(long projectId)
        {
            var projectsForOutput = ""; 
            var projects = await _context.Template
                .Where(x => x.ProjectId == projectId)
                .Select(q => q.RecordTemplate)
                .ToListAsync();

            foreach (var p in projects)
            {
                projectsForOutput += p.RootElement + "\n";
            }

            return projectsForOutput;
        }
    }
}
