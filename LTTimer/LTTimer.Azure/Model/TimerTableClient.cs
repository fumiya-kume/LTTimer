using System.Linq;
using System.Threading.Tasks;
using LTTimer.Azure.DataModel;
using Microsoft.WindowsAzure.MobileServices;

namespace LTTimer.Azure.Model
{
    public class TimerTableClient : ITimerTableClient
    {
        private static MobileServiceClient Client => new MobileServiceClient("http://lttimer.azurewebsites.net");

        public async Task<TimerTable> GetTimer(string key)
        {
            var timer = await Client.GetTable<TimerTable>().Where(table => table.Name == key).ToListAsync();
            return timer.FirstOrDefault();
        }

        public async void StartTimer(string key)
        {
            await Client.GetTable<TimerTable>().InsertAsync(new TimerTable {Name = key});
        }
    }
}
