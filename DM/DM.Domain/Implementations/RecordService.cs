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
        public async Task<long> Create(RecordModel recordModel)
        {
            var record = _mapper.Map<RecordEntity>(recordModel);


            var result = await _context.Records.AddAsync(record);
            await _context.SaveChangesAsync();
            return result.Entity.Id;
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
