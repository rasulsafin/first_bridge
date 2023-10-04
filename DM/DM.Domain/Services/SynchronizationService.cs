using offline_module.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace offline_module.Domain.Services
{
    public class SynchronizationService : ISynchronizationService
    {
        private ISyncDotMimService syncDotMimService;

        public SynchronizationService(ISyncDotMimService syncService)
        {
            this.syncDotMimService = syncService;
        }

        public async Task Synchronize()
        {
            await syncDotMimService.SynchronizeAsync();
        }
    }
}
