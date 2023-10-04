using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace offline_module.Domain.Services
{
    internal class FilesystemUtility
    {
        private static readonly string MY_DOCUMENTS = Path.GetFullPath(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments, Environment.SpecialFolderOption.Create));
        private static readonly string APPLICATION_DIRECTORY_NAME = "Brio MRS";
        private static readonly string DATABASE_DIRECTORY_NAME = "Database";
        private static readonly string SNAPSHOT_STORE_DIRECTORY_NAME = "Snapshots";

        public static string ApplicationFolder => Path.Combine(MY_DOCUMENTS, APPLICATION_DIRECTORY_NAME);

        public static string Database => Path.Combine(ApplicationFolder, DATABASE_DIRECTORY_NAME);

        public static string SnapshotStore => Path.Combine(ApplicationFolder, SNAPSHOT_STORE_DIRECTORY_NAME);

    }
}
