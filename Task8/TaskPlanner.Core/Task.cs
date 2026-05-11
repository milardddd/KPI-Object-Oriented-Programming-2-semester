using System;

namespace TaskPlanner.Core
{
    public class ProjectTask : ISearchable 
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime Deadline { get; set; }
        public bool IsCompleted { get; set; }
        public double ProgressPercentage { get; set; }
        public TeamMember? AssignedTo { get; set; }

        public bool IsDeadlineExpired => DateTime.Now > Deadline;

        public string GetInfo()
        {
            string status = IsCompleted ? "Виконано" : (IsDeadlineExpired ? "Прострочено" : "В процесі");
            return $"[Завдання ID:{Id}] {Title} | Статус: {status} | Прогрес: {ProgressPercentage}% | Виконавець: {AssignedTo?.FullName ?? "Не призначено"}";
        }

        public bool Matches(string query)
        {
            return Title.Contains(query, StringComparison.OrdinalIgnoreCase);
        }
    }
}