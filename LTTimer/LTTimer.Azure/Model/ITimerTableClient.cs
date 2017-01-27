using System.Threading.Tasks;
using LTTimer.Azure.DataModel;

namespace LTTimer.Azure.Model
{
    public interface ITimerTableClient
    {
        Task<TimerTable> GetTimer(string key);
        void StartTimer(string key);
    }
}