using AutoMapper;
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
    public class RecordService : IRecordService
    {
        private readonly DmDbContext _context;
        private readonly IMapper _mapper;

        public RecordService(DmDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public List<RecordModel> GetAll()
        {
            var records = _context.Records.Include(f => f.Fields).ToList();
            return _mapper.Map<List<RecordModel>>(records);
        }
        public RecordModel GetById(long recordId)
        {
            var record = _context.Records.Include(f => f.Fields).FirstOrDefault(x => x.Id == recordId);
            return _mapper.Map<RecordModel>(record);
        }
        public async Task<long> Create(RecordModelForCreate recordModel)
        {
            var fields = new List<FieldsEntity>();
            foreach (var c in recordModel.Fields)
            {
                fields.Add( new FieldsEntity() { Name = c.Name, Description = c.Description, AssigneeId = c.AssigneeId, IssuerId = c.IssuerId, State = c.State} );
            }
            var rec = new RecordEntity()
            { Name = recordModel.Name, ProjectId = recordModel.ProjectId, Fields = fields };

            var result = await _context.Records.AddAsync(rec);
            await _context.SaveChangesAsync();
            return result.Entity.Id;
        }
        /// <summary>
        /// update fields attached to a record
        /// </summary>
        public async Task<bool> Update(FieldsModel fields)
        {
            var fieldForUpdate = await _context.Fields.FirstOrDefaultAsync(x => x.Id == fields.Id);

            if (fieldForUpdate == null) 
            {
                return false;
            }

            _context.Fields.Attach(fieldForUpdate);

            fieldForUpdate.Name = fields.Name;
            fieldForUpdate.Description = fields.Description;
            fieldForUpdate.State = fields.State;
            fieldForUpdate.IssuerId = fields.IssuerId;
            fieldForUpdate.AssigneeId = fields.AssigneeId;

            await _context.SaveChangesAsync();

            _context.Entry(fieldForUpdate).State = EntityState.Detached;
            return true;
        }

        //TODO: Add Checks
        public async Task<bool> Delete(long recordId)
        {
            var result = await _context.Records.Include(x => x.Fields).FirstOrDefaultAsync(x => x.Id == recordId);
            // if result.length == 0 Throw New Exception

            _context.Records.Remove(result);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
