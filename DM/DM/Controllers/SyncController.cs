using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Dotmim.Sync;
using Dotmim.Sync.Web.Server;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DM.Controllers
{
    [ApiController]
    [Route("api/sync")]
    public class SyncController : ControllerBase
    {
        private WebServerAgent webServerAgent;

        public SyncController(WebServerAgent webServerAgent)
        {
            this.webServerAgent = webServerAgent;
        }
        
        [HttpPost]
        public async Task Post()
        {
            
            webServerAgent.RemoteOrchestrator.OnApplyChangesConflictOccured(e =>
            {
                var conflict = e.GetSyncConflictAsync();
            
                var localRow = conflict.Result.LocalRow.ToString();
                var remoteRow = conflict.Result.RemoteRow.ToString();
               
            });
            
            await webServerAgent.HandleRequestAsync(this.HttpContext);
        }
    }
}