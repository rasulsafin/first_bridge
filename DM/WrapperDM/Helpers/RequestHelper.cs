using RestSharp;

namespace WrapperDM.Helpers;

public static class RequestHelper
{
    public static RestRequest GetRequest(string url, Method method)
    {
        return new RestRequest(url, method);
    }
}