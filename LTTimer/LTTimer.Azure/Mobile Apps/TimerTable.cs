using System;
using System.Linq;
using System.Threading.Tasks;
using LTTimer.Model;
using Microsoft.Practices.ObjectBuilder2;
using Microsoft.WindowsAzure.MobileServices;

namespace LTTimer.Azure.Mobile_Apps
{
    public class TimerTable
    {
        public static IMobileServiceTable<Model.TimerTable> Timertable => Variable.MobileAppsInstance.GetTable<Model.TimerTable>();

        public static async Task<bool> StartTimer(string name)
        {
            var deleteResult = await DeleteTimerData(name);
            if (!deleteResult)
                return false;

            await Timertable.InsertAsync(new Model.TimerTable {Name = name});
            return true;
        }

        public static async Task<DateTime> GetTime(string name)
        {
            var timerList =
                await Timertable.Where(table => table.Name == name).Where(table => !table.Deleted).OrderBy(table => table.CreatedAt).ToListAsync();
            if(timerList.Count == 0) throw new NullReferenceException("Dont have enough item");
            return timerList.First().CreatedAt;
        }

        private static async Task<bool> DeleteTimerData(string name)
        {
            if (name == null) return false;

            var idList = await Timertable.Where(table => table.Name == name)
                .Select(table => table.id)
                .ToEnumerableAsync();
            idList.ForEach(s => Timertable.DeleteAsync(new Model.TimerTable {id = s}));
            return !Timertable.Where(table => table.Name == name).RequestTotalCount;
        }
    }
}