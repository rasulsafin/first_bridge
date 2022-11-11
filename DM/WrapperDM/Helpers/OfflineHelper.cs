using System;
using System.Diagnostics;
using System.Net.NetworkInformation;
using System.Threading.Tasks;

namespace WrapperDM.Helpers;

public static class OfflineHelper
{
    public static bool CheckForInternetConnection() 
    {
        var stopwatch = new Stopwatch();
        stopwatch.Start();
        try {
                // пинг вечнодоступного гугла
                Ping myPing = new Ping();
                String host = "google.com";
                byte[] buffer = new byte[32];
                int timeout = 1000;
                PingOptions pingOptions = new PingOptions();
                PingReply reply = myPing.Send(host, timeout, buffer, pingOptions);

                // пинг нашего ресурса, на всякий случай
                if (reply.Status != IPStatus.Success)
                {
                    Ping myPing2 = new Ping();
                    String host2 = "google.com"; // TODO: поменять ссылку перед деплоем
                    byte[] buffer2 = new byte[32];
                    int timeout2 = 1000;
                    PingOptions pingOptions2 = new PingOptions();
                    PingReply reply2 = myPing.Send(host2, timeout2, buffer2, pingOptions2);

                    return false;
                }

                if (reply.Status == IPStatus.Success)
                {
                    return true;
                }
        }
        catch (Exception) {
            return false;
        }

        return false;
    }
}