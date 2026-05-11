using System;
using System.Linq;

namespace TaskPlanner.Core
{
    public class ProjectReporter
    {
        private Project _project;

        public ProjectReporter(Project project)
        {
            _project = project;
        }

        public void ShowAllMembers()
        {
            Console.WriteLine($"\n--- Члени команди проекту {_project.Details.Name} ---");
            if (_project.Members.Count == 0) Console.WriteLine("Команда порожня.");
            _project.Members.ForEach(m => Console.WriteLine(m.GetInfo()));
        }

        public void ShowTasks(bool? completed = null)
        {
            var filtered = completed.HasValue ? _project.Tasks.Where(t => t.IsCompleted == completed.Value).ToList() : _project.Tasks;
            string filterStr = completed.HasValue ? (completed.Value ? "Виконані" : "Невиконані") : "Всі";
            
            Console.WriteLine($"\n--- Список завдань ({filterStr}) ---");
            if (filtered.Count == 0) Console.WriteLine("Завдань не знайдено.");
            foreach (var t in filtered) Console.WriteLine(t.GetInfo());
        }

        public void ShowProjectStatus()
        {
            double avgProgress = _project.Tasks.Count > 0 ? _project.Tasks.Average(t => t.ProgressPercentage) : 0;
            Console.WriteLine($"\n--- Стан проекту: {_project.Details.Name} ---");
            Console.WriteLine($"Загальний прогрес: {avgProgress:F2}%");
            Console.WriteLine($"Виконано: {_project.Tasks.Count(t => t.IsCompleted)} з {_project.Tasks.Count}");
        }

        public void SearchMembers(string query)
        {
            var result = _project.Members.Where(m => m.Matches(query)).ToList();
            Console.WriteLine($"\n--- Результати пошуку ('{query}') ---");
            if (result.Count == 0) Console.WriteLine("Нікого не знайдено.");
            foreach (var m in result) Console.WriteLine(m.GetInfo());
        }
    }
}