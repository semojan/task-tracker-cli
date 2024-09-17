using Newtonsoft.Json;

namespace _00_Task_Tracker
{
    internal class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                string path = "tasks.json";

                string req = Console.ReadLine();
                if (req.ToLower() == "exit") break;


                string[] reqArr = req.Split(" ");

                string result = "";

                if (reqArr[0].ToLower() == "add")
                {
                    result = AddTask(reqArr[1], path);
                }

                Console.WriteLine(result);
            }

            Console.WriteLine("the end");
        }

        static string AddTask(string description, string path)
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
    }
}
