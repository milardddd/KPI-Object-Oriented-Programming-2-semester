using System.Collections.Generic;
using System.Linq;

namespace TaskPlanner.Core
{
    public class TeamMember : Person
    {
        public List<ProjectTask> AssignedTasks { get; set; } = new List<ProjectTask>();

        public override string GetInfo()
        {
            return $"[Виконавець ID:{Id}] {FullName}, Завдань у роботі: {AssignedTasks.Count(t => !t.IsCompleted)}";
        }
    }
}