using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _00_Task_Tracker
{
    internal class TasksRepository
    {
        private string path = "tasks.json";

        public string AddTask(string description)
        {
            List<Task> tasks = new List<Task>();

            if (!File.Exists(path))
            {
                File.WriteAllText(path, JsonConvert.SerializeObject(tasks));
            }
            else
            {
                string jsonData = File.ReadAllText(path);
                tasks = JsonConvert.DeserializeObject<List<Task>>(jsonData) ?? new List<Task>();
            }

            int taskId = tasks.Count > 0 ? tasks[tasks.Count - 1].id + 1 : 1;

            Task task = new Task()
            {
                id = taskId,
                description = description,
                status = "todo",
                createdAt = DateTime.Now,
                updatedAt = DateTime.Now
            };

            tasks.Add(task);

            File.WriteAllText(path, JsonConvert.SerializeObject(tasks, Formatting.Indented));

            return $"Task added successfully (ID: {taskId})";
        }


        public string UpdateTask(int taskId, string newDescription) 
        {
            List<Task> tasks = new List<Task>();

            if (!File.Exists(path))
            {
                return "There are currently no tasks, add some first - no file";
            }

            string jsonData = File.ReadAllText(path);
            tasks = JsonConvert.DeserializeObject<List<Task>>(jsonData) ?? new List<Task>();

            if (tasks.Count < 1)
            {
                return "There are currently no tasks, add some first - no item";
            }

            for (int i = 0; i < tasks.Count; i++)
            {
                if (taskId == tasks[i].id)
                {
                    tasks[i].description = newDescription;
                    tasks[i].updatedAt = DateTime.Now;
                }
            }

            File.WriteAllText(path, JsonConvert.SerializeObject(tasks, Formatting.Indented));

            return $"Task updated successfully (ID: {taskId})";
        }

    }
}
