using Microsoft.WindowsAzure.MobileServices;

namespace LTTimer.Azure.Mobile_Apps
{
     public static class Variable
    {
       public static MobileServiceClient MobileAppsInstance => new MobileServiceClient("http://lttimer.azurewebsites.net");
    }
}
