using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using DM.DAL.Entities;
using DM.DAL.Interfaces;


namespace DM.DAL.Repositories
{
    public class RecordRepository : IRecordRepository<Record>
    {
        private readonly DmDbContext _dbContext;

        public RecordRepository(DmDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> Create(Record record)
        {
            await _dbContext.Records.AddAsync(record);
            return true;
        }

        public bool Delete(long? id)
        {
            Record record = _dbContext.Records.Find(id);
            if (record != null)
            {
                _dbContext.Records.Remove(record);
                return true;
            }
            return false;
        }

        public async Task<IEnumerable<Record>> GetAll()
        {
            IEnumerable<Record> records = await _dbContext.Records
                .Include(x => x.Comments)
                .Include(x => x.Fields)
                .Include(x => x.ListFields).ThenInclude(y => y.Lists)
                // TODO: I don't know why we need it.
                /*.Include(x => x.ChildRecords).Where(y => y.ParentRecord == null)*/
                .ToListAsync();

            return records;
        }

        public Record GetById(long? id)
        {
            Record record = _dbContext.Records
                .Include(x => x.Comments)
                .Include(x => x.Fields)
                .Include(x => x.ListFields).ThenInclude(y => y.Lists)
                .FirstOrDefault(y => y.Id == id);

            return record;
        }

        public void Update(Record record)
        {
            _dbContext.Entry(record).State = EntityState.Modified;
        }
    }
}
