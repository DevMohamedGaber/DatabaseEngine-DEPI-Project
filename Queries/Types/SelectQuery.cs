namespace DatabaseEngine.Queries.Types
{
    public class SelectQuery : Query, IQueryable
    {
        public SelectQuery(string[] stmt) : base(stmt) { }
        public override void Execute()
        {
            
        }
    }
}
