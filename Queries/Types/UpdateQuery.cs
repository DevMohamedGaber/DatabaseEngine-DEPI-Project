namespace DatabaseEngine.Queries.Types
{
    public class UpdateQuery : Query, IQueryable
    {
        public UpdateQuery(string[] stmt) : base(stmt) { }
        public override void Execute()
        {
            
        }
    }
}
