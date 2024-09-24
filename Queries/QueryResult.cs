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

        public void AddSyntaxError(string msg)
        {
            this.Errors.Add($"Syntax Error: {msg}.");
        }
        public void AddProcessError(string msg)
        {
            this.Errors.Add($"Process Error: {msg}.");
        }
    }
}
