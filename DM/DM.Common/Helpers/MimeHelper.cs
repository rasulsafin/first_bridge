namespace DM.Common.Helpers
{
    public static class MimeHelper
    {
        public static string GetMimeTypes(string ext)
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
    }
}
