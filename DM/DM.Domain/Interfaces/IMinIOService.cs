using DM.Domain.DTO;
using Minio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace offline_module.Domain.Interfaces
{
    public interface IMinIOService
    {
        Task<int> UploadItems(int userId, IEnumerable<int>itemIds);

        Task<int> DownloadItems(int userId, IEnumerable<int> itemIds);
    }
}
