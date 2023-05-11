using System.IO;

namespace DM.Common.Helpers
{
    public static class FileHelper
    {
        public static readonly string PathServerStorage = "C:\\others\\";
        public static readonly string CurrentPathServerStorage = "E:\\full-project\\document-manager\\DM\\DM\\";

        public static readonly int lastVersion = 1;  // variable for version tracking

        public static string GetFileTypes(string ext)
        {
            return ext switch
            {
                ".txt" => "text/plain",
                ".csv" => "text/csv",
                ".pdf" => "application/pdf",
                ".doc" => "application/vnd.ms-word",
                ".xls" => "application/vnd.ms-excel",
                ".ppt" => "application/vnd.ms-powerpoint",
                ".docx" => "application/vnd.openxmlformats-officedocument.wordprocessingml.document",
                ".xlsx" => "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                ".pptx" => "application/vnd.openxmlformats-officedocument.presentationml.presentation",
                ".png" => "image/png",
                ".jpg" => "image/jpeg",
                ".jpeg" => "image/jpeg",
                ".gif" => "image/gif",
                _ => "application/octet-stream",
            };
        }

        public static string CreateFilePath(string fileNameWithoutExtension, string lastVersion, string fileExtension)
        {
            return PathServerStorage + fileNameWithoutExtension + @"\" + fileNameWithoutExtension + "_v" + lastVersion + fileExtension;
        }

        public static string GenerateFileVersion(string pathSaveFile)
        {
            long version = 1;
            var files = Directory.GetFiles(pathSaveFile);

            if (files.Length > 0) // Check if folder contains files with certain name
            {
                foreach (var a in Directory.GetFiles(pathSaveFile))
                {

                    if (a.Contains(version.ToString()))
                    {
                        version += 1;
                    }
                }
            }

            return version.ToString();
        }

        public static bool ValidateFileExtension(string fileExtension)
        {
            if (!string.IsNullOrEmpty(fileExtension))
            {
                if (fileExtension != ".jpg")
                    return true;

                if (fileExtension != ".png")
                    return true;

                if (fileExtension != ".bim")
                    return true;

                if (fileExtension != ".ifc")
                    return true;
            }
            return false;
        }

        public static string DeleteFileExtention(string fileName)
        {
            return fileName.Remove(fileName.Length - 4);
        }
    }
}
