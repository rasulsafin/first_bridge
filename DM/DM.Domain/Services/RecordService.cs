using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

using Microsoft.EntityFrameworkCore;

using AutoMapper;

using DM.Domain.Interfaces;
using DM.Domain.Models;

using DM.DAL.Entities;
using DM.DAL;

namespace DM.Domain.Services
{
    public class RecordService : IRecordService
    {
        private readonly DmDbContext _context;
        private readonly UserDto _currentUser;

        private readonly IMapper _mapper;

        public RecordService(DmDbContext context, IMapper mapper, CurrentUserService userService)
        {
            _context = context;
            _mapper = mapper;
            _currentUser = userService.CurrentUser;
        }

        /// <summary>
        /// Get all Records
        /// </summary>
        public List<RecordForReadDto> GetAll()
        {
            var records = _context.Records
                .Include(x => x.Comments)
                .Include(x => x.Fields)
                .Include(x => x.ListFields).ThenInclude(y => y.Lists)
                .ToList();

            return _mapper.Map<List<RecordForReadDto>>(records);
        }

        /// <summary>
        /// Get record by Id
        /// </summary>
        public RecordForReadDto GetById(long recordId)
        {
            var record = _context.Records
                .Include(x => x.Comments)
                .Include(x => x.Fields)
                .Include(x => x.ListFields).ThenInclude(y => y.Lists)
                .FirstOrDefault(x => x.Id == recordId);

            if (record == null) return null;

            return _mapper.Map<RecordForReadDto>(record);
        }

        /// <summary>
        /// Create new Record
        /// </summary>
        public async Task<long> Create(RecordForCreateDto recordForCreateModel)
        {
            var record = _mapper.Map<Record>(new RecordForCreateDto
            {
                Name = recordForCreateModel.Name,
                ProjectId = recordForCreateModel.ProjectId,
                Fields = recordForCreateModel.Fields.ToList(),
                ListFields = recordForCreateModel.ListFields.ToList()
            });

            var result = await _context.Records.AddAsync(record);

            await _context.SaveChangesAsync();

            return result.Entity.Id;
        }

        /// <summary>
        /// Update only the columns of an existing Record
        /// </summary>
        public async Task<bool> Update(RecordDto recordModel)
        {
            var record = await _context.Records.FirstOrDefaultAsync(x => x.Id == recordModel.Id);

            if (record == null) return false;

            _context.Records.Attach(record);

            record.Name = record.Name;
            record.ProjectId = record.ProjectId;
            record.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            _context.Entry(record).State = EntityState.Detached;

            return true;
        }

        /// <summary>
        /// Delete an existing Record along with fields and lists
        /// </summary>
        public async Task<bool> Delete(long recordId)
        {
            var result = await _context.Records
                .Include(x => x.Comments)
                .Include(x => x.Fields)
                .Include(x => x.ListFields).ThenInclude(y => y.Lists)
                .FirstOrDefaultAsync(x => x.Id == recordId);

            if (result == null) return false;

            result.IsInArchive = true;

            _context.Records.Update(result);

            await _context.SaveChangesAsync();

            return true;
        }
    }
}
