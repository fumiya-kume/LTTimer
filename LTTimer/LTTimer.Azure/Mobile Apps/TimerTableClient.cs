using System;
using System.Linq;
using System.Threading.Tasks;
using LTTimer.Model;
using Microsoft.Practices.ObjectBuilder2;
using Microsoft.WindowsAzure.MobileServices;

namespace LTTimer.Azure.Mobile_Apps
{
    public class TimerTableClient
    {
        public static IMobileServiceTable<TimerTable> TimerTable => Variable.MobileAppsInstance.GetTable<TimerTable>();

        public static async Task<bool> StartTimer(string name)
        {
            var deleteResult = await DeleteTimerData(name);
            if (!deleteResult)
                return false;

            await TimerTable.InsertAsync(new TimerTable {Name = name});
            return true;
        }

        public static async Task<DateTime> GetTimeFromTimerTable(string name)
        {
            var timerList =
                await TimerTable.Where(table => table.Name == name).OrderBy(table => table.CreatedAt).ToListAsync();
            return timerList.First().CreatedAt;
        }

        private static async Task<bool> DeleteTimerData(string name)
        {
            if (name == null) return false;

            var idList = await TimerTable.Where(table => table.Name == name)
                .Select(table => table.id)
                .ToEnumerableAsync();
            idList.ForEach(s => TimerTable.DeleteAsync(new TimerTable {id = s}));
            return !TimerTable.Where(table => table.Name == name).RequestTotalCount;
        }
    }
}