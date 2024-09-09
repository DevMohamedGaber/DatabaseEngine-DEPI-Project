namespace DatabaseEngine.Queries
{
    public struct QueryResult
    {
        public List<object> values { get; set; }
        public List<string> Errors { get; set; }

        public QueryResult()
        {
            values = new List<object>();
            Errors = new List<string>();
        }
    }
}
