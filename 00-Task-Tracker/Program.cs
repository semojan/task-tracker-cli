using Newtonsoft.Json;

namespace _00_Task_Tracker
{
    internal class Program
    {
        static void Main(string[] args)
        {
            TasksRepository tasksRepo = new TasksRepository();

            while (true)
            {

                string req = Console.ReadLine();
                if (req.ToLower() == "exit") break;


                string[] reqArr = req.Split(" ");

                string result = "";

                if (reqArr[0].ToLower() == "add")
                {
                    result = tasksRepo.AddTask(reqArr[1]);
                } else if (reqArr[0].ToLower() == "update")
                {
                    int taskId = int.Parse(reqArr[1]);
                    result = tasksRepo.UpdateTask(taskId, reqArr[2]);
                } else if (reqArr[0].ToLower() == "delete")
                {
                    int taskId = int.Parse(reqArr[1]);
                    result = tasksRepo.DeleteTask(taskId);
                } else if (reqArr[0].ToLower() == "mark-in-progress" || reqArr[0].ToLower() == "mark-done")
                {
                    int taskId = int.Parse(reqArr[1]);
                    result = tasksRepo.ChangeStatus(taskId, reqArr[0].ToLower());
                }

                Console.WriteLine(result);
            }

            Console.WriteLine("the end");
        }

        
    }
}
