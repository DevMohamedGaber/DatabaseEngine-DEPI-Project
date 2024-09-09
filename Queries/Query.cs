namespace DatabaseEngine.Queries
{
    public class Query
    {
        public string[] statment { get; private set; }
        public QueryResult Result { get; private set; }
        public bool Success { get; private set; }

        public Query(string statment)
        {
            this.statment = statment.Split(" ");
        }

        public void Resolve()
        {
            if(statment.Length == 0)
            {
                this.Success = false;
                this.Result = new QueryResult();
                Result.Errors.Add("Can't process an empty statment");
                return;
            }

            Result = QueryHandler.Parse(this);
        }
    }
}
