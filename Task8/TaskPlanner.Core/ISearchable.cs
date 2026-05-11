namespace TaskPlanner.Core
{
    public interface ISearchable
    {
        bool Matches(string query);
    }
}