using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace offline_module.Domain.Interfaces
{
    public interface IBearerConfigService
    {
        void WriteFile(string bearerToken);

        string ReadFile();
    }
}
