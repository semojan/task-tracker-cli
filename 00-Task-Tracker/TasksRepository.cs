using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static _00_Task_Tracker.Task;

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
                status = (Task.Status)0,
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

        public string DeleteTask(int taskId)
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

            Task task = tasks.FirstOrDefault(t => t.id == taskId);

            if (task == null)
            {
                return $"Task with ID: {taskId} not found.";
            }

            tasks.Remove(task);

            File.WriteAllText(path, JsonConvert.SerializeObject(tasks, Formatting.Indented));


            return "Task deleted successfully";
        }

        public string ChangeStatus(int taskId, string status)
        {
            List<Task> tasks = new List<Task>();
            int statusCode;

            if (!File.Exists(path))
            {
                return "There are currently no tasks, add some first - no file";
            }

            if (status == "mark-in-progress")
            {
                statusCode = 1;
            } else
            {
                statusCode = 2;
            }

            string jsonData = File.ReadAllText(path);
            tasks = JsonConvert.DeserializeObject<List<Task>>(jsonData) ?? new List<Task>();

            if (tasks.Count < 1)
            {
                return "There are currently no tasks, add some first - no item";
            }

            Task task = tasks.FirstOrDefault(t => t.id == taskId);

            if (task == null)
            {
                return $"Task with ID: {taskId} not found.";
            }

            task.status = (Task.Status)statusCode;
            task.updatedAt = DateTime.Now;

            File.WriteAllText(path, JsonConvert.SerializeObject(tasks, Formatting.Indented));

            return "Task status changed successfully";
        }

        public string ListTasks(string filterStatus = "")
        {
            List<Task> allTasks = new List<Task>();
            int? statusCode = null;

            if (!File.Exists(path))
            {
                return "There are currently no tasks, add some first - no file";
            }

            if (filterStatus == "done")
            {
                statusCode = 2;
            }
            else if (filterStatus == "todo")
            {
                statusCode = 0;
            }
            else if (filterStatus == "in-progress")
            {
                statusCode = 1;
            }

            string jsonData = File.ReadAllText(path);
            allTasks = JsonConvert.DeserializeObject<List<Task>>(jsonData) ?? new List<Task>();

            if (allTasks.Count < 1)
            {
                return "There are currently no tasks, add some first - no item";
            }

            List<Task> result = new List<Task>();

            if (statusCode == null)
            {
                result = allTasks;
            }
            else
            {
                Task.Status status = (Status)statusCode;
                result = allTasks.FindAll(t => t.status == status);
            }

            if (!result.Any())
            {
                return $"No tasks found with status: {filterStatus}";
            }

            Console.WriteLine("\n\nList of your tasks:\n\n");

            foreach (var task in result)
            {
                Console.WriteLine($"ID: {task.id}");
                Console.WriteLine($"Description: {task.description}");
                Console.WriteLine($"Status: {task.status}");
                Console.WriteLine($"Created At: {task.createdAt}");
                Console.WriteLine($"Updated At: {task.updatedAt}");
                Console.WriteLine("\n*--------------------------------------*\n\n");
            }

            return "the list of your tasks ^^ \n\n";
        }
    }
}
