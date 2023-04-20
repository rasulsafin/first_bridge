using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

using Microsoft.EntityFrameworkCore;

using AutoMapper;

using DM.Domain.Interfaces;
using DM.Domain.Models;

using DM.DAL.Entities;
using DM.DAL;

namespace DM.Domain.Implementations
{
    public class RecordService : IRecordService
    {
        private readonly DmDbContext _context;
        private readonly UserEntity _currentUser;

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
        public List<RecordModel> GetAll()
        {
            var records = _context.Records
                .Include(x => x.Comments)
                .Include(x => x.Fields)
                .Include(x => x.ListFields).ThenInclude(y => y.Lists)
                .ToList();

            return _mapper.Map<List<RecordModel>>(records);
        }

        /// <summary>
        /// Get record by Id
        /// </summary>
        public RecordModel GetById(long recordId)
        {
            var record = _context.Records
                .Include(x => x.Comments)
                .Include(x => x.Fields)
                .Include(x => x.ListFields).ThenInclude(y => y.Lists)
                .FirstOrDefault(x => x.Id == recordId);

            if (record == null)
            {
                return null;
            }

            return _mapper.Map<RecordModel>(record);
        }

        /// <summary>
        /// Create new Record
        /// </summary>
        public async Task<long> Create(RecordModel recordModel)
        {
            var record = _mapper.Map<RecordEntity>(new RecordModel
            {
                Name = recordModel.Name,
                ProjectId = recordModel.ProjectId,
                Fields = recordModel.Fields.ToList(),
                ListFields = recordModel.ListFields.ToList()
            });

            var result = await _context.Records.AddAsync(record);

            await _context.SaveChangesAsync();

            return result.Entity.Id;
        }

        /// <summary>
        /// Update only the columns of an existing Record
        /// </summary>
        public async Task<bool> Update(RecordModel record)
        {
            var fieldForUpdate = await _context.Records.FirstOrDefaultAsync(x => x.Id == record.Id);

            if (fieldForUpdate == null)
            {
                return false;
            }

            _context.Records.Attach(fieldForUpdate);

            fieldForUpdate.Name = record.Name;
            fieldForUpdate.ProjectId = record.ProjectId;

            await _context.SaveChangesAsync();

            _context.Entry(fieldForUpdate).State = EntityState.Detached;

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

            if (result == null)
            {
                return false;
            }

            _context.Records.Remove(result);

            await _context.SaveChangesAsync();

            return true;
        }
    }
}
