using DM.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DM.Domain.Interfaces
{
    public interface IRecordService
    {
        public Task<IEnumerable<RecordForReadDto>> GetAll();
        public RecordForReadDto GetById(long recordId);
        public Task<long> Create(RecordForCreateDto recordModel);
        public Task<bool> Update(RecordDto recordModel);
        public Task<bool> Delete(long recordId);
    }
}
