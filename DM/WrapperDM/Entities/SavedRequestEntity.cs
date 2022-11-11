using RestSharp;

namespace WrapperDM.Entities;

public class SavedRequestEntity : BaseEntity
{
    public string Body { get; set; }
    public string Headers { get; set; }
    public string Path { get; set; }
    public Method Method { get; set; }
}