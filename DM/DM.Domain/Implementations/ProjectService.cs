﻿using AutoMapper;
using DM.DAL.Entities;
using DM.Domain.Interfaces;
using DM.Domain.Models;
using DM.repository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DM.Domain.Implementations
{
    public class ProjectService : IProjectService
    {
        private readonly DmDbContext _context;
        private readonly IMapper _mapper;

        public ProjectService(DmDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<ProjectModel>> GetAll()
        {
            var projects = await _context.Projects
                .Include(x => x.Template)
                .ToListAsync();

            var projectModel = new List<ProjectModel>();

            foreach (var project in projects)
            {
                projectModel.Add(new ProjectModel() {
                    Id = project.Id,
                    Title = project.Title, 
                    Description = project.Description, 
                });
            }
         
            return projectModel;
        }

        public ProjectModel GetById(long projectId)
        {
            var project = _context.Projects.Include(x => x.Template)
                .FirstOrDefault(x => x.Id == projectId);

            if (project == null)
            {
                return null;
            }

            var projectModel = new ProjectModel() 
            {   Title = project?.Title,
                Description = project.Description,
            };

            return projectModel;
        }

        public async Task<long> Create(ProjectModel projectModel)
        {
         //   var json = JsonDocument.Parse(projectModel.RecordTemplate.ToString());

            var project = new ProjectEntity
            {
                Title = projectModel.Title,
                Description = projectModel.Description
            };

            var result = await _context.Projects.AddAsync(project);
            await _context.SaveChangesAsync();

            return result.Entity.Id;
        }
    }
}
