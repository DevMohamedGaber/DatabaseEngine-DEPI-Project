namespace DatabaseEngine
{
    public  class Query
    {
        public string[] statment {  get; private set; }
        public QueryResult Result { get; private set; }
        public bool Success { get; private set; }

        public Query(string statment)
        {
            this.statment = statment.Split(" ");
        }

        public void Resolve()
        {
            QueryResult result = new QueryResult();

            this.Result = result;
        }
    }
}
