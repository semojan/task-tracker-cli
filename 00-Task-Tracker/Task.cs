using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _00_Task_Tracker
{
    public class Task
    {
        public int id { get; set; }
        public string description { get; set; }
        public Status status { get; set; }
        public DateTime createdAt { get; set; } 
        public DateTime updatedAt { get; set; }

        public enum Status
        {
            todo = 0,
            inProgress = 1,
            done = 2,
        }
    }
}
