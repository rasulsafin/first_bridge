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
        public List<RecordModel> GetAll();
        public RecordModel GetById(long recordId);
        public Task<long> Create(RecordModel recordModel);
    }
}
