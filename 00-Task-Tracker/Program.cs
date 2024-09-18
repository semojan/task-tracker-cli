﻿using Newtonsoft.Json;

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
                }else if (reqArr[0].ToLower() == "update")
                {
                    int taskId = int.Parse(reqArr[1]);
                    result = tasksRepo.UpdateTask(taskId, reqArr[2]);
                }

                Console.WriteLine(result);
            }

            Console.WriteLine("the end");
        }

        
    }
}
