using AutoMapper;
using DM.DAL.Entities;
using DM.Domain.Helpers;
using DM.Domain.Interfaces;
using DM.Domain.Models;
using DM.repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
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
        public async Task<long> Create(RecordModel recordModel)
        {
            var json = JsonDocument.Parse(recordModel.Fields.ToString());

            var m = new RecordEntity { Name = recordModel.Name, Fields = json, ProjectId = recordModel.ProjectId };

            var result = await _context.Records.AddAsync(m);
            await _context.SaveChangesAsync();
            return result.Entity.Id;
        }
        /// <summary>
        /// update fields attached to a record
        /// </summary>
         
        public async Task<bool> Update(RecordModel record)
        {
            var stringjson = record.Fields.ToString();
            var fieldForUpdate = await _context.Records.FirstOrDefaultAsync(x => x.Name == record.Name);

            if (fieldForUpdate == null) 
            {
                return false;
            }

            _context.Records.Attach(fieldForUpdate);

            fieldForUpdate.Name = record.Name;
            fieldForUpdate.ProjectId = record.ProjectId;
            fieldForUpdate.Fields = JsonDocument.Parse(stringjson);

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
