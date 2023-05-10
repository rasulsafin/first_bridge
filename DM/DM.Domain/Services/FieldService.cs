using System.Threading.Tasks;

using AutoMapper;

using DM.Domain.Interfaces;
using DM.Domain.DTO;

using DM.DAL.Entities;
using DM.DAL.Interfaces;
using DM.Common.Enums;
using System;

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

        public async Task<bool> Create(FieldDto fieldDto)
        {
            var field = _mapper.Map<Field>(new FieldDto
            {
                Name = fieldDto.Name,
                IsMandatory = fieldDto.IsMandatory,
                Data = fieldDto.Data,
                Type = fieldDto.Type,
                RecordId = fieldDto.RecordId == 0 ? null : fieldDto.RecordId,
                TemplateId = fieldDto.TemplateId == 0 ? null : fieldDto.TemplateId,
            });

            await Context.Fields.Create(field);
            await Context.SaveAsync();

            return true;
        }

        public async Task<bool> Create(ListFieldDto listFieldDto)
        {
            var listField = _mapper.Map<ListField>(new ListFieldDto
            {
                Name = listFieldDto.Name,
                IsMandatory = listFieldDto.IsMandatory,
                Lists = listFieldDto.Lists,
                Type = listFieldDto.Type,
                RecordId = listFieldDto.RecordId == 0 ? null : listFieldDto.RecordId,
                TemplateId = listFieldDto.TemplateId == 0 ? null : listFieldDto.TemplateId,
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

        public async Task<bool> GetAccess(long roleId, ActionEnum action)
        {
            try
            {
                var access = await Context.Permissions.GetByRoleAndType(roleId, PermissionEnum.User);

                return action switch
                {
                    ActionEnum.Read => access.Read,
                    ActionEnum.Create => access.Create,
                    ActionEnum.Delete => access.Delete,
                    ActionEnum.Update => access.Update,
                    _ => false,
                };
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void Dispose()
        {
            Context.Dispose();
        }
    }
}
