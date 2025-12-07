using System;
using System.Collections.Generic;
using System.Linq;
using TaskApp.Model;
using TaskApp.Services;

namespace TaskApp
{
    class Program
    {
        static JsonStorageService storage = new JsonStorageService();
        static LoggerService logger = new LoggerService();
        static TaskService taskService = new TaskService(storage, logger);

        static void Main(string[] args)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("===== TASK TRACKER SYSTEM =====");
                Console.WriteLine("1. Add Task");
                Console.WriteLine("2. Update Task Status");
                Console.WriteLine("3. Search Task by Title");
                Console.WriteLine("4. View All Tasks");
                Console.WriteLine("5. Sort Tasks by Due Date");
                Console.WriteLine("6. Sort Tasks by Priority");
                Console.WriteLine("7. View Overdue Tasks");
                Console.WriteLine("8. Exit");
                Console.Write("Choose option: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddTask();
                        break;

                    case "2":
                        UpdateTaskStatus();
                        break;

                    case "3":
                        SearchTask();
                        break;

                    case "4":
                        Display(taskService.GetAllTasks());
                        break;

                    case "5":
                        Display(taskService.SortByDueDate());
                        break;

                    case "6":
                        Display(taskService.SortByPriority());
                        break;

                    case "7":
                        Display(taskService.GetOverdueTasks());
                        break;

                    case "8":
                        return;

                    default:
                        Console.WriteLine("Invalid choice!");
                        break;
                }

                Console.WriteLine("\nPress ENTER to continue...");
                Console.ReadLine();
            }
        }

        static void AddTask()
        {
            Console.Write("Title: ");
            string title = Console.ReadLine();

            Console.Write("Description: ");
            string desc = Console.ReadLine();

            Console.Write("Due Date (yyyy-mm-dd): ");
            DateTime due = DateTime.Parse(Console.ReadLine());

            Console.Write("Priority (Low/Medium/High): ");
            TaskPriority pr = (TaskPriority)Enum.Parse(typeof(TaskPriority), Console.ReadLine(), true);

            Console.Write("Assignee: ");
            string assignee = Console.ReadLine();

            taskService.CreateTask(title, desc, due, pr, assignee);

            Console.WriteLine("Task added!");
        }

        static void UpdateTaskStatus()
        {
            Console.Write("Enter Task ID: ");
            Guid id = Guid.Parse(Console.ReadLine());

            Console.Write("New Status (ToDo/InProgress/Done): ");
            TaskStatus status = (TaskStatus)Enum.Parse(typeof(TaskStatus), Console.ReadLine(), true);

            bool updated = taskService.UpdateTask(id, t => t.Status = status);

            Console.WriteLine(updated ? "Updated." : "Task not found.");
        }

        static void SearchTask()
        {
            Console.Write("Keyword: ");
            string keyword = Console.ReadLine();

            var results = taskService.SearchByTitle(keyword);

            Display(results);
        }

        static void Display(List<TaskItem> items)
        {
            if (items == null || !items.Any())
            {
                Console.WriteLine("No tasks found!");
                return;
            }

            foreach (var t in items)
            {
                Console.WriteLine($"ID: {t.Id}");
                Console.WriteLine($"Title: {t.Title}");
                Console.WriteLine($"Due: {t.DueDate:yyyy-MM-dd}");
                Console.WriteLine($"Priority: {t.Priority}");
                Console.WriteLine($"Status: {t.Status}");
                Console.WriteLine($"Assignee: {t.Assignee}");
                Console.WriteLine(new string('-', 40));
            }
        }
    }
}
