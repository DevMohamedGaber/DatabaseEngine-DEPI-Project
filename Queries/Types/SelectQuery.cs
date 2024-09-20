namespace DatabaseEngine.Queries.Types
{
    public class SelectQuery : Query
    {
        public SelectQuery(string[] stmt) : base(stmt) { }
        protected override void Execute()
        {
            
        }
    }
}
