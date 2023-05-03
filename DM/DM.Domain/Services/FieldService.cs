using System.Threading.Tasks;

using AutoMapper;

using DM.Domain.Interfaces;
using DM.Domain.Models;

using DM.DAL.Entities;
using DM.DAL.Interfaces;

namespace DM.Domain.Services
{
    public class FieldService : IFieldService, IListFieldService
    {
        private readonly UserDto _currentUser;

        private IUnitOfWork Context { get; set; }
        private readonly IMapper _mapper;

        public FieldService(IUnitOfWork unitOfWork, IMapper mapper, CurrentUserService userService)
        {
            Context = unitOfWork;
            _mapper = mapper;
            _currentUser = userService.CurrentUser;
        }

        public async Task<bool> Create(FieldDto fieldModel)
        {
            var field = _mapper.Map<Field>(new FieldDto
            {
                Name = fieldModel.Name,
                IsMandatory = fieldModel.IsMandatory,
                Data = fieldModel.Data,
                Type = fieldModel.Type,
                RecordId = fieldModel.RecordId == 0 ? null : fieldModel.RecordId,
                TemplateId = fieldModel.TemplateId == 0 ? null : fieldModel.TemplateId,
            });

            await Context.Fields.Create(field);
            await Context.SaveAsync();

            return true;
        }

        public async Task<bool> Create(ListFieldDto listFieldModel)
        {
            var listField = _mapper.Map<ListField>(new ListFieldDto
            {
                Name = listFieldModel.Name,
                IsMandatory = listFieldModel.IsMandatory,
                Lists = listFieldModel.Lists,
                Type = listFieldModel.Type,
                RecordId = listFieldModel.RecordId == 0 ? null : listFieldModel.RecordId,
                TemplateId = listFieldModel.TemplateId == 0 ? null : listFieldModel.TemplateId,
            });

            await Context.ListFields.Create(listField);
            await Context.SaveAsync();

            return true;
        }

        async Task<bool> IFieldService.Delete(long fieldId)
        {
            var result = Context.Fields.Delete(fieldId);
            await Context.SaveAsync();

            return result;
        }

        async Task<bool> IListFieldService.Delete(long listFieldId)
        {
            var result = Context.ListFields.Delete(listFieldId);
            await Context.SaveAsync();

            return result;
        }
    }
}
