using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

using AutoMapper;

using DM.Domain.Interfaces;
using DM.Domain.DTO;

using DM.DAL.Entities;
using DM.DAL.Interfaces;
using DM.Common.Enums;

namespace DM.Domain.Services
{
    public class RecordService : IRecordService
    {
        private readonly UserDto _currentUser;
        private IUnitOfWork Context { get; set; }
        private readonly IMapper _mapper;

        public RecordService(IUnitOfWork unitOfWork, IMapper mapper, CurrentUserService userService)
        {
            Context = unitOfWork;
            _mapper = mapper;
            _currentUser = userService.CurrentUser;
        }

        /// <summary>
        /// Get all Records
        /// </summary>
        public async Task<IEnumerable<RecordForReadDto>> GetAll()
        {
            var records = await Context.Records.GetAll();
            return _mapper.Map<IEnumerable<RecordForReadDto>>(records);
        }

        /// <summary>
        /// Get record by Id
        /// </summary>
        public RecordForReadDto GetById(long recordId)
        {
            if (recordId < 1) return null;

            var record = Context.Records.GetById(recordId);
            return _mapper.Map<RecordForReadDto>(record);
        }

        /// <summary>
        /// Create new Record
        /// </summary>
        public async Task<long> Create(RecordForCreateDto recordForCreateDto)
        {
            var record = _mapper.Map<Record>(new RecordForCreateDto
            {
                Name = recordForCreateDto.Name,
                ProjectId = recordForCreateDto.ProjectId,
                Fields = recordForCreateDto.Fields,
                ListFields = recordForCreateDto.ListFields,
                Status = recordForCreateDto.Status
            });

            await Context.Records.Create(record);
            await Context.SaveAsync();
            
            if (recordForCreateDto.ListChildIds != null && recordForCreateDto.ListChildIds.Any())
            {
                foreach (var сhildId in recordForCreateDto.ListChildIds)
                {
                    var childRecord = Context.Records.GetById(сhildId);

                    childRecord.ParentRecordId = record.Id;
                    
                    Context.Records.Update(childRecord);
                    await Context.SaveAsync();
                }
            }

            return record.Id;
        }

        /// <summary>
        /// Update only the columns of an existing Record
        /// </summary>
        public async Task<bool> Update(RecordDto recordDto)
        {
            var record = Context.Records.GetById(recordDto.Id);

            if (record == null) return false;

            record.Name = recordDto.Name;
            record.ProjectId = recordDto.ProjectId;
            record.UpdatedAt = DateTime.UtcNow;

            Context.Records.Update(record);
            await Context.SaveAsync();

            return true;
        }

        /// <summary>
        /// Delete an existing Record along with fields and lists
        /// </summary>
        public async Task<bool> Delete(long recordId)
        {
            var result = Context.Records.Delete(recordId);
            await Context.SaveAsync();

            return result;
        }

        public async Task<bool> GetAccess(long roleId, ActionEnum action)
        {
            try
            {
                var access = await Context.Permissions.GetByRoleAndType(roleId, PermissionEnum.Record);

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
