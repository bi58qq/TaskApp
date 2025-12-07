using System;
using System.Collections.Generic;
using TaskApp.Model;

namespace TaskApp.Services
{
    public interface ITaskService
    {
        TaskItem CreateTask(string title, string description, DateTime dueDate,
                            TaskPriority priority, string assignee);

        bool UpdateTask(Guid id, Action<TaskItem> updateAction);

        TaskItem GetTaskById(Guid id);
        List<TaskItem> SearchByTitle(string keyword);

        List<TaskItem> GetAllTasks();

        // Sorting
        List<TaskItem> SortByDueDate(bool ascending = true);
        List<TaskItem> SortByPriority(bool ascending = true);

        // Reports
        List<TaskItem> GetOverdueTasks();
        List<TaskItem> GetUpcomingTasks(int days = 7);

        void Save();
        void Load();
    }
}
