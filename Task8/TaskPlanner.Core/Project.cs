using System.Collections.Generic;
using System.Linq;

namespace TaskPlanner.Core
{
    public class Project
    {
        public ProjectDetails Details { get; set; } = new ProjectDetails();
        public List<TeamMember> Members { get; set; } = new List<TeamMember>();
        
        public List<ProjectTask> Tasks { get; set; } = new List<ProjectTask>();

        public void AddMember(TeamMember member) => Members.Add(member);
        
        public bool RemoveMember(int id) => Members.RemoveAll(m => m.Id == id) > 0;

        public void AddTask(ProjectTask task) => Tasks.Add(task);

        public bool AssignTask(int taskId, int memberId)
        {
            var task = Tasks.FirstOrDefault(t => t.Id == taskId);
            var member = Members.FirstOrDefault(m => m.Id == memberId);
            
            if (task != null && member != null)
            {
                task.AssignedTo = member;
                if (!member.AssignedTasks.Contains(task))
                {
                    member.AssignedTasks.Add(task);
                }
                return true;
            }
            return false;
        }
    }
}