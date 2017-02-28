using System;

namespace LTTimer.Model
{
    public class TimerTable
    {
        public string id { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Name { get; set; }
        public bool Deleted { get; set; }
    }

}
