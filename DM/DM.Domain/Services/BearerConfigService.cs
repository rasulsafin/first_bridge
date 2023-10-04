using offline_module.Domain.Interfaces;
using System.IO;

namespace offline_module.Domain.Services
{
    public class BearerConfigService : IBearerConfigService
    {
        public string _pathToFile;

        public BearerConfigService()
        {
            _pathToFile = "BearerConfig.json";
        }

        public void WriteFile(string bearerToken)
        {
            if (File.Exists(_pathToFile))
            {
                File.WriteAllText(_pathToFile, bearerToken);
            }
            else
            {
                File.Create(_pathToFile);
            }
        }

        public string ReadFile()
        {
            if (!File.Exists(_pathToFile))
            {
                File.Create(_pathToFile);
            }
            return File.ReadAllText(_pathToFile);
        }
    }
}
