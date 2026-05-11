using System;

namespace TaskPlanner.Core
{
    public abstract class Person : ISearchable
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName => $"{FirstName} {LastName}";

        public abstract string GetInfo();

        // Реалізація пошуку за замовчуванням для всіх людей
        public virtual bool Matches(string query)
        {
            return FullName.Contains(query, StringComparison.OrdinalIgnoreCase);
        }
    }
}