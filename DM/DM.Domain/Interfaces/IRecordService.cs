using DM.DAL.Entities;
using DM.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DM.Domain.Interfaces
{
    public interface IRecordService : IGetAccess
    {
        public Task<IEnumerable<RecordForReadDto>> GetAll();

        public Task<IEnumerable<RecordForReadDto>> GetRecords(int projId);

        public RecordForReadDto GetById(long recordId);
        public Task<long> Create(RecordForCreateDto record);
        public Task<bool> Update(RecordDto record);
        public Task<bool> Delete(long recordId);
        Task<IEnumerable<RecordForReadDto>> GetSubObjectives(int recordId);
        void Dispose();
    }
}
