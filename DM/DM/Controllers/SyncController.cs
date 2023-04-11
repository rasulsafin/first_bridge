using System;
using System.Threading.Tasks;

using Dotmim.Sync;
using Dotmim.Sync.Web.Server;
using Dotmim.Sync.Enumerations;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;


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

        [Authorize]
        [HttpPost]
        public Task Post()
        {
            webServerAgent.RemoteOrchestrator.OnApplyChangesConflictOccured(e =>
            {
                var conflict = e.GetSyncConflictAsync();

                Console.WriteLine();
                Console.WriteLine($"LocalRow : {conflict.Result.LocalRow}");
                Console.WriteLine();
                Console.WriteLine($"RemoteRow : {conflict.Result.RemoteRow}");
                Console.WriteLine();

                //if (conflict.Result.RemoteRow.SchemaTable.TableName == "Users")
                //{
                //    if (Convert.ToDateTime(conflict.Result.RemoteRow["UpdatedAt"]) > Convert.ToDateTime(conflict.Result.LocalRow["UpdatedAt"]))
                //    {
                //        e.Resolution = ConflictResolution.ServerWins;
                //    }
                //    else if (Convert.ToDateTime(conflict.Result.RemoteRow["UpdatedAt"]) < Convert.ToDateTime(conflict.Result.LocalRow["UpdatedAt"]))
                //    {
                //        e.Resolution = ConflictResolution.ClientWins;
                //    }
                //    else
                //    {
                //        e.Resolution = ConflictResolution.ServerWins;
                //    }
                //}
                //else
                //{
                //    e.Resolution = ConflictResolution.ServerWins;
                //}
            });

            return webServerAgent.HandleRequestAsync(this.HttpContext);
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task Get()
        {
            await webServerAgent.HandleRequestAsync(this.HttpContext);
        }
    }
}