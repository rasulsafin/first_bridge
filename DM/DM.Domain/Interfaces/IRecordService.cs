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
        public List<RecordForReadModel> GetAll();
        public RecordForReadModel GetById(long recordId);
        public Task<long> Create(RecordForCreateModel recordModel);
        public Task<bool> Update(RecordModel recordModel);
        public Task<bool> Delete(long recordId);
    }
}
