using System;

namespace LTTimer.Azure.DataModel
{
    public class TimerTable
    {
        public string id { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Name { get; set; }
    }
}
