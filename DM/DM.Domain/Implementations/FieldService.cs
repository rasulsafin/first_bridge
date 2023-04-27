using AutoMapper;
using DM.DAL;
using DM.DAL.Entities;
using DM.Domain.Interfaces;
using DM.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DM.Domain.Implementations
{
    public class FieldService : IFieldService, IListFieldService
    {
        private readonly DmDbContext _context;
        private readonly UserModel _currentUser;

        private readonly IMapper _mapper;

        public FieldService(DmDbContext context, IMapper mapper, CurrentUserService userService)
        {
            _context = context;
            _mapper = mapper;
            _currentUser = userService.CurrentUser;
        }

        public async Task<bool> Create(FieldModel fieldModel)
        {
            var field = _mapper.Map<FieldEntity>(new FieldModel
            {
                Name = fieldModel.Name,
                IsMandatory = fieldModel.IsMandatory,
                Data = fieldModel.Data,
                Type = fieldModel.Type,
                RecordId = fieldModel.RecordId == 0 ? null : fieldModel.RecordId,
                TemplateId = fieldModel.TemplateId == 0 ? null : fieldModel.TemplateId,
            });

            _context.Field.Add(field);

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> Create(ListFieldModel listFieldModel)
        {
            var ListField = _mapper.Map<ListFieldEntity>(new ListFieldModel
            {
                Name = listFieldModel.Name,
                IsMandatory = listFieldModel.IsMandatory,
                Lists = listFieldModel.Lists,
                Type = listFieldModel.Type,
                RecordId = listFieldModel.RecordId == 0 ? null : listFieldModel.RecordId,
                TemplateId = listFieldModel.TemplateId == 0 ? null : listFieldModel.TemplateId,
            });

            _context.ListField.Add(ListField);

            await _context.SaveChangesAsync();

            return true;
        }

        async Task<bool> IFieldService.Delete(long id)
        {
            var field = _context.Field.Where(x => x.Id == id).FirstOrDefault();

            if (field == null) return false;

            _context.Field.Remove(field);

            await _context.SaveChangesAsync();

            return true;
        }

        async Task<bool> IListFieldService.Delete(long id)
        {
            var listField = _context.ListField.Where(x => x.Id == id).FirstOrDefault();

            if (listField == null) return false;

            _context.ListField.Remove(listField);

            await _context.SaveChangesAsync();

            return true;
        }
    }
}
