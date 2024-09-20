namespace DatabaseEngine.Queries.Types
{
    public class DropQuery : Query
    {
        public DropQuery(string[] stmt) : base(stmt) { }
        protected override void Execute()
        {
            
        }
    }
}
