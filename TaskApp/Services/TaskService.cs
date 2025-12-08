using System;
using System.Collections.Generic;
using System.Linq;
using TaskApp.Model;

namespace TaskApp.Services
{
    public class TaskService : ITaskService
    {
        private readonly IStorageService storage;
        private readonly LoggerService logger;
        private readonly string filePath = "tasks.json";

        private List<TaskItem> tasks = new List<TaskItem>();

        public TaskService(IStorageService storage, LoggerService logger)
        {
            this.storage = storage;
            this.logger = logger;
            Load();
        }

        public TaskItem CreateTask(string title, string description, DateTime dueDate,
                                   TaskPriority priority, string assignee)
        {
            var task = new TaskItem
            {
                Title = title,
                Description = description,
                DueDate = dueDate,
                Priority = priority,
                Assignee = assignee
            };

            tasks.Add(task);
            logger.Log($"Task created: {task.Title} (ID: {task.Id})");
            Save();
            return task;
        }

        public bool UpdateTask(Guid id, Action<TaskItem> updateAction)
        {
            var task = tasks.FirstOrDefault(t => t.Id == id);

            if (task == null)
                return false;

            updateAction(task);
            task.UpdatedAt = DateTime.Now;

            logger.Log($"Task updated: {task.Title} (ID: {task.Id})");
            Save();
            return true;
        }

        public TaskItem GetTaskById(Guid id)
        {
            return tasks.FirstOrDefault(t => t.Id == id);
        }

        public List<TaskItem> SearchByTitle(string keyword)
        {
            return tasks
                .Where(t => t.Title != null &&
                            t.Title.ToLower().Contains(keyword.ToLower()))
                .ToList();
        }

        public List<TaskItem> GetAllTasks() => tasks;

        // ----------------------- Sorting ------------------------------

        public List<TaskItem> SortByDueDate(bool ascending = true)
        {
            return ascending
                ? tasks.OrderBy(t => t.DueDate).ToList()
                : tasks.OrderByDescending(t => t.DueDate).ToList();
        }

        public List<TaskItem> SortByPriority(bool ascending = true)
        {
            return ascending
                ? tasks.OrderBy(t => t.Priority).ToList()
                : tasks.OrderByDescending(t => t.Priority).ToList();
        }

        // ----------------------- Reports ------------------------------

        public List<TaskItem> GetOverdueTasks()
        {
            return tasks
                .Where(t => t.DueDate < DateTime.Now && t.Status != TaskStatus.Done)
                .ToList();
        }

        public List<TaskItem> GetUpcomingTasks(int days = 7)
        {
            var threshold = DateTime.Now.AddDays(days);

            return tasks
                .Where(t => t.DueDate >= DateTime.Now && t.DueDate <= threshold)
                .ToList();
        }

        // ----------------------- Persistence --------------------------

        public void Save()
        {
            storage.Save(filePath, tasks);
        }

        public void Load()
        {
            tasks = storage.Load<TaskItem>(filePath);
        }
    }
}
