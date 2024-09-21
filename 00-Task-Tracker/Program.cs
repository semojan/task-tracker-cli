using Newtonsoft.Json;

namespace _00_Task_Tracker
{
    internal class Program
    {
        static void Main(string[] args)
        {
            TasksRepository tasksRepo = new TasksRepository();

            Console.WriteLine("Welcome to the TASK TRACKER app\n");
            Console.WriteLine("list of commands you can use with examples:\n" +
                "add \"task\"               --> ex: add \"Buy groceries\"\n" +
                "update id \"new task\"     --> ex: update 1 \"Buy groceries and cook dinner\"\n" +
                "delete id                  --> ex: delete 1\n" +
                "mark-in-progress id        --> ex: mark-in-progress 1\n" +
                "mark-done id               --> ex: mark-done 1\n" +
                "list                       --> this command lists all your tasks\n" +
                "list done\n" +
                "list todo\n" +
                "list in-progress\n" +
                "clear                      --> clear screen\n" +
                "exit                       --> exit the app\n\n"
                );
            Console.Write(">");

            while (true)
            {
                string req = Console.ReadLine();
                if (req.ToLower() == "exit") break;
                
                string[] reqArr = req.Split(" ");

                string result = "";

                if (reqArr[0].ToLower() == "add")
                {
                    result = tasksRepo.AddTask(reqArr[1]);
                }
                else if (reqArr[0].ToLower() == "update")
                {
                    int taskId = int.Parse(reqArr[1]);
                    result = tasksRepo.UpdateTask(taskId, reqArr[2]);
                }
                else if (reqArr[0].ToLower() == "delete")
                {
                    int taskId = int.Parse(reqArr[1]);
                    result = tasksRepo.DeleteTask(taskId);
                }
                else if (reqArr[0].ToLower() == "mark-in-progress" || reqArr[0].ToLower() == "mark-done")
                {
                    int taskId = int.Parse(reqArr[1]);
                    result = tasksRepo.ChangeStatus(taskId, reqArr[0].ToLower());
                } else if (reqArr[0].ToLower() == "list"){
                    if (reqArr.Length > 1)
                    {
                        result = tasksRepo.ListTasks(reqArr[1].ToLower());
                    }
                    else
                    {
                        result = tasksRepo.ListTasks();
                    }
                }else if (reqArr[0] == "clear")
                {
                    Console.Clear();
                    Console.WriteLine("list of commands you can use with examples:\n" +
                        "add \"task\"               --> ex: add \"Buy groceries\"\n" +
                        "update id \"new task\"     --> ex: update 1 \"Buy groceries and cook dinner\"\n" +
                        "delete id                  --> ex: delete 1\n" +
                        "mark-in-progress id        --> ex: mark-in-progress 1\n" +
                        "mark-done id               --> ex: mark-done 1\n" +
                        "list                       --> this command lists all your tasks\n" +
                        "list done\n" +
                        "list todo\n" +
                        "list in-progress\n" +
                        "clear                      --> clear screen\n" +
                        "exit                       --> exit the app\n\n>"
                        );
                    Console.Write(">");
                }
                else
                {
                    Console.WriteLine("\nplease enter a valid command");
                }


                Console.WriteLine(result);

                Console.Write("\n>");
            }

            Console.WriteLine("the end");
        }

        
    }
}
